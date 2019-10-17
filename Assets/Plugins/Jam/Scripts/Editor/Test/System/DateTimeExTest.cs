using System;
using UnityEngine;
using NUnit.Framework;

#if JAM_TEST_ENABLE
namespace Jam
{
	public class DateTimeExTest
	{
		[Test]
		public void TestRound() {
			Debug.Log("DateTimeExTest.T()");
			var dtArray = new DateTime[]
			{
				new DateTime(2017, 12,  1, 1, 1, 1),
				new DateTime(2017, 12, 14, 1, 1, 1),
				new DateTime(2017, 12, 15, 1, 1, 1),
				new DateTime(2017, 12, 31, 23, 59, 59)
			};
			{
				Debug.Log(" Test.Round() days");
				var span = new TimeSpan(1, 0, 0, 0);
				var expectedRoundArray = new string[]
				{
					"DateTime:{ 2017/12/01 00:00:00, Ticks:636476832000000000, Kind:Unspecified}",
					"DateTime:{ 2017/12/14 00:00:00, Ticks:636488064000000000, Kind:Unspecified}",
					"DateTime:{ 2017/12/15 00:00:00, Ticks:636488928000000000, Kind:Unspecified}",
					"DateTime:{ 2018/01/01 00:00:00, Ticks:636503616000000000, Kind:Unspecified}",
				};
				for (var idx = 0; idx < dtArray.Length; ++idx)
				{
					var dt = dtArray[idx];
					var expected = expectedRoundArray[idx];
					var result = DateTimeEx.Round(dt, span).ToDebugString("", "");
					Debug.Log("  result  :" + result);
					Debug.Log("  expected:" + expected);
					Assert.AreEqual(result, expected);
				}
			}
			{
				Debug.Log(" Test.Round() hours");
				var span = new TimeSpan(0, 1, 0, 0);
				var expectedRoundArray = new string[]
				{
					"DateTime:{ 2017/12/01 01:00:00, Ticks:636476868000000000, Kind:Unspecified}",
					"DateTime:{ 2017/12/14 01:00:00, Ticks:636488100000000000, Kind:Unspecified}",
					"DateTime:{ 2017/12/15 01:00:00, Ticks:636488964000000000, Kind:Unspecified}",
					"DateTime:{ 2018/01/01 00:00:00, Ticks:636503616000000000, Kind:Unspecified}",
				};
				for (var idx = 0; idx < dtArray.Length; ++idx)
				{
					var dt = dtArray[idx];
					var expected = expectedRoundArray[idx];
					var result = DateTimeEx.Round(dt, span).ToDebugString("", "");
					Debug.Log("  result  :" + result);
					Debug.Log("  expected:" + expected);
					Assert.AreEqual(result, expected);
				}
			}
		}

	}
}
#endif // JAM_TEST_ENABLE
