using System;

namespace Bau.Libraries.MVVM.ViewModels.Forms
{
	/// <summary>
	///		Clase base para los ViewModel de un panel de herramientas
	/// </summary>
	public abstract class BasePaneViewModel : BaseFormViewModel, Interfaces.IPaneViewModel
	{ // Constantes protegidas
			protected const string cnstStrActionPaneTypeNew = "New";
			protected const string cnstStrActionPaneTypeProperties = "Properties";
		// Variables privadas
			private BaseCommand objNewCommand, objPropertiesCommand;

		/// <summary>
		///		Comando para un nuevo elemento
		/// </summary>
		public BaseCommand NewCommand
		{ get
			{ // Asigna el comando
					if (objNewCommand == null)
						objNewCommand = new BaseCommand("Nuevo",
																						objParameter => ExecuteAction(cnstStrActionPaneTypeNew, objParameter),
																						objParameter => CanExecuteAction(cnstStrActionPaneTypeNew, objParameter));
				// Devuelve el comando
					return objNewCommand;
			}
		}

		/// <summary>
		///		Comando para mostrar las propiedades del elemento
		/// </summary>
		public BaseCommand PropertiesCommand
		{ get
			{ // Asigna el comando
					if (objPropertiesCommand == null)
						objPropertiesCommand = new BaseCommand("Propiedades",
																									 objParameter => ExecuteAction(cnstStrActionPaneTypeProperties, objParameter),
																									 objParameter => CanExecuteAction(cnstStrActionPaneTypeProperties, objParameter));
				// Devuelve el comando
					return objPropertiesCommand;
			}
		}
	}
}
