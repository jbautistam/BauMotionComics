using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows;

using Bau.Libraries.LibHelper.Extensors;

namespace Bau.Libraries.MVVM.ViewModels
{
	/// <summary>
	///		Base para objetos observables (implementan INotifyPropertyChanged)
	/// </summary>
	public abstract class BaseViewModel : IViewModel
	{ // Eventos públicos
			public event PropertyChangedEventHandler PropertyChanged;
		
		public BaseViewModel(Window frmOwner = null, bool blnChangeUpdated = true)
		{ WindowOwner = frmOwner;
			ChangeUpdated = blnChangeUpdated;
		}

		/// <summary>
		///		Comprueba si se debe modificar un valor de una propiedad
		/// </summary>
		protected bool CheckProperty<TypeData>(ref TypeData objValue, TypeData objNewValue, [CallerMemberName] string strProperty = null)
		{ if (!object.Equals(objValue, objNewValue))
				{ // Cambia el valor
						objValue = objNewValue;
					// Lanza el evento
						OnPropertyChanged(strProperty);
					// Devuelve el valor que indica que se ha modificado
						return true;
				}
			else // ... los objetos son iguales y no se modifica el valor
				return false;
		}

		/// <summary>
		///		Lanza el evento de cambio de propiedad
		/// </summary>
		internal void RaiseEventPropertyChanged(string strProperty)
		{ OnPropertyChanged(strProperty);
		}

		/// <summary>
		///		Lanza el evento <see cref="PropertyChanged"/>
		/// </summary>
		protected virtual void OnPropertyChanged([CallerMemberName] string strProperty = null)
		{ OnPropertyChanged(ChangeUpdated, strProperty);
		}

		/// <summary>
		///		Lanza el evento <see cref="PropertyChanged"/>
		/// </summary>
		protected virtual void OnPropertyChanged(bool blnChangeUpdated, [CallerMemberName] string strProperty = null)
		{ // Indica que se ha modificado
			//! Antes de lanzar el evento para darle la oportunidad a los objetos hijo a cambiar el valor de IsUpdated = false
				if (blnChangeUpdated)
					IsUpdated = true;
			// Lanza el evento
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(strProperty));
		}

		/// <summary>
		///		Rutina que avisa del cierre del ViewModel por si hay que hacer alguna rutina posterior (simplemente implementa
		///	el interface y deja a los ViewModel hijo que implementen sus propias rutinas de cierre)
		/// </summary>
		public virtual void CloseViewModel()
		{
		}

		/// <summary>
		///		Indica si los datos del modelo se han modificado
		/// </summary>
		public bool IsUpdated { get; set; }

		/// <summary>
		///		Indica si se debe cambiar el valor de IsUpdated en el formulario
		/// </summary>
		public bool ChangeUpdated { get; set; }

		/// <summary>
		///		Ventana asociada al ViewModel
		/// </summary>
		public Window WindowOwner { get; set; }
	}
}