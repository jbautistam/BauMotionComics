using System;

namespace Bau.Libraries.MVVM.Controllers.Messengers
{
	/// <summary>
	///		Controlador de mensajes
	/// </summary>
	public class MessengerController
	{	// Eventos públicos
			public event EventHandler<EventArgsSent> Sent;

		/// <summary>
		///		Envía un mensaje a un receptor
		/// </summary>
		public void Send(Message objMessage)
		{ if (Sent != null)
				Sent(this, new EventArgsSent(objMessage));			
		}

		/// <summary>
		///		Envía un mensaje a un receptor
		/// </summary>
		public void Send(string strSource, string strAction, string strMessageType, object objContent)
		{ Send(new Message(strSource, strMessageType, strAction, objContent));
		}

		/// <summary>
		///		Lanza un mensaje de log a los receptores
		/// </summary>
		public void SendLog(string strSource, Common.MessageLog.LogType intLogType, string strMessage, string strDescription, object objContent)
		{ Send(new Common.MessageLog(strSource, intLogType, strMessage, strDescription, objContent));
		}

		/// <summary>
		///		Lanza un mensaje de barra de progreso a los receptores
		/// </summary>
		public void SendBarProgress(string strSource, string strMessage, long lngActual, long lngTotal, object objContent)
		{ Send(new Common.MessageBarProgress(strSource, strMessage, lngActual, lngTotal, objContent));
		}

		/// <summary>
		///		Lanza un mensaje de progreso a los receptores
		/// </summary>
		public void SendProgress(string strID, string strSource, string strAction, string strProcess, long lngActual, long lngTotal, object objContent)
		{ Send(new Common.MessageProgress(strID, strSource, strAction, strProcess, lngActual, lngTotal, objContent));
		}

		/// <summary>
		///		Lanza un mensaje con parámetros a los receptores
		/// </summary>
		public void SendParameters(string strSource, string strMessageType, string strAction, Settings.ParametersModelCollection objColParameters, object objContent)
		{ Send(new Common.MessageParameters(strSource, strMessageType, strAction, objColParameters, objContent));
		}

		/// <summary>
		///		Lanza un mensaje de error a los receptores
		/// </summary>
		public void SendError(string strSource, string strFileName, string strError, int intRow, int intColumn, object objContent) 
		{ Send(new Common.MessageError(strSource, strFileName, strError, intRow, intColumn, objContent));
		}

		/// <summary>
		///		Envía un mensaje de último archivo abierto
		/// </summary>
		public void SendRecentFileOpened(string strSource, string strFileName, string strText)
		{ Send(new Common.MessageRecentFileUsed(strSource, Common.MessageRecentFileUsed.ActionType.Open, strFileName, strText));
		}

		/// <summary>
		///		Envía un mensaje de Click sobre uno de los últimos archivos abiertos
		/// </summary>
		public void SendRecentFileClicked(string strSource, string strFileName)
		{ Send(new Common.MessageRecentFileUsed(strSource, Common.MessageRecentFileUsed.ActionType.Clicked, strFileName, null));
		}
	}
}
