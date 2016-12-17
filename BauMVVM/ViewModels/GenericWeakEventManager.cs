using System;
using System.Windows;

namespace Bau.Libraries.MVVM.ViewModels
{
	/// <summary>
	///		Tratamiento de un WeakEvent
	/// </summary>
	public sealed class GenericWeakEventManager<TypeEventArgs> : IWeakEventListener where TypeEventArgs : EventArgs
	{ 
		public GenericWeakEventManager(EventHandler<TypeEventArgs> evntHandler)
		{ EventHandler = evntHandler;
		}

		public bool ReceiveWeakEvent(Type managerType, Object sender, EventArgs e)
		{ // Lanza el evento
				EventHandler?.Invoke(sender, e as TypeEventArgs);
			// Indica que ha tratado el evento
			return true;
		}

		/// <summary>
		///		Rutina de tratamiento del evento
		/// </summary>
		private EventHandler<TypeEventArgs> EventHandler { get; set; }
	}
}
