using System;
using System.IO;

namespace BauMotionComics.Tools
{	
	/// <summary>
	///		Clase de ayuda para manejo de directorios
	/// </summary>
	internal static class FilesHelper
	{
		/// <summary>
		///		Crea un directorio
		/// </summary>
		internal static bool CreatePath(string strPath)
		{ // Crea el directorio
				try
					{ Directory.CreateDirectory(strPath);
					}
				catch {}
			// Devuelve el valor que indica si se ha creado
				return Directory.Exists(strPath);
		}

		/// <summary>
		///		Borra un directorio
		/// </summary>
		internal static bool DeletePath(string strPath)
		{	// Borra el directorio
				try
					{ Directory.Delete(strPath, true);
					}
				catch (Exception objException) 
					{ System.Diagnostics.Debug.WriteLine("Excepción: " + objException.Message);
					}
			// Devuelve el valor que indica si se ha borrado
				return !Directory.Exists(strPath);
		}

		/// <summary>
		///		Borra un archivo
		/// </summary>
		internal static bool DeleteFile(string strFileName)
		{	// Borra el archivo
				try
					{ File.Delete(strFileName);
					}
				catch (Exception objException) 
					{ System.Diagnostics.Debug.WriteLine("Excepción: " + objException.Message);
					}
			// Devuelve el valor que indica si se ha borrado
				return !File.Exists(strFileName);
		}

		/// <summary>
		///		Copia un archivo en un directorio
		/// </summary>
		internal static void CopyFile(string strFileName, string strPath)
		{ if (File.Exists(strFileName) && Directory.Exists(strPath))
				try
					{ File.Copy(strFileName, Path.Combine(strPath, Path.GetFileName(strFileName)));
					}
				catch {}
		}
	}
}
