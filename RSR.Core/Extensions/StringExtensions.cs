using System.Security.Cryptography;
using System.Text;

namespace RSR.Core.Extensions
{
	public static class StringExtensions
	{
		public static string ToMd5(this string str)
		{
			var enc = new ASCIIEncoding();

			return enc.GetString(new MD5CryptoServiceProvider().ComputeHash(enc.GetBytes(str)));
		}
	}
}
