using System;

namespace Bau.Libraries.MVVM.Controllers.Messengers
{
	/// <summary>
	///		Controlador de mensajes para un controlador
	/// </summary>
	public abstract class PluginMessengerController
	{
		public PluginMessengerController(BaseControllerViewModel objViewModelController, PluginsManager.Host.IHostController objHost,
																		 bool blnReceiveMessage)
		{ ViewModelController = objViewModelController;
			Host = objHost;
			if (blnReceiveMessage)
				objHost.Messenger.Sent += (objSender, objEventArgs) =>
																			 { TreatMessage(objSender, objEventArgs.MessageSent);
																			 };
		}

		/// <summary>
		///		Trata el mensaje enviado por el host
		/// </summary>
		public abstract void TreatMessage(object objSender, Message objMesage);

		/// <summary>
		///		Controlador del ViewModel
		/// </summary>
		public BaseControllerViewModel ViewModelController { get; private set; }

		/// <summary>
		///		Host de la aplicación
		/// </summary>
		public PluginsManager.Host.IHostController Host { get; private set; }

		/// <summary>
		///		Mensajero del host
		/// </summary>
		public MessengerController HostMessenger
		{ get { return Host.Messenger; }
		}
	}
}
