using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

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
			using (MemoryStream ms = new MemoryStream(byteArray))
			{
				return Image.FromStream(ms);
			}
		}
		/// <summary>
		/// Resize an image.  Maintains aspect ratio
		/// </summary>
		/// <param name="image">Image to apply this to</param>
		/// <param name="size"> new size parameters</param>
		/// <returns></returns>
		public static Image Resize(this Image image, Size size)
		{
			if (image == null)
			{
				throw new ArgumentNullException(nameof(image), "image passed as null.");
			}
			if (size == null)
			{
				throw new ArgumentNullException(nameof(size), "Size cannot be null");
			}
			int sourceWidth = image.Width;
			int sourceHeight = image.Height;
			float nPercentW = size.Width / (float)sourceWidth;
			float nPercentH = size.Height / (float)sourceHeight;
			float nPercent;
			if (nPercentH < nPercentW)
			{
				nPercent = nPercentH;
			}
			else
			{
				nPercent = nPercentW;
			}

			int destWidth = (int)(sourceWidth * nPercent);
			int destHeight = (int)(sourceHeight * nPercent);

			Bitmap b = new Bitmap(destWidth, destHeight);
			Graphics g = Graphics.FromImage((Image)b);
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;

			g.DrawImage(image, 0, 0, destWidth, destHeight);
			g.Dispose();

			return (Image)b;
		}
	}
	/// <summary>
	/// ImageMetaData class. Used to hold basic image meta data for quick access.
	/// </summary>
	public class ImageMetaData
	{
		/// <summary>
		/// File name of the image
		/// </summary>
		public string ImageName { get; set; }
		/// <summary>
		/// File extension of the image
		/// </summary>
		public string ImageExtension { get; set; }
		/// <summary>
		/// Mime type for the image
		/// </summary>
		public string MimeType { get; set; }
	}
}
