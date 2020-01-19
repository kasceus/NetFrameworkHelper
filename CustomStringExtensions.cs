using System;

namespace NetFrameworkHelper
{
	/// <summary>
	/// This class contains custom extensions for use with strings
	/// </summary>
	public static class CustomStringExtensions
	{
		/// <summary>
		/// Returns a string literal from a specified number of characters from the right of a string
		/// </summary>
		/// <param name="str">sting this method is called against</param>
		/// <param name="start">Number of characters to return from the right side of a string</param>
		/// <returns>String</returns>
		public static string Right(this string str, int start)
		{
			if (String.IsNullOrEmpty(str))
			{
				throw new ArgumentNullException(nameof(str), "String cannot be null or empty");
			}
			if (start > str.Length)
			{
				throw new ArgumentOutOfRangeException(nameof(str), "Number of characters greater than the string length");
			}
			return str.Substring(str.Length - start, start);
		}
		/// <summary>
		/// Returns a string literal from a specified number of characters from the start of a string
		/// </summary>
		/// <param name="str">sting this method is called against</param>
		/// <param name="nCount">Number of characters to return from the left</param>
		/// <returns>String</returns>
		public static string Left(this string str, int nCount)
		{
			if (String.IsNullOrEmpty(str))
			{
				throw new ArgumentNullException(nameof(str), "String cannot be null or empty");
			}
			if (nCount > str.Length)
			{
				throw new ArgumentOutOfRangeException(nameof(str), "Number of characters greater than the string length");
			}
			return str.Substring(0, nCount);
		}
		/// <summary>
		/// Returns a string from a specified start position
		/// </summary>
		/// <param name="str">sting this method is called against</param>
		/// <param name="start">Character number to start with</param>
		/// <returns>String</returns>
		public static string Mid(this string str, int start)
		{
			if (String.IsNullOrEmpty(str))
			{
				throw new ArgumentNullException(nameof(str), "String cannot be null or empty");
			}
			if (start > str.Length)
			{
				throw new ArgumentOutOfRangeException(nameof(str), "Number of characters greater than the string length");
			}

			return str.Substring(start);
		}
		/// <summary>
		/// Returns a string from a specified start position
		/// </summary>
		/// <param name="str">sting this method is called against</param>
		/// <param name="start">Character number to start with</param>
		/// <param name="stop">Character number to stop with</param>
		/// <returns>String</returns>
		public static string Mid(this string str, int start, int stop)
		{
			if (String.IsNullOrEmpty(str))
			{
				throw new ArgumentNullException(nameof(str), "String cannot be null or empty");
			}
			if (start > str.Length)
			{
				throw new ArgumentOutOfRangeException(nameof(str), "Number of characters greater than the string length");
			}
			//Check if start is less than 0
			start = (start < 0) ? 0 : start;
			//check if the requested stop position is longer than the returnes tring would be minus the start position
			//if it is, then set stop to the string length minus the start position
			stop = (stop > (str.Length - start)) ? str.Length - start : stop;
			return str.Substring(start, stop);
		}
	}
}
