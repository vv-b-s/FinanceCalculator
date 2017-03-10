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
                    futureValue = Round(futureValue, 2);
                    return $"Future Value: {futureValue:0.00}\nUsed formula: FV = PV × (1 + n × r%)\nSolution: {presentValue} × (1 + {period} × {interestRate / 100}) = {futureValue:0.00}";
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

            public static string CDiscursiveInterest(decimal presentValue, decimal interestRate, double period)
            {
                try
                {
                    decimal futureValue = presentValue * (decimal)Pow((double)(1 + interestRate / 100), period);
                    futureValue = Round(futureValue, 2);

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
                    decimal futureValue = presentValue * (decimal)Pow((double)(1 + ((interestRate / 100) / (decimal)iTimesPeriod(iTimes, iPeriods))), period * iTimesPeriod(iTimes, iPeriods));
                    futureValue = Round(futureValue, 2);

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
                    decimal futureValue = presentValue / (decimal)Pow((double)(1 - interestRate / 100), period);
                    futureValue = Round(futureValue, 2);

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
                    decimal futureValue = presentValue / (decimal)Pow((double)(1 - ((interestRate / 100) / (decimal)(iTimesPeriod(iTimes, iPeriods)))), period * iTimesPeriod(iTimes, iPeriods));
                    futureValue = Round(futureValue, 2);

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
                    presentValue = Round(presentValue, 2);
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
                    decimal presentValue = futureValue / (decimal)Pow((double)(1 + interestRate / 100), period);
                    presentValue = Round(presentValue, 2);

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
                    decimal presentValue = futureValue / (decimal)Pow((double)(1 + ((interestRate / 100) / (decimal)iTimesPeriod(iTimes, iPeriods))), period * iTimesPeriod(iTimes, iPeriods));
                    presentValue = Round(presentValue, 2);

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
                    decimal presentValue = futureValue * (decimal)Pow((double)(1 - interestRate / 100), period);
                    presentValue = Round(presentValue, 2);

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
                    decimal presentValue = futureValue * (decimal)Pow((double)(1 - ((interestRate / 100) / (decimal)iTimesPeriod(iTimes, iPeriods))), period * iTimesPeriod(iTimes, iPeriods));
                    presentValue = Round(presentValue, 2);

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
                    decimal eir = (((decimal)Pow((double)(1 + (interestRate / 100) / (decimal)iTimesPeriod(iTimes, iPeriods)), iTimesPeriod(iTimes, iPeriods))) - 1) * 100;
                    eir = Round(eir, 2);

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
                result = Round(result, 2);

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
        public enum CalcType { ExpectedReturns, StandardDeviation, VariationCoefficient, PortfolioCovariation, CorelationCoefficient, PortfolioDeviation, BetaCoefficient }

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
                anticipatedR = (anticipatedR > 1 && anticipatedR > -1) ? anticipatedR * 100 : anticipatedR;

                try
                {
                    probability = (probability > 1 || probability < -1) ? probability / 100 : probability;

                    decimal currentER;
                    Value = currentER = anticipatedR * (probability / 100);
                    _ER = Round(_ER, 3);

                    return $"Expected Returns: {Value}\nUsed formula: ER = {(char)8721}Ri × Pi\nCurrent Expected Returns: {Round(currentER, 3)}";
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
                ER = (ER < 1 && ER > -1) ? ER * 100 : ER;
                try
                {
                    decimal CV = Round(SD / ER, 2);
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
                AR1 = (AR1 < 1 && AR1 > -1) ? AR1 * 100 : AR1;
                AR2 = (AR2 < 1 && AR2 > -1) ? AR2 * 100 : AR2;
                ER1 = (ER1 < 1 && ER1 > -1) ? ER1 * 100 : ER1;
                ER1 = (ER1 < 1 && ER1 > -1) ? ER1 * 100 : ER1;
                try
                {
                    Probability = (Probability > 1 || Probability < -1) ? Probability / 100 : Probability;

                    decimal Cov = ((AR1 - ER1) * (AR2 - ER2)) * Probability;
                    Value = Cov = Round(Cov, 3);

                    return $"Portfolio covariation: {Value}\nUsed formula: Cov = {(char)8721}[(R1i - ER1)(R2i - ER2)](Pi)\nCurrent covariation: {Cov}";
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
            public static readonly string[] attributes = { "Covariation", "Standard Deviation A", "Standard Deviation B" };

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
                    return $"Corelation Coefficient: {Value}\nUsed formula: K = Cov/{(char)963}1{(char)963}2\nSolution: {Cov}/({SDA} × {SDB}) = {Value}";
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
            public static readonly string[] attributes = { "Portfolio Share A", "Standard Deviation A", "Portfolio Share B", "Standard Deviation B", "Corelation Coefficient" };

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

                    return $"Portfolio Deviation: {Round(Sqrt((double)PD) * 100, 2)}\nUsed formula: {(char)963} = {(char)8730}(w1{(char)178}{(char)963}1{(char)178} + w2{(char)178}{(char)963}2{(char)178} + 2 × w1 × w2 × K × {(char)963}1 × {(char)963})\nSoluton: {(char)8730}({PSA}{(char)178} × ({SDA}%){(char)178} + {PSB}{(char)178} × ({SDB}%){(char)178} + 2 × {PSA} × {PSB} × {CC} × {SDA}% × {SDB}%) = {Round(Sqrt((double)PD) * 100, 2)}";
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
            }
        }

        public static class BetaCoefficient
        {
            public static string[] attributes = { "Portfolio Covariation", "Portfolio Dispersion" };

            public static string Calculate(decimal Cov, decimal Dispersion)
            {
                Cov = (Cov < 1 && Cov > -1) ? Cov * 100 : Cov;
                try
                {
                    decimal BC = Cov / Dispersion;
                    BC = Round(BC, 3);

                    return $"Beta Coefficient: {BC}\nUsed formula: {(char)946} = Cov/{(char)963}{(char)178}\nSolution: {Cov}/{Dispersion:0.00} = {BC}";
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

    public static class Deprication
    {
        public enum DepricationType { Linear, DecreasingDecuction}

        public static class LinearDeprication
        {
            public static readonly string[] attributes = {"Аcquisition cost", "Years", "Liquidation value" };

            public static string Calculate(decimal AC, int Years)
            {
                try
                {
                    decimal LD = Round(100m/Years,2);

                    return $"Linear Deprication Norm: {LD}%\nUsed fromula: 100/Years\nSolution: 100 / {Years} = {LD}%\n\n" + CreateTable(LD,AC,Years);
                }
                catch (OverflowException)
                {
                    return "Impossible calculation";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\nPlease check your input.\nIf your input is correct and you get this error, then your calculation is impossible.";
                }
            }

            public static string Calculate(decimal AC, int Years, decimal LV)
            {
                try
                {
                    decimal LD = Round(((AC-LV)/(AC*Years))*100, 2);
                    return $"Linear Deprication Norm: {LD}%\nUsed fromula: [(AC-LV)/(AC × Years)] × 100\nSolution: [({AC} - {LV}) /({AC} × {Years}] × 100 = {LD}%\n\n" + CreateTable(LD, AC, Years);
                }
                catch (OverflowException)
                {
                    return "Impossible calculation";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\nPlease check your input.\nIf your input is correct and you get this error, then your calculation is impossible.";
                }
            }

            private static string CreateTable(decimal LD,decimal AC, int Years)
            {
                LD = LD / 100;
                decimal DepricationDecuction = AC * LD;

                var output = new StringBuilder();
                

                for (int i = 0; i <= Years+1; i++)
                {
                    if(i==0)
                        output.AppendLine($"{"Year",-15}{"Depr. Norm",-17}{(char)8721}{"Depr. Deduct.",-17}{"Comulative Sum",-20}{"Tax Savings"}");
                    else if(i>0&&i<=Years)
                        output.AppendLine($"{i,-20}{LD * 100,-20}{DepricationDecuction,-24}{DepricationDecuction * i,-28}{LD * 10000}");
                    else if(i==Years+1)
                        output.AppendLine($"\n{"Total",-18}{"100",-20}{AC,-24}{"-",-32}{DepricationDecuction}");
                }     

                return output.ToString();
            }
        }

        public static class DecreasingDecuction
        {
            public static readonly string[] attributes = { "Аcquisition cost", "Years", "Increase coefficient" };

            public static string Calculate(decimal AC, int Years, decimal IC)
            {
                try
                {
                    decimal LinearDepricationNorm = Round(100m / Years, 2);
                    decimal DepricationNorm = LinearDepricationNorm * IC;

                    return $"Deprication Norm: {DepricationNorm}\nUsed formula: (100 / Years) × K\nSolution: (100 / {Years}) = {LinearDepricationNorm} × {IC} = {DepricationNorm}\n\n"+CreateTable(DepricationNorm,AC,Years);
                }
                catch (OverflowException)
                {
                    return "Impossible calculation";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\nPlease check your input.\nIf your input is correct and you get this error, then your calculation is impossible.";
                }
            }

            private static string CreateTable(decimal DDecuction, decimal AC, int Years)
            {
                DDecuction /= 100;
                decimal DepricationDecuction = AC * DDecuction;

                var output = new StringBuilder();
                decimal comulSum = DepricationDecuction;
                output.AppendLine($"{"Year",-15}{"Depr. Norm",-17}{(char)8721}{"Depr. Deduct.",-17}{"Comulative Sum",-20}{"Tax Savings"}");

                for (int i = 1; i <= Years; i++)
                {
                    if (i > 0 && i <= Years-2)
                    {
                        output.AppendLine($"{i,-20}{DDecuction * 100,-20:0.00}{DepricationDecuction,-24:0.00}{comulSum,-28:0.00}{DepricationDecuction * 0.2m:0.00}");
                        AC -= DepricationDecuction;

                        if (i<Years-2)
                        {
                            DepricationDecuction = AC * DDecuction;
                            comulSum += DepricationDecuction; 
                        }
                    }
                    else if(i==Years-1||i==Years)
                    {                        
                        DepricationDecuction = AC * 1 /((i==Years-1)?2:1);
                        comulSum += DepricationDecuction;
                        AC -= DepricationDecuction;
                        output.AppendLine($"{i,-20}{ 50.0,-20:0.00}{DepricationDecuction,-24:0.00}{comulSum,-28:0.00}{DepricationDecuction* 0.2m:0.00}");
                    }
                }
                output.AppendLine($"\n{"Total",-18}{"-",-20}{comulSum,-24:0.00}{"-",-32}{comulSum * (100 / Years)/100:0.00}");
                return output.ToString();
            }
        }

    }
}