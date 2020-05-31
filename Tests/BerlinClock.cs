using System;

namespace BerlinClock.Tests
{
    public struct BerlinClock
    {
        private readonly Lazy<string> _stringView;
        
        public readonly string SecondsLine;
        public readonly string FiveHoursLine;
        public readonly string OneHoursLine;
        public readonly string FiveMinutesLine;
        public readonly string OneMinutesLine;

        public BerlinClock(
            string secondsLine,
            string fiveHoursLine,
            string oneHoursLine,
            string fiveMinutesLine,
            string oneMinutesLine)
        {
            SecondsLine     = secondsLine;
            FiveHoursLine   = fiveHoursLine;
            OneHoursLine    = oneHoursLine;
            FiveMinutesLine = fiveMinutesLine;
            OneMinutesLine  = oneMinutesLine;
            
            _stringView = new Lazy<string>(() => ToStringView(
                                               secondsLine,
                                               fiveHoursLine,
                                               oneHoursLine,
                                               fiveMinutesLine,
                                               oneMinutesLine));
        }

        public override string ToString() => _stringView.Value;

        private static string ToStringView(
            string secondsLine,
            string fiveHoursLine,
            string oneHoursLine,
            string fiveMinutesLine,
            string oneMinutesLine)
            => string.Concat(new []
            {
                secondsLine,
                Environment.NewLine,
                fiveHoursLine,
                Environment.NewLine,
                oneHoursLine,
                Environment.NewLine,
                fiveMinutesLine,
                Environment.NewLine,
                oneMinutesLine
            });
    }
}