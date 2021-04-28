using Extreme.Mathematics;
using Extreme.Statistics.TimeSeriesAnalysis;
using System;
using System.Collections.Generic;


namespace IoTApplication.BussinesRules
{
    public class BrArimaModel
    {
        public static List<double> ReturnNextFiveDaysPrognoze(double[] pTimeSeriesData, int pnNumarDePredictiiReturnate)
        {

            // The time series data is stored in a numerical variable:

            if (pTimeSeriesData.Length <= 5)
                return new List<double>();
            // An integrated model (with differencing) is constructed
            // by supplying the degree of differencing. Note the order
            // of the orders is the traditional one for an ARIMA(p,d,q)
            // model (p, d, q).
            // The following constructs an ARIMA(0,1,2) model:
            ArimaModel model = new(pTimeSeriesData, 2, 1, 2)
            {

                // By default, the mean is assumed to be zero for an integrated model.
                // We can override this by setting the EstimateMean property to true:
                EstimateMean = true
                
            };

            // The Compute methods fits the model.
            model.Fit();

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
