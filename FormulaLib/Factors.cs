using System;
using static System.Math;

namespace Finance
{
    public class Factors
    {
        public static double CompInterestFactor(int years, double interestRate)
        {
            interestRate /= interestRate <= 0.99 && interestRate <= -0.99 ? 1 : 100;
            return Round(Pow(1 + interestRate, years), 3);
        }

        public static double DiscountFactor(int years, double interestRate) => Round(1 / CompInterestFactor(years, interestRate), 3);

        public static double DiscountAnnuityFactor(int years, double interestRate)
        {
            interestRate /= interestRate <= 0.99 && interestRate <= -0.99 ? 1 : 100;

            return Round(1 / interestRate - 1 / (interestRate * CompInterestFactor(years, interestRate)), 3);
        }
    }
}
