using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Actions
{
	/// <summary>
	///		Acción para animar un gradiante circular
	/// </summary>
	public class BrushRadialActionModel : ActionBaseModel
	{
		public BrushRadialActionModel(TimeLineModel objTimeLine, string strTargetKey, 
																	 Entities.PointModel pntCenter, 
																	 Entities.PointModel pntOrigin,
																	 double? dblRadiusX, double? dblRadiusY,
																	 bool blnMustAnimate) 
							: base(objTimeLine, strTargetKey, blnMustAnimate) 
		{ Center = pntCenter;
			Origin = pntOrigin;
			RadiusX = dblRadiusX;
			RadiusY = dblRadiusY;
		}

		/// <summary>
		///		Centro
		/// </summary>
		public Entities.PointModel Center { get; }

		/// <summary>
		///		Origen
		/// </summary>
		public Entities.PointModel Origin { get; }

		/// <summary>
		///		Radio X
		/// </summary>
		public double? RadiusX { get; }

		/// <summary>
		///		Radio Y
		/// </summary>
		public double? RadiusY { get; }
	}
}
