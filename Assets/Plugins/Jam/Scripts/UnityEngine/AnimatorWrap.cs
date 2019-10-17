using UnityEngine;
using System.Collections;

namespace Jam
{
	public class AnimatorWrap : MonoBehaviour
	{
		Animator animator;

		void Start()
		{
			animator = GetComponent<Animator>();
		}


		public void SetParameter(string aParamNameAndParamValues)
		{
//			animator.SetParameter(aParamNameAndParamValues);
		}
		/*
		public void AddParameter(string aParamNameAndParamValue)
		{
			var array = aParamNameAndParamValue.Split(',');
			var paramName = array[0];
			var paramValueStr = (array.Length >= 2) ? array[1] : "";
			switch (animator.GetAnimatorControllerParameterType(paramName))
			{
				case AnimatorControllerParameterType.Float:
					animator.SetFloat(paramName, animator.GetFloat(paramName)+paramValueStr.ParseFloat());
					break;
				case AnimatorControllerParameterType.Int:
					animator.SetInteger(paramName, animator.GetInteger(paramName) + paramValueStr.ParseInt());
					break;
				case AnimatorControllerParameterType.Bool:
				case AnimatorControllerParameterType.Trigger:
				default:
					Debug.LogWarning("Not supported yet.");
					break;
			}
		}
	*/
	}
}
