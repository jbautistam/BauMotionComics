using System;
using System.Windows;

using Bau.Libraries.LibHelper.Extensors;

namespace BauMotionComics.Views
{
	/// <summary>
	///		Formulario para edición de cómics
	/// </summary>
	public partial class ComicEditView : Window
	{
		public ComicEditView(string strPath)
		{	InitializeComponent();
			PathComic = strPath;
		}

		/// <summary>
		///		Inicializa el formulario
		/// </summary>
		private void InitForm()
		{ // Asigna el directorio al árbol
				trvFiles.SourcePath = PathComic;
			// Vacía el editor
				NewFile();
		}

		/// <summary>
		///		Abre un archivo
		/// </summary>
		private void OpenFile(string strFileName)
		{	if (IsImage(strFileName))
				OpenImage(strFileName);
			else
				OpenFileText(strFileName);
		}

		/// <summary>
		///		Comprueba si la extensión de un archivo se corresponde con un archivo de imagen
		/// </summary>
		private bool IsImage(string strFileName)
		{ return !strFileName.IsEmpty() && (strFileName.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase) ||
																				strFileName.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase) ||
																				strFileName.EndsWith(".bmp", StringComparison.CurrentCultureIgnoreCase) ||
																				strFileName.EndsWith(".gif", StringComparison.CurrentCultureIgnoreCase));
		}

		/// <summary>
		///		Abre un archivo de texto en el editor
		/// </summary>
		private void OpenFileText(string strFileName)
		{	// Asigna el nombre de archivo
				udtEditor.FileName = strFileName;
			// Carga el texto del archivo
				if (System.IO.File.Exists(strFileName))
					udtEditor.Text = System.IO.File.ReadAllText(udtEditor.FileName);
			// Cambia la extensión a XML para que se resalten las etiquetas
				udtEditor.ChangeHighLightByExtension(".xml");
			// Muestra el nombre de archivo
				txtFile.Text = System.IO.Path.GetFileName(strFileName);
		}

		/// <summary>
		///		Abre un archivo de imagen
		/// </summary>
		private void OpenImage(string strFileName)
		{ ImageView frmView = new ImageView(strFileName);

				// Abre el formulario
					frmView.Owner = this;
					frmView.ShowDialog();
		}

		/// <summary>
		///		Crea un nuevo archivo
		/// </summary>
		private void NewFile()
		{ udtEditor.FileName = null;
			udtEditor.Text = "";
			udtEditor.ChangeHighLightByExtension(".xml");
		}

		/// <summary>
		///		Graba el archivo
		/// </summary>
		private void SaveFile()
		{ // Si no hay nombre de archivo, se obtiene uno
				if (udtEditor.FileName.IsEmpty())
					{ Ookii.Dialogs.Wpf.VistaSaveFileDialog frmDialog = new Ookii.Dialogs.Wpf.VistaSaveFileDialog();
						string strExtension = "*" + Bau.Libraries.LibMotionComic.ComicManager.cnstStrExtension;

							// Asigna las propiedades
								frmDialog.AddExtension = true;
								frmDialog.DefaultExt = ".xml";
								frmDialog.RestoreDirectory = true;
								frmDialog.InitialDirectory = GetSelectedPath();
								frmDialog.Filter = $"Archivos XML (*.xml)|*.xml|Archivo de cómic ({strExtension})|{strExtension}";
							// Abre el formulario
								if (frmDialog.ShowDialog(this) == true)
									{ // Asigna el nombre de archivo
											udtEditor.FileName = frmDialog.FileName;
										// Añade la extensión
											if (System.IO.Path.GetExtension(udtEditor.FileName).IsEmpty())
												udtEditor.FileName = udtEditor.FileName + ".xml";
									}
					}
			// Graba el archivo
				if (!udtEditor.FileName.IsEmpty())
					{ // Graba el archivo y lo recarga
							System.IO.File.WriteAllText(udtEditor.FileName, udtEditor.Text);
							OpenFile(udtEditor.FileName);
						// Actualiza el árbol
							trvFiles.Refresh();
					}
		}

		/// <summary>
		///		Crea una carpeta
		/// </summary>
		private void CreateFolder()
		{ string strPath = GetSelectedPath();
			Ookii.Dialogs.Wpf.VistaFolderBrowserDialog frmPath = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();

				// Asigna las propiedades al formulario
					frmPath.SelectedPath = strPath;
					frmPath.ShowNewFolderButton = true;
				// Abre la ventana de selección de directorio y actualiza el árbol si se ha creado
					if (frmPath.ShowDialog(this) == true)
						trvFiles.Refresh();
		}

		/// <summary>
		///		Copia una imagen
		/// </summary>
		private void CreateImage()
		{ string strPath = GetSelectedPath();
			Ookii.Dialogs.Wpf.VistaOpenFileDialog frmOpenFile = new Ookii.Dialogs.Wpf.VistaOpenFileDialog();
			
				// Asigna las propiedades al formulario
					frmOpenFile.CheckFileExists = true;
					frmOpenFile.DefaultExt = ".png";
					frmOpenFile.Filter = "Todas las imágenes (jpg, png, gif)|*.jpg;*.png;*.gif|Imágenes jpg|*.jpg|Imágenes png|*.png|Imágenes gif|*.gif|Todos los archivos|*.*";
					frmOpenFile.InitialDirectory = strPath;
					frmOpenFile.Multiselect = true;
				// Abre el formulario 
					if (frmOpenFile.ShowDialog(this) == true)
						{ string [] arrStrFiles = frmOpenFile.FileNames;

								// Copia las imágenes
									foreach (string strFileName in arrStrFiles)
										Tools.FilesHelper.CopyFile(strFileName, strPath);
								// Actualiza el árbol
									trvFiles.Refresh();
						}
		}

		/// <summary>
		///		Obtiene el directorio seleccionado
		/// </summary>
		private string GetSelectedPath()
		{ string strPath = trvFiles.GetSelectedFile();

				// Obtiene el directorio base
					if (!System.IO.Directory.Exists(strPath))
						strPath = trvFiles.SourcePath;
				// Devuelve el directorio
					return strPath;
		}

		/// <summary>
		///		Borra el archivo seleccionado
		/// </summary>
		private void DeleteFile()
		{ string strFileName = trvFiles.GetSelectedFile();

				if (!strFileName.IsEmpty())
					{ bool blnDeleted = false;

							// Borra el archivo o directorio
								if (System.IO.Directory.Exists(strFileName))
									{	if (MessageBox.Show($"¿Desea eliminar el directorio {System.IO.Path.GetFileName(strFileName)}?", "BauMotionComic", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
											blnDeleted = Tools.FilesHelper.DeletePath(strFileName);
									}
								else if (System.IO.File.Exists(strFileName))
									{ if (MessageBox.Show($"¿Desea eliminar el archivo {System.IO.Path.GetFileName(strFileName)}?", "BauMotionComic", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
											{ // Borra el archivo
													blnDeleted = Tools.FilesHelper.DeleteFile(strFileName);
												// Si se estaba editando, lo cierra
													if (udtEditor.FileName.EqualsIgnoreCase(strFileName))
														NewFile();
											}
									}
							// Si se ha borrado actualiza el árbol
								if (blnDeleted)
									trvFiles.Refresh();
					}
		}

		/// <summary>
		///		Directorio del cómic que se está editando
		/// </summary>
		private string PathComic { get; }

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{ InitForm();
		}

		private void trvFiles_OpenFile(object sender, Bau.Controls.BauMVVMControls.TreeFiles.EventArguments.FileEventArgs e)
		{ OpenFile(e.FileName);
		}

		private void cmdNewFile_Click(object sender, RoutedEventArgs e)
		{ NewFile();
		}
		
		private void cmdNewFolder_Click(object sender, RoutedEventArgs e)
		{ CreateFolder();
		}
		
		private void cmdNewImage_Click(object sender, RoutedEventArgs e)
		{ CreateImage();
		}

		private void cmdOpenFile_Click(object sender, RoutedEventArgs e)
		{ string strFileName = trvFiles.GetSelectedFile();

				if (!strFileName.IsEmpty() && System.IO.File.Exists(strFileName))
					OpenFile(trvFiles.GetSelectedFile());
		}
		
		private void cmdSave_Click(object sender, RoutedEventArgs e)
		{ SaveFile();
		}

		private void cmdDelete_Click(object sender, RoutedEventArgs e)
		{ DeleteFile();
		}
	}
}
