using System;
using System.Collections.ObjectModel;

namespace Bau.Libraries.MVVM.ViewModels.Menus
{
	/// <summary>
	///		Colección de <see cref="MenuGroupViewModel"/>
	/// </summary>
	public class MenuGroupViewModelCollection : ObservableCollection<MenuGroupViewModel>
	{
		/// <summary>
		///		Añade un elemento a la colección
		/// </summary>
		public MenuGroupViewModel Add(string strName, MenuGroupViewModel.TargetMenuType intTargetMenu,
																	MenuGroupViewModel.TargetMainMenuItemType intTargetMenuItem)
		{ MenuGroupViewModel objGroup = new MenuGroupViewModel(strName, intTargetMenu, intTargetMenuItem);
		
				// Añade el grupo
					Add(objGroup);
				// Devuelve el grupo añadido
					return objGroup;
		}

		/// <summary>
		///		Selecciona una serie de menús
		/// </summary>
		public MenuGroupViewModelCollection Select(MenuGroupViewModel.TargetMenuType intTargetMenuType, 
																							 MenuGroupViewModel.TargetMainMenuItemType intTargetMainMenuItemType)
		{ MenuGroupViewModelCollection objColGroups = new MenuGroupViewModelCollection();

				// Añade los grupos seleccionados
					foreach (MenuGroupViewModel objGroup in this)
						if (objGroup.TargetMenu == intTargetMenuType && objGroup.TargetMenuItem == intTargetMainMenuItemType)
							objColGroups.Add(objGroup);
				// Devuelve la colección de grupos
					return objColGroups;
		}
	}
}
