using System;
using System.ComponentModel;

namespace Bau.Libraries.MVVM.ViewModels.ListItems
{
	/// <summary>
	///		Interface para los ViewModel de los elementos de una lista
	/// </summary>
	public interface IListItemViewModel : INotifyPropertyChanged
	{
		/// <summary>
		///		Identificador del elemento de la lista
		/// </summary>
		string ID { get; set; }

		/// <summary>
		///		Título del elemento de la lista
		/// </summary>
		string Text { get; set; }

		/// <summary>
		///		Indica si el elemento está chequeado
		/// </summary>
    bool IsChecked { get; set; }

		/// <summary>
		///		Indica si el elemento está seleccionado
		/// </summary>
    bool IsSelected { get; set; }

		/// <summary>
		///		Objeto del elemento de la lista
		/// </summary>
    object Tag { get; set; }
	}
}
