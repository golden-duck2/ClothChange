using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Jam
{
	/// <summary>
	/// Stopwatch extension.
	/// </summary>
	/// <example>
	/// var stopwatchEx = StopwatchEx.StartNew();
	///   --- spends time ---
	/// Debug.LogFormat("time:{0}", stopwatchEx.Stop().ElapsedSecStr());	// time:1.2sec
	/// </example>
	public class StopwatchEx
	{
		public Stopwatch stopwatch;

		public StopwatchEx()
		{
			stopwatch = new Stopwatch();
		}

		public static StopwatchEx StartNew()
		{
			var stopwatchEx = new StopwatchEx();
			stopwatchEx.stopwatch.Start();
			return stopwatchEx;
		}

		public void ResetStart()
		{
			stopwatch.Reset();
			stopwatch.Start();
		}

		public StopwatchEx Stop()
		{
			stopwatch.Stop();
			return this;
		}

		public double ElapsedSec()
		{
			return stopwatch.Elapsed.TotalSeconds;
		}

		public string ElapsedSecStr()
		{
			return stopwatch.Elapsed.TotalSeconds + "sec";
		}
	}
}