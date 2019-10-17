using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
using System.Text.RegularExpressions;
#endif


// 参考.
//  http://blog.techium.jp/entry/2016/08/27/090000
//  https://groups.google.com/forum/#!topic/zenfone2/tAOXvzfrmSI
//  http://android-note.open-memo.net/sub/file_and_dir__SDcard_save.html
//  http://qiita.com/wasnot/items/287d191c7da40f2e6080
//  http://qiita.com/wasnot/items/287d191c7da40f2e6080


namespace Jam
{
	public static class AndroidUtil
	{

#if UNITY_EDITOR           // エディター.
#elif UNITY_STANDALONE_WIN // Win実機.
#elif UNITY_ANDROID            // Android実機.
#else                      // その他実機.
#endif

		public static void KeepScreenOn()
		{
#if UNITY_EDITOR        // エディター.
#elif UNITY_ANDROID     // Android実機.
//		using (var activity = GetActivity())			終了時にactivityが削除されるとAndroidJavaRunnableが機能しなくなる為、usingを使用してはいけない.
		var activity = GetActivity();
		{
			AndroidJavaClass layoutParamsClass = new AndroidJavaClass("android.view.WindowManager$LayoutParams");
			var FLAG_KEEP_SCREEN_ON = layoutParamsClass.GetStatic<int>("FLAG_KEEP_SCREEN_ON");
			//		var FLAG_KEEP_SCREEN_ON = 128;
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				var window = activity.Call<AndroidJavaObject>("getWindow");
				window.Call("addFlags", FLAG_KEEP_SCREEN_ON);
				activity.Dispose();
			}));
		}
#else                   // その他実機.
#endif
		}

		/* 動作しないので廃止.
		public static void KeepAirplaneModeOn()
		{
	#if UNITY_EDITOR        // エディター.
	#elif UNITY_ANDROID     // Android実機.
			Log.D("at KeepAirplaneModeOn() A");

			using (var intentClass = new AndroidJavaClass("android.content.Intent"))
			{
				var action = intentClass.GetStatic<string>("ACTION_AIRPLANE_MODE_CHANGED");
				using (var intent = new AndroidJavaObject("android.content.Intent"))
				{
				Log.D("at KeepAirplaneModeOn() B");
					intent.Call<AndroidJavaObject>("putExtra", "state", true);
				Log.D("at KeepAirplaneModeOn() C");
					using (var activity = GetActivity())
					{
				Log.D("at KeepAirplaneModeOn() D");
						activity.Call("sendBroadcast", intent);
					}
				Log.D("at KeepAirplaneModeOn() Z");
				}
			}
	#else                   // その他実機.
	#endif
		}

		/*  動かないので廃止.
		public static void KeepWifiOff()
		{
	#if UNITY_EDITOR        // エディター.
	#elif UNITY_ANDROID     // Android実機.
			using (var activity = GetActivity())
			{
	//			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidJavaClass wifiManagerClass = new AndroidJavaClass("android.net.wifi.WifiManager");
				var isWifiEnabled = wifiManagerClass.Call<bool>("isWifiEnabled");
				bool result = false;
	//			if (isWifiEnabled)
				{
					result = wifiManagerClass.Call<bool>("setWifiEnabled", false);
					Log.W("isWifiEnabled:"+isWifiEnabled + ", result:"+result);
				}
	//			}));
			}
	#else                   // その他実機.
	#endif
		}
		*/
		public static void SetWifiEnabled(bool enabled)
		{
#if UNITY_EDITOR        // エディター.
#elif UNITY_ANDROID     // Android実機.
		using (var activity = GetActivity())
		{
			var wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");
//			var result =
			wifiManager.Call<bool>("setWifiEnabled", enabled);
//			Log.D("result:"+result);
		}
#else                   // その他実機.
#endif
		}

		public static void KeepBluetoothOff()
		{

#if UNITY_EDITOR        // エディター.
#elif UNITY_ANDROID     // Android実機.
		var bluetoothAdapterClass = new AndroidJavaClass("android.bluetooth.BluetoothAdapter");
		var bluetoothAdapter = bluetoothAdapterClass.CallStatic<AndroidJavaObject>("getDefaultAdapter");
		bluetoothAdapter.Call<bool>("disable");
#else                   // その他実機.
#endif
		}

		/*
	public static string GetSSID()
	{
		string ssid = "";
	#if UNITY_EDITOR        // エディター.
	#elif UNITY_ANDROID     // Android実機.
		using (var activity = GetActivity())
		{
			var wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");
			ssid = wifiManager.Call<AndroidJavaObject>("getConnectionInfo").Call<string>("getSSID");
		}
	#else                   // その他実機.
	#endif
		return ssid;
	}
	*/

		public static void OSAlertDialog_OK(string title, string message)
		{
#if UNITY_EDITOR        // エディター.
			Debug.Log(title + ":" + message);
#elif UNITY_ANDROID     // Android実機.
//		using (var activity = GetActivity())			終了時にactivityが削除されると機能しなくなる為、usingを使用してはいけない.
		var activity = GetActivity();
		{
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidJavaObject alertDialogBuilder = new AndroidJavaObject("android.app.AlertDialog$Builder", activity);
				if (!string.IsNullOrEmpty(title))
				{
					alertDialogBuilder.Call<AndroidJavaObject>("setTitle", title);
				}
				alertDialogBuilder.Call<AndroidJavaObject>("setMessage", message);
				alertDialogBuilder.Call<AndroidJavaObject>("setPositiveButton", "OK", null);
				AndroidJavaObject dialog = alertDialogBuilder.Call<AndroidJavaObject>("create");
				dialog.Call("show");
				activity.Dispose();
			}));
			// @TODO アイコン
			//  http://qiita.com/ckazu/items/07dff39449e9f544b038
		}
#else                   // その他実機.
		Debug.Log(title + ":" + message);
#endif
		}

		/*	public static void DLog(string msg)
			{
	#if UNITY_EDITOR            // エディター.
				Debug.Log(msg);
	#elif UNITY_STANDALONE_WIN  // Win実機.
				System.Diagnostics.Trace.WriteLine(msg);
	#elif UNITY_ANDROID         // Android実機.
				var log = new AndroidJavaClass("android.util.Log");
				log.CallStatic<int>("d", "Unity", msg);
	#else                       // その他実機.
				Debug.Log(msg);
	#endif
			}
		*/

#if UNITY_EDITOR        // エディター.
#elif UNITY_ANDROID     // Android実機.
	static AndroidJavaObject progressDialog = null;
#else                   // その他実機.
#endif

		public static void ShowProgressDialog(string title, string message)
		{
#if UNITY_EDITOR        // エディター.
			Debug.Log("at ShowProgressDialog(title:" + title + ", message:" + message + ")");
#elif UNITY_ANDROID     // Android実機.
		var activity = GetActivity();
		activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
			progressDialog = new AndroidJavaObject("android.app.ProgressDialog", activity);
			var STYLE_SPINNER = 0;
			progressDialog.Call("setProgressStyle", STYLE_SPINNER);
			progressDialog.Call("setTitle", title);
			progressDialog.Call("setMessage", message);
			progressDialog.Call("setCancelable", false);
			progressDialog.Call("setCanceledOnTouchOutside", false);
			progressDialog.Call("show");
			progressDialog.Call<AndroidJavaObject>("getWindow").Call("setLayout", -1, -2);
		}));
#else                   // その他実機.
		Debug.Log("at ShowProgressDialog(title:" + title + ", message:" + message + ")");
#endif
		}

		public static void CloseProgressDialog()
		{
#if UNITY_EDITOR        // エディター.
			Debug.Log("at CloseProgressDialog()");
#elif UNITY_ANDROID     // Android実機.
		if (progressDialog != null)
		{
			progressDialog.Call("dismiss");
			progressDialog = null;
		}
#else                   // その他実機.
		Debug.Log("at CloseProgressDialog()");
#endif
		}

		public static void SetMessageProgressDialog(string message)
		{
#if UNITY_EDITOR        // エディター.
			Debug.Log("at SetProgressDialog(message:" + message + ")");
#elif UNITY_ANDROID     // Android実機.
		if (progressDialog != null)
		{
			var activity = GetActivity();
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
//				progressDialog.Call("setTitle", title);
				progressDialog.Call("setMessage", message);
//				progressDialog.Call<AndroidJavaObject>("getWindow").Call("setLayout", -1, -2);
				activity.Dispose();
			}));
		}
#else                   // その他実機.
		Debug.Log("at SetProgressDialog(message:" + message + ")");
#endif
		}

		static string externalMediaDirPath = "";

		/// <summary>
		/// SDカードのmediaディレクトリのパスを返す.
		/// </summary>
		/// <returns>java.io.File externalMediaDir</returns>
		/// 外部SDカードのパスを取得する http://wada811.blogspot.com/2013/10/get-external-sd-card-path-in-android.html
		public static string GetExternalMediaDirPath()
		{
			string resultPath = "";
			if (!string.IsNullOrEmpty(externalMediaDirPath))
			{
				resultPath = externalMediaDirPath;
			}
			else
			{
#if UNITY_EDITOR        // エディター.
				resultPath = Path.Combine(Application.dataPath, "../FdaVod_DataCreator/MoviesConverted");
#elif UNITY_ANDROID     // Android実機.
			/*
			//			通常のAndroid.
			using (var context = GetContext())
			{
			Log.D("-------->");
				var externalMediaDirs = context.Call<AndroidJavaObject[]>("getExternalMediaDirs");
				//  getExternalMediaDirs[0]:/storage/emulated/0/Android/ media/jp.co.vxv.FdaVod2017, exists:True			内部.
				//  getExternalMediaDirs[1]:/storage/sdcard1/Android/media/jp.co.vxv.FdaVod2017, exists:True				SD内に存在するけど、アクセス出来たりできなかったり.
				foreach (var externalMediaDir in externalMediaDirs)
				{
					var absolutePath = externalMediaDir.Call<string>("getAbsolutePath");
					Log.D("@@@externalMediaDir:" + absolutePath + ", exists:" + externalMediaDir.Call<bool>("exists"));
					if (absolutePath.Contains("/sdcard"))
					{
						Log.D(" SDCard found!");
						resultPath = absolutePath;
						break;
					}
				}
			Log.D("<--------");
			}
			*/
			// ASUS.
			resultPath = "/storage/6516-EE25/Android/media/jp.co.vxv.FdaVod2017";
			// Nonbrand
			//  @@@externalMediaDir:/storage/emulated/0/Android/media/jp.co.vxv.FdaVod2017, exists:True
			//  @@@externalMediaDir:/storage/sdcard1/Android/media/jp.co.vxv.FdaVod2017, exists:True
			// ASUS.
			//  @@@externalMediaDir:/storage/emulated/0/Android/media/jp.co.vxv.FdaVod2017, exists:True
			//  @@@externalMediaDir:/storage/6516-EE25/Android/media/jp.co.vxv.FdaVod2017, exists:True


#else                   // その他実機.
			resultPath = Path.Combine(Application.dataPath, "../FdaVod_DataCreator/MoviesConverted");
#endif
			}
			return resultPath;
		}
		/*
		ASUS
		02-16 05:22:17.965: D/Unity(7460): @@@externalMediaDir:/storage/emulated/0/Android/media/jp.co.vxv.FdaVod2017, exists:True
		02-16 05:22:17.966: D/Unity(7460): @@@externalMediaDir:/storage/6516-EE25/Android/media/jp.co.vxv.FdaVod2017, exists:True

		*/


		/// <summary>
		/// SDカードのmediaディレクトリのパスを返す.
		/// </summary>
		/// <returns>java.io.File externalMediaDir</returns>
		/// 外部SDカードのパスを取得する http://wada811.blogspot.com/2013/10/get-external-sd-card-path-in-android.html
		public static string GetExternalDCIMDirPath()
		{
			string resultPath = "";
#if UNITY_EDITOR        // エディター.
			resultPath = Path.Combine(Application.dataPath, "../FdaVod_DataCreator/MoviesConverted");
#elif UNITY_ANDROID     // Android実機.
		/*		内部しか取得できなかったため封印.
		//		File dcim = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_DCIM);
		AndroidJavaClass environmentClass = new AndroidJavaClass("android.os.Environment");
		var DIRECTORY_DCIM = environmentClass.GetStatic<string>("DIRECTORY_DCIM");
		Log.D("at GetExternalDCIMDirPath() DIRECTORY_DCIM:" + DIRECTORY_DCIM);
		var externalStoragePublicDir = environmentClass.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory", DIRECTORY_DCIM);
		var absolutePath = externalStoragePublicDir.Call<string>("getAbsolutePath");
		DLog(" @@@@@@@@@@@@@@@@@@@@@@@@@@@@ externalStoragePublicDir:" + absolutePath + ", exists:" + externalStoragePublicDir.Call<bool>("exists"));
//		if (absolutePath.Contains("/sdcard"))
//		{
//			//				DLog(" SDCard found!");
//			resultPath = externalStoragePublicDir.Call<string>("getAbsolutePath");
//		}
*/
		resultPath = "/storage/sdcard1/DCIM";
#else                   // その他実機.
#endif
			return resultPath;
		}

		public static int getVersionCode()
		{
			int versionCode = 0;
#if UNITY_EDITOR        // エディター.
#elif UNITY_ANDROID     // Android実機.
			try
			{
				using (var context = GetContext())
				{
					var packageManager = context.Call<AndroidJavaObject>("getPackageManager");
					var packageName = context.Call<string>("getPackageName");
//					var activities = packageManager.GetStatic<int>("GET_ACTIVITIES");以前は動いていたが動かなくなったので下記に変更.
					var activities = 1;
					var pInfo = packageManager.Call<AndroidJavaObject>("getPackageInfo", packageName, activities);
					versionCode = pInfo.Get<int>("versionCode");
				}
			}
			catch (Exception e)
			{
				Debug.LogWarning(e.ToString());
			}
#else                   // その他実機.
#endif
			return versionCode;
		}

		public static string getVersionName()
		{
			string versionName = "";
#if UNITY_EDITOR        // エディター.
#elif UNITY_ANDROID     // Android実機.
			try
			{
				using (var context = GetContext())
				{
					var packageManager = context.Call<AndroidJavaObject>("getPackageManager");
					var packageName = context.Call<string>("getPackageName");
//					var activities = packageManager.GetStatic<int>("GET_ACTIVITIES");以前は動いていたが動かなくなったので下記に変更.
					var activities = 1;
					var pInfo = packageManager.Call<AndroidJavaObject>("getPackageInfo", packageName, activities);
					versionName = pInfo.Get<string>("versionName");
				}
			}
			catch (Exception e)
			{
				Debug.LogWarning(e.ToString());
			}
#else                   // その他実機.
#endif
			return versionName;
		}

		public static string getPackageName()
		{
			string packageName = "";
#if UNITY_EDITOR        // エディター.
#elif UNITY_ANDROID     // Android実機.
			try
			{
				using (var context = GetContext())
				{
					packageName = context.Call<string>("getPackageName");
				}
			}
			catch (Exception e)
			{
				Debug.LogWarning(e.ToString());
			}
#else                   // その他実機.
#endif
			return packageName;
		}


		// https://forum.unity3d.com/threads/battery-status-level.268024/

		/// <summary>
		/// 充電ステータスを返す.
		/// </summary>
		/// <returns></returns>
		public static int GetBatteryStatus()
		{
#if UNITY_EDITOR        // エディター.
#elif UNITY_ANDROID     // Android実機.
		try
		{
			using (var activity = GetActivity())
			{
				using (var intentFilter = new AndroidJavaObject("android.content.IntentFilter", new object[] { "android.intent.action.BATTERY_CHANGED" }))
				{
					using (AndroidJavaObject batteryIntent = activity.Call<AndroidJavaObject>("registerReceiver", new object[] { null, intentFilter }))
					{
						int status = batteryIntent.Call<int>("getIntExtra", new object[] { "status", -1 });
						return status;
					}
				}
			}
		}
		catch (System.Exception ex)
		{
			Debug.Log(""+ex.ToString());
		}
#else                   // その他実機.
#endif
			return -1;
		}

		/// <summary>
		/// 充電レベルを0～100で返す.
		/// </summary>
		/// <returns></returns>
		public static float GetBatteryLevel()
		{
#if UNITY_EDITOR        // エディター.
#elif UNITY_ANDROID     // Android実機.
		try
		{
			using (var activity = GetActivity())
			{
				using (var intentFilter = new AndroidJavaObject("android.content.IntentFilter", new object[] { "android.intent.action.BATTERY_CHANGED" }))
				{
					using (AndroidJavaObject batteryIntent = activity.Call<AndroidJavaObject>("registerReceiver", new object[] { null, intentFilter }))
					{
						int level = batteryIntent.Call<int>("getIntExtra", new object[] { "level", -1 });
						int scale = batteryIntent.Call<int>("getIntExtra", new object[] { "scale", -1 });

						// Error checking that probably isn't needed but I added just in case.
						if (level == -1 || scale == -1)
						{
							return 50f;
						}
						return ((float)level / (float)scale) * 100.0f;
					}
				}
			}
		}
		catch (System.Exception ex)
		{
			Debug.Log(""+ex.ToString());
		}
#else                   // その他実機.
#endif
			return 100.0f;
		}

		// Android実機.

		// ※Activity activity = UnityPlayer.currentActivity;が同じものかもしれない.要確認.
		public static AndroidJavaObject GetActivity()
		{
			AndroidJavaObject ret = null;
#if UNITY_EDITOR           // エディター.
#elif UNITY_STANDALONE_WIN // Win実機.
#elif UNITY_ANDROID        // Android実機.
		var unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		ret = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
#else                      // その他実機.
#endif
			return ret;
		}

		public static AndroidJavaObject GetContext()
		{
			AndroidJavaObject ret = null;
#if UNITY_EDITOR           // エディター.
#elif UNITY_STANDALONE_WIN // Win実機.
#elif UNITY_ANDROID        // Android実機.
			var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			ret = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity").Call<AndroidJavaObject>("getApplicationContext");
#else                      // その他実機.
#endif
			return ret;
		}

		public static AndroidJavaObject NewObject(string aClassName, params object[] args)
		{
			AndroidJavaObject ret = null;
#if UNITY_EDITOR           // エディター.
#elif UNITY_STANDALONE_WIN // Win実機.
#elif UNITY_ANDROID        // Android実機.
			ret = new AndroidJavaObject(aClassName, args);
#else                      // その他実機.
#endif
			return ret;
		}

		public static AndroidJavaClass NewClass(string aClassName)
		{
			AndroidJavaClass ret = null;
#if UNITY_EDITOR           // エディター.
#elif UNITY_STANDALONE_WIN // Win実機.
#elif UNITY_ANDROID        // Android実機.
			ret = new AndroidJavaClass(aClassName);
#else                      // その他実機.
#endif
			return ret;
		}
	}
}