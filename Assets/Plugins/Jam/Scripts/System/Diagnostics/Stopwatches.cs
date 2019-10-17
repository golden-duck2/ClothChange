using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace Jam
{
	/// <summary>
	/// Stopwatches.
	/// </summary>
	public class Stopwatches
	{
		class Val
		{
			public Stopwatch sw;
			public double totalSec = 0;
			public double minSec = double.MaxValue;
			public double maxSec = double.MinValue;
			public int calls;
		}

		Dictionary<string, Val> dic;

		public Stopwatches(int aCapacity = 32)
		{
			dic = new Dictionary<string, Val>(aCapacity);
		}

		public void Start(string aTag)
		{
			Val val;
			if (dic.ContainsKey(aTag))
			{
				val = dic[aTag];
				val.sw.Reset();
				val.sw.Start();
			}
			else
			{
				val = new Val()
				{
					sw = Stopwatch.StartNew(),
					calls = 0
				};
				dic[aTag] = val;
			}
		}

		public void Stop(string aTag)
		{
			Val val;
			if (dic.ContainsKey(aTag))
			{
				val = dic[aTag];
				val.sw.Stop();
				var sec = val.sw.Elapsed.TotalSeconds;
				val.totalSec += sec;
				val.minSec = (sec < val.minSec) ? sec : val.minSec;
				val.maxSec = (sec > val.maxSec) ? sec : val.maxSec;
				++val.calls;
			}
			else
			{
				UnityEngine.Debug.LogWarning("@Stopwatches.Stop(aTag:" + aTag + ") aTag is not registed.");
			}
		}

		public void Log()
		{
			var sb = new System.Text.StringBuilder();
			sb.AppendLine("Stopwatches:");
			var tagLenMax = 3;
			var callsMax = 5;
			foreach (var kv in dic)
			{
				var tag = kv.Key;
				var calls = kv.Value.calls;
				tagLenMax = (tag.Length > tagLenMax) ? tag.Length : tagLenMax;
				callsMax = (calls > callsMax) ? calls : callsMax;
			}
			var callsMaxDigits = UnityEngine.Mathf.Max(5, Util.GetDigits(callsMax));
			sb.AppendFormatLine(
				" {0,-" + tagLenMax + "}|Calls, Time(sec), Ave(sec),  Min(sec),  Max(sec)",
				"Tag");
			var sorted = dic.OrderBy((x) => x.Key);
			foreach (var kv in sorted)
			{
				var tag = kv.Key;
				var val = kv.Value;
				if (val.calls == 0)
				{   // Start()後、Stop()前にLog()を呼んだ時はここに来る.
					continue;
				}
				var sw = val.sw;
				if (sw.IsRunning)
				{
					UnityEngine.Debug.LogWarning("@Stopwatches.Log() tag:\"" + tag + "\" is running!");
				}
				sb.AppendFormatLine(
					" {0,-" + tagLenMax + "}|{1," + callsMaxDigits + "}, {2:f7}, {3:f7}, {4:f7}, {5:f7}",
					tag,
					val.calls,
					val.totalSec,
					val.totalSec / val.calls,
					val.minSec,
					val.maxSec);
			}
			UnityEngine.Debug.LogWarning(sb.ToString());
		}
	}
}