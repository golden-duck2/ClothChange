using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Jam;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;


public class DebugMenuRoot : DebugMenuText
{
	public override void Build(string aText, Dictionary<string, object> aSettings)
	{
		base.Build(aText, aSettings);
		//		// 位置を調整する.
		//		transform.GetRectTransform().anchoredPosition = new Vector2(0f, 0f);
	}
}