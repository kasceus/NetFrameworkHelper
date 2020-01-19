using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Mvc;
using System.Web;

namespace Net_Framework_Helper
{
	/// <summary>
	/// The FileHelper is used for common file operations
	/// </summary>
	public static class FileHelper
	{
		/// <summary>
		/// This extension method will retun a FileContentResult from a path to a file.  
		/// <para>This result is return ready- includes mime-type for the specified file</para>
		/// </summary>
		/// <param name="pathToFile">Full path to a file</param>
		/// <returns>FileCointentResult</returns>
		public static FileContentResult FileContentResultFromString(this string pathToFile)
		{
			if (File.Exists(pathToFile))
			{
				return new FileContentResult(pathToFile.FileBytesFromPath(), pathToFile.GetMimeFromFilePath());
			}
			else
			{
				throw new FileNotFoundException(nameof(pathToFile), "The file you requested was not found.");
			}
		}
		/// <summary>
		/// Get a mime type for a file path
		/// </summary>
		/// <param name="pathToFile"></param>
		/// <returns></returns>
		public static string GetMimeFromFilePath(this string pathToFile)
		{
			try
			{
				FileAttributes attr = File.GetAttributes(pathToFile);
				if (attr.HasFlag(FileAttributes.Directory))
				{
					throw new Exception("The specified path is a directory. Please supply the file name in addition to the file.");
				}
			}
			catch
			{
				throw;
			}
			if (File.Exists(pathToFile))
			{
				var fileName = Path.GetFileName(pathToFile);
				return fileName.GetMimeFromFileName();
			}
			else
			{
				throw new FileNotFoundException("File not found.");
			}
		}
		/// <summary>
		/// Get a mime type for a file name
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static string GetMimeFromFileName(this string fileName)
		{
			try
			{
				FileAttributes attr = File.GetAttributes(fileName);
				if (attr.HasFlag(FileAttributes.Directory))
				{
					throw new Exception("The specified path is a directory. Please supply the file name.");
				}
			}
			catch
			{
				throw;
			}
			return MimeMapping.GetMimeMapping(fileName);
		}

		/// <summary>
		/// Returns the byte array for a specified file path
		/// </summary>
		/// <param name="pathToFile">File Path string</param>
		/// <returns>byte[]</returns>
		public static byte[] FileBytesFromPath(this string pathToFile)
		{
			if (File.Exists(pathToFile))
			{
				return File.ReadAllBytes(pathToFile);
			}
			else
			{
				throw new FileNotFoundException(nameof(pathToFile), "The file you requested was not found.");
			}
		}
		/// <summary>
		/// Useful for deleting a file given the full path to the file
		/// </summary>
		/// <param name="pathToFile">path to the file</param>
		public static void DeleteFile(this string pathToFile)
		{
			try
			{
				FileAttributes attr = File.GetAttributes(pathToFile);
				if (attr.HasFlag(FileAttributes.Directory))
				{
					throw new Exception("The specified path is a directory. Please supply the file name in addition to the file.");
				}
			}
			catch
			{
				throw;
			}
			if (File.Exists(pathToFile))
			{
				try
				{
					File.Delete(pathToFile);
				}
				catch
				{ //catch any exceptions and rethrow them
					throw;
				}
			}
			else
			{
				throw new FileNotFoundException("The file was not found");
			}
		}

	}
}
