using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Drawing;
using System.IO;
using System.Web;

namespace NetFrameworkHelper
{
	/// <summary>
	/// Class that contains common operations for handling images
	/// </summary>
	public static class ImageHelper
	{
		/// <summary>
		/// Convert an image to a blob (byte array) for storage in a database or for further byte manipulation
		/// </summary>
		/// <param name="image">Image this operation will be carried out against</param>
		/// <returns>byte[]</returns>
		public static byte[] ToBlob(this Image image)
		{
			//check if the byte array is compressed before moving on
			if (image == null)
			{
				throw new ArgumentNullException(nameof(image), "The image passed was null;");
			}
			return image.ToBlob(false);
		}
		/// <summary>
		/// Convert an image to a blob (byte array) for storage in a database or for further byte manipulation. 
		/// Specify true to return a compressed byte array.
		/// </summary>
		/// <param name="image">Image this operation will be carried out against</param>
		/// <param name="compress">Specify wether or not to compress the byte array using GZip. Default is false</param>
		/// <returns></returns>
		public static byte[] ToBlob(this Image image, bool compress = false)
		{
			//check if the byte array is compressed before moving on
			if (image == null)
			{
				throw new ArgumentNullException(nameof(image), "The image passed was null;");
			}
			ImageConverter imageConverter = new ImageConverter();
			byte[] byteArr = (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
			if (compress)
			{
				return byteArr.Compress();
			}
			return byteArr;
		}
		/// <summary>
		/// Convert a blob (byte array) to Image.  Automatically checks for compression.
		/// </summary>
		/// <param name="byteArray"></param>
		/// <returns></returns>
		public static Image ToImage(this byte[] byteArray)
		{
			if (byteArray == null)
			{
				throw new ArgumentNullException(nameof(byteArray), "Byte array was null.");
			}			
			using (var ms = new MemoryStream(byteArray))
			{
				return Image.FromStream(ms);
			}
		}
		//public static Image Resize(this Image image, int newWidth)
		//{

		//}

	}
	/// <summary>
	/// ImageMetaData class. Used to hold basic image meta data for quick access.
	/// </summary>
	public class ImageMetaData
	{
		public string ImageName { get; set; }
		public string ImageExtension { get; set; }
		public string MimeType { get; set; }
	}
}
