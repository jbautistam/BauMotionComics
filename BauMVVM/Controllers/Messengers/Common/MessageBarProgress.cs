using System;

namespace Bau.Libraries.MVVM.Controllers.Messengers.Common
{
	/// <summary>
	///		Mensaje de progreso común
	/// </summary>
	public class MessageBarProgress : Message
	{
		public MessageBarProgress(string strSource, string strMessage, long lngActual, long lngTotal, object objContent)
								: base(strSource, "BARPROGRESS", "BARPROGRESS", objContent)
		{ Message = strMessage;
			Actual = lngActual;
			Total = lngTotal;
		}

		/// <summary>
		///		Mensaje de progreso
		/// </summary>
		public string Message { get; private set; }

		/// <summary>
		///		Valor actual
		/// </summary>
		public long Actual { get; private set; }

		/// <summary>
		///		Valor total
		/// </summary>
		public long Total { get; private set; }
	}
}