using System;

namespace Bau.Libraries.MVVM.Views.Layout
{
	/// <summary>
	///		Enumerados para el layout de ventanas
	/// </summary>
	public class LayoutEnums
	{ // Enumerados públicos
			/// <summary>
			///		Posición en la que se puede colocar una ventana
			/// </summary>
			public enum DockPosition
				{ 
					/// <summary>Izquierda</summary>
					Left,
					/// <summary>Superior</summary>
					Top,
					/// <summary>Derecha</summary>
					Right,
					/// <summary>Abajo</summary>
					Bottomm
				}
			/// <summary>
			///		Tipos de editores
			/// </summary>
			public enum Editor
				{ 
					/// <summary>Desconocido / texto</summary>
					Unknown,
					/// <summary>Editor de XML</summary>
					Xml,
					/// <summary>Editor de HTML</summary>
					Html,
					/// <summary>Otros editores</summary>
					Other
				}
	}
}
