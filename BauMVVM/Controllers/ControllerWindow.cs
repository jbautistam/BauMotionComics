using System;
using System.Windows;

namespace Bau.Libraries.MVVM.Controllers
{
	/// <summary>
	///		Controlador de ventanas comunes
	/// </summary>
	public abstract class ControllerWindow : IControllerWindow
	{
		/// <summary>
		///		Tipo de resultado del cuadro de diálogo
		/// </summary>
		public enum ResultType
			{ Yes,
				No,
				Cancel
			}

		/// <summary>
		///		Tipo de notificación
		/// </summary>
		public enum NotificationType
			{	
				/// <summary>Información</summary>
				Information,
				/// <summary>Advertencia</summary>
				Warning,
				/// <summary>Error</summary>
				Error,
				/// <summary>Otras notificaciones</summary>
				Other
			}

		public ControllerWindow(string strApplicationName, Window frmMainWindow)
		{ ApplicationName = strApplicationName;
			MainWindow = frmMainWindow;
		}

		/// <summary>
		///		Muestra un cuadro de diálogo
		/// </summary>
		public ResultType ShowDialog(Window frmOwner, Window frmView)
		{ // Si no se le ha pasado una ventana propietario, le asigna una
				if (frmOwner == null)
					frmOwner = MainWindow;
			// Muestra el formulario activo
				frmView.Owner = frmOwner;
				frmView.ShowActivated = true;
			// Muestra el formulario y devuelve el resultado
				return ConvertDialogResult(frmView.ShowDialog());
		}

		/// <summary>
		///		Convierte el resultado de un cuadro de diálogo
		/// </summary>
		protected ResultType ConvertDialogResult(bool? blnResult)
		{ if (blnResult == null)
				return ResultType.Cancel;
			else if (blnResult ?? false)
				return ResultType.Yes;
			else
				return ResultType.No;
		}

		/// <summary>
		///		Abre el cuadro de diálogo de carga de archivos
		/// </summary>
		public string OpenDialogLoad(string strDefaultPath, string strFilter, string strDefaultFileName = null, string strDefaultExtension = null)
		{ string [] arrStrFiles = OpenDialogLoadFiles(false, strDefaultPath, strFilter, strDefaultFileName, strDefaultExtension);

				// Devuelve el archivo
					if (arrStrFiles != null && arrStrFiles.Length > 0)
						return arrStrFiles[0];
					else
						return null;
		}

		/// <summary>
		///		Abre el cuadro de diálogo de carga de varios archivos
		/// </summary>
		public string [] OpenDialogLoadFilesMultiple(string strDefaultPath, string strFilter, string strDefaultFileName = null, string strDefaultExtension = null)
		{ return OpenDialogLoadFiles(true, strDefaultPath, strFilter, strDefaultFileName, strDefaultExtension);
		}


		/// <summary>
		///		Abre el cuadro de diálogo de carga de varios archivos
		/// </summary>
		private string [] OpenDialogLoadFiles(bool blnMultiple, string strDefaultPath, string strFilter, string strDefaultFileName, string strDefaultExtension)
		{ Microsoft.Win32.OpenFileDialog dlgFile = new Microsoft.Win32.OpenFileDialog();

				// Si no se le ha pasado un directorio predeterminado, escoge "Mis documentos"
					if (string.IsNullOrWhiteSpace(strDefaultPath))
						strDefaultPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				// Asigna las propiedades
					dlgFile.Multiselect = blnMultiple;
					dlgFile.InitialDirectory = strDefaultPath;
					dlgFile.FileName = strDefaultFileName;
					dlgFile.DefaultExt = strDefaultExtension;
					dlgFile.Filter = strFilter; 
				// Muestra el cuadro de diálogo y devuelve los archivos
					if (dlgFile.ShowDialog() ?? false)
						return dlgFile.FileNames;
					else
						return null;
		}

		/// <summary>
		///		Abre el cuadro de diálogo de grabación de archivos
		/// </summary>
		public string OpenDialogSave(string strDefaultPath, string strFilter, string strDefaultFileName = null, string strDefaultExtension = null)
		{ Microsoft.Win32.SaveFileDialog dlgFile = new Microsoft.Win32.SaveFileDialog();

				// Asigna las propiedades
					dlgFile.InitialDirectory = strDefaultPath;
					dlgFile.FileName = strDefaultFileName;
					dlgFile.DefaultExt = strDefaultExtension;
					dlgFile.Filter = strFilter; 
				// Muestra el cuadro de diálogo
					if (dlgFile.ShowDialog() ?? false)
						return dlgFile.FileName;
					else
						return null;
		}

		/// <summary>
		///		Muestra un cuadro de mensaje
		/// </summary>
		public void ShowMessage(string strMessage)
		{	MainWindow.Dispatcher.Invoke(new Action(() => MessageBox.Show(MainWindow, strMessage, ApplicationName)), 
																	 System.Windows.Threading.DispatcherPriority.Normal);
		}

		/// <summary>
		///		Muestra un cuadro de mensaje
		/// </summary>
		public bool ShowQuestion(string strMessage)
		{ return MessageBox.Show(MainWindow, strMessage, ApplicationName, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
		}

		/// <summary>
		///		Muestra un cuadro de mensaje para introducir un texto
		/// </summary>
		public abstract ControllerWindow.ResultType ShowInputString(string strMessage, ref string strInput);

		/// <summary>
		///		Abre el cuadro de diálogo de selección de un directorio
		/// </summary>
		public ResultType OpenDialogSelectPath(string strPathSource, out string strPath)
		{	Ookii.Dialogs.Wpf.VistaFolderBrowserDialog dlgFolder = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
			ResultType intType;

				// Inicializa los valores de salida
					strPath = null;
				// Asigna la carpeta inicial
					dlgFolder.SelectedPath = strPathSource;
					dlgFolder.ShowNewFolderButton = true;
				// Muestra el diálogo
					intType = ConvertDialogResult(dlgFolder.ShowDialog());
				// Obtiene el directorio
					if (intType == ResultType.Yes)
						strPath = dlgFolder.SelectedPath;
				// Devuelve el resultado
					return intType;
		}

		/// <summary>
		///		Muestra una pregunta con tres posibles respuestas
		/// </summary>
		public ResultType ShowQuestionCancel(string strMessage)
		{ MessageBoxResult intResult = MessageBox.Show(strMessage, ApplicationName, MessageBoxButton.YesNoCancel);

				switch (intResult)
					{ case MessageBoxResult.Yes:
							return ResultType.Yes;
						case MessageBoxResult.No:
							return ResultType.No;
						default:
							return ResultType.Cancel;
					}
		}

		/// <summary>
		///		Muestra una notificación
		/// </summary>
		public virtual void ShowNotification(ControllerWindow.NotificationType intType, string strMessage, string strTitle, string strUrlImage = null)
		{ ShowMessage(strMessage);
		}

		/// <summary>
		///		Muestra una página en el navegador predeterminado
		/// </summary>
		public void ShowExternalBrowser(string strUrl)
		{ LibHelper.Files.HelperFiles.OpenDocumentShell(strUrl);
		}

		/// <summary>
		///		Nombre de la aplicación
		/// </summary>
		public string ApplicationName { get; set; }

		/// <summary>
		///		Ventana principal de la aplicación
		/// </summary>
		public Window MainWindow { get; private set; }
	}
}
