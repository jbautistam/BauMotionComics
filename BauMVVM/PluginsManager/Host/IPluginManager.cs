using System;

namespace Bau.Libraries.MVVM.PluginsManager.Host
{
	/// <summary>
	///		Interface para las clases controladores de plugins
	/// </summary>
	public interface IPluginManager
	{
		/// <summary>
		///		Inicializa los plugins
		/// </summary>
		void InitPlugins(Host.IHostController objHostController, out string strError);

		/// <summary>
		///		Muestra los paneles de los plugins
		/// </summary>
		void ShowPanes(out string strError);

		/// <summary>
		///		Colección de controladores de plugins
		/// </summary>
		System.Collections.Generic.List<Plugins.IPluginChildController> PluginsController { get; }
	}
}
