using System;

using Bau.Libraries.LibHelper.Extensors;

namespace Bau.Libraries.LibMotionComic.Model.Components.PageItems
{
	/// <summary>
	///		Colección de textos
	/// </summary>
	public class TextContentModelCollection : System.Collections.Generic.List<TextContentModel>
	{
		/// <summary>
		///		Obtiene el texto de un idioma
		/// </summary>
		public string GetText(string strLanguage, string strLanguageDefault)
		{ // Obtiene el texto adecuado al idioma
				foreach (TextContentModel objText in this)
					if (objText.Language.EqualsIgnoreCase(strLanguage))
						return objText.Text;
			// Obtiene el texto adecuado al idioma predeterminado
				foreach (TextContentModel objText in this)
					if (objText.Language.EqualsIgnoreCase(strLanguageDefault))
						return objText.Text;
			// Si ha llegado hasta aquí devuelve el primer texto (o nada)
				if (Count > 0)
					return this[0].Text;
				else
					return "";
		}
	}
}
