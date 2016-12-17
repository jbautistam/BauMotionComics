using System;

namespace Bau.Libraries.MVVM.ViewModels.EventArguments
{
	/// <summary>
	///		Argumentos del evento de cierre de una ventana
	/// </summary>
	public class CloseEventArgs : EventArgs
	{
		public CloseEventArgs(Controllers.ControllerWindow.ResultType intResult)
		{ Result = intResult;
		}

		/// <summary>
		///		Resultado del cierre de la ventana
		/// </summary>
		public Controllers.ControllerWindow.ResultType Result { get; private set; }

		/// <summary>
		///		Boolean compatible con el resultado de un cuadro de diálogo
		/// </summary>
		public bool? DialogResult
		{ get
				{ switch (Result)
						{ case Controllers.ControllerWindow.ResultType.Yes:
								return true;
							case Controllers.ControllerWindow.ResultType.No:
								return false;
							default:
								return null;
						}
				}
		}
	}
}
