using System;

namespace Bau.Libraries.MVVM.Controllers.Messengers.Common
{
	/// <summary>
	///		Mensaje con un diccionario de parámetros
	/// </summary>
	public class MessageParameters : Message
	{
		public MessageParameters(string strSource, string strMessageType, string strAction, Settings.ParametersModelCollection objColParameters, object objContent)
																	: base(strSource, strMessageType, strAction, objContent)
		{ Parameters = objColParameters;
		}

		/// <summary>
		///		Parámetros del mensaje
		/// </summary>
		public Settings.ParametersModelCollection Parameters { get; private set; }
	}
}
