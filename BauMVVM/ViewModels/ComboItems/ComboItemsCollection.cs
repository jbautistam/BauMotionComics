using System;
using System.Collections.ObjectModel;

namespace Bau.Libraries.MVVM.ViewModels.ComboItems
{
	/// <summary>
	///		Colección observable de <see cref="ComboItem"/>
	/// </summary>
	public class ComboItemsCollection : ObservableCollection<ComboItem>
	{
		/// <summary>
		///		Añade un elemento
		/// </summary>
		public void Add(int? intID, string strText, object objTag = null)
		{ Add(new ComboItem(intID, strText, objTag));
		}
	}
}
