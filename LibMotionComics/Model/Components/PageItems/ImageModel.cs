using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.PageItems
{
	/// <summary>
	///		Clase con los datos de una imagen
	/// </summary>
	public class ImageModel : AbstractPageItemModel
	{
		public ImageModel(PageModel objPage, string strKey) : base(objPage, strKey)
		{
		}

		/// <summary>
		///		Clave de la imagen
		/// </summary>
		public string ResourceKey { get; set; }

		/// <summary>
		///		Nombre del archivo
		/// </summary>
		public string FileName { get; set; }
	}
}
