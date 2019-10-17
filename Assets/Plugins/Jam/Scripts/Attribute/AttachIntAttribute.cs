using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jam
{
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class AttachIntAttribute : Attribute
	{
		public int value { get; private set; }

		public AttachIntAttribute(int aValue)
		{
			value = aValue;
		}
	}
}