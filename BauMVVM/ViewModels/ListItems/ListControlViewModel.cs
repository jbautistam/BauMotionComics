using System;


namespace Bau.Libraries.MVVM.ViewModels.ListItems
{
	/// <summary>
	///		ViewModel para un ListView
	/// </summary>
	public abstract class ListControlViewModel : Forms.BasePaneViewModel
	{ // Variables privadas
			private IListItemViewModel objSelectedItem;
			private ListViewItemsViewModelCollection objColItems;

		public ListControlViewModel()
		{ // Inicializa la lista de elementos
				Items = new ListViewItemsViewModelCollection();
			// Asigna la ejecución de los comandos a la modificación del elemento seleccionado
				base.PropertyChanged += (objSender, objArgs) =>
																		{ if (objArgs.PropertyName == "SelectedItem")
																				{ base.PropertiesCommand.OnCanExecuteChanged();
																					base.DeleteCommand.OnCanExecuteChanged();
																				}
																		};
		}

		/// <summary>
		///		Carga los elementos de la lista
		/// </summary>
		protected abstract void LoadItems();

		/// <summary>
		///		Actualiza la lista
		/// </summary>
		public void Refresh()
		{ LoadItems();
		}

		/// <summary>
		///		Elemento seleccionado en el control
		/// </summary>
		public IListItemViewModel SelectedItem
		{ get { return objSelectedItem; }
			set
				{ if (!ReferenceEquals(objSelectedItem, value))
						{ objSelectedItem = value;
							OnPropertyChanged();
						}
				}
		}

		/// <summary>
		///		Elementos a mostrar en la lista
		/// </summary>
		public ListViewItemsViewModelCollection Items 
		{ get { return objColItems; }
			set
				{ if (!ReferenceEquals(objColItems, value))
						{ objColItems = value;
							OnPropertyChanged();
						}
				}
		}
	}
}
