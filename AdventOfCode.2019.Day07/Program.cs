using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AdventOfCode._2019.Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            ImmutableArray<int> program = ImmutableArray.Create(InputReader.Read());

            // ImmutableArray<int> program = ImmutableArray.Create(new int[] { 3, 15, 3, 16, 1002, 16, 10, 16, 1, 16, 15, 15, 4, 15, 99, 0, 0 });

            // ImmutableArray<int> program = ImmutableArray.Create(new int[] { 3, 26, 1001, 26, -4, 26, 3, 27, 1002, 27, 2, 27, 1, 27, 26, 27, 4, 27, 1001, 28, -1, 28, 1005, 28, 6, 99, 0, 0, 5 });
            // ImmutableArray<int> program = ImmutableArray.Create(new int[] { 3, 52, 1001, 52, -5, 52, 3, 53, 1, 52, 56, 54, 1007, 54, 5, 55, 1005, 55, 26, 1001, 54, -5, 54, 1105, 1, 12, 1, 53, 54, 53, 1008, 54, 0, 55, 1001, 55, 1, 55, 2, 53, 55, 53, 4, 53, 1001, 56, -1, 56, 1005, 56, 6, 99, 0, 0, 0, 0, 10 });

            //var maxThrusters = CalculateMaxThrusters(program);
            //Console.WriteLine(maxThrusters.MaxOutput);
            //Console.WriteLine(string.Join(',', maxThrusters.PhaseSetting));

            var maxThrustersWithFeedbackLoop = CalculateMaxThrustersWithFeedbackLoop(program);
            Console.WriteLine(maxThrustersWithFeedbackLoop.MaxOutput);
            Console.WriteLine(string.Join(',', maxThrustersWithFeedbackLoop.PhaseSetting));
        }

        static (int MaxOutput, int[] PhaseSetting) CalculateMaxThrusters(ImmutableArray<int> program)
        {
            Queue<int> inputs = new Queue<int>();
            int currentOutput = 0;
            int maxOutput = 0;
            int[] phaseSettingsForMaxOutput = null;

            Func<int?> inputReader = () => inputs.Dequeue();
            Action<int> outputWriter = o => currentOutput = o;

            foreach (var currentPhaseSettings in PhaseSettingGenerator.Generate(0, 4))
            {
                IntcodeProgram[] amplifiers = Enumerable.Range(0, 5)
                    .Select(i => new IntcodeProgram(program.ToArray(), inputReader, outputWriter))
                    .ToArray();

                for (int i = 0; i < 5; i++)
                {
                    inputs.Enqueue(currentPhaseSettings[i]);
                    inputs.Enqueue(currentOutput);

                    amplifiers[i].Execute();
                }
                if (currentOutput > maxOutput)
                {
                    maxOutput = currentOutput;
                    phaseSettingsForMaxOutput = currentPhaseSettings;
                }

                currentOutput = 0;
            }

            return (maxOutput, phaseSettingsForMaxOutput);
        }

        static (int MaxOutput, int[] PhaseSetting) CalculateMaxThrustersWithFeedbackLoop(ImmutableArray<int> program)
        {
            Queue<int> inputs = new Queue<int>();
            int currentOutput = 0;
            int maxOutput = 0;
            int[] phaseSettingsForMaxOutput = null;

            Func<int?> inputReader = () =>
            {
                return inputs.TryDequeue(out int input) ? input : (int?)null;
            };
            Action<int> outputWriter = o => currentOutput = o;
                        
            foreach (var currentPhaseSettings in PhaseSettingGenerator.Generate(5, 9))
            {
                currentOutput = 0;
                inputs.Clear();

                IntcodeProgram[] amplifiers = Enumerable.Range(0, 5)
                    .Select(i => new IntcodeProgram(program.ToArray(), inputReader, outputWriter))
                    .ToArray();

                bool readPhaseSettings = true;

                while (!amplifiers.All(a => a.Halted))
                {                    
                    for (int i = 0; i < 5; i++)
                    {
                        if (readPhaseSettings)
                        {
                            inputs.Enqueue(currentPhaseSettings[i]);
                        }
                        inputs.Enqueue(currentOutput);

                        amplifiers[i].Execute();
                    }

                    readPhaseSettings = false;
                }

                if (currentOutput > maxOutput)
                {
                    maxOutput = currentOutput;
                    phaseSettingsForMaxOutput = currentPhaseSettings;
                }
            }

            return (maxOutput, phaseSettingsForMaxOutput);
        }
    }
}
