using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Transforms
{
	/// <summary>
	///		Transformación de traslación
	/// </summary>
	public class TranslateTransformModel : AbstractTransformModel
	{
		public TranslateTransformModel(double dblTop = 0, double dblLeft = 0)
		{ Top = dblTop;
			Left = dblLeft;
		}

		/// <summary>
		///		Punto superior
		/// </summary>
		public double Top { get; set; }

		/// <summary>
		///		Punto izquierdo
		/// </summary>
		public double Left { get; set; }
	}
}
