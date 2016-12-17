using System;

namespace Bau.Libraries.MVVM.Controllers.Processes
{
	/// <summary>
	///		Clase base para los procesos planificados
	/// </summary>
	public abstract class AbstractPlannedProcess : AbstractBaseProcess
	{ 
		public AbstractPlannedProcess(string strSource, string strName, string strDescription, int intMinutes, bool blnEnabled = true) : base(strSource, strName, strDescription)
		{ Minutes = intMinutes;
			Enabled = blnEnabled;
			DateLast = DateTime.Now.AddMinutes(-1 * (intMinutes + 1));
		}

		/// <summary>
		///		Indica si se debe ejecutar
		/// </summary>
		internal bool MustExecute()
		{ return Enabled && DateLast.AddMinutes(Minutes) < DateTime.Now && !IsProcessing;
		}

		/// <summary>
		///		Procesa los datos en un hilo
		/// </summary>
		internal void Process()
		{	System.Threading.Tasks.Task objTaskCompiler = new System.Threading.Tasks.Task(() => 
																													{ // Indica que está procesando
																															IsProcessing = true;
																														// Ejecuta
																															Execute();
																														// Indica que ha terminado el proceso
																															IsProcessing = false;
																													});

				// Arranca el hilo
					objTaskCompiler.Start();
				// Marca la fecha de proceso
					DateLast = DateTime.Now;
		}

		/// <summary>
		///		Procesa internamente los datos
		/// </summary>
		protected abstract void Execute();

		/// <summary>
		///		Minutos entre procesos
		/// </summary>
		public virtual int Minutes { get; protected set; }

		/// <summary>
		///		Indica si el proceso está activo
		/// </summary>
		public virtual bool Enabled { get; set; }

		/// <summary>
		///		Indica si se está ejecutando
		/// </summary>
		internal bool IsProcessing { get; private set; }

		/// <summary>
		///		Fecha del último proceso
		/// </summary>
		internal DateTime DateLast { get; private set; }
	}
}
