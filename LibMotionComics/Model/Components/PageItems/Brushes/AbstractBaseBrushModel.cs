using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.PageItems.Brushes
{
	/// <summary>
	///		Clase base para los fondos / brochas
	/// </summary>
	public class AbstractBaseBrushModel : AbstractComponentModel
	{
		/// <summary>
		///		Modo de aplicación de los gradiantes
		/// </summary>
		public enum Spread
			{ 
				/// <summary>Los valores del color al final del vector de gradiante rellena el espacio restante</summary>
				Pad,
				/// <summary>El gradiante se repite en dirección contraria hasta que se rellena el espacio</summary>
				Reflect,
				/// <summary>El gradiante se repiten en la dirección original hasta que se rellena el espacio</summary>
				Repeat
			}

		public AbstractBaseBrushModel(string strKey, string strResourceKey) : base(strKey)
		{ ResourceKey = strResourceKey;
		}

		/// <summary>
		///		Clave del recurso
		/// </summary>
		public string ResourceKey { get; }
	}
}
