using System;
using static System.Math;

namespace Finance
{
    public static class Interest
    {
        public enum IntrestType { Simple, Discursive, Anticipative }
        public enum InterestPeriods { Daily, Weekly, Monthly }

        internal static double IntTimesPeriod(double intTimes, InterestPeriods iPeriods) // if interest is not accounted Annually.
        {
            switch (iPeriods)
            {
                case InterestPeriods.Daily:
                    return 365 / intTimes;
                case InterestPeriods.Weekly:
                    return 52 / intTimes;
                case InterestPeriods.Monthly:
                    return 12 / intTimes;
                default:
                    return 1;

            }
        }

        public static class FutureValue
        {
            public static readonly string[] Attributes = { "Present Value", "Interest Rate", "Period", "Interest periods", "Type of periods (Daily - 0, Weekly - 1, Monthly - 2)" };

            public static string SimpleInterest(decimal presentValue, decimal interestRate, double period)
            {
                try
                {
                    decimal futureValue = presentValue * (1 + (interestRate / 100) * (decimal)period);
                    futureValue = Round(futureValue, 2);
                    return $"Future Value: {futureValue:C2}\n" +
                           "Used formula: FV = PV × (1 + n × r%)\n" +
                           $"Solution: {presentValue} × (1 + {period} × {interestRate / 100}) = {futureValue:0.00}";
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

            public static string CDiscursiveInterest(decimal presentValue, decimal interestRate, double period)
            {
                try
                {
                    decimal futureValue = presentValue * (decimal)Pow((double)(1 + interestRate / 100), period);
                    futureValue = Round(futureValue, 2);

                    return $"Future Value: {futureValue:C2}\n" +
                           "Used formula: FV = PV × (1 + r%)^n\n" +
                           $"Solution: {presentValue} × (1 + {interestRate / 100})^{period} = {futureValue:0.00}";
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

            public static string CDiscursiveInterest(decimal presentValue, decimal interestRate, double period, double intTimes, InterestPeriods iPeriods)     // if interest is not accounted Annually
            {
                try
                {
                    decimal futureValue = presentValue * (decimal)Pow((double)(1 + ((interestRate / 100) / (decimal)IntTimesPeriod(intTimes, iPeriods))), period * IntTimesPeriod(intTimes, iPeriods));
                    futureValue = Round(futureValue, 2);

                    return $"Future Value: {futureValue:C2}\n" +
                           "Used formula: FV = PV × (1 + r%/m)^(m × n)\n" +
                           $"Solution: {presentValue} × (1 + {interestRate / 100}/{IntTimesPeriod(intTimes, iPeriods)})^({period} × {IntTimesPeriod(intTimes, iPeriods)}) = {futureValue:0.00}";
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

            public static string CAnticipativeInterest(decimal presentValue, decimal interestRate, double period)
            {
                try
                {
                    decimal futureValue = presentValue / (decimal)Pow((double)(1 - interestRate / 100), period);
                    futureValue = Round(futureValue, 2);

                    return $"Future Value: {futureValue:C2}\n" +
                           "Used formula: FV = PV/(1-r%)^n\n" +
                           $"Solution: {presentValue}/(1 + {interestRate / 100})^{period} = {futureValue:0.00}";
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

            public static string CAnticipativeInterest(decimal presentValue, decimal interestRate, double period, double intTimes, InterestPeriods iPeriods)     // if interest is not accounted Annually
            {
                try
                {
                    decimal futureValue = presentValue / (decimal)Pow((double)(1 - ((interestRate / 100) / (decimal)(IntTimesPeriod(intTimes, iPeriods)))), period * IntTimesPeriod(intTimes, iPeriods));
                    futureValue = Round(futureValue, 2);

                    return $"Future Value: {futureValue:C2}\n" +
                           "Used formula: FV = PV / (1 - r%/m)^(m × n)\n" +
                           $"Solution: {presentValue} / (1 - {interestRate / 100}/{IntTimesPeriod(intTimes, iPeriods)})^({period} × {IntTimesPeriod(intTimes, iPeriods)}) = {futureValue:0.00}";
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
            public static readonly string[] Attributes = { "Future Value", "Interest Rate", "Period", "Interest periods", "Type of periods (Daily - 0, Weekly - 1, Monthly - 2)" };

            public static string SimpleInterest(decimal futureValue, decimal interestRate, double period)
            {
                try
                {
                    decimal presentValue = futureValue / (1 + (interestRate / 100) * (decimal)period);
                    presentValue = Round(presentValue, 2);
                    return $"Present Value: {presentValue:C2}\n" +
                           $"Used formula: PV = FV / (1 + n × r%)\n" +
                           $"Solution: {futureValue} / (1 + {period} × {interestRate / 100}) = {presentValue:0.00}";
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

            public static string CDiscursiveInterest(decimal futureValue, decimal interestRate, double period)
            {
                try
                {
                    decimal presentValue = futureValue / (decimal)Pow((double)(1 + interestRate / 100), period);
                    presentValue = Round(presentValue, 2);

                    return $"Present Value: {presentValue:C2}\n" +
                           "Used formula: PV = FV / (1 + r%)^n\n" +
                           $"Solution: {futureValue} / (1 + {interestRate / 100})^{period} = {presentValue:0.00}";
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

            public static string CDiscursiveInterest(decimal futureValue, decimal interestRate, double period, double intTimes, InterestPeriods iPeriods)     // if interest is not accounted Annually
            {
                try
                {
                    decimal presentValue = futureValue / (decimal)Pow((double)(1 + ((interestRate / 100) / (decimal)IntTimesPeriod(intTimes, iPeriods))), period * IntTimesPeriod(intTimes, iPeriods));
                    presentValue = Round(presentValue, 2);

                    return $"Present Value: {presentValue:C2}\n" +
                           $"Used formula: PV = FV / (1 + r%/m)^(m × n)\n" +
                           $"Solution: {futureValue} / (1 + {interestRate / 100}/{IntTimesPeriod(intTimes, iPeriods)})^({period} × {IntTimesPeriod(intTimes, iPeriods)}) = {presentValue:0.00}";
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

            public static string CAnticipativeInterest(decimal futureValue, decimal interestRate, double period)
            {
                try
                {
                    decimal presentValue = futureValue * (decimal)Pow((double)(1 - interestRate / 100), period);
                    presentValue = Round(presentValue, 2);

                    return $"Present Value: {presentValue:C2}\n" +
                           "Used formula: FV = PV × (1-r%)^n\n" +
                           $"Solution: {futureValue} × (1 + {interestRate / 100})^{period} = {presentValue:0.00}";
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

            public static string CAnticipativeInterest(decimal futureValue, decimal interestRate, double period, double intTimes, InterestPeriods iPeriods)     // if interest is not accounted Annually
            {
                try
                {
                    decimal presentValue = futureValue * (decimal)Pow((double)(1 - ((interestRate / 100) / (decimal)IntTimesPeriod(intTimes, iPeriods))), period * IntTimesPeriod(intTimes, iPeriods));
                    presentValue = Round(presentValue, 2);

                    return $"Present Value: {presentValue:C2}\n" +
                           $"Used formula: PV = FV × (1 - r%/m)^(m × n)\n" +
                           $"Solution: {futureValue} × (1 - {interestRate / 100}/{IntTimesPeriod(intTimes, iPeriods)})^({period} × {IntTimesPeriod(intTimes, iPeriods)}) = {presentValue:0.00}";
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

        public static class EffectiveIR
        {
            public static readonly string[] Attributes = { "Interest rate", "Interest period times", "Type of periods (Daily - 0, Weekly - 1, Monthly - 2)" };

            public static string Calculate(decimal interestRate, double intTimes, InterestPeriods iPeriods)
            {
                try
                {
                    decimal eir = (((decimal)Pow((double)(1 + (interestRate / 100) / (decimal)IntTimesPeriod(intTimes, iPeriods)), IntTimesPeriod(intTimes, iPeriods))) - 1) * 100;
                    eir = Round(eir, 2);

                    return $"Effective Interest Rate: {eir}%\n" +
                           "Used formula: [(1 + r%/m)^m-1] × 100\n" +
                           $"Solution: [(1 + {interestRate / 100}/{IntTimesPeriod(intTimes, iPeriods)})^{IntTimesPeriod(intTimes, iPeriods)}-1] × 100 = {eir}%";
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
