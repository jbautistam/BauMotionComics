using System;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMotionComic.Model;
using Bau.Libraries.LibMotionComic.Model.Components;
using Bau.Libraries.LibMotionComic.Model.Components.Entities;

namespace Bau.Libraries.LibMotionComic.Repository.Reader
{
	/// <summary>
	///		Repository para la carga de cómics
	/// </summary>
	internal class ComicReaderRepository
	{ 
		/// <summary>
		///		Carga los datos de un cómic
		/// </summary>
		internal ComicModel Load(string strPath, string strFileName, bool blnLoadFull)
		{ ComicModel objComic = new ComicModel(strPath);
			string strFullFileName = System.IO.Path.Combine(strPath, strFileName);

				// Carga los datos del cómic
					// if (System.IO.File.Exists(strFullFileName))
						{ ComicReaderMediator objMediator = new ComicReaderMediator();

								//try
									{ LibMarkupLanguage.Services.XML.XMLParser objXmlParser = new LibMarkupLanguage.Services.XML.XMLParser();
										MLFile objMLFile = objXmlParser.Load(strFullFileName);

											// Recorre los nodos
												foreach (MLNode objMLNode in objMLFile.Nodes)
													if (objMLNode.Name == ComicRepositoryConstants.cnstStrTagRoot)
														{ // Obtiene el ancho y el alto
																objComic.Width = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagWidth].Value.GetInt(1000);
																objComic.Height = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagHeight].Value.GetInt(1000);
															// Asigna las propiedades básicas del cómic
																objComic.Title = objMLNode.Nodes[ComicRepositoryConstants.cnstStrTagTitle].Value.TrimIgnoreNull();
																objComic.Summary = objMLNode.Nodes[ComicRepositoryConstants.cnstStrTagSummary].Value.TrimIgnoreNull();
																objComic.ThumbFileName = objMLNode.Nodes[ComicRepositoryConstants.cnstStrTagThumbFileName].Value.TrimIgnoreNull();
															// Obtiene los componentes del cómic
																if (blnLoadFull)
																	foreach (MLNode objMLChild in objMLNode.Nodes)
																		switch (objMLChild.Name)
																			{	case ComicRepositoryConstants.cnstStrTagInclude:
																				case ComicRepositoryConstants.cnstStrTagResources:
																						objMediator.ResourcesRepository.LoadResources(strPath, objMLChild, objComic);
																					break;
																				case ComicRepositoryConstants.cnstStrTagPage:
																						objMediator.PagesRepository.LoadPage(objMLChild, objComic);
																					break;
																			}
																// Carga los idiomas (aunque no sea una carga completa)
																	LoadLanguage(objComic, objMLNode);
														}
									}
								//catch (Exception objException)
								//	{ System.Diagnostics.Debug.WriteLine(objException.Message);
								//	}
						}
				// Devuelve el cómic cargado
					return objComic;
		}

		/// <summary>
		///		Carga el idioma
		/// </summary>
		private void LoadLanguage(ComicModel objComic, MLNode objMLNode)
		{ // Carga los idiomas
				foreach (MLNode objMLChild in objMLNode.Nodes)
					if (objMLChild.Name == ComicRepositoryConstants.cnstStrTagLanguage)
						{ string strKey = objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value;

								if (!objMLChild.Value.IsEmpty() && !strKey.IsEmpty())
									objComic.Languages.Add(strKey,
																				 new LanguageModel(strKey, objMLChild.Value,
																													 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagDefault].Value.GetBool()));
						}
			// Asigna los predeterminados
				if (objComic.Languages.Count > 0)
					{ bool blnExistsDefault = false;

							// Comprueba si existe un elemento predeterminado
								foreach (System.Collections.Generic.KeyValuePair<string, AbstractComponentModel> objItem in objComic.Languages)
									if (objItem.Value is LanguageModel)
										{ LanguageModel objLanguage = objItem.Value as LanguageModel;

												if (objLanguage.IsDefault)
													blnExistsDefault = true;
										}
							// Si no existe ningún elemento predeterminado
								if (!blnExistsDefault)
									foreach (System.Collections.Generic.KeyValuePair<string, AbstractComponentModel> objItem in objComic.Languages)
										if (!blnExistsDefault && objItem.Value is LanguageModel)
											{ LanguageModel objLanguage = objItem.Value as LanguageModel;

													// Asigna el valor que indica si es predeterminado
														objLanguage.IsDefault = true;
													// Indica que ya se ha asignado un valor predeterminado
														blnExistsDefault = true;
											}
					}
		}
	}
}
