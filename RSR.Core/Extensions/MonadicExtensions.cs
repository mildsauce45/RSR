using System;

namespace RSR.Core.Extensions
{
	public static class MonadicExtensions
	{
		public static TResult Having<TInput, TResult>(this TInput obj, Func<TInput, TResult> func)
			where TInput : class
			where TResult : class
		{
			if (obj == null)
				return null;

			return func(obj);
		}

		public static TResult Return<TInput, TResult>(this TInput obj, Func<TInput, TResult> func, TResult fallback = default(TResult))
			where TInput : class
		{
			if (obj == null)
				return fallback;

			return func(obj);
		}

		public static TInput If<TInput>(this TInput obj, Func<TInput, bool> predicate)
			where TInput : class
		{
			if (obj == null || predicate == null)
				return null;

			return predicate(obj) ? obj : null;
		}
	}
}
