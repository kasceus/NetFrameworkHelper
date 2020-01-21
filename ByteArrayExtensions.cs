using System;
using System.IO;
using System.IO.Compression;

namespace NetFrameworkHelper
{
	/// <summary>
	/// Common byte array extension methods. 
	/// </summary>
	/// <remarks>Byte array extension methods specific to Image operations are located in the Image Helper.</remarks>
	public static class ByteArrayExtensions
	{
		/// <summary>
		/// Compress a byte array using Gzip
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static byte[] Compress(this byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data), "The byte array was passed as null");
			}
			using (MemoryStream compressedStream = new MemoryStream())
			using (GZipStream zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
			{
				zipStream.Write(data, 0, data.Length);
				zipStream.Close();
				return compressedStream.ToArray();
			}
		}
		/// <summary>
		/// Decompress a byte array using gzip
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static byte[] Decompress(this byte[] data)
		{
			try
			{
				using (MemoryStream memStream = new MemoryStream(data))
				using (GZipStream zipStream = new GZipStream(memStream, CompressionMode.Decompress))
				using (MemoryStream resultStream = new MemoryStream())
				{
					zipStream.CopyTo(resultStream);
					zipStream.Dispose();
					byte[] retArray = resultStream.ToArray();
					return retArray;
				}
			}
			catch {/*not compressed*/}
			return data;
		}
	}
}
