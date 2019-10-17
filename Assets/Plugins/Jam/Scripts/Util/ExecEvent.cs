using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
	// イベント実行能力.
	public class ExecEvent : MonoBehaviour
	{
		// 登録イベントのクリア.
		public void Clear()
		{
			execEventDic.Clear();
		}

		// イベントの登録.
		public void RegistExecEvent(string aEventName, Action<object> aAction)
		{
			if (string.IsNullOrEmpty(aEventName))
			{
				Debug.LogError("aEventType is null or empty.");
			}
			if (aAction == null)
			{
				Debug.LogError("aAction is null.");
			}
			execEventDic.Add(aEventName, aAction);
		}

		// イベントの実行.
		public void Exec(string aEventName, object aArg)
		{
			// aEventNameイベントが登録されているときは実行する.
			if (execEventDic.ContainsKey(aEventName))
			{
				execEventDic[aEventName](aArg);
			}
		}

		// イベント辞書.
		Dictionary<string, Action<object>> execEventDic = new Dictionary<string, Action<object>>();
	}
}