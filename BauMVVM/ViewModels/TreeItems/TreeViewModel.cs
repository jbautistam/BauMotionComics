using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Bau.Libraries.LibHelper.Extensors;

namespace Bau.Libraries.MVVM.ViewModels.TreeItems
{
	/// <summary>
	///		Model para un treeView
	/// </summary>
	public abstract class TreeViewModel : Forms.BasePaneViewModel
	{ // Variables privadas
			private ITreeViewItemViewModel objSelectedItem;

		public TreeViewModel()
		{ base.PropertyChanged += (objSender, objArgs) =>
																	{ if (objArgs.PropertyName == "SelectedItem")
																			{ base.PropertiesCommand.OnCanExecuteChanged();
																				base.DeleteCommand.OnCanExecuteChanged();
																			}
																	};
		}

		/// <summary>
		///		Obtiene los elementos seleccionados
		/// </summary>
		public List<ITreeViewItemViewModel> GetSelectedItems()
		{ return GetSelectedItems(Nodes);
		}

		/// <summary>
		///		Obtiene recursivamente los elementos seleccionados
		/// </summary>
		private List<ITreeViewItemViewModel> GetSelectedItems(ObservableCollection<ITreeViewItemViewModel> objColNodes)
		{ List<ITreeViewItemViewModel> objColSelected = new List<ITreeViewItemViewModel>();

				// Recorre los nodos obteniendo los seleccionados
					foreach (TreeViewItemViewModel objNode in objColNodes)
						{ // Añade el nodo si está seleccionado
								if (objNode.IsSelected)
									objColSelected.Add(objNode);
							// Añade los nodos hijo
								if (objNode.Children != null && objNode.Children.Count > 0)
									objColSelected.AddRange(GetSelectedItems(objNode.Children));
						}
				// Devuelve la colección de nodos
					return objColSelected;
		}

		/// <summary>
		///		Carga los nodos hijo
		/// </summary>
		protected abstract TreeViewItemsViewModelCollection LoadNodes();

		/// <summary>
		///		Actualiza el árbol
		/// </summary>
		public virtual void Refresh()
		{ List<Tuple<string, string>> objColNodesExpanded = GetNodesExpanded(Nodes);

				// Limpia los nodos
					Nodes.Clear();
				// Carga los nodos
					Nodes.AddRange(LoadNodes());
				// Vuelve a expandir los elementos
					ExpandNodes(Nodes, objColNodesExpanded);
		}

		/// <summary>
		///		Obtiene recursivamente los elementos seleccionados
		/// </summary>
		private List<Tuple<string, string>> GetNodesExpanded(TreeViewItemsViewModelCollection objColNodes)
		{ List<Tuple<string, string>> objColExpanded = new List<Tuple<string, string>>();

				// Recorre los nodos obteniendo los seleccionados
					foreach (TreeViewItemViewModel objNode in objColNodes)
						{ // Añade el nodo si se ha expandido
								if (objNode.IsExpanded)
									objColExpanded.Add(new Tuple<string, string>(objNode.GetType().ToString(), objNode.NodeID));
							// Añade los nodos hijo
								if (objNode.Children != null && objNode.Children.Count > 0)
									objColExpanded.AddRange(GetNodesExpanded(objNode.Children));
						}
				// Devuelve la colección de nodos
					return objColExpanded;
		}

		/// <summary>
		///		Expande un nodo
		/// </summary>
		private void ExpandNodes(TreeViewItemsViewModelCollection objColNodes, List<Tuple<string, string>> objColNodesExpanded)
		{ if (objColNodes != null)
				foreach (ITreeViewItemViewModel objNode in objColNodes)
					if (CheckIsExpanded(objNode, objColNodesExpanded))
						{ // Expande el nodo
								objNode.IsExpanded = true;
							// Expande los hijos
								ExpandNodes(objNode.Children, objColNodesExpanded);
						}
		}

		/// <summary>
		///		Comprueba si un nodo estaba en la lista de nodos abiertos
		/// </summary>
		private bool CheckIsExpanded(ITreeViewItemViewModel objNode, List<Tuple<string, string>> objColNodesExpanded)
		{ // Recorre la colección
				foreach (Tuple<string, string> objNodeExpanded in objColNodesExpanded)
					if (objNodeExpanded.Item1.EqualsIgnoreCase(objNode.GetType().ToString()) &&
							objNodeExpanded.Item2.EqualsIgnoreCase(objNode.NodeID))
						return true;
			// Si ha llegado hasta aquí es porque no ha encontrado nada
				return false;
		}

		/// <summary>
		///		Debug de los nodos
		/// </summary>
		protected void Debug()
		{ System.Diagnostics.Debug.WriteLine("Debug de TreeViewModel");
			Nodes.Debug();
		}

		/// <summary>
		///		Elemento seleccionado en el árbol
		/// </summary>
		public ITreeViewItemViewModel SelectedItem 
		{ get { return objSelectedItem; }
			set
				{ if (!ReferenceEquals(objSelectedItem, value))
						{	objSelectedItem = value;
							OnPropertyChanged("SelectedItem");
						}
				}
		}

		/// <summary>
		///		Nodos a mostrar en el árbol
		/// </summary>
		public abstract TreeViewItemsViewModelCollection Nodes { get; set; }
	}
}
