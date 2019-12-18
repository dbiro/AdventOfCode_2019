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

        public MoonSystem(MoonSystem moonSystem)
        {
            moons = moonSystem.moons.Select(m => new Moon(m)).ToList();
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

        public long Simulate()
        {
            Dictionary<long, List<MoonSystem>> previousStates = new Dictionary<long, List<MoonSystem>>();

            long i;
            for (i = 0; ; i++)
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

                long totalEnergy = TotalEnergy;
                if (!previousStates.ContainsKey(totalEnergy))
                {
                    previousStates[totalEnergy] = new List<MoonSystem> { new MoonSystem(this) };
                }
                else
                {
                    if (previousStates[totalEnergy].Any(ms => ms.moons.SequenceEqual(moons)))
                    {
                        break;
                    }
                    else
                    {
                        previousStates[totalEnergy].Add(new MoonSystem(this));
                    }
                }

                Console.SetCursorPosition(0, 0);
                Console.Write(i);
            }

            return i;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                foreach (var moon in moons)
                {
                    hash = hash * 31 + moon.GetHashCode();
                }
                return hash;
            }
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
