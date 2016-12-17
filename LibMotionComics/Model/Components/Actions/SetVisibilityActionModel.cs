using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Actions
{
	/// <summary>
	///		Acción para cambiar la visibilidad de un objeto
	/// </summary>
	public class SetVisibilityActionModel : ActionBaseModel
	{
		public SetVisibilityActionModel(TimeLineModel objTimeLine, string strKey, bool blnVisible, double? dblOpacity, bool blnMustAnimate) 
										: base(objTimeLine, strKey, blnMustAnimate) 
		{ if (dblOpacity == null)
				{ if (blnVisible)
						Opacity = 1;
					else
						Opacity = 0;
				}
			else
				Opacity = dblOpacity ?? 1;
		}

		/// <summary>
		///		Opacidad del objeto
		/// </summary>
		public double Opacity { get; }
	}
}
