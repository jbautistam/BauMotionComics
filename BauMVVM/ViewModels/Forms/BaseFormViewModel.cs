using System;

namespace Bau.Libraries.MVVM.ViewModels.Forms
{
	/// <summary>
	///		ViewModel para las clases de ventana
	/// </summary>
	public abstract class BaseFormViewModel : BaseViewModel, Interfaces.IFormViewModel
	{	// Constantes protegidas
			protected const string cnstStrActionTypeClose = "Close";
			protected const string cnstStrActionTypeDelete = "Delete";
			protected const string cnstStrActionTypeRefresh = "Refresh";
			protected const string cnstStrActionTypeSave = "Save";
		// Eventos públicos
			public event EventHandler<EventArguments.CloseEventArgs> RequestClose;

		public BaseFormViewModel(System.Windows.Window wndOwner = null, bool blnChangeUpdated = true) : base(wndOwner, blnChangeUpdated) 
		{ MenuCompositionData = new Menus.MenuComposition();
			CloseCommand = new BaseCommand("Cerrar", objParameter => Close(Controllers.ControllerWindow.ResultType.No));
			RefreshCommand = new BaseCommand("Actualizar",
																			 objParameter => ExecuteAction(cnstStrActionTypeRefresh, objParameter), 
																			 objParameter => CanExecuteAction(cnstStrActionTypeRefresh, objParameter));
			SaveCommand = new BaseCommand("Guardar",
																		objParameter => ExecuteAction(cnstStrActionTypeSave, objParameter), 
																		objParameter => CanExecuteAction(cnstStrActionTypeSave, objParameter));
			DeleteCommand = new BaseCommand("Borrar",
																			objParameter => ExecuteAction(cnstStrActionTypeDelete, objParameter), 
																			objParameter => CanExecuteAction(cnstStrActionTypeDelete, objParameter));
		}

		/// <summary>
		///		Ejecuta una acción
		/// </summary>
		protected abstract void ExecuteAction(string strAction, object objParameter);

		/// <summary>
		///		Comprueba si se puede ejecutar una acción
		/// </summary>
		protected abstract bool CanExecuteAction(string strAction, object objParameter);

		/// <summary>
		///		Llama al evento de cierre de ventana
		/// </summary>
    public virtual void Close(Controllers.ControllerWindow.ResultType intResult)
    { RequestClose?.Invoke(this, new EventArguments.CloseEventArgs(intResult));
		}

		/// <summary>
		///		Menús
		/// </summary>
		public Menus.MenuComposition MenuCompositionData { get; private set; }

		/// <summary>
		///		Comando de cierre de la ventana
		/// </summary>
    public BaseCommand CloseCommand { get; private set; }

		/// <summary>
		///		Comando para actualización
		/// </summary>
		public BaseCommand RefreshCommand { get; private set; }

		/// <summary>
		///		Comando para grabación
		/// </summary>
		public BaseCommand SaveCommand { get; private set; }

		/// <summary>
		///		Comando para borrado
		/// </summary>
		public BaseCommand DeleteCommand { get; private set; }
	}
}
