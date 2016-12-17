using System;

using Bau.Libraries.MVVM.Controllers;

namespace Bau.Libraries.MVVM.PluginsManager.Plugins
{
	/// <summary>
	///		Base para los controladores de plugins
	/// </summary>
	public abstract class BasePluginController : IPluginChildController
	{
		public BasePluginController() : this(null, null) {}

		public BasePluginController(Host.IHostController objHostController, string strName)
		{ HostController = objHostController;
			Name = strName;
		}

		/// <summary>
		///		Inicializa las librerías del plugin
		/// </summary>
		public abstract void InitLibraries(Host.IHostController objHostController);

		/// <summary>
		///		Inicializa las propiedades de las librerías que se asocian a otros plugin
		/// </summary>
		public virtual void InitComunicationBetweenPlugins()
		{ // No hace nada, crea una interface
		}

		/// <summary>
		///		Muestra los paneles del plugin
		/// </summary>
		public abstract void ShowPanes();

		/// <summary>
		///		Obtiene el control de configuración
		/// </summary>
		public abstract System.Windows.Controls.UserControl GetConfigurationControl();

		/// <summary>
		///		Nombre del plugin
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///		Controlador de la aplicación principal
		/// </summary>
		public Host.IHostController HostController { get; set; }
	}
}
