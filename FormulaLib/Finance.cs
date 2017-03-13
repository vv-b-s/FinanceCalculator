using System;
using System.Text;
using static System.Math;

namespace Finance
{
    public enum Calculate { None, FutureValue, PresentValue, EffectiveIR, RateOfReturn, Risk, Deprication }

    public static class Interest
    {
        public enum IntrestType { Simple, Discursive, Anticipative }
        public enum InterestPeriods { Daily, Weekly, Monthly }

        private static double IntTimesPeriod(double intTimes, InterestPeriods iPeriods) // if interest is not accounted Annually.
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
                    return $"Future Value: {futureValue:0.00}\n" +
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

                    return $"Future Value: {futureValue:0.00}\n" +
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

                    return $"Future Value: {futureValue:0.00}\n" +
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

                    return $"Future Value: {futureValue:0.00}\n" +
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

                    return $"Future Value: {futureValue:0.00}\n" +
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
                    return $"Present Value: {presentValue:0.00}\n" +
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

                    return $"Present Value: {presentValue:0.00}\n" +
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

                    return $"Present Value: {presentValue:0.00}\n" +
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

                    return $"Present Value: {presentValue:0.00}\n" +
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

                    return $"Present Value: {presentValue:0.00}\n" +
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

    public static class RateOfReturn
    {
        public static readonly string[] Attributes = { "Value of investment", "Future Value" };

        public static string Calculate(decimal valueOfInvestment, decimal futureValue)
        {
            try
            {
                decimal result = ((futureValue - valueOfInvestment) / valueOfInvestment) * 100;
                result = Round(result, 2);

                return $"Rate of return: {result}%\nUsed formula: ((FV-I)/I) × 100 = r%\nSolution: (({futureValue} - {valueOfInvestment}) / {valueOfInvestment}) × 100 = {result}%";
            }
            catch (OverflowException)
            {
                return "Impossible Calculation!";
            }
            catch (DivideByZeroException)
            {
                return "Dividing by zero error!\nPlease check your input.\nIf your input is correct and you get this error, then your calculation is impossible.";
            }
        }
    }
    public class Risk
    {
        public enum CalcType { ExpectedReturns, StandardDeviation, VariationCoefficient, PortfolioCovariation, CorelationCoefficient, PortfolioDeviation, BetaCoefficient }

        public class ExpectedReturns
        {
            public static readonly string[] Attributes = { "Anticipated Revenues", "Probability" };
            private decimal _ER = 0;
            public decimal Value
            {
                set { _ER += value; }
                get { return _ER; }
            }

            public static ExpectedReturns ER = new ExpectedReturns();

            public string Calculate(decimal anticipatedR, decimal probability)
            {
                anticipatedR = (anticipatedR > 1 && anticipatedR > -1) ? anticipatedR * 100 : anticipatedR;

                try
                {
                    probability = (probability > 1 || probability < -1) ? probability / 100 : probability;

                    decimal currentER;
                    Value = currentER = anticipatedR * (probability / 100);
                    _ER = Round(_ER, 3);

                    return $"Expected Returns: {Value}\n" +
                           $"Used formula: ER = {(char)8721}Ri × Pi\n" +
                           $"Current Expected Returns: {Round(currentER, 3)}";
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
            }

            public void Clear() => _ER = 0;
        }

        public class StandardDeviation
        {
            public static readonly string[] Attributes = { "Anticipated Revenues", "Probability", "Expected Returns" };
            private decimal _SD = 0;

            public decimal Value
            {
                set { _SD += value; }
                get { return Round((decimal)Sqrt((double)_SD), 2); }
            }

            public static StandardDeviation sD = new StandardDeviation();

            public string Calculate(decimal ARevenues, decimal Probability, decimal ExpectedR)
            {
                ExpectedR = (ExpectedR < 1 && ExpectedR > -1) ? ExpectedR * 100 : ExpectedR;            //To make input acurate
                ARevenues = (ARevenues < 1 && ARevenues > -1) ? ARevenues * 100 : ARevenues;

                try
                {
                    Probability = (Probability > 1 || Probability < -1) ? Probability / 100 : Probability;

                    decimal Dispersion = (decimal)Pow((double)(ARevenues - ExpectedR), 2) * (Probability);
                    Value = Dispersion = Round(Dispersion, 2);

                    return $"Standard deviation: {Value}\n" +
                           $"Used formula: {(char)963}{(char)178} = {(char)8721}(Ri - ER){(char)178} × Pi%\n" +
                           $"Current disperison: {Dispersion}\nTotal dispersion: {_SD}";
                }
                catch (OverflowException)
                {

                    return "Impossible Calculation!";
                }
            }

            public void Clear() => _SD = 0;
        }

        public static class VariationCoefficient
        {
            public static readonly string[] Attributes = { "Standard Devration", "Expected Returns" };
            public static string Calculate(decimal SD, decimal ER)
            {
                ER = (ER < 1 && ER > -1) ? ER * 100 : ER;
                try
                {
                    decimal CV = Round(SD / ER, 2);
                    return $"Variation Coefficient: {CV}\n" +
                           $"Used formula: CV = {(char)963} / ER\n" +
                           $"Solution: {SD} / {ER} = {CV}";
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

        public class PortfolioCovariation
        {
            public static readonly string[] Attributes = { "Anticipated Revenues A", "Expected Returns A", "Anticipated Revenues B", "Expected Returns B", "Probability" };

            private decimal _PC = 0;
            public decimal Value
            {
                set { _PC += value; }
                get { return _PC; }
            }

            public static PortfolioCovariation PC = new PortfolioCovariation();

            public string Calculate(decimal AR1, decimal ER1, decimal AR2, decimal ER2, decimal Probability)
            {
                AR1 = (AR1 < 1 && AR1 > -1) ? AR1 * 100 : AR1;
                AR2 = (AR2 < 1 && AR2 > -1) ? AR2 * 100 : AR2;
                ER1 = (ER1 < 1 && ER1 > -1) ? ER1 * 100 : ER1;
                ER1 = (ER1 < 1 && ER1 > -1) ? ER1 * 100 : ER1;
                try
                {
                    Probability = (Probability > 1 || Probability < -1) ? Probability / 100 : Probability;

                    decimal Cov = ((AR1 - ER1) * (AR2 - ER2)) * Probability;
                    Value = Cov = Round(Cov, 3);

                    return $"Portfolio covariation: {Value}\n" +
                           $"Used formula: Cov = {(char)8721}[(R1i - ER1)(R2i - ER2)](Pi)\n" +
                           $"Current covariation: {Cov}";
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
            }

            public void Clear() => _PC = 0;
        }

        public class CorelationCoefficient
        {
            public static readonly string[] Attributes = { "Covariation", "Standard Deviation A", "Standard Deviation B" };

            private decimal _CC;
            public decimal Value
            {
                set { _CC = value; }
                get { return Round(_CC, 2); }
            }

            public static CorelationCoefficient CC = new CorelationCoefficient();

            public string Calculate(decimal Cov, decimal SDA, decimal SDB)
            {
                Cov = (Cov < 1 && Cov > -1) ? Cov * 100 : Cov;
                SDA = (SDA < 1 && SDA > -1) ? SDA * 100 : SDA;
                SDB = (SDB < 1 && SDB > -1) ? SDB * 100 : SDB;
                try
                {
                    Value = Cov / (SDA * SDB);
                    return $"Corelation Coefficient: {Value}\n" +
                           $"Used formula: K = Cov/{(char)963}1{(char)963}2\n" +
                           $"Solution: {Cov}/({SDA} × {SDB}) = {Value}";
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\nPlease check your input.\nIf your input is correct and you get this error, then your calculation is impossible.";
                }
            }

            public void Clear() => Value = 0;
        }

        public static class PortfolioDeviation
        {
            public static readonly string[] Attributes = { "Portfolio Share A", "Standard Deviation A", "Portfolio Share B", "Standard Deviation B", "Corelation Coefficient" };

            public static string Calculate(decimal PSA, decimal SDA, decimal PSB, decimal SDB, decimal CC)
            {
                PSA = (PSA < 1 && PSA > -1) ? PSA : PSA / 100;
                PSB = (PSB < 1 && PSB > -1) ? PSB : PSB / 100;
                SDA = (SDA < 1 && SDA > -1) ? SDA : SDA / 100;
                SDB = (SDB < 1 && SDB > -1) ? SDB : SDB / 100;
                CC = (CC < 1 && CC > -1) ? CC : CC / 100;
                try
                {
                    decimal PD = (decimal)(Pow((double)PSA, 2) * Pow((double)SDA, 2));
                    PD += (decimal)(Pow((double)PSB, 2) * Pow((double)SDB, 2));
                    PD += 2 * PSA * PSB * CC * (SDA) * (SDB);

                    return $"Portfolio Deviation: {Round(Sqrt((double)PD) * 100, 2)}\n" +
                           $"Used formula: {(char)963} = {(char)8730}(w1{(char)178}{(char)963}1{(char)178} + w2{(char)178}{(char)963}2{(char)178} + 2 × w1 × w2 × K × {(char)963}1 × {(char)963})\n" +
                           $"Soluton: {(char)8730}({PSA}{(char)178} × ({SDA}%){(char)178} + {PSB}{(char)178} × ({SDB}%){(char)178} + 2 × {PSA} × {PSB} × {CC} × {SDA}% × {SDB}%) = {Round(Sqrt((double)PD) * 100, 2)}";
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
            }
        }

        public static class BetaCoefficient
        {
            public static string[] Attributes = { "Portfolio Covariation", "Portfolio Dispersion" };

            public static string Calculate(decimal Cov, decimal Dispersion)
            {
                Cov = (Cov < 1 && Cov > -1) ? Cov * 100 : Cov;
                try
                {
                    decimal BC = Cov / Dispersion;
                    BC = Round(BC, 3);

                    return $"Beta Coefficient: {BC}\n" +
                           $"Used formula: {(char)946} = Cov/{(char)963}{(char)178}\n" +
                           $"Solution: {Cov}/{Dispersion:0.00} = {BC}";
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

    public static class Deprication
    {
        public enum DepricationType { Linear, DecreasingDeduction, ComulativeMethod }

        public static class LinearDeprication
        {
            public static readonly string[] Attributes = { "Аcquisition cost", "Years", "Liquidation value" };

            public static string Calculate(decimal AC, int Years)
            {
                try
                {
                    decimal LD = Round(100m / Years, 2);

                    return $"Linear Deprication Norm: {LD}%\n" +
                           "Used fromula: 100/Years\n" +
                           $"Solution: 100 / {Years} = {LD}%\n\n" + CreateTable(LD, AC, Years);
                }
                catch (OverflowException)
                {
                    return "Impossible calculation";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                           "Please check your input.\n" +
                           "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }

            public static string Calculate(decimal AC, int Years, decimal LV)
            {
                try
                {
                    decimal LD = Round(((AC - LV) / (AC * Years)) * 100, 2);
                    return $"Linear Deprication Norm: {LD}%\n" +
                           "Used fromula: [(AC-LV)/(AC × Years)] × 100\n" +
                           $"Solution: [({AC} - {LV}) /({AC} × {Years}] × 100 = {LD}%\n\n" + CreateTable(LD, AC, Years);
                }
                catch (OverflowException)
                {
                    return "Impossible calculation";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                           "Please check your input.\n" +
                           "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }

            private static string CreateTable(decimal LD, decimal AC, int Years)
            {
                LD = LD / 100;
                decimal DepricationDeduction = AC * LD;

                var output = new StringBuilder();

                for (int i = 0; i <= Years + 1; i++)
                {
                    if (i == 0)
                        output.AppendLine($"{"Year",-15}" +
                                          $"{"Depr. Norm",-17}" +
                                          $"{(char)8721}{"Depr. Deduct.",-17}" +
                                          $"{"Comulative Sum",-20}" +
                                          "Tax Savings");

                    else if (i > 0 && i <= Years)
                        output.AppendLine($"{i,-20}" +
                                          $"{LD * 100,-20:0.00}" +
                                          $"{DepricationDeduction,-24:0.00}" +
                                          $"{DepricationDeduction * i,-28:0.00}" +
                                          $"{LD * 10000:0.00}");
                    else if (i == Years + 1)
                        output.AppendLine($"\n{"Total",-18}" +
                                          $"{"100",-20}" +
                                          $"{AC,-24:0.00}" +
                                          $"{"-",-32}" +
                                          $"{DepricationDeduction:0.00}");
                }

                return output.ToString();
            }
        }

        public static class DecreasingDeduction
        {
            public static readonly string[] Attributes = { "Аcquisition cost", "Years", "Increase coefficient" };

            public static string Calculate(decimal AC, int Years, decimal IC)
            {
                if (IC < 1.5m || IC > 2.2m)
                    return $"Increase coefficient can be between {1.5} and {2.2}.";
                try
                {
                    decimal LinearDepricationNorm = Round(100m / Years, 2);
                    decimal DepricationNorm = LinearDepricationNorm * IC;

                    return $"Deprication Norm: {DepricationNorm}%\n" +
                           "Used formula: (100 / Years) × K\n" +
                           $"Solution: (100 / {Years}) = {LinearDepricationNorm} × {IC} = {DepricationNorm}%\n\n" + CreateTable(DepricationNorm, AC, Years);
                }
                catch (OverflowException)
                {
                    return "Impossible calculation";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                           "Please check your input.\n" +
                           "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }

            private static string CreateTable(decimal DDeduction, decimal AC, int Years)
            {
                DDeduction /= 100;
                decimal DepricationDeduction = AC * DDeduction;

                var output = new StringBuilder();
                decimal comulSum = DepricationDeduction;
                output.AppendLine($"{"Year",-15}" +
                                  $"{"Depr. Norm",-17}" +
                                  $"{"Deprication",-17}" +
                                  $"{"Comulative Sum",-20}" +
                                  "Tax Savings");

                for (int i = 1; i <= Years; i++)
                {
                    if (i > 0 && i <= Years - 2)
                    {
                        output.AppendLine($"{i,-20}" +
                                          $"{DDeduction * 100,-20:0.00}" +
                                          $"{DepricationDeduction,-18:0.00}" +
                                          $"{comulSum,-28:0.00}" +
                                          $"{DepricationDeduction * 0.2m:0.00}");
                        AC -= DepricationDeduction;

                        if (i < Years - 2)
                        {
                            DepricationDeduction = AC * DDeduction;
                            comulSum += DepricationDeduction;
                        }
                    }
                    else if (i == Years - 1 || i == Years)
                    {
                        DepricationDeduction = AC * 1 / ((i == Years - 1) ? 2 : 1);
                        comulSum += DepricationDeduction;
                        AC -= DepricationDeduction;
                        output.AppendLine($"{i,-20}" +
                                          $"{ 50.0,-19:0.00}" +
                                          $"{DepricationDeduction,-18:0.00}" +
                                          $"{comulSum,-28:0.00}" +
                                          $"{DepricationDeduction * 0.2m:0.00}");
                    }
                }
                output.AppendLine($"\n{"Total",-18}" +
                                  $"{"-",-20}" +
                                  $"{comulSum,-24:0.00}" +
                                  $"{"-",-32}" +
                                  $"{comulSum * (100 / Years) / 100:0.00}");
                return output.ToString();
            }
        }

        public static class ComulativeMethod
        {
            public static readonly string[] Attributes = { "Аcquisition cost", "Years" };

            public static string Calculate(decimal AC, int Years) => CreateTable(AC, Years);

            private static string CreateTable(decimal AC, int Years)
            {
                try
                {
                    var output = new StringBuilder();
                    decimal comulativeSum = 0;

                    int yearSum = 0;
                    for (int i = Years; i >0; i--)
                        yearSum+=i;

                    output.AppendLine($"{"Year",-15}" +
                                      $"{"Years left",-17}" +
                                      $"{"Depr. Norm",-30}" +
                                      $"{"Deprication",-17}" +
                                      $"{"Comulative Sum",-20}" +
                                      "Tax Savings");

                    for (int i = 0, j = Years; i < Years; i++, j--)
                    {
                        decimal depricationRate = Round(j / (decimal)yearSum * 100m, 2);
                        decimal DepricationDeduction = AC * (depricationRate/100m);
                        comulativeSum += DepricationDeduction;

                        output.AppendLine($"{i,-20}" +
                                          $"{j,-17}" +
                                          $"{j}/{yearSum} × 100 = {depricationRate,-15:0.00}" +
                                          $"{DepricationDeduction,-18:0.00}" +
                                          $"{comulativeSum,-28:0.00}" +
                                          $"{DepricationDeduction * 0.2m:0.00}");
                    }
                    output.AppendLine($"\n{"Total",-18}" +
                                      $"{yearSum,-18}" +
                                      $"{"-",-35}" +
                                      $"{comulativeSum,-24:0.00}" +
                                      $"{"-",-30}" +
                                      $"{AC * (100m / Years) / 100:0.00}\n\n"+
                                      "Notice that the Аcquisition cost remains constant.");

                    return output.ToString();
                }
                catch (OverflowException)
                {
                    return "Impossible calculation";
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