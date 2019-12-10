using System;

namespace AdventOfCode._2019.Day04
{
    class Program
    {        
        static void Main(string[] args)
        {
            int rangeStart = 278384;
            int rangeEnd = 824795;
            int password;
            int passwordCount = 0;

            for (password = rangeStart; password <= rangeEnd; password++)
            {
                string passwordToString = password.ToString();                

                if (PasswordValidator.Validate(passwordToString))
                {
                    passwordCount++;
                }
            }

            Console.WriteLine(passwordCount);
        }
    }
}
