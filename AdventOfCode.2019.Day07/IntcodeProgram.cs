using System;

namespace AdventOfCode._2019.Day07
{
    public class IntcodeProgram
    {
        private readonly int[] program;
        private int currentInstructionIndex;

        public Func<int?> InputReader { get; }
        public Action<int> OutputWriter { get; }
        public bool Halted { get; private set; }
        public bool WaitingForInput { get; private set; }

        public IntcodeProgram(int[] program)
        {
            InputReader = () => int.Parse(Console.ReadLine());
            OutputWriter = o => Console.WriteLine(0);
            this.program = program;
        }

        public IntcodeProgram(int[] program, Func<int?> inputReader, Action<int> outputWriter)
        {
            InputReader = inputReader;
            OutputWriter = outputWriter;
            this.program = program;
        }

        public void Execute()
        {
            Execute(currentInstructionIndex);
        }

        private void Execute(int instructionIndex)
        {
            int nextInstructionIndex = instructionIndex;
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

        private int ReadParameterValue(int paramIndex, int mode)
        {
            switch (mode)
            {
                case 0: // position mode
                    return program[program[paramIndex]];
                case 1: // immediate mode
                    return program[paramIndex];
                default:
                    throw new ArgumentException($"Invalid parameter mode: {mode}, paramIndex: {paramIndex}");
            }
        }

        private (int OpCode, int FirstParamMode, int SecondParamMode) ParseInstructionMode(int instructionMode)
        {
            string instructionModeString = instructionMode.ToString();

            int opCode = instructionModeString.Length == 1 ? int.Parse(instructionModeString) : int.Parse(instructionModeString.Substring(instructionModeString.Length - 2, 2));
            int firsParamMode = instructionModeString.Length > 2 ? int.Parse(new string(instructionModeString[instructionModeString.Length - 3], 1)) : 0;
            int secondParamMode = instructionModeString.Length > 3 ? int.Parse(new string(instructionModeString[instructionModeString.Length - 4], 1)) : 0;

            return (opCode, firsParamMode, secondParamMode);
        }

        private int ExecuteJumpIfTrueInstruction(int instructionIndex, int firstParamMode, int secondParamMode)
        {
            int firstParam = ReadParameterValue(instructionIndex + 1, firstParamMode);
            int secondParam = ReadParameterValue(instructionIndex + 2, secondParamMode);

            if (firstParam != 0)
            {
                return secondParam;
            }
            else
            {
                return instructionIndex + 3;
            }
        }

        private int ExecuteJumpIfFalseInstruction(int instructionIndex, int firstParamMode, int secondParamMode)
        {
            int firstParam = ReadParameterValue(instructionIndex + 1, firstParamMode);
            int secondParam = ReadParameterValue(instructionIndex + 2, secondParamMode);

            if (firstParam == 0)
            {
                return secondParam;
            }
            else
            {
                return instructionIndex + 3;
            }
        }

        private int ExecuteLessThanInstruction(int instructionIndex, int firstParamMode, int secondParamMode)
        {
            int firstParam = ReadParameterValue(instructionIndex + 1, firstParamMode);
            int secondParam = ReadParameterValue(instructionIndex + 2, secondParamMode);

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

        private int ExecuteEqualsInstruction(int instructionIndex, int firstParamMode, int secondParamMode)
        {
            int firstParam = ReadParameterValue(instructionIndex + 1, firstParamMode);
            int secondParam = ReadParameterValue(instructionIndex + 2, secondParamMode);

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

        private int ExecuteReadInputInstruction(int instructionIndex)
        {
            int? inputValue = InputReader();
            if (inputValue.HasValue)
            {
                int inputValueIndex = program[instructionIndex + 1];
                program[inputValueIndex] = inputValue.Value;

                return instructionIndex + 2;
            }
            else
            {
                return instructionIndex;
            }
        }

        private int ExecuteWriteOutputInstruction(int instructionIndex, int paramMode)
        {
            int outputValue = ReadParameterValue(instructionIndex + 1, paramMode);

            OutputWriter(outputValue);

            return instructionIndex + 2;
        }

        private int ExecuteAddInstruction(int instructionIndex, int firsParamMode, int secondParamMode)
        {
            int param1 = ReadParameterValue(instructionIndex + 1, firsParamMode);
            int param2 = ReadParameterValue(instructionIndex + 2, secondParamMode);

            program[program[instructionIndex + 3]] = param1 + param2;

            return instructionIndex + 4;
        }

        private int ExecuteMutiplyInstruction(int instructionIndex, int firsParamMode, int secondParamMode)
        {
            int param1 = ReadParameterValue(instructionIndex + 1, firsParamMode);
            int param2 = ReadParameterValue(instructionIndex + 2, secondParamMode);

            program[program[instructionIndex + 3]] = param1 * param2;

            return instructionIndex + 4;
        }
    }
}
