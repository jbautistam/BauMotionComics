using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Actions
{
	/// <summary>
	///		Acción para un zoom de un elemento
	/// </summary>
	public class ZoomActionModel : ActionBaseModel
	{
		public ZoomActionModel(TimeLineModel objTimeLine, string strTargetKey, 
													 double dblOriginX, double dblOriginY, double dblScaleX, double dblScaleY, bool blnMustAnimate) 
							: base(objTimeLine, strTargetKey, blnMustAnimate) 
		{ OriginX = dblOriginX;
			OriginY = dblOriginY;
			ScaleX = dblScaleX;
			ScaleY = dblScaleY;
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
		///		Escala para el eje X
		/// </summary>
		public double ScaleX { get; }

		/// <summary>
		///		Escala para el eje Y
		/// </summary>
		public double ScaleY { get; }
	}
}
