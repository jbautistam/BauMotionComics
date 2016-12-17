using System;

using Bau.Libraries.MVVM.ViewModels;

namespace Bau.Libraries.MVVM.ViewModels.Forms.Interfaces
{
	/// <summary>
	///		Interface para el ViewModel de los paneles
	/// </summary>
	public interface IPaneViewModel : IFormViewModel
	{
		/// <summary>
		///		Comando para crear un nuevo elemento
		/// </summary>
		BaseCommand NewCommand { get; }

		/// <summary>
		///		Comando para mostrar las propiedades de un elemento
		/// </summary>
		BaseCommand PropertiesCommand { get; }
	}
}
