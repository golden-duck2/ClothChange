using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
	/// <summary>
	/// MonoBehavior extension. 
	/// </summary>
	public static class MonoBehaviorEx
	{
		/// <summary>
		/// StartCoroutine and call the Action after specified seconds.
		/// </summary>
		/// <remarks>
		/// this.StartCoroutineActionDelaySec(2, () =>
		/// {
		/// 	Debug.Log("Called after 2 seconds");
		/// });
		/// </remarks>
		public static Coroutine StartCoroutineActionDelaySec(this MonoBehaviour aSelf, float aDelaySec, Action aAction)
		{
			return aSelf.StartCoroutine(DelaySecE(aDelaySec, aAction));
		}

		/// <summary>
		/// StartCoroutine and call the Action after specified frames.
		/// </summary>
		/// <remarks>
		/// this.StartCoroutineActionDelayFrames(2, () =>
		/// {
		/// 	Debug.Log("Called after 2 frames");
		/// });
		/// </remarks>
		public static Coroutine StartCoroutineActionDelayFrames(this MonoBehaviour aSelf, int aDelayFlames, Action aAction)
		{
			return aSelf.StartCoroutine(DelayFramesE(aDelayFlames, aAction));
		}

		/// <summary>
		/// StartCoroutine and call the Action after one frames.
		/// </summary>
		/// <remarks>
		/// this.StartCoroutineActionDelayOneFrame(() =>
		/// {
		/// 	Debug.Log("Called after 1 frames");
		/// });
		/// </remarks>
		public static Coroutine StartCoroutineActionDelayOneFrame(this MonoBehaviour aSelf, Action aAction)
		{
			return aSelf.StartCoroutine(DelayOneFrameE(aAction));
		}

		#region PrivateMethod
		static IEnumerator DelaySecE(float aDelayTime, Action aAction)
		{
			yield return new WaitForSeconds(aDelayTime);
			aAction();
		}

		static IEnumerator DelayFramesE(int aDelayFrame, Action aAction)
		{
			while (aDelayFrame > 0)
			{
				--aDelayFrame;
				yield return null;
			}
			aAction();
		}

		static IEnumerator DelayOneFrameE(Action aAction)
		{
			yield return null;
			aAction();
		}
		#endregion
	}
}