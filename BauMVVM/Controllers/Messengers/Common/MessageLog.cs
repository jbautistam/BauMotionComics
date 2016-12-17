using System;

namespace Bau.Libraries.MVVM.Controllers.Messengers.Common
{
	/// <summary>
	///		Mensaje de log común
	/// </summary>
	public class MessageLog : Message
	{ // Enumerados públicos
			/// <summary>
			///		Tipo de log
			/// </summary>
			public enum LogType
				{ 
					/// <summary>Mensaje informativo</summary>
					Information,
					/// <summary>Mensaje de advertencia</summary>
					Warning,
					/// <summary>Mensaje de error</summary>
					Error
				}

		public MessageLog(string strSource, LogType intLogType, string strMessage, string strDescription, object objContent) 
							: base(strSource, "LOG", intLogType.ToString(), objContent)
		{ Type = intLogType;
			Message = strMessage;
			Description = strDescription;
		}

		/// <summary>
		///		Tipo de log
		/// </summary>
		public LogType Type { get; private set; }

		/// <summary>
		///		Mensaje
		/// </summary>
		public string Message { get; private set; }

		/// <summary>
		///		Descripción
		/// </summary>
		public string Description { get; private set; }
	}
}
