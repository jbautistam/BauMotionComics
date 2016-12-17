using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.PageItems
{
	/// <summary>
	///		Clase con los datos de un color
	/// </summary>
	public class ColorModel
	{
		public ColorModel(string strColor)
		{ Color = strColor;
			Convert(strColor);
		}

		/// <summary>
		///		Convierte una cadena de color
		/// </summary>
		private void Convert(string strColor)
		{ // Inicializa las propiedades
				Alpha = 255;
				Red = 0;
				Green = 0;
				Blue = 0;
			// Quita los códigos adicionales
				while (!string.IsNullOrEmpty(strColor) && strColor.StartsWith("#"))
					strColor = strColor.Substring(1);
			// Convierte la cadena
				if (!string.IsNullOrEmpty(strColor))
					{ // Añade el canal Alpha al color
							if (strColor.Length < 8)
								strColor = "FF" + strColor;
						// Añade la cadena final al color
							while (strColor.Length < 8)
								strColor += "00";
						// Convierte los colores
							Alpha = ConvertByte(strColor.Substring(0, 2));
							Red = ConvertByte(strColor.Substring(2, 2));
							Green = ConvertByte(strColor.Substring(4, 2));
							Blue = ConvertByte(strColor.Substring(6, 2));
					}
		} 

		/// <summary>
		///		Convierte una cadena en un byte
		/// </summary>
		private byte ConvertByte(string strPart)
		{ return byte.Parse(strPart, System.Globalization.NumberStyles.HexNumber);
		}

		/// <summary>
		///		Cadena con el color
		/// </summary>
		public string Color { get; }

		/// <summary>
		///		Canal alpha del color
		/// </summary>
		public byte Alpha { get; private set; }

		/// <summary>
		///		Canal rojo del color
		/// </summary>
		public byte Red { get; private set; }

		/// <summary>
		///		Canal verde del color
		/// </summary>
		public byte Green { get; private set; }

		/// <summary>
		///		Canal azul del color
		/// </summary>
		public byte Blue { get; private set; }

		/// <summary>
		///		Indica si un color está vacío
		/// </summary>
		public bool IsEmpty 
		{ get { return string.IsNullOrWhiteSpace(Color); }
		}
	}
}
