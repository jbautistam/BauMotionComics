using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Actions
{
	/// <summary>
	///		Acción para rotar un elemento
	/// </summary>
	public class RotateActionModel : ActionBaseModel
	{
		public RotateActionModel(TimeLineModel objTimeLine, string strTargetKey, 
														 double dblOriginX, double dblOriginY, double dblAngle, bool blnMustAnimate) 
							: base(objTimeLine, strTargetKey, blnMustAnimate) 
		{ OriginX = dblOriginX;
			OriginY = dblOriginY;
			Angle = dblAngle;
		}

		/// <summary>
		///		Punto de origen de la rotación (X)
		/// </summary>
		public double OriginX { get; }

		/// <summary>
		///		Punto de origen de la rotación (Y)
		/// </summary>
		public double OriginY { get; }

		/// <summary>
		///		Angulo de rotación
		/// </summary>
		public double Angle { get; }
	}
}
