using System;

namespace Bau.Libraries.MVVM.PluginsManager.Plugins
{
	/// <summary>
	///		Interface que deben cumplir los plugins
	/// </summary>
	public interface IPluginChildController
	{
		/// <summary>
		///		Obtiene el control de configuración
		/// </summary>
		System.Windows.Controls.UserControl GetConfigurationControl();

		/// <summary>
		///		Inicializa las propiedades de las librerías que se asocian a otros plugin
		/// </summary>
		void InitComunicationBetweenPlugins();

		/// <summary>
		///		Inicializa las librerías del plugin
		/// </summary>
		void InitLibraries(Libraries.MVVM.PluginsManager.Host.IHostController objHostController);

		/// <summary>
		///		Muestra los paneles del plugin
		/// </summary>
		void ShowPanes();

		/// <summary>
		///		Nombre del plugin
		/// </summary>
		string Name { get; }

		/// <summary>
		///		Controlador de la aplicación principal
		/// </summary>
		Host.IHostController HostController { get; }
	}
}
