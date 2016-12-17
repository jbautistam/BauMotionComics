using System;

namespace Bau.Libraries.MVVM.ViewModels.TreeItems
{
	/// <summary>
	///		Clase base para los elementos de un árbol
	/// </summary>
	public class TreeViewItemViewModel : BaseViewModel, ITreeViewItemViewModel
	{	// Variables privadas
			private string strID, strText, strImage;
			private bool blnIsExpanded, blnIsLoadedChildren, blnIsSelected, blnIsChecked, blnIsBold;
			private System.Windows.Media.Brush brsForeground, brsBackground;
			private System.Windows.FontWeight intFontWeight;

		public TreeViewItemViewModel(string strNodeID, string strText, 
																 ITreeViewItemViewModel objParent = null, bool blnLazyLoadChildren = true, object objTag = null)
		{	NodeID = strNodeID;
			Text = strText;
			LazyLoadChildren = blnLazyLoadChildren;
			Parent = objParent;
			Tag = objTag;
			Children = new TreeViewItemsViewModelCollection();
			ForegroundBrush = System.Windows.Media.Brushes.Black;
			BackgroundBrush = null;
			if (blnLazyLoadChildren)
				Children.Add(new TreeViewItemViewModel(null, "-----", this, false));
		}

		/// <summary>
		///		Carga los elementos hijo de este nodo
		/// </summary>
		public virtual void LoadChildren()
		{
		}

		/// <summary>
		///		Cambia las propiedades dependiendo de las propiedades del nodo
		/// </summary>
		private void SetProperties()
		{ if (IsBold)
				FontWeightMode = System.Windows.FontWeights.Bold;
			else
				FontWeightMode = System.Windows.FontWeights.Normal;
		}

		/// <summary>
		///		Identificador del nodo
		/// </summary>
		public string NodeID
		{ get
				{ // Si no se ha definido ningún ID se crea
						if (string.IsNullOrEmpty(strID))
							strID = Guid.NewGuid().ToString();
					// Devuelve el ID
						return strID;
				}
			set { CheckProperty(ref strID, value); }
		}

		/// <summary>
		///		Texto del nodo
		/// </summary>
		public virtual string Text
		{ get { return strText; }
			set { CheckProperty(ref strText, value); }
		}

		/// <summary>
		///		Indica si se deben cargar los hijos
		/// </summary>
		public bool LazyLoadChildren { get; protected set; }

		/// <summary>
		///		Indica si el nodo está expandido
		/// </summary>
		public bool IsExpanded
		{ get { return blnIsExpanded; }
			set
				{	// Asigna el valor si se ha modificado
						if (value != blnIsExpanded)
							{	blnIsExpanded = value;
								OnPropertyChanged();
							}
					// Si se ha expandido, expande el elemento raíz
						if (blnIsExpanded && Parent != null)
							Parent.IsExpanded = true;
					// Carga los elementos hijo si es necesario
						if (blnIsExpanded && LazyLoadChildren && !blnIsLoadedChildren)
							{ // Indica que ya se han cargado los hijos
									blnIsLoadedChildren = true;
								// Carga los hijos
									Children.Clear();
									LoadChildren();
							}
				}
		}

		/// <summary>
		///		Indica si un elemento está chequeado
		/// </summary>
		public bool IsChecked
		{ get { return blnIsChecked; }
			set { CheckProperty(ref blnIsChecked, value); }
		}

		/// <summary>
		///		Indica si el elemento está seleccionado
		/// </summary>
		public bool IsSelected
		{ get { return blnIsSelected; }
			set {	CheckProperty(ref blnIsSelected, value); }
		}

		/// <summary>
		///		Indica si el elemento se debe mostrar en negrita
		/// </summary>
		public bool IsBold
		{ get { return blnIsBold; }
			set
				{ if (CheckProperty(ref blnIsBold, value))
						SetProperties();
				}
		}

		/// <summary>
		///		Modo de visualización de un nodo (bold / normal)
		/// </summary>
		public System.Windows.FontWeight FontWeightMode
		{ get { return intFontWeight; }
			set { CheckProperty(ref intFontWeight, value); }
		}

		/// <summary>
		///		Color del nodo
		/// </summary>
		public System.Windows.Media.Brush ForegroundBrush 
		{ get { return brsForeground; }
			set
				{ if (!ReferenceEquals(brsForeground, value))
						{ brsForeground = value;
							OnPropertyChanged();
						}
				}
		}

		/// <summary>
		///		Fondo del nodo
		/// </summary>
		public System.Windows.Media.Brush BackgroundBrush
		{ get { return brsBackground; }
			set
				{ if (!ReferenceEquals(brsBackground, value))
						{ brsBackground = value;
							OnPropertyChanged();
						}
				}
		}

		/// <summary>
		///		Imagen
		/// </summary>
		public string Image
		{ get { return strImage; }
			set { CheckProperty(ref strImage, value); }
		}

		/// <summary>
		///		Elemento padre del nodo
		/// </summary>
		public ITreeViewItemViewModel Parent { get; private set; }

		/// <summary>
		///		Objeto adicional del nodo
		/// </summary>
		public object Tag { get; set; }

		/// <summary>
		///		Hijos del nodo
		/// </summary>
		public TreeViewItemsViewModelCollection Children { get; private set; }
	}
}
