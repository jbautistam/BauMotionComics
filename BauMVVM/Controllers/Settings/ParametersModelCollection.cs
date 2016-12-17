using System;
using System.Collections.Generic;
using System.Linq;

using Bau.Libraries.LibHelper.Extensors;

namespace Bau.Libraries.MVVM.Controllers.Settings
{
	/// <summary>
	///		Colección de <see cref="ParameterModel"/>
	/// </summary>
	public class ParametersModelCollection : List<ParameterModel>
	{
		/// <summary>
		///		Añade / modifica el valor de un parámetro
		/// </summary>
		public void Add(string strApplication, string strName, string strValue)
		{ ParameterModel objParameter = Search(strApplication, strName);

				// Si no existe el parámetro, lo añade, si existe cambia su valor
					if (objParameter == null)
						{ // Crea el parámetro
								objParameter = new ParameterModel(strApplication, strName, strValue);
							// Lo añade a la colección
								Add(objParameter);
						}
					else
						objParameter.Value = strValue;
		}

		/// <summary>
		///		Busca un parámetro en la colección
		/// </summary>
		public ParameterModel Search(string strApplication, string strName)
		{ return this.FirstOrDefault<ParameterModel>(objParameter => objParameter.Application.EqualsIgnoreCase(strApplication) &&
																																 objParameter.Name.EqualsIgnoreCase(strName));
		}

		/// <summary>
		///		Obtiene el valor de un parámetro
		/// </summary>
		public string GetValue(string strApplication, string strName)
		{ ParameterModel objParameter = Search(strApplication, strName);

				if (objParameter == null)
					return "";
				else
					return objParameter.Value;
		}

		/// <summary>
		///		Asigna un valor a un parámetro
		/// </summary>
		public void SetValue(string strApplication, string strName, string strValue)
		{ Add(strApplication, strName, strValue);
		}

		/// <summary>
		///		Asigna un valor a un parámetro
		/// </summary>
		public void SetValue(string strApplication, string strName, double dblValue)
		{ SetValue(strApplication, strName, dblValue.ToString().Replace(',', '.'));
		}
	}
}
