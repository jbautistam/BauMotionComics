using System;
using System.Linq;

using Bau.Libraries.LibHelper.Extensors;

namespace Bau.Libraries.MVVM.ViewModels.ListItems
{
	/// <summary>
	///		ViewModel para un ListView
	/// </summary>
	public class ListViewModel<TypeData> : BaseViewModel where TypeData : IListItemViewModel
	{ // Constantes protegidas
			protected const string cnstStrActionTypeRefresh = "Refresh";
		// Variables privadas
			private TypeData objSelectedItem = default(TypeData);

		public ListViewModel(System.Windows.Window frmOwner = null, bool blnChangeUpdated = true) : base(frmOwner, blnChangeUpdated)
		{ RefreshCommand = new BaseCommand("Actualizar",
																			 objParameter => ExecuteAction(cnstStrActionTypeRefresh, objParameter), 
																			 objParameter => CanExecuteAction(cnstStrActionTypeRefresh, objParameter));
		}

		/// <summary>
		///		Añade un elemento a la colección. Aprovecha para asignar los manejadores de eventos
		/// </summary>
		public void Add(TypeData objListItem)
		{ // Asigna el manejador de eventos para "SelectedItem"
				objListItem.PropertyChanged += (objSender, objEvntArgs) => 
																					{ if (objEvntArgs.PropertyName.EqualsIgnoreCase("IsSelected"))
																							SelectedItem = objListItem;
																					};
			// Llama al método base
				ListItems.Add(objListItem);
		}

		/// <summary>
		///		Ejecuta una acción
		/// </summary>
		protected virtual void ExecuteAction(string strAction, object objParameter)
		{ switch (strAction)
				{ case cnstStrActionTypeRefresh:
							Refresh();
						break;
				}
		}

		/// <summary>
		///		Comprueba si se puede ejecutar una acción
		/// </summary>
		protected virtual bool CanExecuteAction(string strAction, object objParameter)
		{ switch (strAction)
				{ default:
						return true;
				}		
		}

		/// <summary>
		///		Actualiza los elementos --> El raíz no hace nada
		/// </summary>
		protected virtual void Refresh()
		{ 
		}

		/// <summary>
		///		Obtiene el elemento seleccionado
		/// </summary>
		public TypeData SelectedItem
		{ get 
				{ if (objSelectedItem != null)
						return objSelectedItem;
					else
						return ListItems.FirstOrDefault<TypeData>(objItem => objItem.IsSelected); 
				}
			set 
				{ if (!ReferenceEquals(objSelectedItem, value))
						{ objSelectedItem = value;
							OnPropertyChanged();
						}
				}
		}

		/// <summary>
		///		Elementos de la lista
		/// </summary>
		public System.Collections.ObjectModel.ObservableCollection<TypeData> ListItems { get; private set; } = new System.Collections.ObjectModel.ObservableCollection<TypeData>();

		/// <summary>
		///		Comando para actualización
		/// </summary>
		public BaseCommand RefreshCommand { get; private set; }
	}
}