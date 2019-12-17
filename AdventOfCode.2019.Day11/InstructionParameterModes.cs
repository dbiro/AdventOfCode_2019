using System;

namespace AdventOfCode._2019.Day11
{
    struct InstructionParameterModes
    {        
        public InstructionParameterMode First { get; private set; }
        public InstructionParameterMode Second { get; private set; }
        public InstructionParameterMode Third { get; private set; }

        public InstructionParameterModes(InstructionParameterMode first, InstructionParameterMode second, InstructionParameterMode third)
        {            
            First = first;
            Second = second;
            Third = third;
        }

        public override bool Equals(object obj)
        {
            return obj is InstructionParameterModes other &&
                   First == other.First &&
                   Second == other.Second &&
                   Third == other.Third;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(First, Second, Third);
        }                
    }
}
