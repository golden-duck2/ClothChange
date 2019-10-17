using UnityEngine;
using System;

namespace Jam
{
	public class PrimitiveTypes32
	{

		public TypeCode typeCode = TypeCode.Empty;
		public PrimitiveTypes32Union value;

		public void Set(bool val)
		{
			//		typeCode = Type.GetTypeCode(typeof(bool));
			typeCode = TypeCode.Boolean;
			value._bool = val;
		}

		public void Set(char val)
		{
			typeCode = TypeCode.Char;
			value._char = val;
		}

		public void Set(int val)
		{
			typeCode = TypeCode.Int32;
			value._int = val;
		}

		public void Set(float val)
		{
			typeCode = TypeCode.Single;
			value._float = val;
		}

		public static bool TryParse(string s, out PrimitiveTypes32 result)
		{
			result = new PrimitiveTypes32();
			{
				if (bool.TryParse(s, out result.value._bool))
				{
					result.typeCode = TypeCode.Boolean;
					return true;
				}
			}
			{
				if (int.TryParse(s, out result.value._int))
				{
					result.typeCode = TypeCode.Int32;
					return true;
				}
			}
			{
				if (s.EndsWith("f", true, null))
				{   // sが"f"か"F"で終わる.
					var s1 = s.Substring(0, s.Length - 1);
					if (float.TryParse(s1, out result.value._float))
					{
						result.typeCode = TypeCode.Single;
						return true;
					}
				}
				else
				{
					if (float.TryParse(s, out result.value._float))
					{
						result.typeCode = TypeCode.Single;
						return true;
					}
				}
			}
			{
				if (char.TryParse(s, out result.value._char))
				{
					result.typeCode = TypeCode.Char;
					return true;
				}
			}
			{
				if (s.StartsWith("@"))
				{   // sが"@"で始まる.
					var key = s.Substring(1);
					var s1 = Jam.InGameDataManager.instance.GetString(key);
					if (TryParse(s1, out result))
					{
						return true;
					}
				}
			}
			return false;
		}

		public static void Test()
		{
			PrimitiveTypes32 val;
			var sArray = new string[]
			{
			"true",
			"false",
			"@",
			"123",
			"123.4f",
			"123.4",
			"1",
			"1.",
			"1f",
			"@ExpressionTypeIndex",
			"@DifficultyIndex",
			};
			foreach (var s in sArray)
			{
				if (PrimitiveTypes32.TryParse(s, out val))
				{
					Debug.Log("\"" + s + "\"->" + "val:" + val.ToString());
				}
				else
				{
					Debug.Log("PrimitiveTypes32.TryParse(\"" + s + "\") returns false.");
				}
			}
		}

		public override string ToString()
		{
			var sb = StringBuilderEx.sb;
			sb.Set(this.GetType().ToString() + "{typeCode:" + typeCode + ", value:");
			string valStr;
			switch (typeCode)
			{
				case TypeCode.Boolean:
					valStr = value._bool.ToString();
					break;
				case TypeCode.Char:
					valStr = value._char.ToString();
					break;
				case TypeCode.Int32:
					valStr = value._int.ToString();
					break;
				case TypeCode.Single:
					valStr = value._float.ToString();
					break;
				default:
					valStr = "unknown";
					Debug.LogWarning("Not supported.");
					break;
			}
			sb.Append(valStr);
			sb.Append("}");
			return sb.ToString();
		}
	}
}
