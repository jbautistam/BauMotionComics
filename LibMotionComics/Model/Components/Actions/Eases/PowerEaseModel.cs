using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Actions.Eases
{
	/// <summary>
	///		Función potencia asociada a una animación
	/// </summary>
	public class PowerEaseModel : EaseBaseModel
	{
		public PowerEaseModel(ActionBaseModel objAction, Mode intMode, double dblPower) 
						: base(objAction, intMode) 
		{ Power = dblPower;
		}

		/// <summary>
		///		Potencia
		/// </summary>
		public double Power { get; }
	}
}
