using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Day12
{
    class MoonSystem
    {
        private readonly List<Moon> moons;

        public long TotalEnergy
            => moons.Sum(m => m.TotalEnergy);

        public MoonSystem(IEnumerable<Moon> moons)
        {
            this.moons = new List<Moon>(moons);
        }                

        public long Simulate(int steps, bool verbose = false)
        {
            if (verbose)
            {
                PrintMoons();
            }

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

                if (verbose)
                {
                    PrintMoons();
                }
            }

            return TotalEnergy;
        }

        public (long, long, long) FindPeriod()
        {            
            string[] axes = new string[] { "X", "Y", "Z" };
            var gravityActions = new Dictionary<string, Action<Moon, Moon>>()
            {
                { "X", (m,o) => m.ApplyGravityForAxisX(o) },
                { "Y", (m,o) => m.ApplyGravityForAxisY(o) },
                { "Z", (m,o) => m.ApplyGravityForAxisZ(o) }
            };
            var velocityActions = new Dictionary<string, Action<Moon>>()
            {
                { "X", (m) => m.ApplyVelocityForAxisX() },
                { "Y", (m) => m.ApplyVelocityForAxisY() },
                { "Z", (m) => m.ApplyVelocityForAxisZ() }
            };
            var isAtStartActions = new Dictionary<string, Func<Moon, bool>>()
            {
                { "X", (m) => m.IsAtStartOnAxisX() },
                { "Y", (m) => m.IsAtStartOnAxisY() },
                { "Z", (m) => m.IsAtStartOnAxisZ() }
            };
            var periods = new Dictionary<string, long>();

            foreach (var axis in axes)
            {                
                int i = 0;
                while (true)
                {
                    foreach (var moon in moons)
                    {
                        foreach (var otherMoon in moons)
                        {
                            gravityActions[axis](moon, otherMoon);
                        }
                    }

                    foreach (var moon in moons)
                    {
                        velocityActions[axis](moon);
                    }

                    i++;

                    if (moons.All(m => isAtStartActions[axis](m)))
                    {
                        break;
                    }                    
                }

                periods[axis] = i;
            }

            return (periods["X"], periods["Y"], periods["Z"]);
        }
                
        private void PrintMoons()
        {
            foreach (var moon in moons)
            {
                Console.WriteLine(moon);
            }
            Console.WriteLine();
        }

    }
}
