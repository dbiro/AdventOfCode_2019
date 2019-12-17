using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Day09
{
    public class IntcodeProgram
    {
        private readonly Dictionary<long, long> program;
        private long currentInstructionIndex;
        private long relativeBase;
        private long initialProgramSize;

        public Func<long?> InputReader { get; }
        public Action<long> OutputWriter { get; }
        public bool Halted { get; private set; }
        public bool WaitingForInput { get; private set; }

        #region enum InstructionParameterMode
        enum InstructionParameterMode
        {
            Position,
            Immediate,
            Relative
        }
        #endregion

        #region struct InstructionParameterModes
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
        #endregion

        public IntcodeProgram(long[] program)
        {
            InputReader = () => int.Parse(Console.ReadLine());
            OutputWriter = o => Console.WriteLine(o);
            this.program = new Dictionary<long, long>(program.Select((v, i) => new KeyValuePair<long, long>(i, v)), null);
            initialProgramSize = program.Length;
        }

        public IntcodeProgram(long[] program, Func<long?> inputReader, Action<long> outputWriter)
            : this(program)
        {
            InputReader = inputReader;
            OutputWriter = outputWriter;
        }

        public void Execute()
        {
            while (!WaitingForInput && !Halted)
            {
                Execute(currentInstructionIndex);
            }
        }

        private void Execute(long instructionIndex)
        {
            long nextInstructionIndex = -1;

            var parameterModes = ParseInstructionParameterModes(program[instructionIndex]);
            int opCode = ParseInstructionOpCode(program[instructionIndex]);

            switch (opCode)
            {
                case 1:
                    nextInstructionIndex = ExecuteAddInstruction(instructionIndex, parameterModes);
                    break;
                case 2:
                    nextInstructionIndex = ExecuteMutiplyInstruction(instructionIndex, parameterModes);
                    break;
                case 3:
                    nextInstructionIndex = ExecuteReadInputInstruction(instructionIndex, parameterModes);
                    WaitingForInput = nextInstructionIndex == instructionIndex;
                    break;
                case 4:
                    nextInstructionIndex = ExecuteWriteOutputInstruction(instructionIndex, parameterModes);
                    break;
                case 5:
                    nextInstructionIndex = ExecuteJumpIfTrueInstruction(instructionIndex, parameterModes);
                    break;
                case 6:
                    nextInstructionIndex = ExecuteJumpIfFalseInstruction(instructionIndex, parameterModes);
                    break;
                case 7:
                    nextInstructionIndex = ExecuteLessThanInstruction(instructionIndex, parameterModes);
                    break;
                case 8:
                    nextInstructionIndex = ExecuteEqualsInstruction(instructionIndex, parameterModes);
                    break;
                case 9:
                    nextInstructionIndex = ExecuteAdjustRelativeBaseInstruction(instructionIndex, parameterModes);
                    break;
                case 99:
                    Halted = true;
                    break;  // program halted
                default:
                    throw new InvalidOperationException($"Invalid opcode: {opCode}");
            }

            if (!Halted)
            {
                currentInstructionIndex = nextInstructionIndex;
            }
        }

        private long DeterminePositionFromParameterMode(long paramIndex, InstructionParameterMode mode)
        {
            long position;

            switch (mode)
            {
                case InstructionParameterMode.Position: // position mode
                    position = program[paramIndex];
                    break;
                case InstructionParameterMode.Immediate: // immediate mode
                    position = paramIndex;
                    break;
                case InstructionParameterMode.Relative:
                    position = program[paramIndex] + relativeBase;
                    break;
                default:
                    throw new ArgumentException($"Invalid parameter mode: {mode}, paramIndex: {paramIndex}");
            }

            if (position < 0)
            {
                throw new ArgumentOutOfRangeException("Can not read value from memory! Memory position van not be negative!");
            }

            return position;
        }

        private long ReadParameterValue(long paramIndex, InstructionParameterMode mode)
        {
            long position = DeterminePositionFromParameterMode(paramIndex, mode);
            return program.ContainsKey(position) ? program[position] : 0;
        }

        private void WriteValue(long paramIndex, InstructionParameterMode mode, long value)
        {
            long inputValuePosition = DeterminePositionFromParameterMode(paramIndex, mode);

            if (inputValuePosition < 0)
            {
                throw new ArgumentOutOfRangeException("Can not write value in memory! Memory position van not be negative!");
            }

            program[inputValuePosition] = value;
        }

        private int ParseInstructionOpCode(long instruction)
        {
            string instructionModeString = instruction.ToString();

            return instructionModeString.Length == 1 ? int.Parse(instructionModeString) : int.Parse(instructionModeString.Substring(instructionModeString.Length - 2, 2));
        }

        private InstructionParameterModes ParseInstructionParameterModes(long instruction)
        {
            string instructionModeString = instruction.ToString();
                        
            var firsParamMode = instructionModeString.Length > 2 ?
                Enum.Parse<InstructionParameterMode>((new string(instructionModeString[instructionModeString.Length - 3], 1))) : 
                InstructionParameterMode.Position;

            var secondParamMode = instructionModeString.Length > 3 ?
                Enum.Parse<InstructionParameterMode>((new string(instructionModeString[instructionModeString.Length - 4], 1))) :
                InstructionParameterMode.Position;

            var resultParamMode = instructionModeString.Length > 4 ?
                Enum.Parse<InstructionParameterMode>((new string(instructionModeString[instructionModeString.Length - 5], 1))) :
                InstructionParameterMode.Position;

            return new InstructionParameterModes(firsParamMode, secondParamMode, resultParamMode);
        }
                
        private long ExecuteAdjustRelativeBaseInstruction(long instructionIndex, InstructionParameterModes parameterModes)
        {
            long firstParam = ReadParameterValue(instructionIndex + 1, parameterModes.First);
            relativeBase += firstParam;

            return instructionIndex + 2;
        }

        private long ExecuteJumpIfTrueInstruction(long instructionIndex, InstructionParameterModes parameterModes)
        {
            long firstParam = ReadParameterValue(instructionIndex + 1, parameterModes.First);
            long secondParam = ReadParameterValue(instructionIndex + 2, parameterModes.Second);

            if (firstParam != 0)
            {
                return secondParam;
            }
            else
            {
                return instructionIndex + 3;
            }
        }

        private long ExecuteJumpIfFalseInstruction(long instructionIndex, InstructionParameterModes parameterModes)
        {
            long firstParam = ReadParameterValue(instructionIndex + 1, parameterModes.First);
            long secondParam = ReadParameterValue(instructionIndex + 2, parameterModes.Second);

            if (firstParam == 0)
            {
                return secondParam;
            }
            else
            {
                return instructionIndex + 3;
            }
        }

        private long ExecuteLessThanInstruction(long instructionIndex, InstructionParameterModes parameterModes)
        {
            long firstParam = ReadParameterValue(instructionIndex + 1, parameterModes.First);
            long secondParam = ReadParameterValue(instructionIndex + 2, parameterModes.Second);

            if (firstParam < secondParam)
            {
                WriteValue(instructionIndex + 3, parameterModes.Third, 1);
            }
            else
            {
                WriteValue(instructionIndex + 3, parameterModes.Third, 0);
            }

            return instructionIndex + 4;
        }

        private long ExecuteEqualsInstruction(long instructionIndex, InstructionParameterModes parameterModes)
        {
            long firstParam = ReadParameterValue(instructionIndex + 1, parameterModes.First);
            long secondParam = ReadParameterValue(instructionIndex + 2, parameterModes.Second);

            if (firstParam == secondParam)
            {
                WriteValue(instructionIndex + 3, parameterModes.Third, 1);
            }
            else
            {
                WriteValue(instructionIndex + 3, parameterModes.Third, 0);
            }

            return instructionIndex + 4;
        }

        private long ExecuteReadInputInstruction(long instructionIndex, InstructionParameterModes parameterModes)
        {
            long? inputValue = InputReader();
            if (inputValue.HasValue)
            {                
                WriteValue(instructionIndex + 1, parameterModes.First, inputValue.Value);
                return instructionIndex + 2;
            }
            else
            {
                return instructionIndex;
            }
        }

        private long ExecuteWriteOutputInstruction(long instructionIndex, InstructionParameterModes parameterModes)
        {
            long outputValue = ReadParameterValue(instructionIndex + 1, parameterModes.First);

            OutputWriter(outputValue);

            return instructionIndex + 2;
        }

        private long ExecuteAddInstruction(long instructionIndex, InstructionParameterModes parameterModes)
        {
            long param1 = ReadParameterValue(instructionIndex + 1, parameterModes.First);
            long param2 = ReadParameterValue(instructionIndex + 2, parameterModes.Second);

            WriteValue(instructionIndex + 3, parameterModes.Third, param1 + param2);

            return instructionIndex + 4;
        }

        private long ExecuteMutiplyInstruction(long instructionIndex, InstructionParameterModes parameterModes)
        {
            long param1 = ReadParameterValue(instructionIndex + 1, parameterModes.First);
            long param2 = ReadParameterValue(instructionIndex + 2, parameterModes.Second);

            WriteValue(instructionIndex + 3, parameterModes.Third, param1 * param2);

            return instructionIndex + 4;
        }
    }
}
