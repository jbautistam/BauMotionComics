using System;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMotionComic.Model.Components;
using Bau.Libraries.LibMotionComic.Model.Components.Actions;
using Bau.Libraries.LibMotionComic.Model.Components.Actions.Eases;

namespace Bau.Libraries.LibMotionComic.Repository.Reader
{
	/// <summary>
	///		Repository para carga de timeline y acciones
	/// </summary>
	internal class ComicTimeLineRepository
	{
		internal ComicTimeLineRepository(ComicReaderMediator objMediator)
		{ Mediator = objMediator;
		}

		/// <summary>
		///		Carga los datos de un timeline
		/// </summary>
		internal TimeLineModel LoadTimeLine(PageModel objPage, MLNode objMLNode)
		{ TimeLineModel objTimeLine = new TimeLineModel(objPage, objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagMustAnimate].Value.GetBool(true));

				// Asigna los atributos
					AssignAttributesAction(objMLNode, objTimeLine.Parameters, 0, 3);
				// Carga el contenido del timeLine
					foreach (MLNode objMLChild in objMLNode.Nodes)
						{ ActionBaseModel objAction = null;

								// Obtiene la acción
									switch (objMLChild.Name)
										{	case ComicRepositoryConstants.cnstStrTagActionShowImage:
													objAction = new SetVisibilityActionModel(objTimeLine,
																																	 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value,
																																	 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagVisible].Value.GetBool(true),
																																	 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagOpacity].Value.GetDouble(), 
																																	 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagMustAnimate].Value.GetBool(true));
												break;
											case ComicRepositoryConstants.cnstStrTagActionResize:
													objAction = new ResizeActionModel(objTimeLine,
																														objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value,
																														objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagWidth].Value.GetDouble(),
																														objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagHeight].Value.GetDouble(), 
																														objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagMustAnimate].Value.GetBool(true));
												break;
											case ComicRepositoryConstants.cnstStrTagActionRotate:
													objAction = new RotateActionModel(objTimeLine,
																														objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value,
																														objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagOriginX].Value.GetDouble(0.5),
																														objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagOriginY].Value.GetDouble(0.5),
																														objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagAngle].Value.GetDouble(0), 
																														objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagMustAnimate].Value.GetBool(true));
												break;
											case ComicRepositoryConstants.cnstStrTagActionZoom:
													objAction = new ZoomActionModel(objTimeLine,
																													objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value,
																													objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagOriginX].Value.GetDouble(0.5),
																													objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagOriginY].Value.GetDouble(0.5),
																													objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagScaleX].Value.GetDouble(1),
																													objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagScaleY].Value.GetDouble(1), 
																													objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagMustAnimate].Value.GetBool(true));
												break;
											case ComicRepositoryConstants.cnstStrTagActionTranslate:
													objAction = new TranslateActionModel(objTimeLine,
																															 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value,
																															 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagTop].Value.GetDouble(),
																															 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagLeft].Value.GetDouble(), 
																															 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagMustAnimate].Value.GetBool(true));
												break;
											case ComicRepositoryConstants.cnstStrTagActionSetZIndex:
													objAction = new SetZIndexModel(objTimeLine,
																												 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value,
																												 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagMustAnimate].Value.GetBool(true),
																												 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagZIndex].Value.GetInt(1));
												break;
											case ComicRepositoryConstants.cnstStrTagActionPath:
													objAction = new PathActionModel(objTimeLine,
																													objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value,
																													Mediator.CommonRepository.Normalize(objMLChild.Value),
																													objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagRotateWithTangent].Value.GetBool(true),
																													objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagMustAnimate].Value.GetBool(true));
												break;
											case ComicRepositoryConstants.cnstStrTagSetActionViewBox:
													objAction = new BrushViewBoxActionModel(objTimeLine,
																																	objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value,
																																	Mediator.CommonRepository.GetRectangle(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagViewBox].Value),
																																	Mediator.CommonRepository.GetRectangle(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagViewPort].Value),
																																	Mediator.CommonRepository.ConvertTile(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagTileMode].Value),
																																	objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagMustAnimate].Value.GetBool(true));
												break;
											case ComicRepositoryConstants.cnstStrTagSetActionRadialBrush:
													objAction = new BrushRadialActionModel(objTimeLine,
																																 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value,
																																 Mediator.CommonRepository.GetPoint(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagCenter].Value),
																																 Mediator.CommonRepository.GetPoint(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagOrigin].Value),
																																 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagRadiusX].Value.GetDouble(),
																																 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagRadiusY].Value.GetDouble(),
																																 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagMustAnimate].Value.GetBool(true));
												break;
											case ComicRepositoryConstants.cnstStrTagSetActionLinearBrush:
													objAction = new BrushLinearActionModel(objTimeLine,
																																 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value,
																																 Mediator.CommonRepository.GetPoint(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagStart].Value),
																																 Mediator.CommonRepository.GetPoint(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagEnd].Value),
																																 Mediator.CommonRepository.GetSpreadMethod(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagSpread].Value),
																																 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagMustAnimate].Value.GetBool(true));
												break;
										}
								// Si realmente se ha leído alguna acción
									if (objAction != null)
										{ // Asigna los atributos
												AssignAttributesAction(objMLChild, objAction.Parameters, objTimeLine.Parameters.Start, 
																							 objTimeLine.Parameters.Duration);
											// Asigna las funciones
												LoadEases(objAction, objMLChild);
											// Añade la acción al timeline
												objTimeLine.Actions.Add(objAction);
										}
							}
				// Devuelve el timeline
					return objTimeLine;
		}

		/// <summary>
		///		Asigna los atributos básicos a una acción
		/// </summary>
		private void AssignAttributesAction(MLNode objMLNode, AnimationParameters objParameters, int intStartDefault, int intDurationDefault)
		{	objParameters.Start = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagStart].Value.GetInt(intStartDefault);
			objParameters.Duration = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagDuration].Value.GetInt(intDurationDefault);
			objParameters.AccelerationRatio = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagAcceleration].Value.GetDouble();
			objParameters.DecelerationRatio = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagDeceleration].Value.GetDouble();
			objParameters.SpeedRatio = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagSpeed].Value.GetDouble();
		}

		/// <summary>
		///		Carga las funciones asociadas a una acción
		/// </summary>
		private void LoadEases(ActionBaseModel objAction, MLNode objMLNode)
		{ foreach (MLNode objMLChild in objMLNode.Nodes)
				switch (objMLChild.Name)
					{ case ComicRepositoryConstants.cnstStrTagBounceEase:
								objAction.Eases.Add(new BounceEaseModel(objAction, 
																												GetEaseMode(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagEasingMode].Value),
																												objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagBounces].Value.GetInt(2),
																												objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagBounciness].Value.GetDouble(0.5)));
							break;
						case ComicRepositoryConstants.cnstStrTagBackEase:
								objAction.Eases.Add(new BackEaseModel(objAction,
																											GetEaseMode(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagEasingMode].Value),
																											objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagAmplitude].Value.GetDouble(0.5)));
							break;
						case ComicRepositoryConstants.cnstStrTagCircleEase:
								objAction.Eases.Add(new CircleEaseModel(objAction,
																												GetEaseMode(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagEasingMode].Value)));
							break;
						case ComicRepositoryConstants.cnstStrTagCubicEase:
								objAction.Eases.Add(new CubicEaseModel(objAction,
																											 GetEaseMode(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagEasingMode].Value)));
							break;
						case ComicRepositoryConstants.cnstStrTagElasticEase:
								objAction.Eases.Add(new ElasticEaseModel(objAction,
																												 GetEaseMode(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagEasingMode].Value),
																												 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagOscillations].Value.GetInt(2),
																												 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagSpringiness].Value.GetInt(1)));
							break;
						case ComicRepositoryConstants.cnstStrTagQuadraticEase:
								objAction.Eases.Add(new QuadraticEaseModel(objAction,
																													 GetEaseMode(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagEasingMode].Value)));
							break;
						case ComicRepositoryConstants.cnstStrTagQuarticEase:
								objAction.Eases.Add(new QuarticEaseModel(objAction,
																												 GetEaseMode(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagEasingMode].Value)));
							break;
						case ComicRepositoryConstants.cnstStrTagSineEase:
								objAction.Eases.Add(new SineEaseModel(objAction,
																											GetEaseMode(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagEasingMode].Value)));
							break;
						case ComicRepositoryConstants.cnstStrTagExponentialEase:
								objAction.Eases.Add(new ExponentialEaseModel(objAction,
																														 GetEaseMode(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagEasingMode].Value),
																														 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagExponent].Value.GetDouble(1)));
							break;
						case ComicRepositoryConstants.cnstStrTagPowerEase:
								objAction.Eases.Add(new PowerEaseModel(objAction,
																											 GetEaseMode(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagEasingMode].Value),
																											 objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagPower].Value.GetDouble(1)));
							break;
					}
		}

		/// <summary>
		///		Obtiene el modo de ejecución de las funciones
		/// </summary>
		private EaseBaseModel.Mode GetEaseMode(string strValue)
		{ if (strValue == EaseBaseModel.Mode.EaseIn.ToString())
				return EaseBaseModel.Mode.EaseIn;
			else if (strValue == EaseBaseModel.Mode.EaseOut.ToString())
				return EaseBaseModel.Mode.EaseOut;
			else
				return EaseBaseModel.Mode.EaseInOut;
		}

		/// <summary>
		///		Agregrador de repository
		/// </summary>
		private ComicReaderMediator Mediator { get; }
	}
}
