using System;
using System.Windows;
using System.Windows.Controls;

using Bau.Libraries.LibHelper.Extensors;

namespace Bau.Controls.Files
{
	/// <summary>
	///		Control de usuario para selección de un archivo
	/// </summary>
	public partial class FileSelect : UserControl
	{
		/// <summary>
		///		Modo de selección del archivo
		/// </summary>
		public enum ModeType
			{ 
				/// <summary>Cargar</summary>
				Load,
				/// <summary>Grabar</summary>
				Save
			}
		// Propiedades
			public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register("FileName", typeof(string), typeof(FileSelect), 
			                                                                                         new FrameworkPropertyMetadata("", 
			                                                                                                                       FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
			public static readonly DependencyProperty PathBaseProperty = DependencyProperty.Register("PathBase", typeof(string), typeof(FileSelect), 
			                                                                                         new FrameworkPropertyMetadata("", 
			                                                                                                                       FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
			public static readonly DependencyProperty MaskProperty = DependencyProperty.Register("Mask", typeof(string), typeof(FileSelect), 
			                                                                                     new FrameworkPropertyMetadata("Todos los archivos (*.*)|*.*", 
			                                                                                                                   FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
			public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(ModeType), typeof(FileSelect), 
			                                                                                     new FrameworkPropertyMetadata(ModeType.Load, 
			                                                                                                                   FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
		// Eventos
			public event EventHandler Changed;
		//	public static readonly RoutedEvent ChangedEvent = EventManager.RegisterRoutedEvent("Changed", RoutingStrategy.Bubble, 
		//																																										 typeof(RoutedPropertyChangedEventHandler<string>), 
		//																																										 typeof(FileSelect));

		public FileSelect()
		{ InitializeComponent();
			grdFileSelect.DataContext = this;
		}

		/// <summary>
		///		Abre el cuadro de diálogo apropiado
		/// </summary>
		private void OpenDialog()
		{ string strPath = null, strExtension = null;
			string strFileName = FileName;

				// Obtiene el directorio
					if (!strFileName.IsEmpty())
						{ strPath = System.IO.Path.GetDirectoryName(strFileName);
							strFileName = System.IO.Path.GetFileName(strFileName);
							strExtension = System.IO.Path.GetExtension(strFileName);
						}
					else
						strPath = PathBase;
				// Abre el cuadro de diálogo apropiado
					if (Mode == ModeType.Load)
						strFileName = OpenDialogLoad(strPath, Mask, strFileName, strExtension);
					else
						strFileName = OpenDialogSave(strPath, Mask, strFileName, strExtension);
				// Asigna el nombre de archivo
					if (!strFileName.IsEmpty())
						FileName = strFileName;
		}

		/// <summary>
		///		Abre el cuadro de diálogo de carga de archivos
		/// </summary>
		private string OpenDialogLoad(string strDefaultPath, string strFilter, string strDefaultFileName = null, string strDefaultExtension = null)
		{ Microsoft.Win32.OpenFileDialog dlgFile = new Microsoft.Win32.OpenFileDialog();

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
		///		Abre el cuadro de diálogo de grabación de archivos
		/// </summary>
		private string OpenDialogSave(string strDefaultPath, string strFilter, string strDefaultFileName = null, string strDefaultExtension = null)
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
		///		Nombre de archivo
		/// </summary>
		public string FileName
		{ get { return GetValue(FileNameProperty) as string; }
			set 
				{ SetValue(FileNameProperty, value); 
					OnChanged();
				}
		}

		/// <summary>
		///		Directorio base
		/// </summary>
		public string PathBase
		{ get { return GetValue(PathBaseProperty) as string; }
			set 
				{ SetValue(PathBaseProperty, value); 
					OnChanged();
				}
		}

		/// <summary>
		///		Máscara de archivo
		/// </summary>
		public string Mask
		{ get { return GetValue(MaskProperty) as string; }
			set { SetValue(MaskProperty, value); }
		}

		/// <summary>
		///		Modo de selección de archivo
		/// </summary>
		public ModeType Mode
		{ get
				{ object objProperty = GetValue(ModeProperty);

						if (objProperty != null && objProperty is FileSelect.ModeType)
							return (FileSelect.ModeType) ((int) objProperty);
						else
							return ModeType.Load;
				}
			set { SetValue(ModeProperty, ((int) value).ToString()); }
		}

		///// <summary> 
		/////		Ocurre cuando cambia la propiedad con el nombre de archivo
		///// </summary> 
		//public event RoutedPropertyChangedEventHandler<string> Changed
		//{ add { AddHandler(ChangedEvent, value); }
		//	remove { RemoveHandler(ChangedEvent, value); }
		//}

		/// <summary> 
		///		Lanza el evento Changed
		/// </summary> 
		protected virtual void OnChanged()
		{ if (Changed != null)
				Changed(this, EventArgs.Empty);
		}

		private void cmdSelect_Click(object sender, RoutedEventArgs e)
		{ OpenDialog();
		}
	}
}
