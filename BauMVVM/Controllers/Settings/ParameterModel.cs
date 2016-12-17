using System;

namespace Bau.Libraries.MVVM.Controllers.Settings
{
	/// <summary>
	///		Clase con los datos de un parámetro
	/// </summary>
	public class ParameterModel
	{
		public ParameterModel(string strApplication, string strName, string strValue)
		{ Application = strApplication;
			Name = strName;
			Value = strValue;
		}

		/// <summary>
		///		Nombre de aplicación
		/// </summary>
		public string Application { get; private set; }

		/// <summary>
		///		Nombre del parámetro
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		///		Valor del parámetro
		/// </summary>
		public string Value { get; set; }
	}
}
