using System;

namespace Bau.Libraries.MVVM.ViewModels.Menus
{
	/// <summary>
	///		Clase con los datos de los menús y barras de herramientas
	/// </summary>
	public class MenuComposition
	{
		public MenuComposition()
		{ Menus = new MenuGroupViewModelCollection();
			ToolBars = new MenuGroupViewModelCollection();
		}

		/// <summary>
		///		Menús
		/// </summary>
		public MenuGroupViewModelCollection Menus { get; private set; }

		/// <summary>
		///		Barras de herramientas
		/// </summary>
		public MenuGroupViewModelCollection ToolBars { get; private set; }
	}
}
