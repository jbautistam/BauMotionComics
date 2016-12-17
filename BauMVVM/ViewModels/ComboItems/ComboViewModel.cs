using System;

using Bau.Libraries.LibHelper.Extensors;

namespace Bau.Libraries.MVVM.ViewModels.ComboItems
{
	/// <summary>
	///		ViewModel para un combo
	/// </summary>
	public class ComboViewModel : BaseViewModel
	{ // Variables privadas
			private ComboItem objSelectedItem;
			private string strPropertyView;

		public ComboViewModel(BaseViewModel objViewModelParent, string strPropertyView)
		{ ViewModelParent = objViewModelParent;
			this.strPropertyView = strPropertyView;
			Items = new ComboItemsCollection();
		}

		/// <summary>
		///		Añade un elemento
		/// </summary>
		public void AddItem(int? intID, string strText, object objTag = null)
		{ Items.Add(intID, strText, objTag);
		}

		/// <summary>
		///		Elemento seleccionado
		/// </summary>
		public ComboItem SelectedItem 
		{ get { return objSelectedItem; } 
			set
				{ if (!ReferenceEquals(objSelectedItem, value))
						{ objSelectedItem = value;
							OnPropertyChanged("SelectedItem");
							ViewModelParent.RaiseEventPropertyChanged(strPropertyView);
							ViewModelParent.IsUpdated = true;
						}
				}
		}

		/// <summary>
		///		Elementos
		/// </summary>
		public ComboItemsCollection Items { get; private set; }

		/// <summary>
		///		ID del elemento seleccionado
		/// </summary>
		public int? SelectedID
		{ get
				{ if (SelectedItem == null)
						return null;
					else
						return SelectedItem.ID;
				}
			set
				{ foreach (ComboItem objItem in Items)
						if (objItem.ID == value)
							SelectedItem = objItem;
				}
		}

		/// <summary>
		///		Objeto asociado al elemento seleccionado
		/// </summary>
		public object SelectedTag
		{	get
				{ if (SelectedItem == null)
						return null;
					else
						return SelectedItem.Tag;
				}
			set
				{ foreach (ComboItem objItem in Items)
						if ((value == null && objItem.Tag == null) ||
								(value != null && ReferenceEquals(objItem.Tag, value)))
							SelectedItem = objItem;
				}
		}

		/// <summary>
		///		Texto seleccionado
		/// </summary>
		public string SelectedText
		{ get
				{ if (SelectedItem == null)
						return null;
					else
						return SelectedItem.Text;
				}
			set
				{ foreach (ComboItem objItem in Items)
						if (objItem.Text.EqualsIgnoreCase(value))
							SelectedItem = objItem;
				}
		}

		/// <summary>
		///		ViewModel padre
		/// </summary>
		public BaseViewModel ViewModelParent { get; private set; }
	}
}
