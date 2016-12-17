using System;

namespace Bau.Libraries.MVVM.PluginsManager.Plugins
{
	/// <summary>
	///		Interface para los datos de un plugin
	/// </summary>
	public interface IPluginChildData
	{
		/// <summary>
		///		Nombre del plugin
		/// </summary>
		string Name { get; }

		/// <summary>
		///		Descripción del plugin
		/// </summary>
		string Description { get; }
	}
}
