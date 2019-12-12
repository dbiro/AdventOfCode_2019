using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode._2019.Day06
{
    class Space
    {
        public SpaceObject COM { get; }


    }

    class SpaceObject
    {
        public int Value { get; private set; }

        IEnumerable<SpaceObject> Children { get; }
    }

    class Program
    {
        

        static void Main(string[] args)
        {
            string[] orbits = File.ReadAllLines("intput.txt");
            var orbitMap = new Dictionary<string, IEnumerable<string>>();
            foreach (var o in orbits)
            {
                
            }
        }
    }
}
