using System;

namespace Bau.Libraries.MVVM.Controllers.Messengers.Common
{
	/// <summary>
	///		Mensaje de progreso común
	/// </summary>
	public class MessageProgress : Message
	{
		public MessageProgress(string strID, string strSource, string strAction, string strProcess, long lngActual, long lngTotal, object objContent)
								: base(strSource, "PROGRESS", strAction, objContent)
		{ ID = strID;
			Process = strProcess;
			Actual = lngActual;
			Total = lngTotal;
		}

		/// <summary>
		///		ID del progreso
		/// </summary>
		public string ID { get; private set; }

		/// <summary>
		///		Proceso
		/// </summary>
		public string Process { get; private set; }

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