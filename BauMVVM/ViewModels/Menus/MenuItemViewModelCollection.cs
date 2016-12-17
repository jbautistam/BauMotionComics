using System;
using System.Collections.ObjectModel;

namespace Bau.Libraries.MVVM.ViewModels.Menus
{
	/// <summary>
	///		Colección de <see cref="MenuItemViewModel"/>
	/// </summary>
	public class MenuItemViewModelCollection : ObservableCollection<MenuItemViewModel>
	{
		/// <summary>
		///		Añade un separador
		/// </summary>
		public void AddSeparator()
		{ Add(new MenuItemViewModel(null, null, null));
		}

		/// <summary>
		///		Añade un elemento a la colección
		/// </summary>
		public void Add(string strText, string strIcon = null, BaseCommand objCommand = null)
		{ Add(new MenuItemViewModel(strText, strIcon, objCommand));
		}
	}
}
