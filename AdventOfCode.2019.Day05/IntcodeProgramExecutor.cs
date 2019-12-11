using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2019.Day05
{
    public static class IntcodeProgramExecutor
    {
        public static TextReader InputReader { get; set; }
        public static TextWriter OutputWriter { get; set; }
        public static bool EnableVerboseMode { get; set; }

        static IntcodeProgramExecutor()
        {
            InputReader = Console.In;
            OutputWriter = Console.Out;
            EnableVerboseMode = false;
        }

        public static void Execute(int[] program)
        {
            Execute(program, 0);
        }

        private static void Execute(int[] program, int instructionIndex)
        {
            int nextInstructionIndex = instructionIndex;
            bool halted = false;

            var instructionMode = ParseInstructionMode(program[instructionIndex]);

            switch (instructionMode.OpCode)
            {
                case 1:
                    nextInstructionIndex = ExecuteAddInstruction(program, instructionIndex, instructionMode.FirstParamMode, instructionMode.SecondParamMode);
                    break;
                case 2:
                    nextInstructionIndex = ExecuteMutiplyInstruction(program, instructionIndex, instructionMode.FirstParamMode, instructionMode.SecondParamMode);
                    break;
                case 3:
                    nextInstructionIndex = ExecuteReadInputInstruction(program, instructionIndex);
                    break;
                case 4:
                    nextInstructionIndex = ExecuteWriteOutputInstruction(program, instructionIndex, instructionMode.FirstParamMode);
                    break;
                case 5:
                    nextInstructionIndex = ExecuteJumpIfTrueInstruction(program, instructionIndex, instructionMode.FirstParamMode, instructionMode.SecondParamMode);
                    break;
                case 6:
                    nextInstructionIndex = ExecuteJumpIfFalseInstruction(program, instructionIndex, instructionMode.FirstParamMode, instructionMode.SecondParamMode);
                    break;
                case 7:
                    nextInstructionIndex = ExecuteLessThanInstruction(program, instructionIndex, instructionMode.FirstParamMode, instructionMode.SecondParamMode);
                    break;
                case 8:
                    nextInstructionIndex = ExecuteEqualsInstruction(program, instructionIndex, instructionMode.FirstParamMode, instructionMode.SecondParamMode);
                    break;
                case 99:
                    OutputWriter.WriteLine("Program halted");
                    halted = true;
                    break;  // program halted
                default:
                    throw new InvalidOperationException($"Invalid opcode: {instructionMode.OpCode}");
            }           

            if (!halted)
            {
                if (EnableVerboseMode)
                {
                    DumpProgram(program);
                }
                Execute(program, nextInstructionIndex);
            }            
        }

        private static void DumpProgram(int[] program)
        {
            OutputWriter.WriteLine(string.Join(',', program));
        }

        private static int GetParameter(int[] program, int paramIndex, int mode)
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

        private static (int OpCode, int FirstParamMode, int SecondParamMode) ParseInstructionMode(int instructionMode)
        {
            string instructionModeString = instructionMode.ToString();

            int opCode = instructionModeString.Length == 1 ? int.Parse(instructionModeString) : int.Parse(instructionModeString.Substring(instructionModeString.Length - 2, 2));
            int firsParamMode = instructionModeString.Length > 2 ? int.Parse(new string(instructionModeString[instructionModeString.Length - 3], 1)) : 0;
            int secondParamMode = instructionModeString.Length > 3 ? int.Parse(new string(instructionModeString[instructionModeString.Length - 4], 1)) : 0;

            return (opCode, firsParamMode, secondParamMode);
        }

        private static int ExecuteJumpIfTrueInstruction(int[] program, int instructionIndex, int firstParamMode, int secondParamMode)
        {
            int firstParam = GetParameter(program, instructionIndex + 1, firstParamMode);
            int secondParam = GetParameter(program, instructionIndex + 2, secondParamMode);

            if (firstParam != 0)
            {
                return secondParam;
            }
            else
            {
                return instructionIndex + 3;
            }
        }

        private static int ExecuteJumpIfFalseInstruction(int[] program, int instructionIndex, int firstParamMode, int secondParamMode)
        {
            int firstParam = GetParameter(program, instructionIndex + 1, firstParamMode);
            int secondParam = GetParameter(program, instructionIndex + 2, secondParamMode);

            if (firstParam == 0)
            {
                return secondParam;
            }
            else
            {
                return instructionIndex + 3;
            }
        }

        private static int ExecuteLessThanInstruction(int[] program, int instructionIndex, int firstParamMode, int secondParamMode)
        {
            int firstParam = GetParameter(program, instructionIndex + 1, firstParamMode);
            int secondParam = GetParameter(program, instructionIndex + 2, secondParamMode);

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

        private static int ExecuteEqualsInstruction(int[] program, int instructionIndex, int firstParamMode, int secondParamMode)
        {
            int firstParam = GetParameter(program, instructionIndex + 1, firstParamMode);
            int secondParam = GetParameter(program, instructionIndex + 2, secondParamMode);

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

        private static int ExecuteReadInputInstruction(int[] program, int instructionIndex)
        {
            OutputWriter.WriteLine("Provide an input value:");
            int inputValue = int.Parse(InputReader.ReadLine());

            int inputValueIndex = program[instructionIndex + 1];
            program[inputValueIndex] = inputValue;

            return instructionIndex + 2;
        }

        private static int ExecuteWriteOutputInstruction(int[] program, int instructionIndex, int paramMode)
        {
            int outputValue = GetParameter(program, instructionIndex + 1, paramMode);
            OutputWriter.WriteLine($"Output value: {outputValue}");

            return instructionIndex + 2;
        }

        private static int ExecuteAddInstruction(int[] program, int instructionIndex, int firsParamMode, int secondParamMode)
        {
            int param1 = GetParameter(program, instructionIndex + 1, firsParamMode);
            int param2 = GetParameter(program, instructionIndex + 2, secondParamMode);

            program[program[instructionIndex + 3]] = param1 + param2;

            return instructionIndex + 4;
        }

        private static int ExecuteMutiplyInstruction(int[] program, int instructionIndex, int firsParamMode, int secondParamMode)
        {
            int param1 = GetParameter(program, instructionIndex + 1, firsParamMode);
            int param2 = GetParameter(program, instructionIndex + 2, secondParamMode);

            program[program[instructionIndex + 3]] = param1 * param2;

            return instructionIndex + 4;
        }
    }
}
