using System;
using static System.Math;

namespace Finance
{
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
}
