using System;

namespace Bau.Libraries.MVVM.Views.Configuration
{
	/// <summary>
	///		Interface de las vistas de los controles de usuario de configuración
	/// </summary>
	public interface IUserControlConfigurationView
	{
		/// <summary>
		///		Comprueba los datos introducidos por el usuario
		/// </summary>
		bool ValidateData(out string strError);

		/// <summary>
		///		Graba los datos introducidos por el usuario
		/// </summary>
		void Save();
	}
}
