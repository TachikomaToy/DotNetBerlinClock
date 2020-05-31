using System;

namespace BerlinClock.Tests
{
    internal static class BerlinClockGenerator
    {
        private static string GenerateBinaryLightLine(int lightCount, char color)
        {
            var lamps = new char[lightCount];
            
            var illuminatedLights = StaticRandom.Next(0, lightCount + 1);
            
            var i = 0;

            for (; i < illuminatedLights - 1; i++)
            {
                lamps[i] = color;
            }

            for (; i < lightCount; i++)
            {
                lamps[i] = LampColor.Off;
            }
            
            return new string(lamps);
        }

        private static string GenerateSecondsLine() => StaticRandom.Next() % 2 == 0 ? LampColor.Yellow.ToString() : LampColor.Off.ToString();

        private static string GenerateHourLine() => GenerateBinaryLightLine(4, LampColor.Red);

        private static string GenerateFiveMinuteLine()
        {
            var lamps = new char[11];
            var illuminatedLights = StaticRandom.Next(0, 12);

            var i = 0;
            
            for (; i < illuminatedLights - 1; i++)
            {
                if ((i + 1) % 3 == 0)
                {
                    lamps[i] = LampColor.Red;
                }
                else
                {
                    lamps[i] = LampColor.Yellow;
                }
            }
            
            for (; i < lamps.Length; i++)
            {
                lamps[i] = LampColor.Off;
            }
            
            return new string(lamps);
        }
        
        private static string GenerateOneMinuteLine() => GenerateBinaryLightLine(4, LampColor.Yellow);

        internal static BerlinClock GenerateClock() => new BerlinClock(
            GenerateSecondsLine(),
            GenerateHourLine(),
            GenerateHourLine(),
            GenerateFiveMinuteLine(),
            GenerateOneMinuteLine());
    }
}