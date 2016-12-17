using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Bau.Libraries.MVVM.ViewModels.TreeItems
{
	/// <summary>
	///		Interface para los ViewModel de los elementos de un árbol
	/// </summary>
	public interface ITreeViewItemViewModel : INotifyPropertyChanged
	{
		/// <summary>
		///		Carga los nodos hijo
		/// </summary>
		void LoadChildren();

		/// <summary>
		///		Identificador del nodo en el árbol
		/// </summary>
		string NodeID { get; set; }

		/// <summary>
		///		Título del nodo del árbol
		/// </summary>
		string Text { get; set; }

		/// <summary>
		///		Indica si es un nodo abierto
		/// </summary>
    bool IsExpanded { get; set; }

		/// <summary>
		///		Indica si es un nodo seleccionado
		/// </summary>
    bool IsSelected { get; set; }

		/// <summary>
		///		Elemento padre
		/// </summary>
    ITreeViewItemViewModel Parent { get; }

		/// <summary>
		///		Nodos hijo
		/// </summary>
    TreeViewItemsViewModelCollection Children { get; }
	}
}
