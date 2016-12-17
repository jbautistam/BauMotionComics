using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Actions.Eases
{
	/// <summary>
	///		Función exponencial asociada a una animación
	/// </summary>
	public class ExponentialEaseModel : EaseBaseModel
	{
		public ExponentialEaseModel(ActionBaseModel objAction, Mode intMode, double dblExponent) 
						: base(objAction, intMode) 
		{ Exponent = dblExponent;
		}
		
		/// <summary>
		///		Exponente
		/// </summary>
		public double Exponent { get; }
	}
}
