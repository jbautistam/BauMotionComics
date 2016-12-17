using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Transforms
{
	/// <summary>
	///		Transformación de escala
	/// </summary>
	public class ScaleTransformModel : AbstractTransformModel
	{
		public ScaleTransformModel(double dblScaleX, double dblScaleY, Entities.PointModel pntCenter)
		{ ScaleX = dblScaleX;
			ScaleY = dblScaleY;
			Center = pntCenter;
		}

		/// <summary>
		///		Escala sobre el eje X
		/// </summary>
		public double ScaleX { get; }

		/// <summary>
		///		Escala sobre el eje Y
		/// </summary>
		public double ScaleY { get; }

		/// <summary>
		///		Centro del escalado
		/// </summary>
		public Entities.PointModel Center { get; }
	}
}
