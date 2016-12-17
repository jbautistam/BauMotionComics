using System;
using System.Windows;
using System.Windows.Controls;

namespace Bau.Libraries.MVVM.Views.Controllers
{
	/// <summary>
	///		Controlador de dragDrop de un árbol
	/// </summary>
	public class DragDropTreeExplorerController
	{
		public DragDropTreeExplorerController(string strKeyDataObject = "BaseNodeViewModel")
		{ KeyDataObject = strKeyDataObject;
		}

		/// <summary>
		///		Inicia la operación de Drag & Drop
		/// </summary>
		public void InitDragOperation(TreeView trvExplorer, ViewModels.TreeItems.ITreeViewItemViewModel objNode)
		{	if (objNode != null)
				DragDrop.DoDragDrop(trvExplorer, new DataObject(KeyDataObject, objNode), DragDropEffects.Move);
		}

		/// <summary>
		///		Obtiene el nodo de archivo que se está arrastrando
		/// </summary>
		public ViewModels.TreeItems.ITreeViewItemViewModel GetDragDropFileNode(IDataObject objDataObject)
		{ ViewModels.TreeItems.ITreeViewItemViewModel objNode = null;

				// Obtiene los datos que se están arrastrando
					if (objDataObject.GetDataPresent(KeyDataObject))
						objNode = objDataObject.GetData(KeyDataObject) as ViewModels.TreeItems.ITreeViewItemViewModel;
				// Devuelve los datos del nodo que se está arrastrando
					return objNode;
		}

		/// <summary>
		///		Trata el evento de entrada en un control
		/// </summary>
		public void TreatDragEnter(DragEventArgs e)
		{ if (GetDragDropFileNode(e.Data) != null)
				e.Effects = DragDropEffects.Copy;
			else
				e.Effects = DragDropEffects.None;
		}

		/// <summary>
		///		Clave del objeto utilizado para almacenar los datos a arrastrar y copiar
		/// </summary>
		public string KeyDataObject { get; private set; }
	}
}
