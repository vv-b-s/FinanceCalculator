/*Notice: if there are any problems with the table,
 *use Font: Courier New (as it simply works)
 * or visit http://stackoverflow.com/questions/763257/string-padding-problem
 */
using System;
using System.Text;
using static System.Math;
using System.Linq;

using TextTable;

namespace Finance
{
    public enum Calculate { None, FutureValue, PresentValue, EffectiveIR, RateOfReturn, Risk, Deprication, Annuity }

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
                anticipatedR = (anticipatedR > 1 && anticipatedR > -0.99m) ? anticipatedR * 100 : anticipatedR;

                try
                {
                    probability = (probability > 0.99m || probability > -0.99m) ? probability / 100 : probability;

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
                ExpectedR = (ExpectedR < 0.99m && ExpectedR > -0.99m) ? ExpectedR * 100 : ExpectedR;            //To make input acurate
                ARevenues = (ARevenues < 0.99m && ARevenues > -0.99m) ? ARevenues * 100 : ARevenues;

                try
                {
                    Probability = (Probability > 0.99m || Probability > -0.99m) ? Probability / 100 : Probability;

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
                ER = (ER < 0.99m && ER > -0.99m) ? ER * 100 : ER;
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
                AR1 = (AR1 < 0.99m && AR1 > -0.99m) ? AR1 * 100 : AR1;
                AR2 = (AR2 < 0.99m && AR2 > -0.99m) ? AR2 * 100 : AR2;
                ER1 = (ER1 < 0.99m && ER1 > -0.99m) ? ER1 * 100 : ER1;
                ER1 = (ER1 < 0.99m && ER1 > -0.99m) ? ER1 * 100 : ER1;
                try
                {
                    Probability = (Probability > 0.99m || Probability > -0.99m) ? Probability / 100 : Probability;

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
                Cov = (Cov < 0.99m && Cov > -0.99m) ? Cov * 100 : Cov;
                SDA = (SDA < 0.99m && SDA > -0.99m) ? SDA * 100 : SDA;
                SDB = (SDB < 0.99m && SDB > -0.99m) ? SDB * 100 : SDB;
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
                PSA = (PSA < 0.99m && PSA > -0.99m) ? PSA : PSA / 100;
                PSB = (PSB < 0.99m && PSB > -0.99m) ? PSB : PSB / 100;
                SDA = (SDA < 0.99m && SDA > -0.99m) ? SDA : SDA / 100;
                SDB = (SDB < 0.99m && SDB > -0.99m) ? SDB : SDB / 100;
                CC = (CC < 0.99m && CC > -0.99m) ? CC : CC / 100;
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
                Cov = (Cov < 0.99m && Cov > -0.99m) ? Cov * 100 : Cov;
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
        public enum DepricationType { Linear, DecreasingDeduction, ComulativeMethod, EqualDegression }

        public static class LinearDeprication
        {
            public static readonly string[] Attributes = { "Аcquisition cost", "Tax Rate", "Years", "Liquidation value" };

            public static string Calculate(decimal AC, decimal TaxRate, int Years)
            {
                try
                {
                    decimal LD = Round(100m / Years, 2);

                    return $"Linear Deprication Norm: {LD}%\n" +
                           "Used fromula: 100/Years\n" +
                           $"Solution: 100 / {Years} = {LD}%\n\n" + CreateTable(LD, AC, Years, TaxRate);
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

            public static string Calculate(decimal AC, decimal TaxRate, int Years, decimal LV)
            {
                try
                {
                    decimal LD = Round(((AC - LV) / (AC * Years)) * 100, 2);
                    return $"Linear Deprication Norm: {LD}%\n" +
                           "Used fromula: [(AC-LV)/(AC × Years)] × 100\n" +
                           $"Solution: [({AC} - {LV}) /({AC} × {Years}] × 100 = {LD}%\n\n" + CreateTable(LD, AC, Years, TaxRate);
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

            private static string CreateTable(decimal LD, decimal AC, int Years, decimal TaxRate)
            {
                TaxRate = TaxRate > 0.99m || TaxRate < -0.99m ? TaxRate / 100 : TaxRate;
                LD = LD / 100;
                decimal DepricationDeduction = AC * LD;

                var output = Table.Create(Years + 2, 5, true, true);

                #region Head
                output.Modify(0, 0) = $"Year";
                output.Modify(0, 1) = $"Depr. Norm";
                output.Modify(0, 2) = $"{(char)8721}{"Depr. Deduct."}";
                output.Modify(0, 3) = $"{"Comulative Sum"}";
                output.Modify(0, 4) = "Tax Savings";
                #endregion

                #region Body
                for (int i = 1; i <= Years; i++)
                    for (int k = 0; k < 5; k++)
                    {
                        switch (k)
                        {
                            case 0:
                                output.Modify(i, k) = $"{i}"; break;
                            case 1:
                                output.Modify(i, k) = $"{LD * 100:0.00}"; break;
                            case 2:
                                output.Modify(i, k) = $"{DepricationDeduction:0.00}"; break;
                            case 3:
                                output.Modify(i, k) = $"{DepricationDeduction * i:0.00}"; break;
                            case 4:
                                output.Modify(i, k) = $"{DepricationDeduction * TaxRate:0.00}"; break;
                        }
                    }
                #endregion

                #region Legs
                output.Modify(Years + 1, 0) = $"Total";
                output.Modify(Years + 1, 1) = $"100";
                output.Modify(Years + 1, 2) = $"{AC:0.00}";
                output.Modify(Years + 1, 3) = $"-";
                output.Modify(Years + 1, 4) = $"{DepricationDeduction:0.00}";
                #endregion

                return output.ToString();
            }
        }

        public static class DecreasingDeduction
        {
            public static readonly string[] Attributes = { "Аcquisition cost", "Tax Rate", "Years", "Increase coefficient" };

            public static string Calculate(decimal AC, decimal TaxRate, int Years, decimal IC)
            {
                if (IC < 1.5m || IC > 2.2m)
                    return $"Increase coefficient can be between {1.5} and {2.2}.";
                try
                {
                    decimal LinearDepricationNorm = Round(100m / Years, 2);
                    decimal DepricationNorm = LinearDepricationNorm * IC;

                    return $"Deprication Norm: {DepricationNorm}%\n" +
                           "Used formula: (100 / Years) × K\n" +
                           $"Solution: (100 / {Years}) = {LinearDepricationNorm} × {IC} = {DepricationNorm}%\n\n" + CreateTable(DepricationNorm, AC, Years, TaxRate);
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

            private static string CreateTable(decimal DDeduction, decimal AC, int Years, decimal TaxRate)
            {
                TaxRate = TaxRate > 0.99m || TaxRate < -0.99m ? TaxRate / 100 : TaxRate;
                DDeduction /= 100;
                decimal DepricationDeduction = 0;
                decimal comulSum = DepricationDeduction;

                var output = Table.Create(Years + 2, 5, true, true);

                #region Head
                output.Modify(0, 0) = $"{"Year"}";
                output.Modify(0, 1) = $"{"Depr. Norm"}";
                output.Modify(0, 2) = $"{"Deprication"}";
                output.Modify(0, 3) = $"{"Comulative Sum"}";
                output.Modify(0, 4) = "Tax Savings";
                #endregion

                #region Body
                for (int i = 1; i <= Years; i++)
                {
                    DepricationDeduction = i > 0 && i <= Years - 2 ? AC * DDeduction : AC * 1 / ((i == Years - 1) ? 2 : 1);         // Last two years are deducted with 50%                      
                    comulSum += DepricationDeduction;
                    AC -= DepricationDeduction;

                    for (int k = 0; k < 5; k++)
                        switch (k)
                        {
                            case 0:
                                output.Modify(i, k) = $"{i}"; break;
                            case 1:
                                output.Modify(i, k) = $"{(i <= Years - 2 ? DDeduction * 100 : 50):0.00}"; break;
                            case 2:
                                output.Modify(i, k) = $"{DepricationDeduction:0.00}"; break;
                            case 3:
                                output.Modify(i, k) = $"{comulSum:0.00}"; break;
                            case 4:
                                output.Modify(i, k) = $"{DepricationDeduction * TaxRate:0.00}"; break;
                        }
                }
                #endregion

                #region Legs
                output.Modify(Years + 1, 0) = $"Total";
                output.Modify(Years + 1, 1) = $"{"-"}";
                output.Modify(Years + 1, 2) = $"{comulSum:0.00}";
                output.Modify(Years + 1, 3) = $"{"-"}";
                output.Modify(Years + 1, 4) = $"{comulSum * (100 / Years) / 100:0.00}";
                #endregion

                return output.ToString();
            }
        }

        public static class ComulativeMethod
        {
            public static readonly string[] Attributes = { "Аcquisition cost", "Tax Rate", "Years" };

            public static string Calculate(decimal AC, decimal TaxRate, int Years) => CreateTable(AC, Years, TaxRate);

            private static string CreateTable(decimal AC, int Years, decimal TaxRate)
            {
                try
                {
                    var output = Table.Create(Years + 2, 6, true, true);
                    decimal comulativeSum = 0;
                    TaxRate = TaxRate > 0.99m || TaxRate < -0.99m ? TaxRate / 100 : TaxRate;

                    int yearSum = 0;
                    for (int i = Years; i > 0; i--)
                        yearSum += i;

                    #region Head
                    output.Modify(0, 0) = $"Year";
                    output.Modify(0, 1) = $"Years left";
                    output.Modify(0, 2) = $"Depr. Norm";
                    output.Modify(0, 3) = $"Deprication";
                    output.Modify(0, 4) = $"Comulative Sum";
                    output.Modify(0, 5) = "Tax Savings";
                    #endregion

                    #region Body
                    for (int i = 1, j = Years; i <= Years; i++, j--)
                    {
                        decimal depricationRate = Round(j / (decimal)yearSum * 100m, 2);
                        decimal DepricationDeduction = AC * (depricationRate / 100m);
                        comulativeSum += DepricationDeduction;

                        for (int k = 0; k < 6; k++)
                        {
                            switch (k)                      // To define what each cell should contain.
                            {
                                case 0:
                                    output.Modify(i, k) = i.ToString(); break;
                                case 1:
                                    output.Modify(i, k) = j.ToString(); break;
                                case 2:
                                    output.Modify(i, k) = $"{j}/{yearSum} × 100 = {depricationRate:0.00}"; break;
                                case 3:
                                    output.Modify(i, k) = $"{DepricationDeduction:0.00}"; break;
                                case 4:
                                    output.Modify(i, k) = $"{comulativeSum:0.00}"; break;
                                case 5:
                                    output.Modify(i, k) = $"{DepricationDeduction * TaxRate:0.00}"; break;

                            }
                        }
                    }

                    #endregion

                    #region Legs
                    output.Modify(Years + 1, 0) = $"Total";
                    output.Modify(Years + 1, 1) = $"{yearSum}";
                    output.Modify(Years + 1, 2) = $"{"-"}";
                    output.Modify(Years + 1, 3) = $"{comulativeSum:0.00}";
                    output.Modify(Years + 1, 4) = $"{"-"}";
                    output.Modify(Years + 1, 5) = $"{AC * (100m / Years) / 100:0.00}";

                    #endregion

                    string additionalInfo = "\n\nNotice that the Аcquisition cost remains constant.";

                    return string.Concat(output.ToString(), additionalInfo);
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

        public static class EqualDegression
        {
            public static readonly string[] Attributes = { "Аcquisition cost", "Tax Rate", "Years" };

            static int Years = 0;
            static decimal CoE                                  // Coefficient of equality. used to calculate the deprication norm each year.
            {
                get
                {
                    int eqC = 2;
                    for (int i = 1, j = 2; i < Years; i++)
                        eqC += j + i;
                    return Round(100m / eqC, 2);
                }
            }

            public static string Calculate(decimal AC, decimal TaxRate, int years) => CreateTable(AC, years, TaxRate);

            static string CreateTable(decimal AC, int years, decimal TaxRate)
            {
                TaxRate = TaxRate > 0.99m || TaxRate < -0.99m ? TaxRate / 100 : TaxRate;
                Years = years;
                decimal DDeduction = CoE / 100;
                decimal comulSum = 0;

                var output = Table.Create(Years + 2, 5, true, true);

                #region Head
                output.Modify(0, 0) = $"Year";
                output.Modify(0, 1) = $"Depr. Norm";
                output.Modify(0, 2) = $"{(char)8721}{"Depr. Deduct."}";
                output.Modify(0, 3) = $"{"Comulative Sum"}";
                output.Modify(0, 4) = "Tax Savings";
                #endregion

                #region Body
                for (int i = 1, j = years + 1; i <= Years; i++, j--)
                {
                    comulSum += AC * DDeduction * j;
                    for (int k = 0; k < 5; k++)
                    {
                        switch (k)
                        {
                            case 0:
                                output.Modify(i, k) = $"{i}"; break;
                            case 1:
                                output.Modify(i, k) = $"{j * CoE:0.00}"; break;
                            case 2:
                                output.Modify(i, k) = $"{Round(AC * DDeduction * j, 2):0.00}"; break;
                            case 3:
                                output.Modify(i, k) = $"{Round(comulSum, 2):0.00}"; break;
                            case 4:
                                output.Modify(i, k) = $"{Round(AC * DDeduction * j * TaxRate, 2):0.00}"; break;
                        }
                    }
                }
                #endregion

                #region Legs
                output.Modify(Years + 1, 0) = $"Total";
                output.Modify(Years + 1, 1) = $"100";
                output.Modify(Years + 1, 2) = $"{AC:0.00}";
                output.Modify(Years + 1, 3) = $"-";
                output.Modify(Years + 1, 4) = $"{AC * TaxRate:0.00}";
                #endregion

                return output.ToString();
            }
        }
    }

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
                                   $"Solution: {presentValue} × [ ((1+{interestRate})^{period} - 1)/{interestRate} ] * (1 + {interestRate}) = {futureValue}";

                        case (int)PayPeriod.EndYear:
                            futureValue = presentValue * ((decimal)(Pow((double)(1 + interestRate), period) - 1) / interestRate);
                            futureValue = Round(futureValue, 2);

                            return $"Future value: {futureValue}\n" +
                                   $"Used formula: FV = A[ ((1+r)^n -1)/r ]\n" +
                                   $"Solution: {presentValue} × [ ((1+{interestRate})^{period} - 1)/{interestRate} ] = {futureValue}";
                        default: return "";
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
                                ((interestRate) / (decimal)Interest.IntTimesPeriod(intTimes, iPeriods)));
                            futureValue = Round(futureValue, 2);

                            return $"Future Value: {futureValue:0.00}\n" +
                                  "Used formula: FV = PV × [ (1 + r%/m)^(m × n) / (r / m) - 1]\n" +
                                   $"Solution: {presentValue} × [ (1 + {interestRate}/{Interest.IntTimesPeriod(intTimes, iPeriods)})^({period} × {Interest.IntTimesPeriod(intTimes, iPeriods)}) / ({interestRate} / {Interest.IntTimesPeriod(intTimes, iPeriods)})  ] = {futureValue:0.00}";

                        case (int)PayPeriod.EndYear:
                            futureValue = presentValue * (((decimal)(Pow((double)(1 + (interestRate) /
                                (decimal)Interest.IntTimesPeriod(intTimes, iPeriods)),
                                (period * Interest.IntTimesPeriod(intTimes, iPeriods)))) - 1) /
                                (interestRate / (decimal)Interest.IntTimesPeriod(intTimes, iPeriods))) *
                                (1 + interestRate / (decimal)Interest.IntTimesPeriod(intTimes, iPeriods));
                            futureValue = Round(futureValue, 2);

                            return $"Future Value: {futureValue:0.00}\n" +
                                  "Used formula: FV = PV × [ (1 + r%/m)^(m × n) / (r / m) - 1] × (1 + r/m)\n" +
                                   $"Solution: {presentValue} × [ (1 + {interestRate}/{Interest.IntTimesPeriod(intTimes, iPeriods)})^({period} × {Interest.IntTimesPeriod(intTimes, iPeriods)}) / ({interestRate} / {Interest.IntTimesPeriod(intTimes, iPeriods)})  ] × (1 + {interestRate}/{Interest.IntTimesPeriod(intTimes, iPeriods)} = {futureValue:0.00}";

                        default: return "";
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