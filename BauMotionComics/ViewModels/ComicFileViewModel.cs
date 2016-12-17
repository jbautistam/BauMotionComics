using System;

namespace BauMotionComics.ViewModels
{
	/// <summary>
	///		Modelo con los datos de un archivo de cómic
	/// </summary>
	public class ComicFileViewModel : Bau.Libraries.MVVM.ViewModels.BaseViewModel
	{ // Variables privadas
			private string strPath, strFileName, strThumbFileName;
			private string strTitle, strSummary;

		public ComicFileViewModel(string strPath, string strFileName)
		{ Path = strPath;
			FileName = strFileName;
		}

		/// <summary>
		///		Directorio
		/// </summary>
		public string Path 
		{ get { return strPath; }
			set { CheckProperty(ref strPath, value); }
		}

		/// <summary>
		///		Archivo
		/// </summary>
		public string FileName
		{ get { return strFileName; }
			set { CheckProperty(ref strFileName, value); }
		}

		/// <summary>
		///		Título del cómic
		/// </summary>
		public string Title
		{ get { return strTitle; }
			set { CheckProperty(ref strTitle, value); }
		}

		/// <summary>
		///		Resumen del cómic
		/// </summary>
		public string Summary
		{ get { return strSummary; }
			set { CheckProperty(ref strSummary, value); }
		}

		/// <summary>
		///		Nombre del archivo de thumb
		/// </summary>
		public string ThumbFileName
		{ get { return strThumbFileName; }
			set { CheckProperty(ref strThumbFileName, value); }
		}
	}
}
