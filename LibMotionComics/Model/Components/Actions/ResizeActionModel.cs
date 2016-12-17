using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Actions
{
	/// <summary>
	///		Acción para cambiar el tamaño de un elemento
	/// </summary>
	public class ResizeActionModel : ActionBaseModel
	{
		public ResizeActionModel(TimeLineModel objTimeLine, string strTargetKey, 
														 double? dblWidth, double? dblHeight, bool blnMustAnimate) 
							: base(objTimeLine, strTargetKey, blnMustAnimate) 
		{ Width = dblWidth;
			Height = dblHeight;
		}

		/// <summary>
		///		Ancho
		/// </summary>
		public double? Width { get; }

		/// <summary>
		///		Alto
		/// </summary>
		public double? Height { get; }
	}
}
