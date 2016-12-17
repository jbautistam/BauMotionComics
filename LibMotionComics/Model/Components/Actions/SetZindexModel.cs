using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Actions
{
	/// <summary>
	///		Acción para cambiar la visibilidad de un objeto
	/// </summary>
	public class SetZIndexModel : ActionBaseModel
	{
		public SetZIndexModel(TimeLineModel objTimeLine, string strKey, bool blnMustAnimate, int intZIndex) 
										: base(objTimeLine, strKey, blnMustAnimate) 
		{ ZIndex = intZIndex;
		}

		/// <summary>
		///		ZIndex del control
		/// </summary>
		public int ZIndex { get; }
	}
}
