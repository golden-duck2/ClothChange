#if JAM_LOG_DETAIL
	#define LOG_DETAIL
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

#if JAM_TEST_ENABLE
namespace Jam
{
	public class EnumUtilTest
	{
		enum TestE
		{
			A,
			B,
			Max
		}

		struct TestS
		{
			int A;
		}

		[Test]
		public void Test0_Parse()
		{
			Debug.Log("at " + Log.MethodInfo() + " Parse()のテスト.");
			{
				var a = EnumUtil.Parse<TestE>("A");
				Debug.Log(" a:" + a + ", expected:" + TestE.A.ToString());
				Assert.AreEqual(a, TestE.A);
			}

			Debug.Log("\n");
			Debug.Log("at EnumUtilTest.Test_Prse() 正しくパースできなかったときにデフォルト引数のdefaultValueが返るかのテスト.");
			{

				LogAssert.Expect(LogType.Warning, "at EnumUtil.Parse<T:Jam.EnumUtilTest+TestE>(value:X, defaultValue:A) parse failed.");
				var x = EnumUtil.Parse<TestE>("X");
				Debug.Log(" x:" + x + ", expected:" + TestE.A.ToString());
				Assert.AreEqual(x, TestE.A);
			}

			Debug.Log("\n");
			Debug.Log("at EnumUtilTest.Test_Prse() 正しくパースできなかったときに指定したdefaultValueが返るかのテスト.");
			{
				LogAssert.Expect(LogType.Warning, "at EnumUtil.Parse<T:Jam.EnumUtilTest+TestE>(value:X, defaultValue:Max) parse failed.");
				var x = EnumUtil.Parse<TestE>("X", TestE.Max);
				Debug.Log(" x:" + x + ", expected:" + TestE.Max.ToString());
				Assert.AreEqual(x, TestE.Max);
			}

			Debug.Log("\n");
			Debug.Log("at EnumUtilTest.Test_Prse() Enum以外が指定されたときにエラー表示されるかのテスト.");
			{
				LogAssert.Expect(LogType.Error, "at EnumUtil.Parse<T:Jam.EnumUtilTest+TestS>(value:A, defaultValue:Jam.EnumUtilTest+TestS) T is not enum type.");
				var x = EnumUtil.Parse<TestS>("A");
			}
		}

		[Test]
		public void Test1_IsDefined()
		{
			Debug.Log("at " + Log.MethodInfo() + " 定義されていることを認識できるかのテスト.");
			{
				var result = EnumUtil.IsDefined<TestE>("A");
				Debug.Log(" result:" + result + ", expected:" + true);
				Assert.AreEqual(result, true);
			}

			Debug.Log("at " + Log.MethodInfo() + " 定義されてないことを認識できるかのテスト.");
			{
				var result = EnumUtil.IsDefined<TestE>("X");
				Debug.Log(" result:" + result + ", expected:" + false);
				Assert.AreEqual(result, false);
			}
		}

		[Test]
		public void Test2_Others()
		{
			Debug.Log("at " + Log.MethodInfo() + " GetValueArray()のテスト.");
			{
				var result = EnumUtil.GetValueArray<TestE>();
				Debug.Log("result:");
				foreach (var r in result)
				{
					Debug.Log(" r:" + r.ToString());
				}
			}

			Debug.Log("at " + Log.MethodInfo() + " GetValueList()のテスト.");
			{
				var result = EnumUtil.GetValueList<TestE>();
				Debug.Log("result:");
				foreach (var r in result)
				{
					Debug.Log(" r:" + r.ToString());
				}
			}

			Debug.Log("at " + Log.MethodInfo() + " GetCount()のテスト.");
			{
				var result = EnumUtil.GetCount<TestE>();
				Debug.Log(" result:" + result + ", expected:" + 3);
				Assert.AreEqual(result, 3);
			}
		}
	}
}
#endif // JAM_TEST_ENABLE
