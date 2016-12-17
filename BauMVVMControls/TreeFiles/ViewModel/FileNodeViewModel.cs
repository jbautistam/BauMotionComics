using System;

using Bau.Libraries.MVVM.ViewModels.TreeItems;

namespace Bau.Controls.BauMVVMControls.TreeFiles.ViewModel
{
	/// <summary>
	///		Nodo del árbol para un archivo o carpeta
	/// </summary>
	public class FileNodeViewModel : TreeViewItemViewModel
	{	
		public FileNodeViewModel(string strFileName, TreeViewItemViewModel objParent) 
										: base(strFileName, System.IO.Path.GetFileName(strFileName), objParent, true)
		{ File = strFileName;
			if (IsFolder)
				Image = "/BauMVVMControls;component/Themes/Images/Folder.png";
			else
				Image = "/BauMVVMControls;component/Themes/Images/File.png";
		}

		/// <summary>
		///		Carga los hijos del nodo
		/// </summary>
		public override void LoadChildren()
		{ if (IsFolder)
				try
					{ // Carga los directorios
							foreach (string strPath in System.IO.Directory.GetDirectories(File))
								Children.Add(new FileNodeViewModel(strPath, this));
						// Carga los archivos
							foreach (string strFile in System.IO.Directory.GetFiles(File))
								Children.Add(new FileNodeViewModel(strFile, this));
					}
				catch (Exception objException)
					{ System.Diagnostics.Debug.WriteLine("Excepción: " + objException.Message);
					}
		}

		/// <summary>
		///		Archivo
		/// </summary>
		public string File { get; private set; }

		/// <summary>
		///		Indica si es una carpeta
		/// </summary>
		public bool IsFolder
		{ get { return System.IO.Directory.Exists(File); }
		}
	}
}
