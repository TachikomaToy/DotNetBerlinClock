using System;
using System.Collections.Generic;
using Xunit;

namespace BerlinClock.Tests
{
    public class TimeConverterTests
    {
        [Theory, MemberData(nameof(TestData))]
        public void GivenTimeString_WhenConvertTimeToStringOfBerlinClock_ThenThisTransformationShouldReturnExpectedResult(
            string inputTime, 
            string expectedClock)
        {
            var converter = new TimeConverter();
            
            var actualClock = converter.convertTime(inputTime);
            
            Assert.Equal(expectedClock, actualClock, StringComparer.Ordinal);
        }

        public static IEnumerable<object[]> TestData()
        {
            for (int i = 0; i < 100; i++)
            {
                var (inputTime, expectedClock) = TimeTestCaseGenerator.Generate();

                yield return new object[]
                {
                    inputTime, expectedClock
                };
            }
        }
    }
}