using System;
using System.Collections.Generic;

namespace RSR.Core.Extensions
{
	public static class EnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
		{
			if (collection != null && action != null)
			{
				foreach (var t in collection)
					action(t);
			}
		}
	}
}