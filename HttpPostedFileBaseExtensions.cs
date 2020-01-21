using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace NetFrameworkHelper
{
	/// <summary>
	/// Extension methods for handling HttpPostedFileBase files.
	/// </summary>
	public static class HttpPostedFileBaseExtensions
	{
		/// <summary>
		/// Convert a HttpPostedFileBase to an byte array.  To compress the byte array, specify true when calling the extension.
		/// </summary>
		/// <param name="file">Posted file to apply this to</param>
		/// <returns>byte[]</returns>
		public static byte[] ConvertToBytes(this HttpPostedFileBase file)
		{
			if (file == null)
			{
				throw new ArgumentNullException(nameof(file), "The passed in file parameter was null.");
			}
			return file.ConvertToBytes(false);
		}
		/// <summary>
		/// Convert a HttpPostedFileBase to an byte array.  To compress the byte array, specify true when calling the extension.
		/// </summary>
		/// <param name="file">Posted file to apply this to</param>
		/// <param name="compress">Set to true to compress the returned byte array. Default is false.</param>
		/// <returns>byte[]</returns>
		public static byte[] ConvertToBytes(this HttpPostedFileBase file, bool compress = false)
		{
			if (file == null)
			{
				throw new ArgumentNullException(nameof(file), "The passed in file parameter was null.");
			}
			BinaryReader rdr = new BinaryReader(file.InputStream);
			byte[] byteArr = rdr.ReadBytes(file.ContentLength);
			rdr.Dispose();
			if (compress)
			{
				return byteArr.Compress();
			}
			return byteArr;
		}
		/// <summary>
		/// Convert a list of HttpPostedFileBase to a List of Images. Can specify to convert the images. Default is false.
		/// </summary>
		/// <param name="files">List of HttpPostedFileBase to run this against</param>
		/// <returns></returns>
		public static List<Image> ToImageList(this List<HttpPostedFileBase> files)
		{
			if (files == null)
			{
				throw new ArgumentNullException(nameof(files), "No files passed in.");
			}
			List<Image> images = new List<Image>();
			Parallel.ForEach(files, file =>
			{
				images.Add(file.ToImage());
			});
			return images;
		}
		/// <summary>
		/// Convert <see cref="HttpPostedFileBase"/> to Image.  To get Image meta data, cast Image.Tags to <see cref="ImageMetaData"/>
		/// </summary>
		/// <param name="file">file to apply this method to.</param>
		/// <returns>Image</returns>
		public static Image ToImage(this HttpPostedFileBase file)
		{
			if (file == null)
			{
				throw new ArgumentNullException(nameof(file), "File passed was null");
			}
			Image img = Image.FromStream(file.InputStream, true, true);
			img.Tag = new ImageMetaData
			{
				ImageName = file.FileName,
				ImageExtension = file.FileName.GetFileExtension(),
				MimeType = file.FileName.GetMimeFromFileName()
			};
			return img;
		}
		/// <summary>
		/// Gets file extensions of files.
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns>string with the file extension</returns>
		private static string GetFileExtension(this string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException(nameof(fileName), "File name not passed in");
			}
			string extension;
			try
			{
				extension = Path.GetExtension(fileName);
			}
			catch (ArgumentException)
			{
				extension = fileName.Mid(fileName.LastIndexOf('.'));
			}
			return extension;
		}
	}
}
