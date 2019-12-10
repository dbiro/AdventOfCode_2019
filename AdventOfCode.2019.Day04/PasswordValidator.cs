namespace AdventOfCode._2019.Day04
{
    public static class PasswordValidator
    {
        public static bool Validate(string password)
        {
            if (password.Length != 6)
            {
                return false;
            }

            bool neverDecrease = true;
            bool hasMatchingPair = false;
            int matchingPairsCount = 0;

            for (int i = 1; i < password.Length; i++)
            {
                int prevDigit = int.Parse(new string(password[i - 1], 1));
                int currentDigit = int.Parse(new string(password[i], 1));

                if (prevDigit > currentDigit)
                {
                    neverDecrease = false;
                    break;
                }

                if (prevDigit == currentDigit)
                {
                    matchingPairsCount++;
                }
                else if (matchingPairsCount > 0)
                {
                    hasMatchingPair |= matchingPairsCount == 1;
                    matchingPairsCount = 0;
                }
            }

            hasMatchingPair |= matchingPairsCount == 1;

            return neverDecrease && hasMatchingPair;
        }
    }
}
