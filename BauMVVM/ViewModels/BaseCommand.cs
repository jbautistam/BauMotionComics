using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;

namespace Bau.Libraries.MVVM.ViewModels
{
	/// <summary>
	///		Clase base para los comandos
	/// </summary>
	public class BaseCommand : ICommand
	{ // Eventos públicos
			public event EventHandler CanExecuteChanged;
		// Variables privadas
			private readonly Action<object> objActionExecute = null;
			private readonly Predicate<object> objPredicateCanExecute = null;
			private GenericWeakEventManager<PropertyChangedEventArgs> objWeakEventManager = null; 

	  public BaseCommand(Action<object> objExecute, Predicate<object> objCanExecute = null, 
											 INotifyPropertyChanged objSource = null, string strPropertyName = null) : this(null, objExecute, objCanExecute, objSource, strPropertyName)
		{
		}

    public BaseCommand(string strCaption, Action<object> objExecute, Predicate<object> objCanExecute = null, 
											 INotifyPropertyChanged objSource = null, string strPropertyName = null)
    { Caption = strCaption;
			objActionExecute = objExecute;
      objPredicateCanExecute = objCanExecute;
			if (objSource != null)
				AddListener(objSource, strPropertyName);
		}

		/// <summary>
		///		Ejecuta un comando
		/// </summary>
    public void Execute(object objParameter)
    { if (objActionExecute != null)
				objActionExecute(objParameter);
    }

		/// <summary>
		///		Comprueba si se puede ejecutar un comando
		/// </summary>
    public bool CanExecute(object objParameter)
    { if (objPredicateCanExecute != null)
				return objPredicateCanExecute(objParameter);
			else
				return true;
    }

		/// <summary>
		///		Añade un listener de eventos al comando para una propiedad
		/// </summary>
		public BaseCommand AddListener<TEntity>(INotifyPropertyChanged objSource, Expression<Func<TEntity, object>> objProperty)
		{ return AddListener(objSource, GetPropertyName(objProperty));
		}

		/// <summary>
		///		Añade un listener de eventos al comando para un nombre de propiedad
		/// </summary>
		public BaseCommand AddListener(INotifyPropertyChanged objSource, string strPropertyName)
		{ // Añade el listener
				PropertyChangedEventManager.AddListener(objSource, WeakEventManager, strPropertyName);
			// Devuelve este objeto (permite un interface fluent)
				return this;
		}

		/// <summary>
		///		Traduce una expresión lambda en un nombre de propiedad / método
		/// </summary>
		private string GetPropertyName<TEntity>(Expression<Func<TEntity, object>> expression)
		{	LambdaExpression objLambda = expression as LambdaExpression;
			MemberExpression objMemberExpression;
			ConstantExpression objConstantExpression;
			PropertyInfo objPropertyInfo;

				// Obtiene la expresión
					if (objLambda.Body is UnaryExpression)
						objMemberExpression = (objLambda.Body as UnaryExpression).Operand as MemberExpression;
					else
						objMemberExpression = objLambda.Body as MemberExpression;
				// Obtiene una expresión constante
					objConstantExpression = objMemberExpression.Expression as ConstantExpression;
				// Obtiene la información de la propiedad
					objPropertyInfo = objMemberExpression.Member as PropertyInfo;
				// Obtiene el nombre de la propiedad
					return objPropertyInfo.Name;
		}

		/// <summary>
		///		Rutina a la que se llama para lanzar el evento de modificación de CanExecute
		/// </summary>
    public void OnCanExecuteChanged()
    { if (CanExecuteChanged != null)
				CanExecuteChanged(this, EventArgs.Empty);
    }

		/// <summary>
		///		Crea un manejador para INotifyPropertyChanged 
		/// </summary>
		private void RequeryCanExecute(object sender, PropertyChangedEventArgs evntArgsPropertyChanged)
		{ OnCanExecuteChanged();
		}

		/// <summary>
		///		Título del comando
		/// </summary>
		public string Caption { get; private set; }

		/// <summary>
		///		Manager de WeakEvents
		/// </summary>
		private GenericWeakEventManager<PropertyChangedEventArgs> WeakEventManager
		{ get
				{ // Crea el manager de eventos
						if (objWeakEventManager == null)
							objWeakEventManager = new GenericWeakEventManager<PropertyChangedEventArgs>(RequeryCanExecute);
					// Devuelve el manager de eventos
						return objWeakEventManager;
				}
		}
	}
}
