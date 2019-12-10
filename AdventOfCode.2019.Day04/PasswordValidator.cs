namespace AdventOfCode._2019.Day04
{
    public static class PasswordValidator
    {
        public static bool Validate(string password)
        {
            bool neverDecrease = true;
            bool hasDoubleDigits = false;

            for (int i = 1; i < password.Length; i++)
            {
                int prevDigit = int.Parse(new string(password[i - 1], 1));
                int currentDigit = int.Parse(new string(password[i], 1));

                if (prevDigit > currentDigit)
                {
                    neverDecrease = false;
                }

                if (prevDigit == currentDigit)
                {
                    hasDoubleDigits = true;
                }
            }

            return neverDecrease && hasDoubleDigits;
        }
    }
}
