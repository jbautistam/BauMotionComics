using System;

using Bau.Libraries.MVVM.ViewModels;
using Bau.Libraries.MVVM.ViewModels.TreeItems;

namespace Bau.Controls.BauMVVMControls.TreeFiles.ViewModel
{
	/// <summary>
	///		ViewModel para el árbol de soluciones
	/// </summary>
	public class TreeExplorerViewModel : TreeViewModel
	{	// Constantes privadas
			private const string cnstStrActionCopy = "Copy";
			private const string cnstStrActionCut = "Cut";
			private const string cnstStrActionPaste = "Paste";
			private const string cnstStrActionRename = "Rename";
		// Eventos públicos
			public event EventHandler<EventArguments.FileEventArgs> OpenFile;
		// Variables privadas
			private string strSourcePath;
			private TreeViewItemsViewModelCollection objColNodes;
			private ITreeViewItemViewModel objNodeToCopy;
			private bool blnCut;

		public TreeExplorerViewModel(string strSourcePath)
		{ SourcePath = strSourcePath;
			ChangeUpdated = false;
			InitCommands();
		}

		/// <summary>
		///		Inicializa los comandos
		/// </summary>
		private void InitCommands()
		{ PropertiesCommand.AddListener(this, "SelectedItem");
			CopyCommand = new BaseCommand("Copiar", objParameter => ExecuteAction(cnstStrActionCopy, objParameter),
																					objParameter => CanExecuteAction(cnstStrActionCopy, objParameter))
																.AddListener(this, "SelectedItem");
			CutCommand = new BaseCommand("Cortar", objParameter => ExecuteAction(cnstStrActionCut, objParameter),
																					objParameter => CanExecuteAction(cnstStrActionCut, objParameter))
																.AddListener(this, "SelectedItem");
			PasteCommand = new BaseCommand("Pegar", objParameter => ExecuteAction(cnstStrActionPaste, objParameter),
																					objParameter => CanExecuteAction(cnstStrActionPaste, objParameter))
																.AddListener(this, "SelectedItem");
			DeleteCommand.AddListener(this, "SelectedItem");
			RenameCommand = new BaseCommand("Cambiar nombre", objParameter => ExecuteAction(cnstStrActionRename, objParameter),
																			objParameter => CanExecuteAction(cnstStrActionRename, objParameter))
																.AddListener(this, "SelectedItem");
		}

		/// <summary>
		///		Comprueba si se puede ejecutar una acción
		/// </summary>
		protected override void ExecuteAction(string strAction, object objParameter)
		{ switch (strAction)
					{ case cnstStrActionCopy:
						case cnstStrActionCut:
								AddFileToCopyBuffer(strAction == cnstStrActionCut);
							break;
						case cnstStrActionPaste:
								PasteFile();
							break;
						case cnstStrActionTypeDelete:
								// DeleteFile(SelectedItem as FileNodeViewModel);
							break;
						case cnstStrActionPaneTypeProperties:
								RaiseEventOpenFile(GetSelectedFile()?.File);
							break;
						case cnstStrActionTypeRefresh:
								Refresh();
							break;
						case cnstStrActionRename:
							break;
					}
		}

		/// <summary>
		///		Comprueba si se puede ejecutar una acción
		/// </summary>
		protected override bool CanExecuteAction(string strAction, object objParameter)
		{ switch (strAction)
				{ case cnstStrActionPaneTypeProperties:
					case cnstStrActionTypeDelete:
					case cnstStrActionCopy:
					case cnstStrActionCut:
					case cnstStrActionRename:
						return SelectedItem != null;
					case cnstStrActionPaste:
						return objNodeToCopy != null;
					case cnstStrActionTypeRefresh:
						return true;
					default:
						return false;
				}
		}

		/// <summary>
		///		Lanza el evento de abrir archivo
		/// </summary>
		public void RaiseEventOpenFile(string strFileName)
		{ if (!string.IsNullOrEmpty(strFileName) && !System.IO.Directory.Exists(strFileName))
				OpenFile?.Invoke(this, new EventArguments.FileEventArgs(strFileName));
		}

		/// <summary>
		///		Añade un archivo al buffer de copia
		/// </summary>
		private void AddFileToCopyBuffer(bool blnCut)
		{ objNodeToCopy = SelectedItem;
			this.blnCut = blnCut;
		}

		/// <summary>
		///		Pega un archivo
		/// </summary>
		private void PasteFile()
		{ if (objNodeToCopy != null)
				{ // Copia el elemento que teníamos en memoria sobre el nodo seleccionado
						Copy(objNodeToCopy, SelectedItem, !blnCut);
					// Indica que ya no hay ningún archivo que copiar
						objNodeToCopy = null;
				}
		}

		/// <summary>
		///		Copia un nodo sobre otro
		/// </summary>
		public void Copy(ITreeViewItemViewModel objNodeSource, ITreeViewItemViewModel objNodeTarget, bool blnCopy)
		{ 
/*
			if (CanCopy(objNodeSource, objNodeTarget))
				{ // Dependiendo de cuál sea el destino, llama a una rutina de copia
						if (objNodeTarget is SolutionFolderNodeViewModel)
							{ if (objNodeSource is ProjectNodeViewModel)
									PasteProject(objNodeSource as ProjectNodeViewModel, Solution,
															 (objNodeTarget as SolutionFolderNodeViewModel).Folder, blnCopy);
								else if (objNodeSource is SolutionFolderNodeViewModel)
									PasteSolution((objNodeSource as SolutionFolderNodeViewModel).Folder,
																(objNodeTarget as SolutionFolderNodeViewModel).Folder, blnCopy);
							}
						else
							PasteFile(GetCopyFile(objNodeSource), GetCopyFile(objNodeTarget), blnCopy);
					// Actualiza
						Refresh();
				}
*/
		}

		/// <summary>
		///		Comprueba si puede copiar un archivo en otro
		/// </summary>
		private bool CanCopy(ITreeViewItemViewModel objNodeSource, ITreeViewItemViewModel objNodeTarget)
		{ bool blnCanCopy = false; // ... supone que no se puede copiar

/*
				// Comprueba si se puede copiar
					if (!objNodeSource.NodeID.EqualsIgnoreCase(objNodeTarget.NodeID))
						{ if (objNodeTarget is SolutionFolderNodeViewModel && 
										(objNodeSource is SolutionFolderNodeViewModel || objNodeSource is ProjectNodeViewModel))
								blnCanCopy = true;
							else if (objNodeTarget is ProjectNodeViewModel && objNodeSource is FileNodeViewModel)
								{ ProjectModel objProjectTarget = (objNodeTarget as ProjectNodeViewModel).Project;
									FileModel objFileSource = (objNodeSource as FileNodeViewModel).File;

										if (objProjectTarget.Definition.GlobalID.EqualsIgnoreCase(objFileSource.SearchProject().Definition.GlobalID))
											blnCanCopy = true;
								}
							else if (objNodeTarget is FileNodeViewModel && objNodeSource is FileNodeViewModel)
								{ FileModel objSource = (objNodeSource as FileNodeViewModel).File;
									FileModel objTarget = (objNodeTarget as FileNodeViewModel).File;

										if (objSource.SearchProject().Definition.GlobalID.EqualsIgnoreCase(objTarget.SearchProject().Definition.GlobalID))
											{ if (objTarget.IsFolder)
													blnCanCopy = true;
											}
								}
						}
*/
				// Devuelve el valor que indica si se puede copiar
					return blnCanCopy;
		}

/*
		/// <summary>
		///		Pega un archivo
		/// </summary>
		private void PasteFile(FileModel objFileToCopy, FileModel objFileTarget, bool blnCopy)
		{ if (objFileToCopy != null)
				{ string strPathTarget = objFileTarget.FullFileName;
					bool blnIsCopied = false;

						// Obtiene el directorio destino
							if (!System.IO.Directory.Exists(strPathTarget))
								strPathTarget = System.IO.Path.GetDirectoryName(strPathTarget);
						// Copia / mueve el archivo / carpeta
							if (objFileToCopy.IsFolder || CheckIsPackage(objFileToCopy))
								{ LibHelper.Files.HelperFiles.CopyPath(objFileToCopy.FullFileName, 
																											 LibHelper.Files.HelperFiles.GetConsecutivePath(strPathTarget,
																																																			System.IO.Path.GetFileName(objFileToCopy.FullFileName)));
									blnIsCopied = true; // ... supone que se ha podido copiar
								}
							else
								blnIsCopied = LibHelper.Files.HelperFiles.CopyFile(objFileToCopy.FullFileName, 
																																	 LibHelper.Files.HelperFiles.GetConsecutiveFileName(strPathTarget, 
																																																											System.IO.Path.GetFileName(objFileToCopy.FullFileName)));
						// Si la acción es para cortar, elimina el archivo inicial
							if (blnIsCopied && !blnCopy)
								{ if (objFileToCopy.IsFolder || CheckIsPackage(objFileToCopy))
										LibHelper.Files.HelperFiles.KillPath(objFileToCopy.FullFileName);
									else
										LibHelper.Files.HelperFiles.KillFile(objFileToCopy.FullFileName);
								}
				}
		}
*/

		/// <summary>
		///		Abre el archivo en el explorador
		/// </summary>
		private void OpenFileExplorer()
		{ FileNodeViewModel objFile = GetSelectedFile();

				if (objFile != null)
					{ string strPath = objFile.File;

							// Obtiene el nombre del directorio si se le ha pasado un archivo
								if (!System.IO.Directory.Exists(strPath))
									strPath = System.IO.Path.GetDirectoryName(strPath);
							// Abre el explorador de archivos
								if (System.IO.Directory.Exists(strPath))
									Bau.Libraries.LibHelper.Files.HelperFiles.OpenDocumentShell("explorer.exe", strPath);
					}
		}

		/// <summary>
		///		Abre el archivo con Windows
		/// </summary>
		private void OpenWithWindows()
		{ FileNodeViewModel objFile = GetSelectedFile();

				if (objFile != null && !string.IsNullOrEmpty(objFile.File) && System.IO.File.Exists(objFile.File))
					Libraries.LibHelper.Files.HelperFiles.OpenDocumentShell(objFile.File);
		}

		/// <summary>
		///		Carga los nodos
		/// </summary>
		protected override TreeViewItemsViewModelCollection LoadNodes()
		{ TreeViewItemsViewModelCollection objColNodes = new TreeViewItemsViewModelCollection();

				// Obtiene los directorios y archivos de la carpeta
					if (!string.IsNullOrEmpty(SourcePath) && System.IO.Directory.Exists(SourcePath))
						{ // Carga las carpetas
								foreach (string strPath in System.IO.Directory.GetDirectories(SourcePath))
									objColNodes.Add(new FileNodeViewModel(strPath, null));
							// Carga los archivos
								foreach (string strFile in System.IO.Directory.GetFiles(SourcePath))
									objColNodes.Add(new FileNodeViewModel(strFile, null));
						}
				// Devuelve la colección de nodos
					return objColNodes;
		}

		/// <summary>
		///		Obtiene el archivo seleccionado
		/// </summary>
		public FileNodeViewModel GetSelectedFile()
		{ if (SelectedItem != null && SelectedItem is FileNodeViewModel)
				return (SelectedItem as FileNodeViewModel);
			else
				return null;
		}

		/// <summary>
		///		Carpeta inicial a mostrar en el árbol
		/// </summary>
		public override TreeViewItemsViewModelCollection Nodes 
		{ get
				{ // Carga los nodos
						if (objColNodes == null)
							objColNodes = LoadNodes();
					// Devuelve los nodos
						return objColNodes;
				}
			set 
				{ if (!ReferenceEquals(objColNodes, value))
						{ objColNodes = value; 
							OnPropertyChanged("Nodes");
						}
				}
		}

		/// <summary>
		///		Directorio base del árbol
		/// </summary>
		public string SourcePath 
		{ get { return strSourcePath; }
			set
				{ if (CheckProperty(ref strSourcePath, value))
						Refresh();
				}
		}

		/// <summary>
		///		Comando para copiar archivos / proyectos
		/// </summary>
		public BaseCommand CopyCommand { get; private set; }

		/// <summary>
		///		Comando para cortar archivos / proyectos
		/// </summary>
		public BaseCommand CutCommand { get; private set; }

		/// <summary>
		///		Comando para pegar archivos / proyectos
		/// </summary>
		public BaseCommand PasteCommand { get; private set; }

		/// <summary>
		///		Comando para cambiar nombre de archivo
		/// </summary>
		public BaseCommand RenameCommand { get; private set; }
	}
}
