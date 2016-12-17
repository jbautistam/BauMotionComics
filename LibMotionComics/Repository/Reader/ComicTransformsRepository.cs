using System;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMotionComic.Model.Components.Transforms;

namespace Bau.Libraries.LibMotionComic.Repository.Reader
{
	/// <summary>
	///		Repository para las transformaciones
	/// </summary>
	internal class ComicTransformsRepository
	{
		internal ComicTransformsRepository(ComicReaderMediator objMediator)
		{ Mediator = objMediator;
		}

		/// <summary>
		///		Carga una serie de transformaciones
		/// </summary>
		internal TransformModelCollection LoadTransforms(MLNode objMLNode)
		{ TransformModelCollection objColTransforms = new TransformModelCollection();

				// Carga las transformaciones
					foreach (MLNode objMLChild in objMLNode.Nodes)
						switch (objMLChild.Name)
							{	case ComicRepositoryConstants.cnstStrTagTranslate:
										objColTransforms.Add(new TranslateTransformModel(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagTop].Value.GetDouble(0),
																																		 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagLeft].Value.GetDouble(0)));
									break;
								case ComicRepositoryConstants.cnstStrTagMatrix:
										objColTransforms.Add(new MatrixTransformModel(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagData].Value));
									break;
								case ComicRepositoryConstants.cnstStrTagRotate:
										objColTransforms.Add(new RotateTransformModel(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagAngle].Value.GetDouble(0),
																																	Mediator.CommonRepository.GetPoint(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagOrigin].Value,
																																																		 0.5, 0.5)));
									break;
								case ComicRepositoryConstants.cnstStrTagScale:
										objColTransforms.Add(new ScaleTransformModel(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagScaleX].Value.GetDouble(1),
																																 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagScaleY].Value.GetDouble(1),
																																 Mediator.CommonRepository.GetPoint(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagCenter].Value,
																																																		0.5, 0.5)));
									break;
							}
				// Devuelve la colección de transformaciones
					return objColTransforms;
		}

		/// <summary>
		///		Agregador de repository
		/// </summary>
		private ComicReaderMediator Mediator { get; }
	}
}
