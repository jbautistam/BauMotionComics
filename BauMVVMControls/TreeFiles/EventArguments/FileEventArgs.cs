using System;

namespace Bau.Controls.BauMVVMControls.TreeFiles.EventArguments
{
	/// <summary>
	///		Argumento de los eventos de tratamiento de archivos
	/// </summary>
	public class FileEventArgs : EventArgs
	{
		public FileEventArgs(string strFileName)
		{ FileName = strFileName;
		}

		/// <summary>
		///		Nombre de archivo
		/// </summary>
		public string FileName { get; }
	}
}
