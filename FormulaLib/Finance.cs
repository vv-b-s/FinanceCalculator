using System;

namespace Finance
{
    public enum Calculate { None, FutureValue, PresentValue, EffectiveIR, RateOfReturn, Risk }

    public static class Interest
    {
        public enum IntrestType { Simple, Discursive, Anticipative }
        public enum InterestPeriods { Daily, Weekly, Monthly }

        static double iTimesPeriod(double iTimes, InterestPeriods iPeriods) // if interest is not accounted Annually.
        {
            switch (iPeriods)
            {
                case InterestPeriods.Daily:
                    return 365 / iTimes;
                case InterestPeriods.Weekly:
                    return 52 / iTimes;
                case InterestPeriods.Monthly:
                    return 12 / iTimes;
                default:
                    return 1;

            }
        }

        public static class FutureValue
        {
            public static readonly string[] attributes = { "Present Value", "Interest Rate", "Period", "Interest periods", "Type of periods (Daily - 0, Weekly - 1, Monthly - 2)" };

            public static string SimpleInterest(decimal presentValue, decimal interestRate, double period)
            {
                try
                {
                    decimal futureValue = presentValue * (1 + (interestRate / 100) * (decimal)period);
                    futureValue = Math.Round(futureValue, 2);
                    return $"Future Value: {futureValue:0.00}\nUsed formula: FV = PV × (1 + n × r%)\nSolution: {presentValue} × (1 + {period} × {interestRate / 100}) = {futureValue:0.00}";
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
                catch(DivideByZeroException)
                {
                    return "Dividing by zero error!\nPlease check your input.\nIf your input is correct and you get this error, then your calculation is impossible.";
                }
            }

            public static string CDiscursiveInterest(decimal presentValue, decimal interestRate, double period)
            {
                try
                {
                    decimal futureValue = presentValue * (decimal)Math.Pow((double)(1 + interestRate / 100), period);
                    futureValue = Math.Round(futureValue, 2);

                    return $"Future Value: {futureValue:0.00}\nUsed formula: FV = PV × (1 + r%)^n\nSolution: {presentValue} × (1 + {interestRate / 100})^{period} = {futureValue:0.00}";
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

            public static string CDiscursiveInterest(decimal presentValue, decimal interestRate, double period, double iTimes, InterestPeriods iPeriods)     // if interest is not accounted Annually
            {
                try
                {
                    decimal futureValue = presentValue * (decimal)Math.Pow((double)(1 + ((interestRate / 100) / (decimal)iTimesPeriod(iTimes, iPeriods))), period * iTimesPeriod(iTimes, iPeriods));
                    futureValue = Math.Round(futureValue, 2);

                    return $"Future Value: {futureValue:0.00}\nUsed formula: FV = PV × (1 + r%/m)^(m × n)\nSolution: {presentValue} × (1 + {interestRate / 100}/{iTimesPeriod(iTimes, iPeriods)})^({period} × {iTimesPeriod(iTimes, iPeriods)}) = {futureValue:0.00}";
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

            public static string CAnticipativeInterest(decimal presentValue, decimal interestRate, double period)
            {
                try
                {
                    decimal futureValue = presentValue / (decimal)Math.Pow((double)(1 - interestRate / 100), period);
                    futureValue = Math.Round(futureValue, 2);

                    return $"Future Value: {futureValue:0.00}\nUsed formula: FV = PV/(1-r%)^n\nSolution: {presentValue}/(1 + {interestRate / 100})^{period} = {futureValue:0.00}";
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

            public static string CAnticipativeInterest(decimal presentValue, decimal interestRate, double period, double iTimes, InterestPeriods iPeriods)     // if interest is not accounted Annually
            {
                try
                {
                    decimal futureValue = presentValue / (decimal)Math.Pow((double)(1 - ((interestRate / 100) / (decimal)(iTimesPeriod(iTimes, iPeriods)))), period * iTimesPeriod(iTimes, iPeriods));
                    futureValue = Math.Round(futureValue, 2);

                    return $"Future Value: {futureValue:0.00}\nUsed formula: FV = PV / (1 - r%/m)^(m × n)\nSolution: {presentValue} / (1 - {interestRate / 100}/{iTimesPeriod(iTimes, iPeriods)})^({period} × {iTimesPeriod(iTimes, iPeriods)}) = {futureValue:0.00}";
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

        public static class PresentValue
        {
            public static readonly string[] attributes = { "Future Value", "Interest Rate", "Period", "Interest periods", "Type of periods (Daily - 0, Weekly - 1, Monthly - 2)" };

            public static string SimpleInterest(decimal futureValue, decimal interestRate, double period)
            {
                try
                {
                    decimal presentValue = futureValue / (1 + (interestRate / 100) * (decimal)period);
                    presentValue = Math.Round(presentValue, 2);
                    return $"Present Value: {presentValue:0.00}\nUsed formula: PV = FV / (1 + n × r%)\nSolution: {futureValue} / (1 + {period} × {interestRate / 100}) = {presentValue:0.00}";
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

            public static string CDiscursiveInterest(decimal futureValue, decimal interestRate, double period)
            {
                try
                {
                    decimal presentValue = futureValue / (decimal)Math.Pow((double)(1 + interestRate / 100), period);
                    presentValue = Math.Round(presentValue, 2);

                    return $"Present Value: {presentValue:0.00}\nUsed formula: PV = FV / (1 + r%)^n\nSolution: {futureValue} / (1 + {interestRate / 100})^{period} = {presentValue:0.00}";
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

            public static string CDiscursiveInterest(decimal futureValue, decimal interestRate, double period, double iTimes, InterestPeriods iPeriods)     // if interest is not accounted Annually
            {
                try
                {
                    decimal presentValue = futureValue / (decimal)Math.Pow((double)(1 + ((interestRate / 100) / (decimal)iTimesPeriod(iTimes, iPeriods))), period * iTimesPeriod(iTimes, iPeriods));
                    presentValue = Math.Round(presentValue, 2);

                    return $"Present Value: {presentValue:0.00}\nUsed formula: PV = FV / (1 + r%/m)^(m × n)\nSolution: {futureValue} / (1 + {interestRate / 100}/{iTimesPeriod(iTimes, iPeriods)})^({period} × {iTimesPeriod(iTimes, iPeriods)}) = {presentValue:0.00}";
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

            public static string CAnticipativeInterest(decimal futureValue, decimal interestRate, double period)
            {
                try
                {
                    decimal presentValue = futureValue * (decimal)Math.Pow((double)(1 - interestRate / 100), period);
                    presentValue = Math.Round(presentValue, 2);

                    return $"Present Value: {presentValue:0.00}\nUsed formula: FV = PV × (1-r%)^n\nSolution: {futureValue} × (1 + {interestRate / 100})^{period} = {presentValue:0.00}";
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

            public static string CAnticipativeInterest(decimal futureValue, decimal interestRate, double period, double iTimes, InterestPeriods iPeriods)     // if interest is not accounted Annually
            {
                try
                {
                    decimal presentValue = futureValue * (decimal)Math.Pow((double)(1 - ((interestRate / 100) / (decimal)iTimesPeriod(iTimes, iPeriods))), period * iTimesPeriod(iTimes, iPeriods));
                    presentValue = Math.Round(presentValue, 2);

                    return $"Present Value: {presentValue:0.00}\nUsed formula: PV = FV × (1 - r%/m)^(m × n)\nSolution: {futureValue} × (1 - {interestRate / 100}/{iTimesPeriod(iTimes, iPeriods)})^({period} × {iTimesPeriod(iTimes, iPeriods)}) = {presentValue:0.00}";
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

        public static class EffectiveIR
        {
            public static readonly string[] attributes = { "Interest rate", "Interest period times", "Type of periods (Daily - 0, Weekly - 1, Monthly - 2)" };

            public static string Calculate(decimal interestRate, double iTimes, InterestPeriods iPeriods)
            {
                try
                {
                    decimal eir = (((decimal)Math.Pow((double)(1 + (interestRate / 100) / (decimal)iTimesPeriod(iTimes, iPeriods)), iTimesPeriod(iTimes, iPeriods))) - 1) * 100;
                    eir = Math.Round(eir, 2);

                    return $"Effective Interest Rate: {eir}%\nUsed formula: [(1 + r%/m)^m-1] × 100\nSolution: [(1 + {interestRate / 100}/{iTimesPeriod(iTimes, iPeriods)})^{iTimesPeriod(iTimes, iPeriods)}-1] × 100 = {eir}%";
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
    }

    public static class RateOfReturn
    {
        public static readonly string[] attributes = { "Value of investment", "Future Value" };

        public static string Calculate(decimal ValueOfInvestment, decimal FutureValue)
        {
            try
            {
                decimal result = ((FutureValue - ValueOfInvestment) / ValueOfInvestment) * 100;
                result = Math.Round(result, 2);

                return $"Rate of return: {result}%\nUsed formula: ((FV-I)/I) × 100 = r%\nSolution: (({FutureValue} - {ValueOfInvestment}) / {ValueOfInvestment}) × 100 = {result}%";
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
        public enum CalcType { ExpectedReturns, StandardDeviation, VariationCoefficient, PortfolioCovariation, CorelationCoeficient, PortfolioDeviation, BetaCoeficient }

        public class ExpectedReturns
        {
            public static readonly string[] attributes = { "Anticipated Revenues", "Probability" };
            private decimal _ER = 0;
            public decimal Value
            {
                set { _ER += value; }
                get { return _ER; }
            }

            public static ExpectedReturns eR = new ExpectedReturns();

            public string Calculate(decimal anticipatedR, decimal probability)
            {
                try
                {
                    probability = (probability > 1 || probability < -1) ? probability / 100 : probability;

                    decimal currentER;
                    Value = currentER = anticipatedR * (probability / 100);
                    _ER = Math.Round(_ER, 3);

                    return $"Expected Returns: {Value}\nUsed formula: ER = {(char)8721}Ri × Pi\nCurrent Expected Returns: {Math.Round(currentER, 3)}";
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
            public static readonly string[] attributes = { "Anticipated Revenues", "Probability", "Expected Returns" };
            private decimal _SD = 0;

            public decimal Value
            {
                set { _SD += value; }
                get { return Math.Round((decimal)Math.Sqrt((double)_SD), 2); }
            }

            public static StandardDeviation sD = new StandardDeviation();

            public string Calculate(decimal ARevenues, decimal Probability, decimal ExpectedR)
            {
                try
                {
                    Probability = (Probability > 1 || Probability < -1) ? Probability / 100 : Probability;

                    decimal Dispersion = (decimal)Math.Pow((double)(ARevenues - ExpectedR), 2) * (Probability);
                    Value = Dispersion = Math.Round(Dispersion, 2);

                    return $"Standard deviation: {Value}\nUsed formula: {(char)963}{(char)178} = {(char)8721}(Ri - ER){(char)178} × Pi%\nCurrent disperison: {Dispersion}\nTotal dispersion: {_SD}";
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
            public static readonly string[] attributes = { "Standard Devration", "Expected Returns" };
            public static string Calculate(decimal SD, decimal ER)
            {
                try
                {
                    decimal CV = Math.Round(SD / ER, 2);
                    return $"Variation Coefficient: {CV}\nUsed formula: CV = {(char)963} / ER\nSolution: {SD} / {ER} = {CV}";
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

        public class PortfolioCovariation
        {
            public static readonly string[] attributes = { "Anticipated Revenues A", "Expected Returns A", "Anticipated Revenues B", "Expected Returns B", "Probability" };

            private decimal _PC = 0;
            public decimal Value
            {
                set { _PC += value; }
                get { return _PC; }
            }

            public static PortfolioCovariation PC = new PortfolioCovariation();

            public string Calculate(decimal AR1, decimal ER1, decimal AR2, decimal ER2, decimal Probability)
            {
                try
                {
                    Probability = (Probability > 1 || Probability < -1) ? Probability / 100 : Probability;

                    decimal Cov = ((AR1 - ER1) * (AR2 - ER2)) * Probability;
                    Value = Cov = Math.Round(Cov*100, 3);

                    return $"Portfolio covariation: {Value}\nUsed formula: Cov = {(char)8721}[(R1i - ER1)(R2i - ER2)](Pi)\nCurrent covariation: {Cov}";
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
            }

            public void Clear() => _PC = 0;
        }

        public class CorelationCoeficient
        {
            public static readonly string[] attributes = { "Covariation", "Standard Deviation A", "Standard Deviation B" };

            private decimal _CC;
            public decimal Value
            {
                set { _CC = value; }
                get { return Math.Round(_CC, 2); }
            }

            public static CorelationCoeficient CC = new CorelationCoeficient();

            public string Calculate(decimal Cov, decimal SDA, decimal SDB)
            {
                Value = Cov / (SDA * SDB);

                return $"Corelation Coeficient: {Value}\nUsed formula: K = Cov/{(char)963}1{(char)963}2\nSolution: {Cov}/({SDA} × {SDB}) = {Value}";
            }

            public void Clear() => Value = 0;
        }

        public static class PortfolioDeviation
        {
            public static readonly string[] attributes = { "Portfolio Share A", "Standard Deviation A", "Portfolio Share B", "Standard Deviation B", "Corelation Coeficient" };

            public static string Calculate(decimal PSA, decimal SDA, decimal PSB, decimal SDB, decimal CC)
            {
                try
                {
                    decimal PD = (decimal)(Math.Pow((double)PSA, 2) * Math.Pow((double)SDA / 100, 2));
                    PD += (decimal)(Math.Pow((double)PSB, 2) * Math.Pow((double)SDB / 100, 2));
                    PD += 2 * PSA * PSB * CC * (SDA / 100) * (SDB / 100);

                    return $"Portfolio Deviation: {Math.Round(Math.Sqrt((double)PD) * 100, 2)}\nUsed formula: {(char)963} = {(char)8730}(w1{(char)178}{(char)963}1{(char)178} + w2{(char)178}{(char)963}2{(char)178} + 2 × w1 × w2 × K × {(char)963}1 × {(char)963})\nSoluton: {(char)8730}({PSA}{(char)178} × ({SDA}%){(char)178} + {PSB}{(char)178} × ({SDB}%){(char)178} + 2 × {PSA} × {PSB} × {CC} × {SDA}% × {SDB}%) = {Math.Round(Math.Sqrt((double)PD) * 100, 2)}";
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
            }
        }

        public static class BetaCoeficient
        {
            public static string[] attributes = { "Portfolio Covariation", "Portfolio Dispersion" };

            public static string Calculate(decimal Cov, decimal Dispersion)
            {
                try
                {
                    decimal BC = Cov / Dispersion;
                    BC = Math.Round(BC, 3);

                    return $"Beta Coeficient: {BC}\nUsed formula: {(char)946} = Cov/{(char)963}{(char)178}\nSolution: {Cov}/{Dispersion:0.00} = {BC}";
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
    }
}