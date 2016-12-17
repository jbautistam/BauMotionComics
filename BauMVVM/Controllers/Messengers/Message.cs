using System;

namespace Bau.Libraries.MVVM.Controllers.Messengers
{
	/// <summary>
	///		Clase para envío de datos entre ViewModel
	/// </summary>
	public class Message
	{
		public Message(string strSource, string strMessageType, string strAction, object objContent)
		{ Source = strSource;
			MessageType = strMessageType;
			Action = strAction;
			Content = objContent;
			DateNew = DateTime.Now;
		}

		/// <summary>
		///		Fuente del mensaje
		/// </summary>
		public string Source { get; private set; }

		/// <summary>
		///		Identificador del mensaje
		/// </summary>
		public string MessageType { get;  private set; }

		/// <summary>
		///		Acción asociada al mensaje
		/// </summary>
		public string Action { get; private set; }

		/// <summary>
		///		Contenido del mensaje
		/// </summary>
		public object Content { get; private set; }

		/// <summary>
		///		Fecha de alta
		/// </summary>
		public DateTime DateNew { get; private set; }
	}
}
