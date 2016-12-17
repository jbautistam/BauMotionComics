using System;
using System.Collections.Generic;

namespace Bau.Libraries.MVVM.Controllers.Processes
{
	/// <summary>
	///		Controlador de procesos planificados
	/// </summary>
	public class SchedulerController
	{ // Variables privadas
			private List<AbstractPlannedProcess> objColPlanned;
			private System.Timers.Timer tmrScheduler;
		
		public SchedulerController()
		{ objColPlanned = new List<AbstractPlannedProcess>();
			tmrScheduler = new System.Timers.Timer(60000);
			tmrScheduler.Elapsed += (objSender, objEvntArgs) => Process();
		}

		/// <summary>
		///		Añade un proceso a la colección
		/// </summary>
		public void AddProcess(AbstractPlannedProcess objProcess)
		{ objColPlanned.Add(objProcess);
		}

		/// <summary>
		///		Arranca el temporizador
		/// </summary>
		public void Start()
		{ Enabled = true;
			tmrScheduler.Start();
		}

		/// <summary>
		///		Detiene el temporizador
		/// </summary>
		public void Stop()
		{ Pause();
			Enabled = false;
		}

		/// <summary>
		///		Pausa el temporizador
		/// </summary>
		private void Pause()
		{ tmrScheduler.Stop();
		}

		/// <summary>
		///		Arranca los procesos
		/// </summary>
		private void Process()
		{ if (Enabled)
				{	// Detiene el temporizador para evitar llamada reentrantes
						Pause();
					// Realiza los procesos
						foreach (AbstractPlannedProcess objProcess in objColPlanned)
							if (objProcess.MustExecute())
								objProcess.Process();
					// Reinicia el temporizador
						tmrScheduler.Start();
			}
		}

		/// <summary>
		///		Indica si el planificador está activo
		/// </summary>
		public bool Enabled { get; set; }
	}
}
