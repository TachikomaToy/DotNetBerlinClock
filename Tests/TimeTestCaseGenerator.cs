using System;

namespace BerlinClock.Tests
{
    public static class TimeTestCaseGenerator
    {
        private static int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        private static int GenerateEvenSeconds()
        {
            var value = StaticRandom.Next(0, 58);

            return Clamp(value % 2 == 0 ? value : value - 1, 0, 58);
        }

        private static int GenerateOddSeconds()
        {
            var value = StaticRandom.Next(0, 59);

            return Clamp(value % 2 == 0 ? value - 1 : value, 1, 59);
        }

        private static TimeSpan ChangeTime(
            TimeSpan time, string berlinClockTimePart, int value, Func<int, TimeSpan> createTimePart)
        {
            var idx = 0;

            while (berlinClockTimePart[idx] != LampColor.Off)
            {
                time = time.Add(createTimePart(value));
                idx++;
            }

            return time;
        }

        public static (string inputTime, string expectedResult) Generate()
        {
            var clock = BerlinClockGenerator.GenerateClock();

            var time = TimeSpan.Zero;

            var seconds = clock.SecondsLine[0] == LampColor.Yellow ? GenerateEvenSeconds() : GenerateOddSeconds();

            time = time.Add(TimeSpan.FromSeconds(seconds));

            time = ChangeTime(time, clock.FiveHoursLine, 5, x => TimeSpan.FromHours(x));
            time = ChangeTime(time, clock.OneHoursLine, 1, x => TimeSpan.FromHours(x));
            time = ChangeTime(time, clock.FiveMinutesLine, 5, x => TimeSpan.FromMinutes(x));
            time = ChangeTime(time, clock.OneMinutesLine, 1, x => TimeSpan.FromMinutes(x));

            return (time.ToString(@"hh\:mm\:ss"), clock.ToString());
        }
    }
}