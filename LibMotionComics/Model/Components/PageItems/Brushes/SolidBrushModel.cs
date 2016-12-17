using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.PageItems.Brushes
{
	/// <summary>
	///		Fondo sólido
	/// </summary>
	public class SolidBrushModel : AbstractBaseBrushModel
	{
		public SolidBrushModel(string strKey, string strResourceKey, string strColor) : base(strKey, strResourceKey)
		{ Color = new ColorModel(strColor);
		}

		/// <summary>
		///		Color del fondo
		/// </summary>
		public ColorModel Color { get; }
	}
}
