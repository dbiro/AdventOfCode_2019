using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Day07
{
    public static class PhaseSettingGenerator
    {
        public static IEnumerable<int[]> Generate(int phaseSettingStart, int phaseSettingEnd)
        {
            int phaseSettingCount = phaseSettingEnd - phaseSettingStart + 1;

            foreach (int phase1 in Enumerable.Range(phaseSettingStart, phaseSettingCount))
                foreach (int phase2 in Enumerable.Range(phaseSettingStart, phaseSettingCount))
                    foreach (int phase3 in Enumerable.Range(phaseSettingStart, phaseSettingCount))
                        foreach (int phase4 in Enumerable.Range(phaseSettingStart, phaseSettingCount))
                            foreach (int phase5 in Enumerable.Range(phaseSettingStart, phaseSettingCount))
                            {
                                int[] currentPhaseSettings = new int[] { phase1, phase2, phase3, phase4, phase5 };
                                if (currentPhaseSettings.Distinct().Count() != currentPhaseSettings.Length)
                                {
                                    continue;
                                }
                                else
                                {
                                    yield return currentPhaseSettings;
                                }
                            }
        }
    }
}
