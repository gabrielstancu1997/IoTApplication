using Extreme.Statistics.TimeSeriesAnalysis;
using System.Collections.Generic;


namespace IoTApplication.BussinesRules
{
    public class BrArimaModel
    {
        public static List<double> ReturnNextDaysPrognoze(double[] pTimeSeriesData, int pnNumarDePredictiiReturnate)
        {

            if (pTimeSeriesData.Length <= 5)
                return new List<double>();
        
            // ARIMA(p,d,q) -> ARIMA(2,1,2) este modelul cu parametrii p = 2, d = 1, q = 2:
            ArimaModel model = new(pTimeSeriesData, 2, 1, 2)
            {

                // EstimateMean trebuie să fie pe true
                EstimateMean = true
                
            };

            // Potrivirea datelor cu toate proprietățile.
            model.Fit();
            
            // Returnarea vectorului de predicții
            var vectorPredictions = model.Forecast(pnNumarDePredictiiReturnate);

            List<double> predictionsResult = new();

            for (int index = 0; index < pnNumarDePredictiiReturnate; index++)
            {
                predictionsResult.Add(vectorPredictions[index]);
            }

            return predictionsResult;

        }

    }
}
