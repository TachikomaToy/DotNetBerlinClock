using System;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        // Since this contract receives only string representation of time, I've kept this solution as simple as possible (without unnecessary intermediate
        // objects for Berlin clock, time formatters, renderers, etc.)
        // As soon as this contract assume input/output time format then I would consider to implement parsing and formatting logic in separate objects
        public string convertTime(string aTime)
        {
            var (hours, minutes, seconds) = TryParse(aTime);

            string secondsLine, fiveHoursLine, oneHoursLine, fiveMinutesLine, oneMinuteLine;

            secondsLine     = seconds % 2 == 0 ? LampColor.Yellow.ToString() : LampColor.Off.ToString();
            fiveHoursLine   = new string(LampColor.Red, hours / 5) + new string(LampColor.Off, 4 - hours / 5);
            oneHoursLine    = new string(LampColor.Red, hours % 5) + new string(LampColor.Off, 4 - hours % 5);
            fiveMinutesLine = new string(LampColor.Yellow, minutes / 5) + new string(LampColor.Off, 11 - minutes / 5);
            oneMinuteLine   = new string(LampColor.Yellow, minutes % 5) + new string(LampColor.Off, 4 - minutes % 5);
            var fiveMinutesLineChars = fiveMinutesLine.ToCharArray();

            for (int i = 1; i <= minutes / 15; i++)
            {
                fiveMinutesLineChars[i * 3 - 1] = LampColor.Red;
            }
            
            fiveMinutesLine = new string(fiveMinutesLineChars);

            var result = string.Concat(
                secondsLine,
                Environment.NewLine,
                fiveHoursLine,
                Environment.NewLine,
                oneHoursLine,
                Environment.NewLine,
                fiveMinutesLine,
                Environment.NewLine,
                oneMinuteLine);

            return result;
        }

        private static (int hour, int minute, int second) TryParse(string time)
        {
            if (string.IsNullOrWhiteSpace(time))
            {
                throw new ArgumentNullException(nameof(time), "Input time should not be null or empty");
            }

            var tokenizedTimeString = time.Split(':');

            if (tokenizedTimeString.Length != 3)
            {
                throw new FormatException("Input string should have time format HH:MM:SS");
            }

            var hasHours   = int.TryParse(tokenizedTimeString[0], out var hours);
            var hasMinutes = int.TryParse(tokenizedTimeString[1], out var minutes);
            var hasSeconds = int.TryParse(tokenizedTimeString[2], out var seconds);

            if (!hasHours || hours < 0 || hours > 24)
            {
                throw new FormatException("Hours time part should be an integer with 0-24 interval");
            }

            if (!hasMinutes || minutes < 0 || minutes > 59)
            {
                throw new FormatException("Minutes time part should be an integer with 0-59 interval");
            }

            if (!hasSeconds || seconds < 0 || seconds > 59)
            {
                throw new FormatException("Seconds time part should be an integer with 0-59 interval");
            }

            return (hours, minutes, seconds);
        }
    }
}