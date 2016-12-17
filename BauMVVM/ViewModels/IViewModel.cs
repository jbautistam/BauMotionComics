using System;

namespace Bau.Libraries.MVVM.ViewModels
{
	/// <summary>
	///		Interface para los ViewModel
	/// </summary>
	public interface IViewModel : System.ComponentModel.INotifyPropertyChanged
	{
		/// <summary>
		///		Rutina que avisa del cierre del ViewModel
		/// </summary>
		void CloseViewModel();

		/// <summary>
		///		Indica si los datos del modelo se han modificado
		/// </summary>
		bool IsUpdated { get; set; }

		/// <summary>
		///		Ventana asociada al ViewModel
		/// </summary>
		System.Windows.Window WindowOwner { get; set; }
	}
}
