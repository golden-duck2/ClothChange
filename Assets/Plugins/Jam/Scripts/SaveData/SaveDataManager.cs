using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Jam
{

	public class SaveDataManager : SaveDataManagerBase<SaveDataManager>
	{
		private static readonly string EncryptKey = "c6eahbq9sjuawhvdr9kvhpsm5qv393ga";
		private static readonly int EncryptPasswordCount = 16;
		private static readonly string PasswordChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private static readonly int PasswordCharsLength = PasswordChars.Length;
		static readonly string SaveDataFileName = "save.bytes";

		public override void Save()
		{
			base.Save();
			var SaveDataPath = Application.persistentDataPath + "/" + SaveDataFileName;

			string iv;
			string base64;

			string json = JsonUtility.ToJson(data);
			EncryptAesBase64(json, out iv, out base64);
			//		Debug.Log("[Encrypt]json:" + json);
			//		Debug.Log("[Encrypt]base64:" + base64);


			byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
			byte[] base64Bytes = Encoding.UTF8.GetBytes(base64);
			using (FileStream fs = new FileStream(SaveDataPath, FileMode.Create, FileAccess.Write))
			using (BinaryWriter bw = new BinaryWriter(fs))
			{
				bw.Write(ivBytes.Length);
				bw.Write(ivBytes);

				bw.Write(base64Bytes.Length);
				bw.Write(base64Bytes);
			}
		}

		public override void Load()
		{
			base.Load();
			var SaveDataPath = Application.persistentDataPath + "/" + SaveDataFileName;
			//		Debug.Log("at SaveDataManager.Load()" + transform.GetPath());
			byte[] ivBytes = null;
			byte[] base64Bytes = null;
			if (!File.Exists(SaveDataPath))
			{
				Debug.Log("save data not found.");
				return;
			}
			using (FileStream fs = new FileStream(SaveDataPath, FileMode.Open, FileAccess.Read))
			using (BinaryReader br = new BinaryReader(fs))
			{
				int length = br.ReadInt32();
				ivBytes = br.ReadBytes(length);

				length = br.ReadInt32();
				base64Bytes = br.ReadBytes(length);
			}

			// 複合
			string json;
			string iv = Encoding.UTF8.GetString(ivBytes);
			string base64 = Encoding.UTF8.GetString(base64Bytes);
			DecryptAesBase64(base64, iv, out json);
			//		Debug.Log("[Decrypt]json:" + json);

			// セーブデータ復元
			//		SaveData obj = JsonMapper.ToObject<SaveData>(json);
			data = JsonUtility.FromJson<SerializableDictionary<string, string>>(json);
		}


		/// <summary>
		/// AES暗号化(Base64形式)
		/// </summary>
		public static void EncryptAesBase64(string json, out string iv, out string base64)
		{
			byte[] src = Encoding.UTF8.GetBytes(json);
			byte[] dst;
			EncryptAes(src, out iv, out dst);
			base64 = Convert.ToBase64String(dst);
		}

		/// <summary>
		/// AES複合(Base64形式)
		/// </summary>
		public static void DecryptAesBase64(string base64, string iv, out string json)
		{
			byte[] src = Convert.FromBase64String(base64);
			byte[] dst;
			DecryptAes(src, iv, out dst);
			json = Encoding.UTF8.GetString(dst).Trim('\0');
		}

		/// <summary>
		/// AES暗号化
		/// </summary>
		public static void EncryptAes(byte[] src, out string iv, out byte[] dst)
		{
			iv = CreatePassword(EncryptPasswordCount);
			dst = null;
			using (RijndaelManaged rijndael = new RijndaelManaged())
			{
				rijndael.Padding = PaddingMode.PKCS7;
				rijndael.Mode = CipherMode.CBC;
				rijndael.KeySize = 256;
				rijndael.BlockSize = 128;

				byte[] key = Encoding.UTF8.GetBytes(EncryptKey);
				byte[] vec = Encoding.UTF8.GetBytes(iv);

				using (ICryptoTransform encryptor = rijndael.CreateEncryptor(key, vec))
				using (MemoryStream ms = new MemoryStream())
				using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
				{
					cs.Write(src, 0, src.Length);
					cs.FlushFinalBlock();
					dst = ms.ToArray();
				}
			}
		}

		/// <summary>
		/// AES複合
		/// </summary>
		public static void DecryptAes(byte[] src, string iv, out byte[] dst)
		{
			dst = new byte[src.Length];
			using (RijndaelManaged rijndael = new RijndaelManaged())
			{
				rijndael.Padding = PaddingMode.PKCS7;
				rijndael.Mode = CipherMode.CBC;
				rijndael.KeySize = 256;
				rijndael.BlockSize = 128;

				byte[] key = Encoding.UTF8.GetBytes(EncryptKey);
				byte[] vec = Encoding.UTF8.GetBytes(iv);

				using (ICryptoTransform decryptor = rijndael.CreateDecryptor(key, vec))
				using (MemoryStream ms = new MemoryStream(src))
				using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
				{
					cs.Read(dst, 0, dst.Length);
				}
			}
		}

		/// <summary>
		/// パスワード生成
		/// </summary>
		/// <param name="count">文字列数</param>
		/// <returns>パスワード</returns>
		public static string CreatePassword(int count)
		{
			StringBuilder sb = new StringBuilder(count);
			for (int i = count - 1; i >= 0; i--)
			{
				char c = PasswordChars[UnityEngine.Random.Range(0, PasswordCharsLength)];
				sb.Append(c);
			}
			return sb.ToString();
		}
	}
}