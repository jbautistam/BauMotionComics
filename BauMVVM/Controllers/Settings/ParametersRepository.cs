using System;

using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;

namespace Bau.Libraries.MVVM.Controllers.Settings
{
	/// <summary>
	///		Repository de <see cref="ParameterModel"/>
	/// </summary>
	public class ParametersRepository
	{ // Constantes privadas
			private const string cnstStrTagRoot = "Parameters";
			private const string cnstStrTagParameter = "Parameter";
			private const string cnstStrAttributeApplication = "Application";
			private const string cnstStrAttributeName = "Name";

		/// <summary>
		///		Carga un archivo de parámetros
		/// </summary>
		public ParametersModelCollection Load(string strFileName)
		{ ParametersModelCollection objColParameters = new ParametersModelCollection();

				// Carga el archivo
					if (System.IO.File.Exists(strFileName))
						{ MLFile objMLFile = new XMLParser().Load(strFileName);

								foreach (MLNode objMLNode in objMLFile.Nodes)
									if (objMLNode.Name == cnstStrTagRoot)
										foreach (MLNode objMLChild in objMLNode.Nodes)
											if (objMLChild.Name == cnstStrTagParameter)
												objColParameters.Add(new ParameterModel(objMLChild.Attributes[cnstStrAttributeApplication].Value,
																																objMLChild.Attributes[cnstStrAttributeName].Value,
																																objMLChild.Value));
						}
				// Devuelve los parámetros
					return objColParameters;
		}

		/// <summary>
		///		Graba un archivo de parámetros
		/// </summary>
		public void Save(string strFileName, ParametersModelCollection objColParameters)
		{ MLFile objMLFile = new MLFile();
			MLNode objMLNode = objMLFile.Nodes.Add(cnstStrTagRoot);

				// Añade los nodos
					foreach (ParameterModel objParameter in objColParameters)
						{ MLNode objMLChild = objMLNode.Nodes.Add(cnstStrTagParameter, objParameter.Value);

								// Añade los atributos del nodo
									objMLChild.Attributes.Add(cnstStrAttributeApplication, objParameter.Application);
									objMLChild.Attributes.Add(cnstStrAttributeName, objParameter.Name);
						}
				// Crea el directorio del archivo
					LibHelper.Files.HelperFiles.MakePath(System.IO.Path.GetDirectoryName(strFileName));
				// Graba el archivo
					new XMLWriter().Save(objMLFile, strFileName);
		}
	}
}
