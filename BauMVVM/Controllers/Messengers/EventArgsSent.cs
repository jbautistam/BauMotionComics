using System;

namespace Bau.Libraries.MVVM.Controllers.Messengers
{
	/// <summary>
	///		Argumentos del evento de envío de mensajes
	/// </summary>
	public class EventArgsSent : EventArgs
	{
		public EventArgsSent(Message objMessage)
		{ MessageSent = objMessage;
		}

		/// <summary>
		///		Mensaje enviado
		/// </summary>
		public Message MessageSent { get; private set; }
	}
}
