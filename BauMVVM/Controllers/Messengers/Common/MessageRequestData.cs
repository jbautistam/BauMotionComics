using System;

namespace Bau.Libraries.MVVM.Controllers.Messengers.Common
{
	/// <summary>
	///		Mensaje de solicitud de datos
	/// </summary>
	public class MessageRequestData<TypeData> : Message
	{
		public MessageRequestData(string strSource, string strContentRequiredType, string strContentGlobalID = null)
								: base(strSource, "REQUESTDATA", "REQUESTDATA", null)
		{ ContentRequiredType = strContentRequiredType;
			ContentGlobalID = strContentGlobalID;
		}

		/// <summary>
		///		Tipo de contenido solicitado
		/// </summary>
		public string ContentRequiredType { get; private set; }

		/// <summary>
		///		Global ID del contenido solicitado (puede ser null si se desean todos los datos)
		/// </summary>
		public string ContentGlobalID { get; private set; }

		/// <summary>
		///		Resultado de la solicitud
		/// </summary>
		public System.Collections.Generic.List<TypeData> Result { get; } = new System.Collections.Generic.List<TypeData>();
	}
}