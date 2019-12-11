using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Day03
{
    public static class WirePathParser
    {
        public static IEnumerable<WirePathInstruction> Parse(string wirePath)
        {
            return wirePath
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(p => new WirePathInstruction(p.Trim()))
                .ToList();
        }
    }
}
