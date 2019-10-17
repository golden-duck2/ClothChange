using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Jam
{
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class TextAttribute : PropertyAttribute
	{
//		public readonly int lines;

		public TextAttribute()
		{
//			this.lines = 3;
		}

//		public TextAttribute(int lines)
//		{
//			this.lines = lines;
//		}
	}
}
