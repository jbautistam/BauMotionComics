using System;

using Bau.Libraries.LibHelper.Extensors;

namespace BauMotionComics
{
	/// <summary>
	///		Configuración
	/// </summary>
	internal static class BauMotionComicConfiguration
	{	
		/// <summary>
		///		Graba la configuración
		/// </summary>
		internal static void Save()
		{ Properties.Settings.Default.Save();
		}

		/// <summary>
		///		Directorio de cómics que se está utilizando
		/// </summary>
		/// <remarks>
		///		El directorio inicial es el directorio Samples, es decir, si se ejecuta en /bin/Debug, el directorio será ../../../Samples
		/// </remarks>
		internal static string ComicPath
		{ get
				{ // Inicializa el directorio
						if (Properties.Settings.Default.ComicPath.IsEmpty() || 
								!System.IO.Directory.Exists(Properties.Settings.Default.ComicPath))
							{ string strPath = AppDomain.CurrentDomain.BaseDirectory;

									// Le quita el directorio bin y el directorio BauMotionComics
										strPath = System.IO.Path.GetDirectoryName(strPath); 
										strPath = System.IO.Path.GetDirectoryName(strPath); //... le quita el directorio Debug
										strPath = System.IO.Path.GetDirectoryName(strPath); // sin bin
										strPath = System.IO.Path.GetDirectoryName(strPath); // sin BauMotionComics
									// Y asigna el directorio Samples a la configuración
										Properties.Settings.Default.ComicPath = System.IO.Path.Combine(strPath, "Samples");
							}
					// Devuelve el directorio
						return Properties.Settings.Default.ComicPath;
				}
			set { Properties.Settings.Default.ComicPath = value; }
		}
	}
}
