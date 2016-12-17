using System;
using System.Windows.Controls;

namespace Bau.Libraries.MVVM.Views.Layout
{
	/// <summary>
	///		Interface para los controladores de layout
	/// </summary>
	public interface ILayoutController
	{ 
		/// <summary>
		///		Muestra un panel en uno de los paneles de la ventana principal
		/// </summary>
		void ShowDockPane(string strWindowID, LayoutEnums.DockPosition intPosition, string strTitle, UserControl ctlInnerControl, 
											ViewModels.IViewModel objViewModel);

		/// <summary>
		///		Muestra un documento
		/// </summary>
		void ShowDocument(string strWindowID, string strTitle, UserControl objDocument, 
											ViewModels.Forms.Interfaces.IFormViewModel objViewModel = null);

		/// <summary>
		///		Muestra una ventana de edición de código
		/// </summary>
		void ShowCodeEditor(string strFileName, string strTemplate, LayoutEnums.Editor intEditor);

		/// <summary>
		///		Muestra el navegador Web de una URL
		/// </summary>
		void ShowWebBrowser(string strUrl);

		/// <summary>
		///		Muestra una imagen a partir de un nombre de archivo
		/// </summary>
		void ShowImage(string strFileName);

		/// <summary>
		///		Muestra un archivo de texto a partir de un nombre de archivo
		/// </summary>
		void ShowTextFile(string strFileName);

		/// <summary>
		///		Graba todos los documentos
		/// </summary>
		void SaveAllDocuments();
	}
}
