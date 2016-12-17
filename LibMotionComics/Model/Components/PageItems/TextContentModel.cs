using System;

using Bau.Libraries.LibHelper.Extensors;

namespace Bau.Libraries.LibMotionComic.Model.Components.PageItems
{
	/// <summary>
	///		Contenido del texto
	/// </summary>
	public class TextContentModel
	{
		public TextContentModel(string strLanguage, string strText)
		{ Language = strLanguage;
			Text = Normalize(strText);
		}

		/// <summary>
		///		Normaliza el texto
		/// </summary>
		private string Normalize(string strText)
		{ // Normaliza el texto
				if (!strText.IsEmpty())
					{ // Quita los caracteres de espacio
							strText = strText.ReplaceWithStringComparison("\r\n", " ");
							strText = strText.ReplaceWithStringComparison("\r", " ");
							strText = strText.ReplaceWithStringComparison("\n", " ");
							strText = strText.ReplaceWithStringComparison("\t", " ");
							strText = strText.TrimIgnoreNull();
						// Quita los espacios dobles
							while (!strText.IsEmpty() && strText.IndexOf("  ") >= 0)
								strText = strText.ReplaceWithStringComparison("  ", " ");
					}
			// Devuelve el texto
				return strText;
		}

		/// <summary>
		///		Clave del idioma
		/// </summary>
		public string	Language { get; }

		/// <summary>
		///		Texto
		/// </summary>
		public string Text { get; }
	}
}
