using UnityEngine;


//【Unity】Transform.SetParentでやってたことがUnity5.4だとInstantiateで指定できるようになったんですね。 
//  http://halcyonsystemblog.blog.fc2.com/blog-entry-330.html

namespace Jam
{
	/// <summary>
	/// Component extension.
	/// </summary>
	public static class ComponentEx
	{
		/// <summary>
		/// Get RectTransform.
		/// </summary>
		public static RectTransform GetRectTransform(this Component aSelf)
    {
        return aSelf.transform as RectTransform;
    }
  }
}
