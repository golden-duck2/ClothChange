using UnityEngine;

/*
namespace Jam
{
	public static class AnimatorEx
	{
		static public void SetParameter(this Animator self, string aParamNameAndParamValues)
		{
			var valueArray = aParamNameAndParamValues.Split(',');      // name and param array.
			foreach (var value in valueArray)
			{
				//			Debug.Log(" value:" + value);

				string lValue;
				string separator;
				string rValue;
				Parser.SplitExpression(value, new string[] { "+=", "-=", "=" }, out lValue, out separator, out rValue);
				switch (self.GetAnimatorControllerParameterType(lValue))
				{
					case AnimatorControllerParameterType.Float:
						{
							var val = rValue.ParseFloat();
							switch (separator)
							{
								case "+=":
									val = self.GetFloat(lValue) + val;
									break;
								case "-=":
									val = self.GetFloat(lValue) - val;
									break;
								case "=":
									break;
								default:
									Debug.LogWarning("Not supported.");
									return;
							}
							self.SetFloat(lValue, val);
						}
						break;
					case AnimatorControllerParameterType.Int:
						{
							var val = rValue.ParseInt();
							switch (separator)
							{
								case "+=":
									val = self.GetInteger(lValue) + val;
									break;
								case "-=":
									val = self.GetInteger(lValue) - val;
									break;
								case "=":
									break;
								default:
									Debug.LogWarning("Not supported.");
									return;
							}
							self.SetInteger(lValue, val);
						}
						break;
					case AnimatorControllerParameterType.Bool:
						{
							switch (separator)
							{
								case "=":
									self.SetBool(lValue, rValue.ParseBool());
									break;
								default:
									Debug.LogWarning("Not supported.");
									return;
							}
						}
						break;
					case AnimatorControllerParameterType.Trigger:
						{
							switch (separator)
							{
								case "":
									self.SetTrigger(lValue);
									break;
								default:
									Debug.LogWarning("Not supported.");
									return;
							}
						}
						break;
					default:
						Debug.LogWarning("Not supported.");
						break;
				}
			}
		}

		static public AnimatorControllerParameterType GetAnimatorControllerParameterType(this Animator self, string aParamName)
		{
			foreach (var p in self.parameters)
			{
				if (aParamName.Equals(p.name))
				{
					return p.type;
				}
			}
			Debug.LogWarning("at AnimatorEx.GetAnimatorControllerParameterType(aParamName:" + aParamName + ") Not found parameter name.");
			return AnimatorControllerParameterType.Int;
		}
	}
}
*/