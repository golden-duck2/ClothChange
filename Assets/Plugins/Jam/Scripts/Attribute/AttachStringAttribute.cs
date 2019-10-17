using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jam
{
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class AttachStringAttribute : Attribute
	{
		public string value { get; private set; }

		public AttachStringAttribute(string aValue)
		{
			value = aValue;
		}
	}
}