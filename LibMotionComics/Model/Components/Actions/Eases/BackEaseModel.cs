using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Actions.Eases
{
	/// <summary>
	///		Vuelve una animación hacia atrás
	/// </summary>
	public class BackEaseModel : EaseBaseModel
	{
		public BackEaseModel(ActionBaseModel objAction, Mode intMode, double dblAmplitude) 
						: base(objAction, intMode) 
		{ Amplitude = dblAmplitude;
		}

		/// <summary>
		///		Amplitud
		/// </summary>
		public double Amplitude { get; }
	}
}
