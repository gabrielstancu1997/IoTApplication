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

        [Serializable]
        public class DateValueDay
        {
            public string DayDescription;
            public double AvgDay;
            public int DayNumber;
        }


        [Serializable]
        public class DateValueToday
        {
            public string HourDescription;
            public double AvgHour;
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

            var temperature = _context.Values
                    .Where(x => x.MetricId == (int)MetricType.Temperature && x.Timestamp.Value.Year == year)
                    .GroupBy(x => new { x.Timestamp.Value.Year, x.Timestamp.Value.Month })
                    .Select(y => new DateValue
                    {
                        DateMonthName = getFullMonthName(y.Key.Month),
                        AvgMonth = y.Average(x => x.Value1.Value)
                    });

            return JsonConvert.SerializeObject(temperature.ToList().OrderBy(r => getNumberMonthFromName(r.DateMonthName)));
        }
        [HttpGet("current-month-temperature")]
        public ActionResult<string> GetLastMonthTemperatureValue()
        {
            var temperature = _context.Values
                .Where(x => x.MetricId == (int)MetricType.Temperature && x.Timestamp.Value.Month == DateTime.Today.Month && x.Timestamp.Value.Year == DateTime.Today.Year)
                .GroupBy(x => new { x.Timestamp.Value.Day })
                .Select(x => new DateValueDay
                {  
                    DayDescription =  x.Key.Day.ToString() + " "  + getFullDayName(x.Key.Day),
                    AvgDay = x.Average(x => x.Value1.Value),
                    DayNumber = x.Key.Day
                });

            return JsonConvert.SerializeObject(temperature.ToList().OrderBy(r => r.DayNumber));
        }
        [HttpGet("current-today-temperature")]
        public ActionResult<string> GetTodayTemperature()
        {
            var temperature = _context.Values
                .Where(x => x.MetricId == (int)MetricType.Temperature && x.Timestamp.Value.Day == DateTime.Today.Day && x.Timestamp.Value.Month == DateTime.Today.Month && x.Timestamp.Value.Year == DateTime.Today.Year)
                .GroupBy(x => new { x.Timestamp.Value.Hour })
                .Select(x => new DateValueToday
                {
                    HourDescription = getFullDayHour(x.Key.Hour),
                    AvgHour = x.Average(x => x.Value1.Value),
                });

            return JsonConvert.SerializeObject(temperature.ToList().OrderBy(r => r.HourDescription));
        }
        [HttpGet("months-humidity-values/{year}")]
        public ActionResult<string> GetMonthsHumidityValues(int year)
        {
            var humidity = _context.Values
                 .Where(x => x.MetricId == (int)MetricType.Humidity && x.Timestamp.Value.Year == year)
                 .GroupBy(x => new { x.Timestamp.Value.Year, x.Timestamp.Value.Month })
                 .Select(y => new DateValue
                 {
                     DateMonthName = getFullMonthName(y.Key.Month),
                     AvgMonth = y.Average(x => x.Value1.Value)
                 });

            return JsonConvert.SerializeObject(humidity.ToList().OrderBy(r => getNumberMonthFromName(r.DateMonthName)));
        }
        [HttpGet("current-month-humidity")]
        public ActionResult<string> GetLastMonthHumidityValue()
        {
            var humidity = _context.Values
               .Where(x => x.MetricId == (int)MetricType.Humidity && x.Timestamp.Value.Month == DateTime.Today.Month && x.Timestamp.Value.Year == DateTime.Today.Year)
               .GroupBy(x => new { x.Timestamp.Value.Day })
               .Select(x => new DateValueDay
               {
                   DayDescription = x.Key.Day.ToString() + " " + getFullDayName(x.Key.Day),
                   AvgDay = x.Average(x => x.Value1.Value),
                   DayNumber = x.Key.Day
               });

            return JsonConvert.SerializeObject(humidity.ToList().OrderBy(r => r.DayNumber));
        }
        [HttpGet("current-today-humidity")]
        public ActionResult<string> GetTodayHumidity()
        {
            var humidity = _context.Values
                .Where(x => x.MetricId == (int)MetricType.Humidity && x.Timestamp.Value.Day == DateTime.Today.Day && x.Timestamp.Value.Month == DateTime.Today.Month && x.Timestamp.Value.Year == DateTime.Today.Year)
                .GroupBy(x => new { x.Timestamp.Value.Hour })
                .Select(x => new DateValueToday
                {
                    HourDescription = getFullDayHour(x.Key.Hour),
                    AvgHour = x.Average(x => x.Value1.Value),
                });

            return JsonConvert.SerializeObject(humidity.ToList().OrderBy(r => r.HourDescription));
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
    


        [HttpPost]
        public async Task<ActionResult<Value>> PostValue(Value value)
        {
            _context.Values.Add(value);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostValue", new { id = value.Id }, value);
        }

    }
}
