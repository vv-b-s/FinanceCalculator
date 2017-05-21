using System;
using static System.Math;
using TextTable;

namespace Finance
{
    public class Factors
    {
        public static readonly string[] Attributes = { "Years", "Interest rate" }; 

        public static double CompInterestFactor(int years, double interestRate)
        {
            interestRate /= interestRate <= 0.99 && interestRate >= -0.99 ? 1 : 100;
            return Round(Pow(1 + interestRate, years), 3);
        }

        public static double DiscountFactor(int years, double interestRate) => Round(1 / CompInterestFactor(years, interestRate), 3);

        public static double AnnuityFactor(int years, double interestRate)
        {
            interestRate /= interestRate <= 0.99 && interestRate >= -0.99 ? 1 : 100;

            return Round(Pow((1 + interestRate), years) / interestRate, 3);
        }

        public static double DiscountAnnuityFactor(int years, double interestRate)
        {
            interestRate /= interestRate <= 0.99 && interestRate >= -0.99 ? 1 : 100;

            return Round(1 / interestRate - 1 / (interestRate * CompInterestFactor(years, interestRate)), 3);
        }

        public static string ShowFactors(int years, double interestRate)
        {
            try
            {
                return $"Compound interest factor: {CompInterestFactor(years, interestRate)}\n" +
                        $"Discount interest factor: {DiscountFactor(years, interestRate)}\n" +
                        $"Annuity factor: {AnnuityFactor(years, interestRate)}\n" +
                        $"Discount annuity factor: {DiscountAnnuityFactor(years, interestRate)}";
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
