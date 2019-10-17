
//#define JAM_USE_SPY

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jam
{
	/// <summary>
	/// Spy.
	///  ※PlayerSettings->Other Settings->Scripting Define SymbolsにJAM_USE_SPYを定義して使用する.
	/// </summary>
	public static class Spy
	{
		[System.Diagnostics.Conditional("JAM_USE_SPY")]
		public static void StopwatchesStart(string aTag)
		{
#if JAM_USE_SPY
			stopwatches.Start(aTag);
#endif
		}

		[System.Diagnostics.Conditional("JAM_USE_SPY")]
		public static void StopwatchesStop(string aTag)
		{
#if JAM_USE_SPY
			stopwatches.Stop(aTag);
#endif
		}

		[System.Diagnostics.Conditional("JAM_USE_SPY")]
		public static void StopwatchesLog()
		{
#if JAM_USE_SPY
			stopwatches.Log();
#endif
		}

#if JAM_USE_SPY
		static Stopwatches stopwatches = new Stopwatches();
#endif

		/// <summary>
		/// Pauses the editor.
		/// </summary>
		[System.Diagnostics.Conditional("JAM_USE_SPY")]
		public static void Break()
		{
			Debug.Break();
		}
	}
}
