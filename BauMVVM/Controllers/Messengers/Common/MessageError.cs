using System;

namespace Bau.Libraries.MVVM.Controllers.Messengers.Common
{
	/// <summary>
	///		Mensaje de error común
	/// </summary>
	public class MessageError : Message
	{ 
		public MessageError(string strSource, string strFileName, string strError, int intRow, int intColumn, object objContent) 
							: base(strSource, "ERROR", "ERROR", objContent)
		{ FileName = strFileName;
			Error = strError;
			Row = intRow;
			Column = intColumn;
		}

		/// <summary>
		///		Nombre de archivo
		/// </summary>
		public string FileName { get; private set; }

		/// <summary>
		///		Error
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		///		Fila
		/// </summary>
		public int Row { get; private set; }

		/// <summary>
		///		Columna
		/// </summary>
		public int Column { get; private set; }
	}
}
