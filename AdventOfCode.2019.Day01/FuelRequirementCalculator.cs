namespace AdventOfCode._2019.Day01
{
    static class FuelRequirementCalculator
    {
        private static int CalculateWithAddedFuel(int mass, int sumOfRequiredFuel)
        {
            int requiredFuel = Calculate(mass);                       

            if (requiredFuel > 0)
            {
                sumOfRequiredFuel += requiredFuel;
                return CalculateWithAddedFuel(requiredFuel, sumOfRequiredFuel);
            }
            else
            {
                return sumOfRequiredFuel;
            }
        }

        public static int CalculateWithAddedFuel(int mass)
        {
            return CalculateWithAddedFuel(mass, 0);
        }

        public static int Calculate(int mass)
        {
            return mass / 3 - 2;
        }
    }
}
