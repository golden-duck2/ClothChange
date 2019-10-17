using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Jam
{
	public static class ApplicationEx
	{
		public static string GetIdentifier()
		{
			var identifier = "";
#if UNITY_EDITOR        // エディター.
			identifier = PlayerSettings.applicationIdentifier;
#elif UNITY_ANDROID     // Android実機.
			identifier = AndroidUtil.getPackageName();
#else                   // その他実機.
#endif
			return identifier;
		}

		public static int GetVersionCode()
		{
			var versionCode = 0;
#if UNITY_EDITOR        // エディター.
			versionCode = PlayerSettings.Android.bundleVersionCode;
#elif UNITY_ANDROID     // Android実機.
			versionCode = AndroidUtil.getVersionCode();
#else                   // その他実機.
#endif
			return versionCode;
		}

		public static string ToDebugString(string aPrefix = "", string aName = "Application", string aSeparator = "\n")
		{
			var sb = StringBuilderEx.sb.Clear();
			sb.AppendFormat("{0}{1}:{{{2}", aPrefix, aName, aSeparator);
			sb.AppendFormat("{0} identifier:{1},{2}", aPrefix, Application.identifier, aSeparator);
			sb.AppendFormat("{0} installerName:{1},{2}", aPrefix, Application.installerName, aSeparator);
			sb.AppendFormat("{0} version:{1}{2}", aPrefix, Application.version, aSeparator);
			sb.AppendFormat("{0} unityVersion:{1}{2}", aPrefix, Application.unityVersion, aSeparator);
			sb.AppendFormat("{0} absoluteURL:{1}{2}", aPrefix, Application.absoluteURL, aSeparator);
//			sb.AppendFormat("{0} srcValue:{1}{2}", aPrefix, Application.srcValue, aSeparator);
			sb.AppendFormat("{0} temporaryCachePath:{1}{2}", aPrefix, Application.temporaryCachePath, aSeparator);
			sb.AppendFormat("{0} persistentDataPath:{1}{2}", aPrefix, Application.persistentDataPath, aSeparator);
			sb.AppendFormat("{0} streamingAssetsPath:{1}{2}", aPrefix, Application.streamingAssetsPath, aSeparator);
			sb.AppendFormat("{0} dataPath:{1}{2}", aPrefix, Application.dataPath, aSeparator);
			sb.AppendFormat("{0} runInBackground:{1}{2}", aPrefix, Application.runInBackground, aSeparator);
			sb.AppendFormat("{0} isConsolePlatform:{1}{2}", aPrefix, Application.isConsolePlatform, aSeparator);
			sb.AppendFormat("{0} isMobilePlatform:{1}{2}", aPrefix, Application.isMobilePlatform, aSeparator);
			sb.AppendFormat("{0} buildGUID:{1}{2}", aPrefix, Application.buildGUID, aSeparator);
			sb.AppendFormat("{0} installMode:{1}{2}", aPrefix, Application.installMode, aSeparator);
			sb.AppendFormat("{0} sandboxType:{1}{2}", aPrefix, Application.sandboxType, aSeparator);
			sb.AppendFormat("{0} productName:{1}{2}", aPrefix, Application.productName, aSeparator);
			sb.AppendFormat("{0} companyName:{1}{2}", aPrefix, Application.companyName, aSeparator);
			sb.AppendFormat("{0} genuineCheckAvailable:{1}{2}", aPrefix, Application.genuineCheckAvailable, aSeparator);
//			sb.AppendFormat("{0} isWebPlayer:{1}{2}", aPrefix, Application.isWebPlayer, aSeparator);
			sb.AppendFormat("{0} genuine:{1}{2}", aPrefix, Application.genuine, aSeparator);
			sb.AppendFormat("{0} backgroundLoadingPriority:{1}{2}", aPrefix, Application.backgroundLoadingPriority, aSeparator);
			sb.AppendFormat("{0} systemLanguage:{1}{2}", aPrefix, Application.systemLanguage, aSeparator);
			sb.AppendFormat("{0} targetFrameRate:{1}{2}", aPrefix, Application.targetFrameRate, aSeparator);
			sb.AppendFormat("{0} cloudProjectId:{1}{2}", aPrefix, Application.cloudProjectId, aSeparator);
			sb.AppendFormat("{0} internetReachability:{1}{2}", aPrefix, Application.internetReachability, aSeparator);
			sb.AppendFormat("{0} isEditor:{1}{2}", aPrefix, Application.isEditor, aSeparator);
			sb.AppendFormat("{0} platform:{1}{2}", aPrefix, Application.platform, aSeparator);
			sb.AppendFormat("{0} isPlaying:{1}{2}", aPrefix, Application.isPlaying, aSeparator);
//			sb.AppendFormat("{0} streamedBytes:{1}{2}", aPrefix, Application.streamedBytes, aSeparator);
			sb.AppendFormat("{0} isFocused:{1}{2}", aPrefix, Application.isFocused, aSeparator);
			var buildTags = Application.GetBuildTags();
			for (var idx = 0; idx < buildTags.Length; ++idx)
			{
				var buildTag = buildTags[idx];
				sb.AppendFormat("{0} buildTags[1]:{2}{3}", aPrefix, idx, buildTag, aSeparator);
			}
			sb.AppendFormat("{0} GetIdentifier():{1}{2}", aPrefix, GetIdentifier(), aSeparator);
			sb.AppendFormat("{0} GetVersionCode():{1}{2}", aPrefix, GetVersionCode(), aSeparator);
			sb.AppendFormat("{0}}}{1}", aPrefix, aSeparator);
			return sb.ToString();
		}
	}
}
