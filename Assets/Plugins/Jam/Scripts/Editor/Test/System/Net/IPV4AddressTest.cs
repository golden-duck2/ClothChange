using System;
using UnityEngine;
using NUnit.Framework;

#if JAM_TEST_ENABLE
namespace Jam
{
	public class IPV4AddressTest
	{
		const string IP_STR = "192.168.0.1";
		readonly uint IP_UINT = (BitConverter.IsLittleEndian) ? 16820416 : 3232235521;
		readonly byte[] IP_BYTE_ARRAY = new byte[] { 192, 168, 0, 1 };

		[Test]
		public void Test()
		{
			Debug.Log("IPV4AddressTest.Test()");
			{
				{   // Test string constructor.
					Debug.Log(" Test string constructor");
					IPv4Address ipAddress = new IPv4Address(IP_STR);
					Debug.Log("  " + ipAddress.ToDebugString("", "ToDebugString()", ""));
					Assert.AreEqual(ipAddress.ToString(), IP_STR);
					Assert.AreEqual(ipAddress.ToUInt(), IP_UINT);
					Assert.AreEqual(ipAddress.ToByteArray(), IP_BYTE_ARRAY);
				}
				{   // Test uint constructor.
					Debug.Log(" Test uint constructor");
					IPv4Address ipAddress = new IPv4Address(IP_UINT);
					Debug.Log("  " + ipAddress.ToDebugString("", "ToDebugString()", ""));
					Assert.AreEqual(ipAddress.ToString(), IP_STR);
					Assert.AreEqual(ipAddress.ToUInt(), IP_UINT);
					Assert.AreEqual(ipAddress.ToByteArray(), IP_BYTE_ARRAY);
				}
				{   // Test byteArray constructor.
					Debug.Log(" Test byteArray constructor");
					IPv4Address ipAddress = new IPv4Address(IP_BYTE_ARRAY);
					Debug.Log("  " + ipAddress.ToDebugString("", "ToDebugString()", ""));
					Assert.AreEqual(ipAddress.ToString(), IP_STR);
					Assert.AreEqual(ipAddress.ToUInt(), IP_UINT);
					Assert.AreEqual(ipAddress.ToByteArray(), IP_BYTE_ARRAY);
				}
			}
		}
	}
}
#endif // JAM_TEST_ENABLE
