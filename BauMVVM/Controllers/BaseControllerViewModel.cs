using System;

namespace Bau.Libraries.MVVM.Controllers
{
	/// <summary>
	///		Clase base para los controladores de ViewModel
	/// </summary>
	public abstract class BaseControllerViewModel
	{
		public BaseControllerViewModel(string strModuleName, PluginsManager.Host.IHostController objHostController, IControllerViews objViewController)
		{ ModuleName = strModuleName;
			HostController = objHostController;
			ViewsController = objViewController;
		}
		
		/// <summary>
		///		Inicializa la aplicación
		/// </summary>
		public abstract void InitModule();

		/// <summary>
		///		Inicializa la solicitud de información a otros plugins
		/// </summary>
		public virtual void InitComunicationBetweenPlugins()
		{ // ... no hace nada, simplemente crea el esqueleto de la función
		}

		/// <summary>
		///		Obtiene un parámetro
		/// </summary>
		protected string GetParameter(string strName)
		{ if (HostController != null)
				return HostController.Configuration.Parameters.GetValue(ModuleName, strName);
			else
				return "";
		}

		/// <summary>
		///		Asigna un parámetro
		/// </summary>
		/// <remarks>
		///		No tiene un if MainController == null porque me interesa que lance una excepción en ejecución si no se ha definido el controlador
		///	de la aplicación principal
		/// </remarks>
		protected void SetParameter(string strName, string strValue)
		{ HostController.Configuration.Parameters.SetValue(ModuleName, strName, strValue);
		}

		/// <summary>
		///		Asigna un parámetro
		/// </summary>
		protected void SetParameter(string strName, int intValue)
		{ SetParameter(strName, intValue.ToString());
		}

		/// <summary>
		///		Asigna un parámetro
		/// </summary>
		protected void SetParameter(string strName, bool blnValue)
		{ SetParameter(strName, blnValue.ToString());
		}

		/// <summary>
		///		Nombre del módulo
		/// </summary>
		public string ModuleName { get; private set; }															 

		/// <summary>
		///		Controlador de la aplicación principal
		/// </summary>
		public PluginsManager.Host.IHostController HostController { get; private set; }

		/// <summary>
		///		Controlador de vistas
		/// </summary>
		public virtual IControllerViews ViewsController { get; protected set; }
	}
}
