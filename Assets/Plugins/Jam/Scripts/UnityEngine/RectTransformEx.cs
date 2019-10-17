using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
	/// <summary>
	/// RectTransform extension.
	/// </summary>
	public static class RectTransformEx
	{
		public static string ToDebugString(this RectTransform aSelf, string aPrefix = "", string aName = "RectTransform", string aSeparator = "\n")
		{
			var sb = StringBuilderEx.sb.Clear();
			sb.AppendFormat("{0}{1}:{{{2}", aPrefix, aName, aSeparator);
			sb.AppendFormat("{0} offsetMax:{1}, offsetMin:{2},{3}", aPrefix, aSelf.offsetMax, aSelf.offsetMin, aSeparator);
			sb.AppendFormat("{0} anchorMax:{1}, anchorMin:{2},{3}", aPrefix, aSelf.anchorMax, aSelf.anchorMin, aSeparator);
			sb.AppendFormat("{0} pivot:{1},{2}", aPrefix, aSelf.pivot, aSeparator);
			sb.AppendFormat("{0} sizeDelta:{1},{2}", aPrefix, aSelf.sizeDelta, aSeparator);
			sb.AppendFormat("{0} anchoredPosition:{1}, {2}", aPrefix, aSelf.anchoredPosition, aSeparator);
			sb.AppendFormat("{0} anchoredPosition3D:{1},{2}", aPrefix, aSelf.anchoredPosition3D, aSeparator);
			sb.AppendFormat("{0} rect:{1},{2}", aPrefix, aSelf.rect, aSeparator);
			var fourCornersArray = new Vector3[4];
			aSelf.GetLocalCorners(fourCornersArray);
			sb.AppendFormat("{0} LocalCorners[0]:{1}{2},", aPrefix, fourCornersArray[0], aSeparator);
			sb.AppendFormat("{0} LocalCorners[1]:{1}{2},", aPrefix, fourCornersArray[1], aSeparator);
			sb.AppendFormat("{0} LocalCorners[2]:{1}{2},", aPrefix, fourCornersArray[2], aSeparator);
			sb.AppendFormat("{0} LocalCorners[3]:{1}{2},", aPrefix, fourCornersArray[3], aSeparator);
			aSelf.GetWorldCorners(fourCornersArray);
			sb.AppendFormat("{0} WorldCorners[0]:{1}{2},", aPrefix, aPrefix, fourCornersArray[0], aSeparator);
			sb.AppendFormat("{0} WorldCorners[1]:{1}{2},", aPrefix, aPrefix, fourCornersArray[1], aSeparator);
			sb.AppendFormat("{0} WorldCorners[2]:{1}{2},", aPrefix, aPrefix, fourCornersArray[2], aSeparator);
			sb.AppendFormat("{0} WorldCorners[3]:{1}{2}",  aPrefix, fourCornersArray[3], aSeparator);
			sb.Append(aPrefix + "}"+ aSeparator);
			return sb.ToString();
		}
	}
}