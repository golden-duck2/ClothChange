using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Reflection;

#if JAM_TEST_ENABLE
namespace Jam
{
	public static class StringBuilderExTest
	{
		static List<StringBuilder> sbList = new List<StringBuilder>();

/*		[Test]
		public static void Test1()
		{
			Debug.Log("at " + Log.MethodInfo());
			// 最初の数が0かのテスト.
			var instanceNum = StringBuilderEx.TEST_GetInstanceNum();
			Debug.Log("Test instanceNum == 0.");
			StringBuilderEx.TEST_LogPool();
			Assert.AreEqual(0, instanceNum);
			Debug.Log("at " + Log.MethodInfo() + " end.");
		}*/

		[Test]
		public static void Test1()
		{
			// MAX_POOL_COUNTはprivateなので無理やり取得する.
			var StringBuilderEx_MAX_POOL_COUNT = (int)(typeof(StringBuilderEx).GetField("MAX_POOL_COUNT", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null));
			Debug.Log("StringBuilderEx_MAX_POOL_COUNT:" + StringBuilderEx_MAX_POOL_COUNT);
			// inUseDicはprivateなので無理やり取得する.
			var StringBuilderEx_inUseDic = (Dictionary<StringBuilder, string>)(typeof(StringBuilderEx).GetField("inUseDic", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null));
//			var StringBuilderEx_inUseCount = StringBuilderEx_inUseDic.Count;
//			Debug.Log("StringBuilderEx_inUseDicCount:" + StringBuilderEx_inUseCount);

			Debug.Log("at StringBuilderExTest.Test1() MAX_POOL_COUNTまで取得するテスト.");
			sbList.Clear();
			for (var idx = 0; idx < StringBuilderEx_MAX_POOL_COUNT; ++idx)
			{
				Debug.Log("StringBuilderEx.Rent()");
				var sb = StringBuilderEx.Rent();
				sbList.Add(sb);
				var expectedInstanceNum = idx + 1;
				StringBuilderEx.LogPoolInfo();
				// poolInstanceNumはprivateなので無理やり取得する.
				var StringBuilderEx_poolInstanceNum = (int)(typeof(StringBuilderEx).GetField("poolInstanceNum", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null));
				Debug.Log(" idx:" + idx + ", expectedInstanceNum:" + expectedInstanceNum + ", InstanceNum:" + StringBuilderEx_poolInstanceNum);
				Assert.AreEqual(expectedInstanceNum, StringBuilderEx_poolInstanceNum);
			}

			Debug.Log("\n");
			Debug.Log("at StringBuilderExTest.Test1() MAX_POOL_COUNT を超えて呼び出したときにnullが返ってくるかのテスト.");
			{   // MAX_POOL_COUNT を超えて呼び出したときにnullが返ってくるかのテスト.
				//				LogAssert.Expect(LogType.Log, "ログ");
				LogAssert.Expect(LogType.Error, "at StringBuilder.Rent() Reached Max PoolSize.");
				var sb = StringBuilderEx.Rent();
				Assert.AreEqual(sb, null);
			}

			Debug.Log("\n");
			Debug.Log("at StringBuilderExTest.Test1() nullのinstanceの返却. エラー表示されるかのテスト.");
			{   // nullのinstanceの返却. エラー表示されるかのテスト.
				LogAssert.Expect(LogType.Error, "at StringBuilder.Return() instance is null.");
				StringBuilderEx.Return(null);
			}

			Debug.Log("\n");
			Debug.Log("at StringBuilderExTest.Test1() 管理対象外のinstanceの返却. エラー表示されるかのテスト.");
			{   // 管理対象外のinstanceの返却. エラー表示されるかのテスト.
				var sb = new StringBuilder();
				LogAssert.Expect(LogType.Error, "at StringBuilder.Return() instance is unmanaged.");
				StringBuilderEx.Return(sb);
			}

			// すべて返却されるかのテスト.
			Debug.Log("\n");
			Debug.Log("at StringBuilderExTest.Test1() すべて返却されるかのテスト.");
			for (var idx = 0; idx < StringBuilderEx_MAX_POOL_COUNT; ++idx)
			{
				var sb = sbList[sbList.Count - 1];
				sbList.RemoveAt(sbList.Count - 1);
				StringBuilderEx.Return(sb);
				var expectedInUseNum = StringBuilderEx_MAX_POOL_COUNT - idx - 1;
				Debug.Log(" idx:" + idx + ", expectedInUseNum:" + expectedInUseNum + ", StringBuilderEx_inUseDic.Count:" + StringBuilderEx_inUseDic.Count);
				Assert.AreEqual(expectedInUseNum, StringBuilderEx_inUseDic.Count);

				var expectedInstanceNum = StringBuilderEx_MAX_POOL_COUNT;
				// poolInstanceNumはprivateなので無理やり取得する.
				var StringBuilderEx_poolInstanceNum = (int)(typeof(StringBuilderEx).GetField("poolInstanceNum", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null));
				Debug.Log(" idx:" + idx + ", expectedInstanceNum:" + expectedInstanceNum + ", InstanceNum:" + StringBuilderEx_poolInstanceNum);
				Assert.AreEqual(expectedInstanceNum, StringBuilderEx_poolInstanceNum);
			}
			Debug.Log("at StringBuilderExTest.Test1() Z");
		}
	}
}
#endif // JAM_TEST_ENABLE
