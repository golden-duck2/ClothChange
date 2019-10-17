using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
	public static class SystemInfoEx
	{
		public static string ToDebugString(string aPrefix = "", string aName = "SystemInfo", string aSeparator = "\n")
		{
			var sb = StringBuilderEx.sb.Clear();
			sb.AppendFormat("{0}{1}:{{{2}", aPrefix, aName, aSeparator);
			sb.AppendFormat("{0} deviceModel:{1},{2}", aPrefix, SystemInfo.deviceModel, aSeparator);
			sb.AppendFormat("{0} deviceName:{1},{2}", aPrefix, SystemInfo.deviceName, aSeparator);
			sb.AppendFormat("{0} deviceType:{1},{2}", aPrefix, SystemInfo.deviceType, aSeparator);
			sb.AppendFormat("{0} deviceUniqueIdentifier:{1},{2}", aPrefix, SystemInfo.deviceUniqueIdentifier, aSeparator);
			sb.AppendFormat("{0} maxTextureSize:{1},{2}", aPrefix, SystemInfo.maxTextureSize, aSeparator);
			sb.AppendFormat("{0} operatingSystem:{1},{2}", aPrefix, SystemInfo.operatingSystem, aSeparator);
			sb.AppendFormat("{0} processorCount:{1},{2}", aPrefix, SystemInfo.processorCount, aSeparator);
			sb.AppendFormat("{0} processorFrequency:{1},{2}", aPrefix, SystemInfo.processorFrequency, aSeparator);
			sb.AppendFormat("{0} processorType:{1},{2}", aPrefix, SystemInfo.processorType, aSeparator);
			sb.AppendFormat("{0}}}{1}", aPrefix, aSeparator);
			return sb.ToString();
		}
	}
}
