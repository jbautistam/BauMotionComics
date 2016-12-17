using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Controls.BauMVVMControls.TreeFiles.ViewModel;
using Bau.Libraries.MVVM.Views.Controllers;

namespace Bau.Controls.BauMVVMControls.TreeFiles.Control
{
	/// <summary>
	///		Ventana que muestra el árbol de proyectos de SourceEditor
	/// </summary>
	public partial class TreeFilesView : UserControl
	{ // Propiedades
			public static readonly DependencyProperty SourcePathProperty = 
								DependencyProperty.Register("SourcePath", typeof(string), typeof(TreeFilesView),
																						new FrameworkPropertyMetadata("C:\\", 
																																					FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
		// Eventos públicos
			public event EventHandler<EventArguments.FileEventArgs> OpenFile;
		// Variables privadas
			private TreeExplorerViewModel objTreeViewModel;
			private Point pntStartDrag;
			private DragDropTreeExplorerController objDragDropController = new DragDropTreeExplorerController();

		public TreeFilesView()
		{ // Inicializa los componentes
				InitializeComponent();
			// Inicializa el formulario
				InitForm();
		}

		/// <summary>
		///		Inicializa el formulario
		/// </summary>
		private void InitForm()
		{ trvExplorer.DataContext = ViewModelData;
			trvExplorer.ItemsSource = ViewModelData.Nodes;
		}

		/// <summary>
		///		Obtiene el nombre de archivo seleccionado
		/// </summary>
		public string GetSelectedFile()
		{ return (trvExplorer.SelectedItem as FileNodeViewModel)?.File;
		}

		/// <summary>
		///		Actualiza el árbol
		/// </summary>
		public void Refresh()
		{ ViewModelData.Refresh();	
		}

		/// <summary>
		///		ViewModel del formulario
		/// </summary>
		public TreeExplorerViewModel ViewModelData
		{ get
				{ // Crea la colección de nodos si no estaba en memoria
						if (objTreeViewModel == null)
							{ // Asigna el dataContext
									objTreeViewModel = new TreeExplorerViewModel(SourcePath);
								// Asigna los manejadores de eventos
									objTreeViewModel.OpenFile += (objSender, objEventArgs) => OpenFile?.Invoke(this, objEventArgs);
							}
					// Devuelve el ViewModel
						return objTreeViewModel;
				}
		}

		/// <summary>
		///		Directorio origen
		/// </summary>
		public string SourcePath
		{	get { return (string) GetValue(SourcePathProperty); }
			set 
				{ SetValue(SourcePathProperty, value); 
					ViewModelData.SourcePath = value;
				}
		}

		private void trvExplorer_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{ if (trvExplorer.DataContext is TreeExplorerViewModel &&  (sender as TreeView).SelectedItem is Libraries.MVVM.ViewModels.TreeItems.ITreeViewItemViewModel)
				(trvExplorer.DataContext as TreeExplorerViewModel).SelectedItem = (Libraries.MVVM.ViewModels.TreeItems.ITreeViewItemViewModel) (sender as TreeView).SelectedItem;
		}

		private void trvExplorer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{ ViewModelData.PropertiesCommand.Execute(null);
		}

		private void trvExplorer_MouseDown(object sender, MouseButtonEventArgs e)
		{ if (e.ChangedButton == MouseButton.Left)
				ViewModelData.SelectedItem = null;
		}

		private void trvExplorer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{ pntStartDrag = e.GetPosition(null);
		}

		private void trvExplorer_PreviewMouseMove(object sender, MouseEventArgs e)
		{ if (e.LeftButton == MouseButtonState.Pressed)
				{	Point pntMouse = e.GetPosition(null);
					Vector vctDifference = pntStartDrag - pntMouse;

						if (pntMouse.X < trvExplorer.ActualWidth - 50 &&
								(Math.Abs(vctDifference.X) > SystemParameters.MinimumHorizontalDragDistance || 
								 Math.Abs(vctDifference.Y) > SystemParameters.MinimumVerticalDragDistance))
							objDragDropController.InitDragOperation(trvExplorer, trvExplorer.SelectedItem as FileNodeViewModel);
				}
		}
		
		private void trvExplorer_DragEnter(object sender, DragEventArgs e)
		{ objDragDropController.TreatDragEnter(e);
		}

		private void trvExplorer_Drop(object sender, DragEventArgs e)
		{ FileNodeViewModel objNodeSource = objDragDropController.GetDragDropFileNode(e.Data) as FileNodeViewModel;

				if (objNodeSource != null)
					{ TreeViewItem trvNode = new Bau.Libraries.MVVM.Tools.ToolsWpf().FindAncestor<TreeViewItem>((DependencyObject) e.OriginalSource);

							if (trvNode != null)
								{ FileNodeViewModel objNodeTarget = trvNode.Header as FileNodeViewModel;

										if (objNodeSource != null && objNodeTarget != null)
											ViewModelData.Copy(objNodeSource, objNodeTarget, 
																				 (e.KeyStates & DragDropKeyStates.ControlKey) == DragDropKeyStates.ControlKey);
								}
				}
		}
	}
}