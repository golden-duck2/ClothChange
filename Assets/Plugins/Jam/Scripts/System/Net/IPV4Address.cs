using System;
using System.Net;

namespace Jam
{
	/// <summary>
	/// IPv4アドレス.
	///  コンストラクタにIPv6の値が入った時を想定していない.
	/// </summary>
	public class IPv4Address
	{
		public IPAddress ipAddress;

		public IPv4Address(string aIPV4StringAddress)
		{
			ipAddress = IPAddress.Parse(aIPV4StringAddress);
		}

		public IPv4Address(uint aIPV4IntAddress)
		{
			ipAddress = new IPAddress(aIPV4IntAddress);
//			ipAddress = new IPAddress(BitConverter.GetBytes(aIPV4IntAddress));
		}

		public IPv4Address(byte[] aIPV4ByteArrayAddress)
		{
			ipAddress = new IPAddress(aIPV4ByteArrayAddress);
		}

		public IPv4Address Set(string aIPV4StringAddress)
		{
			ipAddress = IPAddress.Parse(aIPV4StringAddress);
			return this;
		}

		public IPv4Address Set(uint aIPV4IntAddress)
		{
			ipAddress = new IPAddress(aIPV4IntAddress);
			return this;
		}

		public IPv4Address Set(byte[] aIPV4ByteArrayAddress)
		{
			ipAddress = new IPAddress(aIPV4ByteArrayAddress);
			return this;
		}

		/// <summary>
		/// IPv4アドレスをuintで返す.
		/// </summary>
		public uint ToUInt()
		{
			return BitConverter.ToUInt32(ipAddress.GetAddressBytes(), 0);
		}

		/// <summary>
		/// IPv4アドレスをstringで返す.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return ipAddress.ToString();
		}

		/// <summary>
		/// IPv4アドレスをbyte[]で返す.
		/// </summary>
		/// <returns></returns>
		public byte[] ToByteArray()
		{
			return ipAddress.GetAddressBytes();
		}

		public string ToDebugString(string aPrefix = "", string aName = "IPV4Address", string aSeparator = "\n")
		{
			var sb = StringBuilderEx.sb.Clear();
			sb.AppendFormat("{0}{1}:{{{2}", aPrefix, aName, aSeparator);
			sb.AppendFormat("{0} ToString():{1},{2}", aPrefix, ToString(), aSeparator);
			sb.AppendFormat("{0} ToUInt():{1},{2}", aPrefix, ToUInt(), aSeparator);
			var byteArray = ToByteArray();
			sb.AppendFormat("{0} ToByteArray():{1}.{2}.{3}.{4}{5}", aPrefix, byteArray[0], byteArray[1], byteArray[2], byteArray[3], aSeparator);
			sb.AppendFormat("{0}}}", aPrefix);
			return sb.ToString();
		}

		/// <summary>
		/// Convert string to uint.
		/// </summary>
		/// <param name="aIPV4StringAddress">IPv4アドレス(stringフォーマット)</param>
		/// <returns>IPv4アドレス(uintフォーマット)</returns>
		public static uint ConvertStringToUInt(string aIPV4StringAddress)
		{
			var ipAddress = IPAddress.Parse(aIPV4StringAddress);
			return BitConverter.ToUInt32(ipAddress.GetAddressBytes(), 0);
		}

		/// <summary>
		/// Convert uint to string.
		/// </summary>
		/// <param name="aIPV4UIntAddress">IPv4アドレス(uintフォーマット)</param>
		/// <returns>IPv4アドレス(stringフォーマット)</returns>
		public string ConvertUIntToString(uint aIPV4UIntAddress)
		{
			var ipAddress = new IPAddress(BitConverter.GetBytes(aIPV4UIntAddress));
			return ipAddress.ToString();
		}
	}
}
