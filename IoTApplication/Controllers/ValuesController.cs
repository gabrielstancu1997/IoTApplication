using IoTApplication.BussinesRules;
using IoTApplication.Data;
using IoTApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplicatieLicentaIoT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : Controller
    {
        enum MetricType
        {
            Temperature = 1,
            Humidity = 2
        }

        [Serializable]
        public class DateValue
        {
            public string DateMonthName;
            public double AvgMonth;
        }

        public class DateValuePerDays
        {
            public string DateMonthName;
            public double AvgDay;
        }


        [Serializable]
        public class DateValueDay
        {
            public string DayDescription;
            public double AvgDay;
            public int DayNumber;
        }

        public class DateValuePerHours
        {
            public double AvgHour;
            public int DayNumber;
        }

        [Serializable]
        public class DateValueToday
        {
            public string HourDescription;
            public double AvgHour;
        }

        public class DateValueTodayPerMinute
        {
            public int Hour;
            public double AvgMinute;
        }


        [Serializable]
        public class CurrentValue
        {
            public double AvgMinute;
        }


        private readonly ApplicationDbContext _context;

        public ValuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Value>>> GetValues()
        {
            return await _context.Values.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Value>> GetValue(int id)
        {
            var value = await _context.Values.FindAsync(id);

            if (value == null)
            {
                return NotFound();
            }

            return value;

        }

        [HttpGet("months-temperature-values/{year}")]
        public ActionResult<string> GetMonthsTemperatureValues(int year)
        {
            var temperatureFromPast = _context.Values
                    .Where(x => x.MetricId == (int)MetricType.Temperature && x.Timestamp.Value.Year == year)
                    .GroupBy(x => new { x.Timestamp.Value.Year, x.Timestamp.Value.Month, x.Timestamp.Value.Day })
                    .Select(y => new DateValuePerDays
                    {
                        DateMonthName = getFullMonthName(y.Key.Month),
                        AvgDay = y.Average(x => x.Value1.Value)
                    }).ToList();

            double[] listaValoriTrecut = new double[temperatureFromPast.Count];
            int index = 0;
            temperatureFromPast.ForEach(r =>
            {
                listaValoriTrecut[index] = r.AvgDay;
                index++;
            });

            var temperatureWithPredictions = temperatureFromPast.GroupBy(row => row.DateMonthName)
                                    .Select(r => new DateValue
                                    {
                                        DateMonthName = r.Key,
                                        AvgMonth = r.Average(avg => avg.AvgDay)
                                    }).ToList();

            var predictions = BrArimaModel.ReturnNextFiveDaysPrognoze(listaValoriTrecut, temperatureWithPredictions.Count);

            int lastMonthNumber = getNumberMonthFromName(temperatureWithPredictions.
                                                                                    OrderByDescending(r => getNumberMonthFromName(r.DateMonthName))
                                                                                    .Select(x => x.DateMonthName)
                                                                                    .FirstOrDefault());
            lastMonthNumber++;
            predictions.ForEach(r =>
            {
                temperatureWithPredictions.Add(new DateValue
                {
                    AvgMonth = r,
                    DateMonthName = getFullMonthName(lastMonthNumber)
                });
                lastMonthNumber++;
                if (lastMonthNumber == 13)
                {
                    lastMonthNumber = 1;
                }
            });

            return JsonConvert.SerializeObject(temperatureWithPredictions.OrderBy(r => getNumberMonthFromName(r.DateMonthName)));
        }

        [HttpGet("current-month-temperature")]
        public ActionResult<string> GetLastMonthTemperatureValue()
        {
            var temperatureFromPast = _context.Values
                .Where(x => x.MetricId == (int)MetricType.Temperature && x.Timestamp.Value.Month == DateTime.Today.Month - 1 && x.Timestamp.Value.Year == DateTime.Today.Year)
                .GroupBy(x => new { x.Timestamp.Value.Day, x.Timestamp.Value.Hour })
                .Select(x => new DateValuePerHours
                {
                    AvgHour = x.Average(x => x.Value1.Value),
                    DayNumber = x.Key.Day
                }).ToList();

            double[] listaValoriTrecut = new double[temperatureFromPast.Count];
            int index = 0;
            temperatureFromPast.ForEach(r =>
            {
                listaValoriTrecut[index] = r.AvgHour;
                index++;
            });

            var temperatureWithPredictions = temperatureFromPast.GroupBy(row => row.DayNumber)
                                    .Select(r => new DateValueDay
                                    {
                                        DayDescription = r.Key.ToString() + " " + getFullDayName(r.Key),
                                        AvgDay = r.Average(avg => avg.AvgHour),
                                        DayNumber = r.Key
                                    }).ToList();

            var predictions = BrArimaModel.ReturnNextFiveDaysPrognoze(listaValoriTrecut, temperatureWithPredictions.Count);

            temperatureWithPredictions.OrderBy(r => r.DayNumber);

            int dayNumber = 1;
            predictions.ForEach(r =>
            {
                temperatureWithPredictions.Add(new DateValueDay
                {
                    DayDescription = dayNumber.ToString() + " " + getFullDayName(dayNumber),
                    AvgDay = r,
                    DayNumber = dayNumber
                });
                dayNumber++;
            });

            return JsonConvert.SerializeObject(temperatureWithPredictions);
        }

        [HttpGet("current-today-temperature")]
        public ActionResult<string> GetTodayTemperature()
        {
            var temperatureFromPast = _context.Values
                .Where(x => x.MetricId == (int)MetricType.Temperature && x.Timestamp.Value.Day == DateTime.Today.Day - 1
                                                                        && x.Timestamp.Value.Month == DateTime.Today.Month
                                                                        && x.Timestamp.Value.Year == DateTime.Today.Year)
                .GroupBy(x => new { x.Timestamp.Value.Hour, x.Timestamp.Value.Minute })
                .Select(x => new DateValueTodayPerMinute
                {
                    Hour = x.Key.Hour,
                    AvgMinute = x.Average(x => x.Value1.Value),
                }).ToList();

            double[] listaValoriTrecut = new double[temperatureFromPast.Count];
            int index = 0;
            temperatureFromPast.ForEach(r =>
            {
                listaValoriTrecut[index] = r.AvgMinute;
                index++;
            });

            var temperatureWithPredictions = temperatureFromPast.GroupBy(row => row.Hour)
                                   .Select(r => new DateValueToday
                                   {
                                       HourDescription = getFullDayHour(r.Key),
                                       AvgHour = r.Average(avg => avg.AvgMinute)
                                   }).ToList();

            temperatureWithPredictions.OrderBy(r => r.HourDescription);

            var predictions = BrArimaModel.ReturnNextFiveDaysPrognoze(listaValoriTrecut, temperatureWithPredictions.Count);

            int hourNumber = 0;
            predictions.ForEach(r =>
            {
                temperatureWithPredictions.Add(new DateValueToday
                {
                    HourDescription = getFullDayHour(hourNumber),
                    AvgHour = r
                });
                hourNumber++;
            });


            return JsonConvert.SerializeObject(temperatureWithPredictions);
        }


        [HttpGet("current-temperature")]
        public double GetCurrentTemperature()
        {

            double ts_current = DateTime.Now.TimeOfDay.TotalMinutes;

            double temperature = _context.Values
                .Where(x => x.MetricId == (int)MetricType.Temperature && x.Timestamp.Value.Hour == DateTime.Now.Hour &&
                                                                            x.Timestamp.Value.Day == DateTime.Today.Day &&
                                                                            x.Timestamp.Value.Month == DateTime.Today.Month &&
                                                                            x.Timestamp.Value.Year == DateTime.Today.Year)
                .GroupBy(x => new { x.Timestamp.Value.Hour })
                .Select(x => x.Average(x => x.Value1.Value))
                .FirstOrDefault();

            if (temperature != null)
            {
                return round(temperature, 2);
            }

            return -99;
        }


        [HttpGet("months-humidity-values/{year}")]
        public ActionResult<string> GetMonthsHumidityValues(int year)
        {
            var humidityFromPast = _context.Values
                  .Where(x => x.MetricId == (int)MetricType.Humidity && x.Timestamp.Value.Year == year)
                  .GroupBy(x => new { x.Timestamp.Value.Year, x.Timestamp.Value.Month, x.Timestamp.Value.Day })
                  .Select(y => new DateValuePerDays
                  {
                      DateMonthName = getFullMonthName(y.Key.Month),
                      AvgDay = y.Average(x => x.Value1.Value)
                  }).ToList();

            double[] listaValoriTrecut = new double[humidityFromPast.Count];
            int index = 0;
            humidityFromPast.ForEach(r =>
            {
                listaValoriTrecut[index] = r.AvgDay;
                index++;
            });

            var humidityWithPredictions = humidityFromPast.GroupBy(row => row.DateMonthName)
                                    .Select(r => new DateValue
                                    {
                                        DateMonthName = r.Key,
                                        AvgMonth = r.Average(avg => avg.AvgDay)
                                    }).ToList();

            var predictions = BrArimaModel.ReturnNextFiveDaysPrognoze(listaValoriTrecut, humidityWithPredictions.Count);

            int lastMonthNumber = getNumberMonthFromName(humidityWithPredictions.
                                                                                    OrderByDescending(r => getNumberMonthFromName(r.DateMonthName))
                                                                                    .Select(x => x.DateMonthName)
                                                                                    .FirstOrDefault());
            lastMonthNumber++;
            predictions.ForEach(r =>
            {
                humidityWithPredictions.Add(new DateValue
                {
                    AvgMonth = r,
                    DateMonthName = getFullMonthName(lastMonthNumber)
                });
                lastMonthNumber++;
                if (lastMonthNumber == 13)
                {
                    lastMonthNumber = 1;
                }
            });

            return JsonConvert.SerializeObject(humidityWithPredictions.ToList().OrderBy(r => getNumberMonthFromName(r.DateMonthName)));
        }
        [HttpGet("current-month-humidity")]
        public ActionResult<string> GetLastMonthHumidityValue()
        {
            var humidityFromPast = _context.Values
                 .Where(x => x.MetricId == (int)MetricType.Humidity && x.Timestamp.Value.Month == DateTime.Today.Month - 1 && x.Timestamp.Value.Year == DateTime.Today.Year)
                 .GroupBy(x => new { x.Timestamp.Value.Day, x.Timestamp.Value.Hour })
                 .Select(x => new DateValuePerHours
                 {
                     AvgHour = x.Average(x => x.Value1.Value),
                     DayNumber = x.Key.Day
                 }).ToList();

            double[] listaValoriTrecut = new double[humidityFromPast.Count];
            int index = 0;
            humidityFromPast.ForEach(r =>
            {
                listaValoriTrecut[index] = r.AvgHour;
                index++;
            });

            var humidityWithPredictions = humidityFromPast.GroupBy(row => row.DayNumber)
                                    .Select(r => new DateValueDay
                                    {
                                        DayDescription = r.Key.ToString() + " " + getFullDayName(r.Key),
                                        AvgDay = r.Average(avg => avg.AvgHour),
                                        DayNumber = r.Key
                                    }).ToList();

            var predictions = BrArimaModel.ReturnNextFiveDaysPrognoze(listaValoriTrecut, humidityWithPredictions.Count);

            humidityWithPredictions.OrderBy(r => r.DayNumber);

            int dayNumber = 1;
            predictions.ForEach(r =>
            {
                humidityWithPredictions.Add(new DateValueDay
                {
                    DayDescription = dayNumber.ToString() + " " + getFullDayName(dayNumber),
                    AvgDay = r,
                    DayNumber = dayNumber
                });
                dayNumber++;
            });

            return JsonConvert.SerializeObject(humidityWithPredictions);
        }
        [HttpGet("current-today-humidity")]
        public ActionResult<string> GetTodayHumidity()
        {
            var humidityFromPast = _context.Values
                .Where(x => x.MetricId == (int)MetricType.Humidity && x.Timestamp.Value.Day == DateTime.Today.Day - 1
                                                                        && x.Timestamp.Value.Month == DateTime.Today.Month
                                                                        && x.Timestamp.Value.Year == DateTime.Today.Year)
                .GroupBy(x => new { x.Timestamp.Value.Hour, x.Timestamp.Value.Minute })
                .Select(x => new DateValueTodayPerMinute
                {
                    Hour = x.Key.Hour,
                    AvgMinute = x.Average(x => x.Value1.Value),
                }).ToList();

            double[] listaValoriTrecut = new double[humidityFromPast.Count];
            int index = 0;
            humidityFromPast.ForEach(r =>
            {
                listaValoriTrecut[index] = r.AvgMinute;
                index++;
            });

            var humidityWithPredictions = humidityFromPast.GroupBy(row => row.Hour)
                                   .Select(r => new DateValueToday
                                   {
                                       HourDescription = getFullDayHour(r.Key),
                                       AvgHour = r.Average(avg => avg.AvgMinute)
                                   }).ToList();

            humidityWithPredictions.OrderBy(r => r.HourDescription);

            var predictions = BrArimaModel.ReturnNextFiveDaysPrognoze(listaValoriTrecut, humidityWithPredictions.Count);

            int hourNumber = 0;
            predictions.ForEach(r =>
            {
                humidityWithPredictions.Add(new DateValueToday
                {
                    HourDescription = getFullDayHour(hourNumber),
                    AvgHour = r
                });
                hourNumber++;
            });

            return JsonConvert.SerializeObject(humidityWithPredictions);
        }

        [HttpGet("current-humidity")]
        public double GetCurrentHumidity()
        {
            double humidity = _context.Values
                .Where(x => x.MetricId == (int)MetricType.Humidity && x.Timestamp.Value.Hour == DateTime.Now.Hour &&
                                                                        x.Timestamp.Value.Day == DateTime.Today.Day &&
                                                                        x.Timestamp.Value.Month == DateTime.Today.Month &&
                                                                        x.Timestamp.Value.Year == DateTime.Today.Year)
                .GroupBy(x => new { x.Timestamp.Value.Hour })
                .Select(x => x.Average(x => x.Value1.Value))
                .FirstOrDefault();

            if (humidity != null)
            {
                return round(humidity, 2);
            }
            return -99;
        }


        // function to get the full month name 
        static string getFullMonthName(int month)
        {
            DateTime date = new DateTime(DateTime.Today.Year, month, 1);

            return date.ToString("MMMM");
        }

        // function to get the full month name 
        static string getFullDayName(int day)
        {
            DateTime date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, day);

            return date.ToString("dddd");
        }

        // function to get the full month name 
        static string getFullDayHour(int hour)
        {
            DateTime date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, hour, 0, 0);

            return date.ToString("HH");
        }

        static int getNumberMonthFromName(string monthName)
        {
            switch (monthName)
            {
                case "January":
                    return 1;
                case "February":
                    return 2;
                case "March":
                    return 3;
                case "April":
                    return 4;
                case "May":
                    return 5;
                case "June":
                    return 6;
                case "July":
                    return 7;
                case "August":
                    return 8;
                case "September":
                    return 9;
                case "October":
                    return 10;
                case "November":
                    return 11;
                case "December":
                    return 12;
            }
            return -1;
        }

        public static double round(double value, int places)
        {
            if (places < 0) throw new Exception("Specific places");

            long factor = (long)Math.Pow(10, places);
            value = value * factor;
            long tmp = (long)Math.Round(value);
            return (double)tmp / factor;
        }


        [HttpPost]
        public async Task<ActionResult<Value>> PostValue(Value value)
        {
            _context.Values.Add(value);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostValue", new { id = value.Id }, value);
        }

    }
}
