using System;


namespace Jam
{
	/// <summary>
	/// 
	/// </summary>
	public static class DateTimeEx
	{
		// UnixEpochTime.
		public static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		/// <summary>
		/// 四捨五入.
		/// </summary>
		public static DateTime Round(this DateTime aDate, TimeSpan aSpan)
		{
			var ticks = (aDate.Ticks + (aSpan.Ticks / 2) + 1) / aSpan.Ticks;
			return new DateTime(ticks * aSpan.Ticks, aDate.Kind);
		}

		/// <summary>
		/// 切り上げ.
		/// </summary>
		public static DateTime Ceil(this DateTime aDate, TimeSpan aSpan)
		{
			var ticks = (aDate.Ticks + aSpan.Ticks - 1) / aSpan.Ticks;
			return new DateTime(ticks * aSpan.Ticks, aDate.Kind);
		}

		/// <summary>
		/// 切り捨て.
		/// </summary>
		public static DateTime Floor(this DateTime aDate, TimeSpan aSpan)
		{
			var ticks = (aDate.Ticks / aSpan.Ticks);
			return new DateTime(ticks * aSpan.Ticks, aDate.Kind);
		}

		public static string ToDebugString(this DateTime aSelf, string aPrefix = "", string aName = "DateTime", string aSeparator = "\n")
		{
			var sb = StringBuilderEx.sb.Clear();
			sb.AppendFormat("{0}{1}:{{{2}", aPrefix, aName, aSeparator);
			sb.AppendFormat("{0} {1},{2}", aPrefix, aSelf.ToString("yyyy/MM/dd HH:mm:ss"), aSeparator);
			sb.AppendFormat("{0} Ticks:{1},{2}", aPrefix, aSelf.Ticks, aSeparator);
			sb.AppendFormat("{0} Kind:{1}{2}", aPrefix, aSelf.Kind, aSeparator);
			sb.AppendFormat("{0}}}{1}", aPrefix, aSeparator);
			return sb.ToString();
		}
	}
}
