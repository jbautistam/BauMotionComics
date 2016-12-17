using System;

namespace Bau.Libraries.MVVM.Controllers.Processes
{
	/// <summary>
	///		Clase base para los procesos
	/// </summary>
	public abstract class AbstractBaseProcess
	{ // Eventos
			public event EventHandler<EventArguments.ActionEventArgs> ActionProcess;
			public event EventHandler<EventArguments.ProgressActionEventArgs> ProgressAction;
			public event EventHandler<EventArguments.EndProcessEventArgs> EndProcess;
			public event EventHandler<EventArguments.ProgressEventArgs> Progress;

		public AbstractBaseProcess(string strSource, string strName, string strDescription)
		{ Source = strSource;
			Name = strName;
			Description = strDescription;
		}

		/// <summary>
		///		Nombre
		/// </summary>
		public string Name { get; }

		/// <summary>
		///		Descripción
		/// </summary>
		public string Description { get; }

		/// <summary>
		///		Origen del proceso
		/// </summary>
		public string Source { get; private set; }

		/// <summary>
		///		Lanza el evento de inicio
		/// </summary>
		protected void RaiseEventStart(string strMessage)
		{ RaiseEvent(EventArguments.ActionEventArgs.ActionType.Start, strMessage);
		}

		/// <summary>
		///		Lanza un evento de error
		/// </summary>
		protected void RaiseEventError(string strMessage, Exception objException)
		{ if (objException != null)
				strMessage += Environment.NewLine + objException.Message;
			RaiseEvent(EventArguments.ActionEventArgs.ActionType.Error, strMessage);
		}

		/// <summary>
		///		Lanza el evento de fin
		/// </summary>
		protected void RaiseEventEnd(string strMessage)
		{ RaiseEvent(EventArguments.ActionEventArgs.ActionType.Start, strMessage);
		}

		/// <summary>
		///		Lanza un mensaje en un evento
		/// </summary>
		protected void RaiseMessageEvent(string strMessage)
		{ RaiseEvent(EventArguments.ActionEventArgs.ActionType.Other, strMessage);
		}

		/// <summary>
		///		Lanza un evento
		/// </summary>
		protected void RaiseEvent(EventArguments.ActionEventArgs.ActionType intAction, string strMessage)
		{ if (ActionProcess != null)
				ActionProcess(this, new EventArguments.ActionEventArgs(intAction, Source, strMessage));
		}

		/// <summary>
		///		Lanza un evento de progreso
		/// </summary>
		protected void RaiseEventProgress(int intActual, int intTotal, string strMessage)
		{ if (Progress != null)
				Progress(this, new EventArguments.ProgressEventArgs(intActual, intTotal, strMessage));
		}

		/// <summary>
		///		Lanza un evento de fin de proceso
		/// </summary>
		protected void RaiseEventEndProcess(string strMessage, System.Collections.Generic.List<string> objColErrors)
		{	EndProcess?.Invoke(this, new EventArguments.EndProcessEventArgs(strMessage, objColErrors));
		}

		/// <summary>
		///		Lanza un evento de progreso de una acción
		/// </summary>
		protected void RaiseEventProgressAction(string strID, string strAction, string strProcess, long lngActual, long lngTotal)
		{ ProgressAction?.Invoke(this, new EventArguments.ProgressActionEventArgs(strID, Source, strAction, strProcess, lngActual, lngTotal));
		}
	}
}