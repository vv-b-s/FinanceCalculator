using System;
using static System.Math;

namespace Finance
{
    public static class Annuity
    {
        public enum PresentOrFuture { Future, Present }
        private enum PayPeriod { StartYear, EndYear }

        public static class FutureValue
        {
            public static readonly string[] Attributes = { "When is the annuity paid? (0 - at the start of the year, 1 - at the end of the year)",
                "Present Value", "Interest Rate", "Period", "Interest periods", "Type of periods (Daily - 0, Weekly - 1, Monthly - 2)" };

            public static string Calculate(int payPeriod, decimal presentValue, decimal interestRate, double period)
            {
                try
                {
                    decimal futureValue;
                    interestRate /= 100;

                    switch (payPeriod)
                    {
                        case (int)PayPeriod.StartYear:
                            futureValue = presentValue * ((decimal)(Pow((double)(1 + interestRate), period) - 1) / interestRate) * (1 + interestRate);
                            futureValue = Round(futureValue, 2);

                            return $"Future value: {futureValue}\n" +
                                   $"Used formula: FV = A[ ((1+r)^n -1)/r ] × (1 + r)\n" +
                                   $"Solution: {presentValue} × [ ((1+{interestRate})^{period} - 1)/{interestRate}) - 1 ] * (1 + {interestRate}) = {futureValue}";

                        case (int)PayPeriod.EndYear:
                            futureValue = presentValue * ((decimal)(Pow((double)(1 + interestRate), period) - 1) / interestRate);
                            futureValue = Round(futureValue, 2);

                            return $"Future value: {futureValue}\n" +
                                   $"Used formula: FV = A[ ((1+r)^n -1)/r ]\n" +
                                   $"Solution: {presentValue} × [ ((1+{interestRate})^{period} - 1)/{interestRate} ] = {futureValue}";
                        default:
                            return $"{payPeriod} is not available option!\n" +
                                $"Please enter 0 or 1 for the pay period!";
                    }
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                        "Please check your input.\n" +
                        "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }
            public static string Calculate(int payPeriod, decimal presentValue, decimal interestRate, double period, double intTimes, Interest.InterestPeriods iPeriods)
            {
                try
                {

                    decimal futureValue;
                    interestRate /= 100;

                    switch (payPeriod)
                    {
                        case (int)PayPeriod.StartYear:
                            futureValue = presentValue * (((decimal)(Pow((double)(1 + (interestRate) /
                                (decimal)Interest.IntTimesPeriod(intTimes, iPeriods)),
                                (period * Interest.IntTimesPeriod(intTimes, iPeriods)))) - 1) /
                                (interestRate / (decimal)Interest.IntTimesPeriod(intTimes, iPeriods))) *
                                (1 + interestRate / (decimal)Interest.IntTimesPeriod(intTimes, iPeriods));
                            futureValue = Round(futureValue, 2);

                            return $"Future Value: {futureValue:0.00}\n" +
                                  "Used formula: FV = A × [ (1 + r%/m)^(m × n) / (r / m) - 1] × (1 + r/m)\n" +
                                   $"Solution: {presentValue} × [ (1 + {interestRate}/{Interest.IntTimesPeriod(intTimes, iPeriods)})^({period} × {Interest.IntTimesPeriod(intTimes, iPeriods)}) / ({interestRate} / {Interest.IntTimesPeriod(intTimes, iPeriods)}) - 1 ] × (1 + {interestRate}/{Interest.IntTimesPeriod(intTimes, iPeriods)} = {futureValue:0.00}";

                        case (int)PayPeriod.EndYear:
                            futureValue = presentValue * (((decimal)(Pow((double)(1 + (interestRate) /
                                (decimal)Interest.IntTimesPeriod(intTimes, iPeriods)),
                                (period * Interest.IntTimesPeriod(intTimes, iPeriods)))) - 1) /
                                ((interestRate) / (decimal)Interest.IntTimesPeriod(intTimes, iPeriods)));
                            futureValue = Round(futureValue, 2);

                            return $"Future Value: {futureValue:0.00}\n" +
                                  "Used formula: FV = A × [ (1 + r%/m)^(m × n) / (r / m) - 1]\n" +
                                   $"Solution: {presentValue} × [ (1 + {interestRate}/{Interest.IntTimesPeriod(intTimes, iPeriods)})^({period} × {Interest.IntTimesPeriod(intTimes, iPeriods)}) / ({interestRate} / {Interest.IntTimesPeriod(intTimes, iPeriods)})  ] = {futureValue:0.00}";

                        default:
                            return $"{payPeriod} is not available option!\n" +
                                $"Please enter 0 or 1 for the pay period!";
                    }
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                        "Please check your input.\n" +
                        "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }
        }

        public static class PresentValue
        {
            public static readonly string[] Attributes = { "When is the annuity paid? (0 - at the start of the year, 1 - at the end of the year)",
                "Annual Receipts", "Interest Rate", "Period", "Interest periods", "Type of periods (Daily - 0, Weekly - 1, Monthly - 2)" };

            public static string Calculate(int payPeriod, decimal annualReceipts, decimal interestRate, double period)
            {
                try
                {
                    decimal presentValue;
                    interestRate /= 100;

                    switch (payPeriod)
                    {
                        case (int)PayPeriod.StartYear:
                            presentValue = annualReceipts * (1 / interestRate - 1 / (interestRate * (decimal)Pow((double)(1 + interestRate), period))) * (1 + interestRate);
                            presentValue = Round(presentValue, 2);

                            return $"Future value: {presentValue}\n" +
                                   $"Used formula: PV = A[ 1/r - 1/(r × (1+r)^n ) ] × (1 + r)\n" +
                                   $"Solution: {annualReceipts} × [ (1/{interestRate} - 1/({interestRate} × (1+{interestRate})^{period})) ] * (1 + {interestRate}) = {presentValue}";

                        case (int)PayPeriod.EndYear:
                            presentValue = annualReceipts * (1 / interestRate - 1 / (interestRate * (decimal)Pow((double)(1 + interestRate), period)));
                            presentValue = Round(presentValue, 2);

                            return $"Future value: {presentValue}\n" +
                                   $"Used formula: PV = [ 1/r - 1/(r × (1+r)^n) ]\n" +
                                   $"Solution: {annualReceipts} × [ (1/{interestRate} - 1/({interestRate} × (1+{interestRate})^{period})) ] = {presentValue}";
                        default:
                            return $"{payPeriod} is not available option!\n" +
                                $"Please enter 0 or 1 for the pay period!";
                    }
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                        "Please check your input.\n" +
                        "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }
            public static string Calculate(int payPeriod, decimal annualReceipts, decimal interestRate, double period, double intTimes, Interest.InterestPeriods iPeriods)
            {
                try
                {

                    decimal presentValue;
                    interestRate /= 100;
                    decimal rm = interestRate / (decimal)Interest.IntTimesPeriod(intTimes, iPeriods);

                    switch (payPeriod)
                    {
                        case (int)PayPeriod.StartYear:
                            presentValue = annualReceipts * (1 / rm - 1 / (rm * (decimal)Pow((double)(1 + rm),
                                (period * Interest.IntTimesPeriod(intTimes, iPeriods))))) *
                                (1 + rm);
                            presentValue = Round(presentValue, 2);

                            return $"Future Value: {presentValue:0.00}\n" +
                                  "Used formula: PV = A × [ 1/(r/m) - 1/(r/m) × (1 + r%/m)^(m × n) ] × (1 + r/m)\n" +
                                   $"Solution: {annualReceipts} × [ (1/{rm:0.00} - 1/{rm:0.00} × (1+{rm:0.00})^({period} × {Interest.IntTimesPeriod(intTimes, iPeriods)}) ] × (1 + {interestRate}/{Interest.IntTimesPeriod(intTimes, iPeriods)} = {presentValue:0.00}";

                        case (int)PayPeriod.EndYear:
                            presentValue = annualReceipts * (1 / rm - 1 / (rm * (decimal)Pow((double)(1 + rm),
                                (period * Interest.IntTimesPeriod(intTimes, iPeriods)))));
                            presentValue = Round(presentValue, 2);

                            return $"Future Value: {presentValue:0.00}\n" +
                                  "Used formula: PV = A × [ 1/(r/m) - 1/(r/m) × (1 + r%/m)^(m × n) ]\n" +
                                  $"Solution: {annualReceipts} × [ (1/{rm:0.00} - 1/{rm:0.00} × (1+{rm:0.00})^({period} × {Interest.IntTimesPeriod(intTimes, iPeriods)}) ] = {presentValue}";

                        default:
                            return $"{payPeriod} is not available option!\n" +
                                $"Please enter 0 or 1 for the pay period!";
                    }
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                        "Please check your input.\n" +
                        "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }
        }
    }
}
