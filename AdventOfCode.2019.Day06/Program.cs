using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode._2019.Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] orbits = File.ReadAllLines("input.txt");

            //string[] orbits = new string[]
            //{
            //    "COM)B",
            //    "B)C",
            //    "C)D",
            //    "D)E",
            //    "E)F",
            //    "B)G",
            //    "G)H",
            //    "D)I",
            //    "E)J",
            //    "J)K",
            //    "K)L"
            //};

            var orbitMap = new Dictionary<string, ICollection<string>>();
            foreach (var orbit in orbits)
            {
                string[] spaceObjects = orbit.Split(")");
                if (!orbitMap.ContainsKey(spaceObjects[0]))
                {
                    orbitMap[spaceObjects[0]] = new List<string>();
                }
                orbitMap[spaceObjects[0]].Add(spaceObjects[1]);
            }

            int orbitCount = TraverseOrbitMap(orbitMap);

            Console.WriteLine(orbitCount);
        }

        static int TraverseOrbitMap(Dictionary<string, ICollection<string>> orbitMap)
        {
            int orbitCount = 0;
            TraverseSpaceObject("COM", orbitMap, 0, ref orbitCount);
            return orbitCount;
        }

        static void TraverseSpaceObject(string spaceObject, Dictionary<string, ICollection<string>> orbitMap, int depth, ref int orbitCount)
        {            
            if (orbitMap.ContainsKey(spaceObject))
            {
                orbitCount += depth;

                foreach (var child in orbitMap[spaceObject])
                {
                    TraverseSpaceObject(child, orbitMap, depth + 1, ref orbitCount);
                }
            }
            else
            {                
                orbitCount += depth;
            }            
        }
    }
}
