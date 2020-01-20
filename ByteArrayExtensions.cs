using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NetFrameworkHelper
{
	/// <summary>
	/// Common byte array extension methods. 
	/// </summary>
	/// <remarks>Byte array extension methods specific to Image operations are located in the Image Helper.</remarks>
	public static class ByteArrayExtensions
	{
		public static byte[] Compress(this byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data), "The byte array was passed as null");
			}
			using (var compressedStream = new MemoryStream())
			using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
			{
				zipStream.Write(data, 0, data.Length);
				zipStream.Close();
				return compressedStream.ToArray();
			}
		}
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
