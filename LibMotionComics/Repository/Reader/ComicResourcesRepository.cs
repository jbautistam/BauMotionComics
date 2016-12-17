using System;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMotionComic.Model;
using Bau.Libraries.LibMotionComic.Model.Components.PageItems;
using Bau.Libraries.LibMotionComic.Model.Components.PageItems.Brushes;

namespace Bau.Libraries.LibMotionComic.Repository.Reader
{
	/// <summary>
	///		Repository para la carga de recursos de Cómic
	/// </summary>
	internal class ComicResourcesRepository
	{
		internal ComicResourcesRepository(ComicReaderMediator objMediator)
		{ Mediator = objMediator;
		}

		/// <summary>
		///		Carga los recursos
		/// </summary>
		internal void LoadResources(string strPath, MLNode objMLChild, ComicModel objComic)
		{ string strFileName = objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagFileName].Value;

				// Carga los recursos del nodo o un nuevo archivo
					if (!strFileName.IsEmpty())
						LoadFileResource(System.IO.Path.Combine(strPath, strFileName), objComic);
					else
						LoadResources(objMLChild, objComic);
		}

		/// <summary>
		///		Carga un archivo de recursos
		/// </summary>
		private void LoadFileResource(string strFileName, ComicModel objComic)
		{ // if (System.IO.File.Exists(strFileName))
				{ MLFile objMLFile = new LibMarkupLanguage.Services.XML.XMLParser().Load(strFileName);

						foreach (MLNode objMLNode in objMLFile.Nodes)
							if (objMLNode.Name == ComicRepositoryConstants.cnstStrTagResources)
								LoadResources(objMLNode, objComic);
				}
		}

		/// <summary>
		///		Carga los recursos de un cómic
		/// </summary>
		private void LoadResources(MLNode objMLNode, ComicModel objComic)
		{ foreach (MLNode objMLChild in objMLNode.Nodes)
				switch (objMLChild.Name)
					{	case ComicRepositoryConstants.cnstStrTagImage:
								LoadImageResource(objMLChild, objComic);
							break;
						case ComicRepositoryConstants.cnstStrTagShape:
								LoadShapeResource(objMLChild, objComic);
							break;
						default:
								LoadBrushResource(objMLChild, objComic);
							break;
					}
		}

		/// <summary>
		///		Carga un recurso de imagen
		/// </summary>
		private void LoadImageResource(MLNode objMLNode, ComicModel objComic)
		{ ImageModel objImage = Mediator.PagesRepository.LoadContentImage(null, objComic, objMLNode);

				// Añade la imagen a la lista de recursos
					objComic.Resources.Add(objImage.Key, objImage);
		}

		/// <summary>
		///		Carga un recurso de figura
		/// </summary>
		private void LoadShapeResource(MLNode objMLNode, ComicModel objComic)
		{ ShapeModel objShape = Mediator.ShapesRepository.LoadShape(null, objMLNode);

				// Añade la figura
					objComic.Resources.Add(objShape.Key, objShape);
		}

		/// <summary>
		///		Carga los recursos de brocha
		/// </summary>
		private void LoadBrushResource(MLNode objMLNode, ComicModel objComic)
		{ AbstractBaseBrushModel objBrush = Mediator.BrushesRepository.LoadBrush(objMLNode);
			
				// Si realmente se ha cargado un dato de brocha, se añade a la colección
					if (objBrush != null)
						objComic.Resources.Add(objBrush.Key, objBrush);
		}

		/// <summary>
		///		Agregrador de repository
		/// </summary>
		private ComicReaderMediator Mediator { get; }
	}
}
