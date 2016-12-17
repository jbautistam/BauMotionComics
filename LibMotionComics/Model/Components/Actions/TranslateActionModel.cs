using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Actions
{
	/// <summary>
	///		Acción para mover un elemento
	/// </summary>
	public class TranslateActionModel : ActionBaseModel
	{
		public TranslateActionModel(TimeLineModel objTimeLine, string strTargetKey, 
																double? dblTop, double? dblLeft, bool blnMustAnimate) 
							: base(objTimeLine, strTargetKey, blnMustAnimate) 
		{ Top = dblTop;
			Left = dblLeft;
		}

		/// <summary>
		///		Posición superior
		/// </summary>
		public double? Top { get; set; }

		/// <summary>
		///		Posición izquierda
		/// </summary>
		public double? Left { get; set; }
	}
}
