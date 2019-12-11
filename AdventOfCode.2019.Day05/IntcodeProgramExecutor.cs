using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2019.Day05
{
    public static class IntcodeProgramExecutor
    {
        public static TextReader InputReader { get; set; }
        public static TextWriter OutputWriter { get; set; }

        static IntcodeProgramExecutor()
        {
            InputReader = Console.In;
            OutputWriter = Console.Out;
        }

        public static int[] Execute(int[] program)
        {
            return Execute(program, 0);
        }

        private static int[] Execute(int[] program, int instructionIndex)
        {
            int[] modifiedProgram = null;
            int nextInstructionIndex = 0;
            bool halted = false;

            var instructionMode = ParseInstructionMode(program[instructionIndex]);

            switch (instructionMode.OpCode)
            {
                case 1:
                    modifiedProgram = ExecuteAddInstruction(program, instructionIndex, instructionMode.FirstArgMode, instructionMode.SecondArgMode);
                    nextInstructionIndex = instructionIndex + 4;
                    break;
                case 2:
                    modifiedProgram = ExecuteMutiplyInstruction(program, instructionIndex, instructionMode.FirstArgMode, instructionMode.SecondArgMode);
                    nextInstructionIndex = instructionIndex + 4;
                    break;
                case 3:
                    modifiedProgram = ExecuteReadInputInstruction(program, instructionIndex);
                    nextInstructionIndex = instructionIndex + 2;
                    break;
                case 4:
                    ExecuteWriteOutputInstruction(program, instructionIndex, instructionMode.FirstArgMode);
                    nextInstructionIndex = instructionIndex + 2;
                    modifiedProgram = program;
                    break;
                case 99:
                    OutputWriter.WriteLine("Program halted");
                    halted = true;
                    break;  // program halted
                default:
                    throw new InvalidOperationException($"Invalid opcode: {instructionMode.OpCode}");
            }

            if (halted)
            {
                return program;
            }
            else
            {
                return Execute(modifiedProgram, nextInstructionIndex);
            }
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

        private static (int OpCode, int FirstArgMode, int SecondArgMode, int ResultArgMode) ParseInstructionMode(int instructionMode)
        {
            string instructionModeString = instructionMode.ToString();

            int opCode = instructionModeString.Length == 1 ? int.Parse(instructionModeString) : int.Parse(instructionModeString.Substring(instructionModeString.Length - 2, 2));
            int firsArgMode = instructionModeString.Length > 2 ? int.Parse(new string(instructionModeString[instructionModeString.Length - 3], 1)) : 0;
            int secondArgMode = instructionModeString.Length > 3 ? int.Parse(new string(instructionModeString[instructionModeString.Length - 4], 1)) : 0;
            int resultArgMode = instructionModeString.Length > 4 ? int.Parse(new string(instructionModeString[instructionModeString.Length - 5], 1)) : 0;

            return (opCode, firsArgMode, secondArgMode, resultArgMode);
        }

        private static int[] ExecuteReadInputInstruction(int[] program, int instructionIndex)
        {
            OutputWriter.WriteLine("Provide an input value:");
            int inputValue = int.Parse(InputReader.ReadLine());

            int inputValueIndex = program[instructionIndex + 1];
            program[inputValueIndex] = inputValue;

            return program;
        }

        private static void ExecuteWriteOutputInstruction(int[] program, int instructionIndex, int paramMode)
        {
            int outputValue = GetParameter(program, instructionIndex + 1, paramMode);
            OutputWriter.WriteLine($"Output value: {outputValue}");
        }

        private static int[] ExecuteAddInstruction(int[] program, int instructionIndex, int firsParamMode, int secondParamMode)
        {                        
            int resultIndex = program[instructionIndex + 3];

            int param1 = GetParameter(program, instructionIndex + 1, firsParamMode);
            int param2 = GetParameter(program, instructionIndex + 2, secondParamMode);

            int result = param1 + param2;
            program[resultIndex] = result;

            return program;
        }

        private static int[] ExecuteMutiplyInstruction(int[] program, int instructionIndex, int firsParamMode, int secondParamMode)
        {
            int resultIndex = program[instructionIndex + 3];

            int param1 = GetParameter(program, instructionIndex + 1, firsParamMode);
            int param2 = GetParameter(program, instructionIndex + 2, secondParamMode);

            int result = param1 * param2;
            program[resultIndex] = result;

            return program;
        }
    }
}
