using System;

namespace Bau.Libraries.MVVM.Views.Interfaces
{
	/// <summary>
	///		Interface para las vistas de formulario
	/// </summary>
	public interface IFormView<TypeViewModel> where TypeViewModel : ViewModels.BaseViewModel
	{
		/// <summary>
		///		Datos del ViewModel
		/// </summary>
		TypeViewModel ViewModelData { get; }
	}
}
