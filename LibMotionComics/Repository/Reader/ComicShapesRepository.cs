using System;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMotionComic.Model.Components;
using Bau.Libraries.LibMotionComic.Model.Components.PageItems;
using Bau.Libraries.LibMotionComic.Model.Components.Transforms;

namespace Bau.Libraries.LibMotionComic.Repository.Reader
{
	/// <summary>
	///		Repository para las figuras
	/// </summary>
	internal class ComicShapesRepository
	{
		internal ComicShapesRepository(ComicReaderMediator objMediator)
		{ Mediator = objMediator;
		}

		/// <summary>
		///		Carga una figura
		/// </summary>
		internal ShapeModel LoadShape(PageModel objPage, MLNode objMLNode)
		{ ShapeModel objShape = new ShapeModel(objPage, objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value);

				// Asigna los atributos básicos
					Mediator.CommonRepository.AssignAttributesPageItem(objMLNode, objShape);
				// Asigna el resto de propiedades
					objShape.ComponentKey = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagResourceKey].Value;
					objShape.FillMode = GetFillMode(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagFillRule].Value);
				// Carga las figuras
					foreach (MLNode objMLChild in objMLNode.Nodes)
						switch (objMLChild.Name)
							{	case ComicRepositoryConstants.cnstStrTagFigure:
										objShape.Figures.Add(Mediator.ShapesRepository.LoadFigure(objShape, objMLChild));
									break;
								case ComicRepositoryConstants.cnstStrTagTransform:
										objShape.Transforms.AddRange(Mediator.ShapesRepository.LoadTransforms(objMLChild));
									break;
							}
				// Devuelve la figura
					return objShape;
		}

		/// <summary>
		///		Obtiene el modo de relleno
		/// </summary>
		private ShapeModel.FillRule GetFillMode(string strValue)
		{ if (strValue.EqualsIgnoreCase("None"))
				return ShapeModel.FillRule.None;
			else if (strValue.EqualsIgnoreCase("NonZero"))
				return ShapeModel.FillRule.NonZero;
			else 
				return ShapeModel.FillRule.EvenOdd;
		}

		/// <summary>
		///		Carga los datos de una figura
		/// </summary>
		internal FigureModel LoadFigure(AbstractComponentModel objPageItem, MLNode objMLNode)
		{ FigureModel objFigure = new FigureModel();

				// Asigna los datos
					objFigure.FillMode = GetFillMode(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagFillRule].Value);
				// Asigna las propiedades
					objFigure.Brush = Mediator.BrushesRepository.LoadFirstBrush(objMLNode);
				// Asigna el contenido de la figura
					foreach (MLNode objMLChild in objMLNode.Nodes)
						switch (objMLChild.Name)
							{	case ComicRepositoryConstants.cnstStrTagData:
										// Asigna los datos de la figura
											objFigure.Data = objMLChild.Value;
										// Normaliza la cadena de datos
											objFigure.Data = Mediator.CommonRepository.Normalize(objFigure.Data);
									break;
								case ComicRepositoryConstants.cnstStrTagTransform:
										objFigure.Transforms.AddRange(Mediator.ShapesRepository.LoadTransforms(objMLChild));
									break;
						}
				// Devuelve la figura cargada
					return objFigure;
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
