using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

using Bau.Libraries.LibHelper.Extensors;
using BauMotionComics.ViewModels;

namespace BauMotionComics
{
	/// <summary>
	///		Ventana principal de la aplicación
	/// </summary>
	public partial class MainWindow : Window
	{	
		public MainWindow()
		{ InitializeComponent();
		}

		/// <summary>
		///		Inicializa el formulario
		/// </summary>
		private void InitForm()
		{	LoadComicFiles(BauMotionComicConfiguration.ComicPath);
		}

		/// <summary>
		///		Carga los archivos de cómic
		/// </summary>
		private void LoadComicFiles(string strPath)
		{ ObservableCollection<ComicFileViewModel> objColFiles;

				// Carga los archivos
					objColFiles = LoadComicViewModel(strPath);
				// Rellena la lista
					lstFiles.ItemsSource = objColFiles;
				// Guarda el directorio
					if (!strPath.EqualsIgnoreCase(Properties.Settings.Default.ComicPath))
						{ BauMotionComicConfiguration.ComicPath = strPath;
							BauMotionComicConfiguration.Save();
						}
		}

		/// <summary>
		///		Carga los archivos de cómic
		/// </summary>
		private ObservableCollection<ComicFileViewModel> LoadComicViewModel(string strPath)
		{ ObservableCollection<ComicFileViewModel> objColFiles = new ObservableCollection<ComicFileViewModel>();

				// Carga los directorios
					if (System.IO.Directory.Exists(strPath))
						{ string [] arrStrDirectories = System.IO.Directory.GetDirectories(strPath);

								// Añade los archivos a la lista
									foreach (string strDirectory in arrStrDirectories)
										{ string [] arrStrFiles = System.IO.Directory.GetFiles(strDirectory, "*" + Bau.Libraries.LibMotionComic.ComicManager.cnstStrExtension);
							
												foreach (string strFile in arrStrFiles)
													{ string strFileName = System.IO.Path.GetFileName(strFile);
										
															// Se salta los archivos de recursos y añade los nombres de los archivos de imagen
																if (!strFileName.StartsWith("_"))
																	objColFiles.Add(LoadComic(strDirectory, strFileName));
													}
										}
						}
				// Devuelve la colección de cómics
					return objColFiles;
		}

		/// <summary>
		///		Carga los datos de un cómic
		/// </summary>
		private ComicFileViewModel LoadComic(string strPath, string strFile)
		{ ComicFileViewModel objFile = new ComicFileViewModel(strPath, strFile);

					if (System.IO.Directory.Exists(strPath))
						{ string strFileName = System.IO.Path.Combine(strPath, strFile);
							Bau.Libraries.LibMotionComic.ComicManager objComicManager = new Bau.Libraries.LibMotionComic.ComicManager(strPath, strFile);

								// Carga los datos
									objComicManager.Load(false);
								// Asigna las propiedades
									objFile.Path = strPath;
									objFile.FileName = strFile;
									if (!string.IsNullOrEmpty(objComicManager.Comic.ThumbFileName))
										objFile.ThumbFileName = System.IO.Path.Combine(strPath, objComicManager.Comic.ThumbFileName);
									else
										objFile.ThumbFileName = "pack://application:,,,/Assets/DefaultCover.png";
									objFile.Title = objComicManager.Comic.Title;
									if (string.IsNullOrEmpty(objFile.Title))
										objFile.Title = System.IO.Path.GetFileNameWithoutExtension(strFile);
									objFile.Summary = objComicManager.Comic.Summary;
						}
				// Devuelve el archivo
					return objFile;
		}

		/// <summary>
		///		Abre un cómic
		/// </summary>
		private void OpenComic(ComicFileViewModel objFile)
		{ if (objFile != null)
				{ string strError;

						if (!udtComic.LoadComic(System.IO.Path.Combine(objFile.Path, objFile.FileName), out strError))
							MessageBox.Show($"Error al cargar el cómic.{Environment.NewLine}{strError}", "BauMotionComic");
			}
		}

		/// <summary>
		///		Edita un cómic
		/// </summary>
		private void EditComic(ComicFileViewModel objFile)
		{ string strPath;

				// Obtiene el directorio donde se crea el archivo
					if (objFile == null)
						{ // Recoge el directorio
								strPath = GetPath();
							// Crea el directorio
								if (!strPath.IsEmpty())
									Tools.FilesHelper.CreatePath(strPath);
						}
					else
						strPath = objFile.Path;
				// Abre el formulario de edición
					if (!strPath.IsEmpty() && System.IO.Directory.Exists(strPath))
						{	Views.ComicEditView frmNewComic = new Views.ComicEditView(strPath);

								// Muestra la ventana
									frmNewComic.Owner = this;
									frmNewComic.Show();
						}
		}

		/// <summary>
		///		Borra un cómic
		/// </summary>
		private void DeleteComic(ComicFileViewModel objFile)
		{ if (objFile != null &&
					MessageBox.Show($"¿Realmente desea eliminar el directorio {objFile.Path} del cómic {objFile.Title}", "BauMotionComic",
													MessageBoxButton.YesNo) == MessageBoxResult.Yes)
				{ // Borra el directorio (sin tener en cuenta las excepciones)
						try
							{ System.IO.Directory.Delete(objFile.Path, true);
							}
						catch (Exception objException) 
							{ System.Diagnostics.Debug.WriteLine("Excepción: " + objException.Message);
							}
					// Actualiza los cómics
						LoadComicFiles(BauMotionComicConfiguration.ComicPath);
				}
		}


		/// <summary>
		///		Selecciona un directorio
		/// </summary>
		private string GetPath()
		{ Ookii.Dialogs.Wpf.VistaFolderBrowserDialog dlgFolder = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
			
				// Asigna la carpeta inicial
					dlgFolder.SelectedPath = Properties.Settings.Default.ComicPath;
					dlgFolder.ShowNewFolderButton = true;
				// Muestra el diálogo
					if (dlgFolder.ShowDialog() ?? false)
						return dlgFolder.SelectedPath;
					else
						return null;
		}

		/// <summary>
		///		Selecciona un directorio
		/// </summary>
		private void SelectPath()
		{ string strPath = GetPath();

				// Actualiza la lista
					if (!strPath.IsEmpty())
						LoadComicFiles(strPath);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{ InitForm();
		}

		private void lstFiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{ OpenComic(lstFiles.SelectedItem as ComicFileViewModel);
		}

		private void cmdRefresh_Click(object sender, RoutedEventArgs e)
		{ LoadComicFiles(Properties.Settings.Default.ComicPath);
		}

		private void cmdEdit_Click(object sender, RoutedEventArgs e)
		{ EditComic(lstFiles.SelectedItem as ComicFileViewModel);
		}

		private void cmdNew_Click(object sender, RoutedEventArgs e)
		{ EditComic(null);
		}

		private void cmdDelete_Click(object sender, RoutedEventArgs e)
		{ DeleteComic(lstFiles.SelectedItem as ComicFileViewModel);
		}

		private void cmdSelectPath_Click(object sender, RoutedEventArgs e)
		{ SelectPath();
		}
	}
}
