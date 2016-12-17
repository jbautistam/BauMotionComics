using System;

namespace Bau.Libraries.MVVM.Controllers
{
	/// <summary>
	///		Interface para los controladores genéricos para las acciones sobre formularios comunes
	/// </summary>
	public interface IControllerWindow
	{
		/// <summary>
		///		Muestra un cuadro de diálogo
		/// </summary>
		ControllerWindow.ResultType ShowDialog(System.Windows.Window frmOwner, System.Windows.Window frmView);

		/// <summary>
		///		Muestra una ventana con un mensaje
		/// </summary>
		void ShowMessage(string strMessage);

		/// <summary>
		///		Muestra una ventana con una pregunta
		/// </summary>
		bool ShowQuestion(string strMessage);

		/// <summary>
		///		Muestra una notificación
		/// </summary>
		void ShowNotification(ControllerWindow.NotificationType intType, string strMessage, string strTitle, string strUrlImage = null);

		/// <summary>
		///		Muestra una pregunta con tres posibles respuestas
		/// </summary>
		ControllerWindow.ResultType ShowQuestionCancel(string strMessage);

		/// <summary>
		///		Muestra un cuadro de diálogo para introducir un texto
		/// </summary>
		ControllerWindow.ResultType ShowInputString(string strMessage, ref string strInput);

		/// <summary>
		///		Abre el cuadro de diálogo de carga
		/// </summary>
		string OpenDialogLoad(string strDefaultPath, string strFilter, string strDefaultFileName = null, string strDefaultExtension = null);

		/// <summary>
		///		Abre el cuadro de diálogo de carga de varios archivos
		/// </summary>
		string [] OpenDialogLoadFilesMultiple(string strDefaultPath, string strFilter, string strDefaultFileName = null, string strDefaultExtension = null);

		/// <summary>
		///		Abre el cuadro de diálogo de grabación
		/// </summary>
		string OpenDialogSave(string strDefaultPath, string strFilter, string strDefaultFileName = null, string strDefaultExtension = null);

		/// <summary>
		///		Abre el cuadro de diálogo para seleccionar un directorio
		/// </summary>
		ControllerWindow.ResultType OpenDialogSelectPath(string strPathSource, out string strPath);

		/// <summary>
		///		Abre un explorador externo
		/// </summary>
		void ShowExternalBrowser(string strUrl);
	}
}
