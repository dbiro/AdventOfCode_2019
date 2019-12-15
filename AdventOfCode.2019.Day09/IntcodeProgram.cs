using System;

namespace AdventOfCode._2019.Day09
{
    public class IntcodeProgram
    {
        private readonly long[] program;
        private long currentInstructionIndex;
        private long relativeBase;

        public Func<long?> InputReader { get; }
        public Action<long> OutputWriter { get; }
        public bool Halted { get; private set; }
        public bool WaitingForInput { get; private set; }

        public IntcodeProgram(long[] program)
        {
            InputReader = () => int.Parse(Console.ReadLine());
            OutputWriter = o => Console.WriteLine(0);
            this.program = program;
        }

        public IntcodeProgram(long[] program, Func<long?> inputReader, Action<long> outputWriter)
        {
            InputReader = inputReader;
            OutputWriter = outputWriter;
            this.program = program;
        }

        public void Execute()
        {
            Execute(currentInstructionIndex);
        }

        private void Execute(long instructionIndex)
        {
            long nextInstructionIndex = instructionIndex;
            var instructionMode = ParseInstructionMode(program[instructionIndex]);

            switch (instructionMode.OpCode)
            {
                case 1:
                    nextInstructionIndex = ExecuteAddInstruction(instructionIndex, instructionMode.FirstParamMode, instructionMode.SecondParamMode);
                    break;
                case 2:
                    nextInstructionIndex = ExecuteMutiplyInstruction(instructionIndex, instructionMode.FirstParamMode, instructionMode.SecondParamMode);
                    break;
                case 3:
                    nextInstructionIndex = ExecuteReadInputInstruction(instructionIndex);
                    WaitingForInput = nextInstructionIndex == instructionIndex;
                    break;
                case 4:
                    nextInstructionIndex = ExecuteWriteOutputInstruction(instructionIndex, instructionMode.FirstParamMode);
                    break;
                case 5:
                    nextInstructionIndex = ExecuteJumpIfTrueInstruction(instructionIndex, instructionMode.FirstParamMode, instructionMode.SecondParamMode);
                    break;
                case 6:
                    nextInstructionIndex = ExecuteJumpIfFalseInstruction(instructionIndex, instructionMode.FirstParamMode, instructionMode.SecondParamMode);
                    break;
                case 7:
                    nextInstructionIndex = ExecuteLessThanInstruction(instructionIndex, instructionMode.FirstParamMode, instructionMode.SecondParamMode);
                    break;
                case 8:
                    nextInstructionIndex = ExecuteEqualsInstruction(instructionIndex, instructionMode.FirstParamMode, instructionMode.SecondParamMode);
                    break;
                case 9:
                    nextInstructionIndex = ExecuteAdjustRelativeBaseInstruction(instructionIndex, instructionMode.FirstParamMode);
                    break;
                case 99:
                    Halted = true;
                    break;  // program halted
                default:
                    throw new InvalidOperationException($"Invalid opcode: {instructionMode.OpCode}");
            }

            currentInstructionIndex = instructionIndex;

            if (!WaitingForInput && !Halted)
            {
                Execute(nextInstructionIndex);
            }            
        }

        private long ReadParameterValue(long paramIndex, int mode)
        {
            switch (mode)
            {
                case 0: // position mode
                    return program[program[paramIndex]];
                case 1: // immediate mode
                    return program[paramIndex];
                case 2:
                    return program[program[paramIndex] + relativeBase];
                default:
                    throw new ArgumentException($"Invalid parameter mode: {mode}, paramIndex: {paramIndex}");
            }
        }

        private (int OpCode, int FirstParamMode, int SecondParamMode) ParseInstructionMode(long instructionMode)
        {
            string instructionModeString = instructionMode.ToString();

            int opCode = instructionModeString.Length == 1 ? int.Parse(instructionModeString) : int.Parse(instructionModeString.Substring(instructionModeString.Length - 2, 2));
            int firsParamMode = instructionModeString.Length > 2 ? int.Parse(new string(instructionModeString[instructionModeString.Length - 3], 1)) : 0;
            int secondParamMode = instructionModeString.Length > 3 ? int.Parse(new string(instructionModeString[instructionModeString.Length - 4], 1)) : 0;

            return (opCode, firsParamMode, secondParamMode);
        }

        private long ExecuteAdjustRelativeBaseInstruction(long instructionIndex, int firstParamMode)
        {
            long firstParam = ReadParameterValue(instructionIndex + 1, firstParamMode);
            relativeBase = firstParam;

            return instructionIndex + 2;
        }

        private long ExecuteJumpIfTrueInstruction(long instructionIndex, int firstParamMode, int secondParamMode)
        {
            long firstParam = ReadParameterValue(instructionIndex + 1, firstParamMode);
            long secondParam = ReadParameterValue(instructionIndex + 2, secondParamMode);

            if (firstParam != 0)
            {
                return secondParam;
            }
            else
            {
                return instructionIndex + 3;
            }
        }

        private long ExecuteJumpIfFalseInstruction(long instructionIndex, int firstParamMode, int secondParamMode)
        {
            long firstParam = ReadParameterValue(instructionIndex + 1, firstParamMode);
            long secondParam = ReadParameterValue(instructionIndex + 2, secondParamMode);

            if (firstParam == 0)
            {
                return secondParam;
            }
            else
            {
                return instructionIndex + 3;
            }
        }

        private long ExecuteLessThanInstruction(long instructionIndex, int firstParamMode, int secondParamMode)
        {
            long firstParam = ReadParameterValue(instructionIndex + 1, firstParamMode);
            long secondParam = ReadParameterValue(instructionIndex + 2, secondParamMode);

            if (firstParam < secondParam)
            {
                program[program[instructionIndex + 3]] = 1;
            }
            else
            {
                program[program[instructionIndex + 3]] = 0;
            }

            return instructionIndex + 4;
        }

        private long ExecuteEqualsInstruction(long instructionIndex, int firstParamMode, int secondParamMode)
        {
            long firstParam = ReadParameterValue(instructionIndex + 1, firstParamMode);
            long secondParam = ReadParameterValue(instructionIndex + 2, secondParamMode);

            if (firstParam == secondParam)
            {
                program[program[instructionIndex + 3]] = 1;
            }
            else
            {
                program[program[instructionIndex + 3]] = 0;
            }

            return instructionIndex + 4;
        }

        private long ExecuteReadInputInstruction(long instructionIndex)
        {
            long? inputValue = InputReader();
            if (inputValue.HasValue)
            {
                long inputValueIndex = program[instructionIndex + 1];
                program[inputValueIndex] = inputValue.Value;

                return instructionIndex + 2;
            }
            else
            {
                return instructionIndex;
            }
        }

        private long ExecuteWriteOutputInstruction(long instructionIndex, int paramMode)
        {
            long outputValue = ReadParameterValue(instructionIndex + 1, paramMode);

            OutputWriter(outputValue);

            return instructionIndex + 2;
        }

        private long ExecuteAddInstruction(long instructionIndex, int firsParamMode, int secondParamMode)
        {
            long param1 = ReadParameterValue(instructionIndex + 1, firsParamMode);
            long param2 = ReadParameterValue(instructionIndex + 2, secondParamMode);

            program[program[instructionIndex + 3]] = param1 + param2;

            return instructionIndex + 4;
        }

        private long ExecuteMutiplyInstruction(long instructionIndex, int firsParamMode, int secondParamMode)
        {
            long param1 = ReadParameterValue(instructionIndex + 1, firsParamMode);
            long param2 = ReadParameterValue(instructionIndex + 2, secondParamMode);

            program[program[instructionIndex + 3]] = param1 * param2;

            return instructionIndex + 4;
        }
    }
}
