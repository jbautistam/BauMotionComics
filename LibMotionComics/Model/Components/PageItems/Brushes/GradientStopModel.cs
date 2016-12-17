using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.PageItems.Brushes
{
	/// <summary>
	///		Punto de parada de un gradiante
	/// </summary>
	public class GradientStopModel
	{
		public GradientStopModel(ColorModel clrColor, double dblOffset)
		{ Color = clrColor;
			Offset = dblOffset;
		}

		/// <summary>
		///		Color
		/// </summary>
		public ColorModel Color { get; }

		/// <summary>
		///		Desplazamiento del punto de parada
		/// </summary>
		public double Offset { get; }
	}
}
