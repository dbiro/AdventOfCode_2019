using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Day12
{
    class Program
    {
        static void PrintMoons(IEnumerable<Moon> moons)
        {
            foreach (var moon in moons)
            {
                Console.WriteLine(moon);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            /*  INPUT */
            List<Moon> moons = new List<Moon>()
            {
                new Moon(14, 15, -2),
                new Moon(17, -3, 4),
                new Moon(6, 12, -13),
                new Moon(-2, 10, -8)
            };

            /* SAMPLE1 */
            //List<Moon> moons = new List<Moon>()
            //{
            //    new Moon(-1, 0, 2),
            //    new Moon(2, -10, -7),
            //    new Moon(4, -8, 8),
            //    new Moon(3, 5, -1)
            //};

            /*  SAMPLE2 */
            //List<Moon> moons = new List<Moon>()
            //{
            //    new Moon(-8, -10, 0),
            //    new Moon(5, 5, 10),
            //    new Moon(2, -7, 3),
            //    new Moon(9, -8, -3)
            //};

            PrintMoons(moons);

            const int steps = 1000;

            for (int i = 0; i < steps; i++)
            {
                foreach (var moon in moons)
                {
                    foreach (var otherMoon in moons)
                    {
                        moon.ApplyGravity(otherMoon);
                    }

                }

                foreach (var moon in moons)
                {
                    moon.ApplyVelocity();
                }

                //PrintMoons(moons);
            }

            Console.WriteLine(moons.Sum(m => m.TotalEnergy));            
        }
    }
}
