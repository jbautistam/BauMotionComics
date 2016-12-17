using System;
using System.Collections.ObjectModel;

namespace Bau.Libraries.MVVM.ViewModels.TreeItems
{
	/// <summary>
	///		Colección de <see cref="TreeViewItemViewModel"/>
	/// </summary>
	public class TreeViewItemsViewModelCollection : ObservableCollection<ITreeViewItemViewModel>
	{
		/// <summary>
		///		Añade una serie de elementos
		/// </summary>
		public void AddRange(ObservableCollection<ITreeViewItemViewModel> objColItems)
		{ foreach (ITreeViewItemViewModel objItem in objColItems)
				Add(objItem);
		}

		/// <summary>
		///		Depuración
		/// </summary>
		public void Debug()
		{ Debug(this, 0);
		}

		/// <summary>
		///		Depuración
		/// </summary>
		private void Debug(ObservableCollection<ITreeViewItemViewModel> objColNodes, int intIndent)
		{ foreach (TreeViewItemViewModel objNode in objColNodes)
				{ // Indenta la cadena
						for (int intIndex = 0; intIndex < intIndent; intIndex++)
							System.Diagnostics.Debug.Write("  ");
					// Escribe el elemento
						System.Diagnostics.Debug.WriteLine(objNode.Text);
					// Escribe los hijos
						if (objNode.Children != null && objNode.Children.Count > 0)
							Debug(objNode.Children, intIndent + 1);
				}
		}
	}
}
