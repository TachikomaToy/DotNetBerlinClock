using System;

namespace BerlinClock.Tests
{
    public static class StaticRandom
	{
		private static readonly Random Random = new Random();
		private static readonly object LockObj = new object();

		public static int Next()
		{
			lock (LockObj)
			{
				return Random.Next();
			}
		}

		public static int Next(int max)
		{
			lock (LockObj)
			{
				return Random.Next(max);
			}
		}

		public static int Next(int min, int max)
		{
			lock (LockObj)
			{
				return Random.Next(min, max);
			}
		}

		public static double NextDouble()
		{
			lock (LockObj)
			{
				return Random.Next();
			}
		}

		public static void NextBytes(byte[] buffer)
		{
			lock (LockObj)
			{
				Random.NextBytes(buffer);
			}
		}
	}
}