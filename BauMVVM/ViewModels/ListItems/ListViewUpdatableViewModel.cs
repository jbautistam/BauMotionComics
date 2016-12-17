using System;
using System.Linq;

namespace Bau.Libraries.MVVM.ViewModels.ListItems
{
	/// <summary>
	///		ViewModel de un ListView modificable
	/// </summary>
	public abstract class ListViewUpdatableViewModel<TypeData> : ListViewModel<TypeData> where TypeData : IListItemViewModel
	{ // Constantes protegidas
			protected const string cnstStrActionTypeNew = "New";
			protected const string cnstStrActionTypeUpdate = "Update";
			protected const string cnstStrActionTypeDelete = "Delete";
		// Variables privadas
			private bool blnItemsUpdated = false;

		public ListViewUpdatableViewModel(System.Windows.Window frmOwner = null, bool blnChangeUpdated = true) : base(frmOwner, blnChangeUpdated)
		{ NewItemCommand = new BaseCommand("Insertar",
																			 objParameter => ExecuteAction(cnstStrActionTypeNew, objParameter), 
																			 objParameter => CanExecuteAction(cnstStrActionTypeNew, objParameter));
			UpdateItemCommand = new BaseCommand("Modificar",
																				  objParameter => ExecuteAction(cnstStrActionTypeUpdate, objParameter), 
																				  objParameter => CanExecuteAction(cnstStrActionTypeUpdate, objParameter))
																.AddListener(this, "SelectedItem");
			DeleteItemCommand = new BaseCommand("Borrar",
																					objParameter => ExecuteAction(cnstStrActionTypeDelete, objParameter), 
																					objParameter => CanExecuteAction(cnstStrActionTypeDelete, objParameter))
																.AddListener(this, "SelectedItem");
		}

		/// <summary>
		///		Ejecuta una acción
		/// </summary>
		protected override void ExecuteAction(string strAction, object objParameter)
		{ switch (strAction)
				{ case cnstStrActionTypeNew:
							if (NewItem())
								ItemsUpdated = true;
						break;
					case cnstStrActionTypeUpdate:
							if (SelectedItem != null)
								{ if (UpdateItem(SelectedItem))
										ItemsUpdated = true;
								}
						break;
					case cnstStrActionTypeDelete:
							if (SelectedItem != null)
								{ if (DeleteItem(SelectedItem))
										ItemsUpdated = true;
								}
						break;
					default:
							base.ExecuteAction(strAction, objParameter);
						break;
				}
		}

		/// <summary>
		///		Comprueba si se puede ejecutar una acción
		/// </summary>
		protected override bool CanExecuteAction(string strAction, object objParameter)
		{ switch (strAction)
				{ case cnstStrActionTypeNew:
						return true;
					case cnstStrActionTypeUpdate:
					case cnstStrActionTypeDelete:
						return SelectedItem != null;
					default:
						return base.CanExecuteAction(strAction, objParameter);
				}		
		}

		/// <summary>
		///		Crea un nuevo elemento
		/// </summary>
		protected abstract bool NewItem();

		/// <summary>
		///		Modifica el elemento seleccionado. Se le pasa null si es un elemento nuevo
		/// </summary>
		protected abstract bool UpdateItem(TypeData objSelectedItem);
	
		/// <summary>
		///		Borra un elemento
		/// </summary>
		protected abstract bool DeleteItem(TypeData objSelectedItem);

		/// <summary>
		///		Comando de nuevo elemento
		/// </summary>
    public BaseCommand NewItemCommand { get; private set; }

		/// <summary>
		///		Comando de nuevo elemento
		/// </summary>
    public BaseCommand UpdateItemCommand { get; private set; }

		/// <summary>
		///		Comando para borrado de un elemento
		/// </summary>
		public BaseCommand DeleteItemCommand { get; private set; }

		/// <summary>
		///		Indica si los elementos se han modificado (añadido, modificado o eliminado)
		/// </summary>
		public bool ItemsUpdated
		{ get { return blnItemsUpdated; }
			set { base.CheckProperty(ref blnItemsUpdated, value); }
		}
	}
}

