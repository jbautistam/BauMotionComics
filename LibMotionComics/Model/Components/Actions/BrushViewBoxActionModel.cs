using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Actions
{
	/// <summary>
	///		Acción para animar el ViewBox o ViewPort de una brocha
	/// </summary>
	public class BrushViewBoxActionModel : ActionBaseModel
	{
		public BrushViewBoxActionModel(TimeLineModel objTimeLine, string strTargetKey, 
																	 Entities.RectangleModel rctViewBox, 
																	 Entities.RectangleModel rctViewPort,
																	 PageItems.Brushes.ImageBrushModel.TileType intTileMode,
																	 bool blnMustAnimate) 
							: base(objTimeLine, strTargetKey, blnMustAnimate) 
		{ ViewBox = rctViewBox;
			ViewPort = rctViewPort;
			TileMode = intTileMode;
		}

		/// <summary>
		///		ViewBox
		/// </summary>
		public Entities.RectangleModel ViewBox { get; }

		/// <summary>
		///		ViewPort
		/// </summary>
		public Entities.RectangleModel ViewPort { get; }

		/// <summary>
		///		Tipo de relleno
		/// </summary>
		public PageItems.Brushes.ImageBrushModel.TileType TileMode { get; }
	}
}
