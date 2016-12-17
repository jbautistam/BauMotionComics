using System;


namespace Bau.Libraries.MVVM.Controllers.Messengers.Common
{
	/// <summary>
	///		Mensaje para dar comunicación de archivos
	/// </summary>
	public class MessageRecentFileUsed : Message
	{
		/// <summary>
		///		Tipo de acción
		/// </summary>
		public enum ActionType
			{
				/// <summary>Indica que se ha abierto el archivo</summary>
				Open,
				/// <summary>Indica que se ha pulsado sobre el archivo</summary>
				Clicked
			}
		public MessageRecentFileUsed(string strSource, ActionType intAction, string strFileName, string strText) 
							: base(strSource, "MRU", intAction.ToString(), null)
		{ Type = intAction;
			FileName = strFileName;
			Text = strText;
		}

		/// <summary>
		///		Tipo de acción
		/// </summary>
		public ActionType Type { get; private set; }

		/// <summary>
		///		Nombre de archivo
		/// </summary>
		public string FileName { get; private set; }

		/// <summary>
		///		Texto a mostrar en el menú de archivos recientes
		/// </summary>
		public string Text { get; private set; }
	}
}
