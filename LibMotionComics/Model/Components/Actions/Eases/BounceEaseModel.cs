using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Actions.Eases
{
	/// <summary>
	///		Rebote de una animación
	/// </summary>
	public class BounceEaseModel : EaseBaseModel
	{
		public BounceEaseModel(ActionBaseModel objAction, Mode intMode, int intBounces, double dblBounciness) 
						: base(objAction, intMode) 
		{ Bounces = intBounces;
			Bounciness = dblBounciness;
		}

		/// <summary>
		///		Número de rebotes
		/// </summary>
		public int Bounces { get; }

		/// <summary>
		///		Escala de amplitud del siguiente rebote
		/// </summary>
		public double Bounciness { get; }
	}
}
