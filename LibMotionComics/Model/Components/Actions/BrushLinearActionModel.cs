using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Actions
{
	/// <summary>
	///		Acción para animar el gradiante lineal
	/// </summary>
	public class BrushLinearActionModel : ActionBaseModel
	{
		public BrushLinearActionModel(TimeLineModel objTimeLine, string strTargetKey, 
																	Entities.PointModel pntStart, 
																	Entities.PointModel pntEnd,
																	PageItems.Brushes.LinearGradientBrushModel.Spread intSpread,
																	bool blnMustAnimate) 
							: base(objTimeLine, strTargetKey, blnMustAnimate) 
		{ Start = pntStart;
			End = pntEnd;
			SpreadMethod = intSpread;
		}

		/// <summary>
		///		Inicio del gradiante
		/// </summary>
		public Entities.PointModel Start { get; }

		/// <summary>
		///		Final
		/// </summary>
		public Entities.PointModel End { get; }

		/// <summary>
		///		Radio X
		/// </summary>
		public PageItems.Brushes.LinearGradientBrushModel.Spread SpreadMethod { get; }
	}
}
