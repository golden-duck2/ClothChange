using System.Runtime.InteropServices;

namespace Jam
{
	[StructLayout(LayoutKind.Explicit)]
	public struct PrimitiveTypes32Union
	{
		[FieldOffset(0)]
		public bool _bool;

		[FieldOffset(0)]
		public char _char;

		[FieldOffset(0)]
		public int _int;

		[FieldOffset(0)]
		public float _float;
	}
}
