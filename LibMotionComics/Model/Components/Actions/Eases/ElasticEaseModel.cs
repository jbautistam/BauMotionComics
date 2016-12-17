using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Actions.Eases
{
	/// <summary>
	///		Función elástica asociada a una animación
	/// </summary>
	public class ElasticEaseModel : EaseBaseModel
	{
		public ElasticEaseModel(ActionBaseModel objAction, Mode intMode, int intOscillations, 
														double dblSpringiness) 
						: base(objAction, intMode) 
		{ Oscillations = intOscillations;
			Springiness = dblSpringiness;
		}

		/// <summary>
		///		Número de oscilaciones
		/// </summary>
		public int Oscillations { get; }

		/// <summary>
		///		Escala de elasticidad
		/// </summary>
		public double Springiness { get; }
	}
}
