using System;

using Bau.Libraries.LibHelper.Extensors;

namespace Bau.Libraries.MVVM.Controllers.Settings
{
	/// <summary>
	///		Clase de configuración de las aplicaciones
	/// </summary>
	public class Configuration
	{	// Variables privadas
			private ParametersModelCollection objColParameters;
			private string strPathBaseConfiguration;

		public Configuration(string strShortApplicationName, string strPathBaseData)
		{ ShortApplicationName = strShortApplicationName;
			PathBaseData = strPathBaseData;
		}

		/// <summary>
		///		Carga la configuración
		/// </summary>
		public void Load()
		{ Parameters = new ParametersRepository().Load(FileName);
		}

		/// <summary>
		///		Graba la configuración
		/// </summary>
		public void Save()
		{ new ParametersRepository().Save(FileName, Parameters);
		}

		/// <summary>
		///		Nombre corto de la aplicación
		/// </summary>
		public string ShortApplicationName { get; private set; }

		/// <summary>
		///		Directorio de datos de la aplicación
		/// </summary>
		public string PathBaseData
		{ get { return strPathBaseConfiguration; }
			set 
				{ if (value.IsEmpty())
						strPathBaseConfiguration = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ShortApplicationName); 
					else
						strPathBaseConfiguration = value; 
				}
		}
		
		/// <summary>
		///		Directorio de la aplicación
		/// </summary>
		public string PathBaseApplication
		{ get { return AppDomain.CurrentDomain.BaseDirectory; }
		}

		/// <summary>
		///		Nombre de archivo de configuración
		/// </summary>
		public string FileName 
		{ get { return System.IO.Path.Combine(PathBaseData, "Configuration.xml"); }
		}

		/// <summary>
		///		Paramétros de configuración
		/// </summary>
		public ParametersModelCollection Parameters 
		{ get
				{ // Carga los parámetros
						if (objColParameters == null)
							Load();
					// Devuelve los parámetros
						return objColParameters;
				}
			private set { objColParameters = value; }
		}
	}
}
