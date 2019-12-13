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
            //ImmutableArray<int> program = ImmutableArray.Create(new int[] { 3, 31, 3, 32, 1002, 32, 10, 32, 1001, 31, -2, 31, 1007, 31, 0, 33, 1002, 33, 7, 33, 1, 33, 31, 31, 1, 32, 31, 31, 4, 31, 99, 0, 0, 0 });
            ImmutableArray<int> program = ImmutableArray.Create(InputReader.Read());

            Queue<int> inputs = new Queue<int>(2);
            int currentOutput = 0;
            int maxOutput = 0;
            int[] phaseSettingsForMaxOutput = null;

            Func<int> inputReader = () => inputs.Dequeue();
            Action<int> outputWriter = o => currentOutput = o;

            IntcodeProgram[] amplifiers = Enumerable.Range(0, 5)
                .Select(i => new IntcodeProgram(program.ToArray(), inputReader, outputWriter))
                .ToArray();
                                    
            foreach (int phase1 in Enumerable.Range(0, 5))
                foreach (int phase2 in Enumerable.Range(0, 5))
                    foreach (int phase3 in Enumerable.Range(0, 5))
                        foreach (int phase4 in Enumerable.Range(0, 5))
                            foreach (int phase5 in Enumerable.Range(0, 5))
                            {
                                int[] currentPhaseSettings = new int[] { phase1, phase2, phase3, phase4, phase5 };
                                if (currentPhaseSettings.Distinct().Count() != currentPhaseSettings.Length)
                                {
                                    continue;
                                }
                                for (int i = 0; i < 5; i++)
                                {
                                    inputs.Clear();
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
                        
            Console.WriteLine(maxOutput);
            Console.WriteLine(string.Join(',', phaseSettingsForMaxOutput));
        }
    }
}
