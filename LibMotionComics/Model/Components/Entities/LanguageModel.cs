using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Entities
{
	/// <summary>
	///		Modelo con los datos de un idioma
	/// </summary>
	public class LanguageModel : AbstractComponentModel
	{
		public LanguageModel(string strKey, string strName, bool blnIsDefault) : base(strKey)
		{ Name = strName;
			IsDefault = blnIsDefault;
		}

		/// <summary>
		///		Nombre del idioma
		/// </summary>
		public string Name { get; }

		/// <summary>
		///		Indica si es el predeterminado
		/// </summary>
		public bool IsDefault { get; set; }
	}
}
