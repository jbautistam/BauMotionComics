using System;

using Bau.Libraries.LibHelper.Extensors;

namespace Bau.Libraries.MVVM.ViewModels.ListItems
{
	/// <summary>
	///		Elemento base para los ViewModel de los elementos de una lista
	/// </summary>
	public class BaseListItemViewModel : MVVM.ViewModels.BaseViewModel, IListItemViewModel
	{	// Variables privadas
			private string strID, strText;
			private bool blnIsSelected, blnIsChecked;
			private object objTag;

		public BaseListItemViewModel(Forms.BaseFormViewModel objFormParent = null)
		{ FormParent = objFormParent;
			if (FormParent != null)
				PropertyChanged += (objSender, objEvnArgs) =>
															{ if (FormParent != null && objEvnArgs.PropertyName.EqualsIgnoreCase("IsChecked"))
																	FormParent.IsUpdated = true;
															};
		}

		/// <summary>
		///		Formulario padre
		/// </summary>
		public Forms.BaseFormViewModel FormParent { get; private set; }

		/// <summary>
		///		ID del elemento del nodo
		/// </summary>
		public string ID
		{ get { return strID; }
			set { CheckProperty(ref strID, value); }
		}

		/// <summary>
		///		Título del nodo
		/// </summary>
		public string Text
		{ get { return strText; }
			set { CheckProperty(ref strText, value); }
		}

		/// <summary>
		///		Indica si el elemento está chequeado
		/// </summary>
		public bool IsChecked
		{ get { return blnIsChecked; }
			set
				{ if (blnIsChecked != value)
						{ blnIsChecked = value;
							OnPropertyChanged();
						}
				}
		}

		/// <summary>
		///		Indica si el elemento está seleccionado
		/// </summary>
		public bool IsSelected
		{ get { return blnIsSelected; }
			set
				{ if (blnIsSelected != value)
						{ blnIsSelected = value;
							OnPropertyChanged();
						}
				}
		}

		/// <summary>
		///		Tag del elemento
		/// </summary>
		public object Tag
		{ get { return objTag; }
			set
				{ if (!ReferenceEquals(objTag, value))
						{ objTag = value;
							OnPropertyChanged(); 
						}
				}
		}
	}
}