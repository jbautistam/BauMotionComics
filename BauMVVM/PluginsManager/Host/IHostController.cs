using System;
using System.Windows.Controls;

namespace Bau.Libraries.MVVM.PluginsManager.Host
{
	/// <summary>
	///		Interface para el controlador de Layout del host
	/// </summary>
	public interface IHostController
	{ 
		/// <summary>
		///		Nombre de la aplicación
		/// </summary>
		string ApplicationName { get; }

		/// <summary>
		///		Ventana principal
		/// </summary>
		System.Windows.Window MainWindow { get; }

		/// <summary>
		///		Controlador del layout
		/// </summary>
		Views.Layout.ILayoutController LayoutController { get; }

		/// <summary>
		///		Controlador de la configuración de las aplicaciones
		/// </summary>
		Controllers.Settings.Configuration Configuration { get; }

		/// <summary>
		///		Controlador para envío de mensajes de la aplicación
		/// </summary>
		Controllers.Messengers.MessengerController Messenger { get; }

		/// <summary>
		///		Controlador de mensajes de Windows
		/// </summary>
		Controllers.IControllerWindow ControllerWindow { get; }

		/// <summary>
		///		Cola de procesos
		/// </summary>
		Controllers.Processes.TasksQueue TasksProcessor { get; }

		/// <summary>
		///		Planificador de procesos
		/// </summary>
		Controllers.Processes.SchedulerController Scheduler { get; }
	}
}
