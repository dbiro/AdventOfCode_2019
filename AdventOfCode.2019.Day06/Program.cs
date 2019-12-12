using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2019.Day06
{
    class OrbitMap : Dictionary<string, ICollection<string>>
    { }

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
            //    "K)L",
            //    "K)YOU",
            //    "I)SAN"
            //};

            var orbitMap = ParseOrbitMap(orbits);

            int checksum = CalculateChecksumForOrbitMap(orbitMap);
            Console.WriteLine(checksum);

            var visitedObjectsYOU = FindSpaceObjectInOrbitMap("YOU", orbitMap).ToList();
            //Console.WriteLine(visitedObjectsYOU.Count);
            //Console.WriteLine(string.Join(',', visitedObjectsYOU));

            var visitedObjectsSAN = FindSpaceObjectInOrbitMap("SAN", orbitMap).ToList();
            //Console.WriteLine(visitedObjectsSAN.Count);
            //Console.WriteLine(string.Join(',', visitedObjectsSAN));

            int commonVisitedObjectCount = visitedObjectsYOU.Intersect(visitedObjectsSAN).Count();
            int minimumTransfersRequired = visitedObjectsYOU.Count + visitedObjectsSAN.Count - 2 * commonVisitedObjectCount;
            Console.WriteLine(minimumTransfersRequired);
        }

        static OrbitMap ParseOrbitMap(string[] orbits)
        {
            var orbitMap = new OrbitMap();
            foreach (var orbit in orbits)
            {
                string[] spaceObjects = orbit.Split(")");
                if (!orbitMap.ContainsKey(spaceObjects[0]))
                {
                    orbitMap[spaceObjects[0]] = new List<string>();
                }
                orbitMap[spaceObjects[0]].Add(spaceObjects[1]);
            }
            return orbitMap;
        }

        static int CalculateChecksumForOrbitMap(OrbitMap orbitMap)
        {
            return CalculateChecksumForSpaceObject("COM", orbitMap, 0);
        }

        static int CalculateChecksumForSpaceObject(string spaceObject, OrbitMap orbitMap, int depth)
        {
            if (orbitMap.ContainsKey(spaceObject))
            {
                int orbitCount = depth;

                foreach (var child in orbitMap[spaceObject])
                {
                    orbitCount += CalculateChecksumForSpaceObject(child, orbitMap, depth + 1);
                }

                return orbitCount;
            }
            else
            {
                return depth;
            }
        }

        static IEnumerable<string> FindSpaceObjectInOrbitMap(string objectToFind, OrbitMap orbitMap)
        {
            var visitedObjects = new HashSet<string>();
            FindSpaceObject("COM", objectToFind, orbitMap, visitedObjects);
            return visitedObjects;
        }

        static bool FindSpaceObject(string spaceObject, string spaceObjectToFind, OrbitMap orbitMap, HashSet<string> visitedObjects)
        {
            if (spaceObject.Equals(spaceObjectToFind))
            {
                return true;
            }
                       
            if (orbitMap.ContainsKey(spaceObject))
            {
                visitedObjects.Add(spaceObject);
                bool found = false;

                foreach (var child in orbitMap[spaceObject])
                {
                    found |= FindSpaceObject(child, spaceObjectToFind, orbitMap, visitedObjects);
                }

                if (!found)
                {
                    visitedObjects.Remove(spaceObject);
                }

                return found;
            }
            else
            {
                return false;
            }
        }
    }
}
