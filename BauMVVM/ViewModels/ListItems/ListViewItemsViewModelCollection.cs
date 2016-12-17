using System;
using System.Collections.ObjectModel;

namespace Bau.Libraries.MVVM.ViewModels.ListItems
{
	/// <summary>
	///		Colección de elementos de un ListView
	/// </summary>
	public class ListViewItemsViewModelCollection : ObservableCollection<IListItemViewModel>
	{
		/// <summary>
		///		Añade una serie de elementos
		/// </summary>
		public void AddRange(ObservableCollection<IListItemViewModel> objColItems)
		{ foreach (IListItemViewModel objItem in objColItems)
				Add(objItem);
		}
	}
}
