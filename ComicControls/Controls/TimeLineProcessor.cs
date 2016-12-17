using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMotionComic.Model.Components.Actions;
using Bau.Libraries.LibMotionComic.Model.Components.Actions.Eases;

namespace Bau.Controls.ComicControls.Controls
{
	/// <summary>
	///		Procesador para un TimeLine
	/// </summary>
	internal class TimeLineProcessor
	{	// Eventos públicos
			public event EventHandler AnimationStart;
			public event EventHandler AnimationEnd;
		// Variables privadas
			private Storyboard sbStoryBoard;

		internal TimeLineProcessor(ComicPageView objComicPageView, bool blnUseAnimation)
		{ PageView = objComicPageView;
			UseAnimation = blnUseAnimation;
		}

		/// <summary>
		///		Ejecuta una serie de acciones
		/// </summary>
		internal void Execute(TimeLineModel objTimeLine)
		{	// Crea el storyboard de las animaciones
				if (sbStoryBoard == null)
					{ // Crea el storyBoard
							sbStoryBoard = new Storyboard();
						// Asigna el evento de fin de animación
							sbStoryBoard.Completed += (objSender, objEventArgs) =>
																						{	AnimationEnd?.Invoke(this, EventArgs.Empty);
																						};
					}
			// Limpia el storyBoard
				sbStoryBoard.Children.Clear();
			// Asigna las propiedades de duración
				sbStoryBoard.BeginTime = TimeSpan.FromSeconds(objTimeLine.Parameters.Start);
				sbStoryBoard.Duration = new Duration(TimeSpan.FromSeconds(objTimeLine.Parameters.Duration));
			// Recorre las acciones añadiéndolas al storyboard
				foreach (ActionBaseModel objAction in objTimeLine.Actions)
					if (objAction != null)
						{ FrameworkElement objControl = PageView.GetPageControl(objAction.TargetKey);

								// Ejecuta la acción
									if (objControl != null && objControl != null) // && objControl.RenderTransform is TransformGroup)
										{ if (objAction is SetVisibilityActionModel)
												ExecuteVisibility(objControl, objAction as SetVisibilityActionModel);
											else if (objAction is ResizeActionModel)
												ExecuteResize(objTimeLine, objControl, objAction as ResizeActionModel);
											else if (objAction is RotateActionModel)
												ExecuteRotation(objControl, objAction as RotateActionModel);
											else if (objAction is ZoomActionModel)
												ExecuteZoom(objControl, objAction as ZoomActionModel);
											else if (objAction is TranslateActionModel)
												ExecuteTranslate(objControl, objAction as TranslateActionModel);
											else if (objAction is PathActionModel)
												ExecutePathAnimation(objControl, objAction as PathActionModel);
											else if (objAction is SetZIndexModel)
												ExecuteZIndexAnimation(objControl, objAction as SetZIndexModel);
											else if (objAction is BrushViewBoxActionModel)
												ExecuteViewBoxAnimation(objControl, objAction as BrushViewBoxActionModel);
											else if (objAction is BrushRadialActionModel)
												ExecuteBrushRadial(objControl, objAction as BrushRadialActionModel);
											else if (objAction is BrushLinearActionModel)
												ExecuteBrushLinear(objControl, objAction as BrushLinearActionModel);
										}
						}
				// Inicia la animación
					if (sbStoryBoard.Children.Count > 0)
						{ // Lanza el evento de inicio de animación
								AnimationStart?.Invoke(this, EventArgs.Empty);
							// Arranca la animación
								sbStoryBoard.Begin();
						}
		}

		/// <summary>
		///		Comprueba si se deben añadir animaciones
		/// </summary>
		private bool CheckMustAnimate(ActionBaseModel objAction)
		{ return sbStoryBoard != null && UseAnimation && objAction.MustAnimate && objAction.TimeLine.MustAnimate;
		}

		/// <summary>
		///		Ejecuta una acción
		/// </summary>
		private void ExecuteVisibility(FrameworkElement objControl, SetVisibilityActionModel objAction)
		{if (CheckMustAnimate(objAction))
				CreateDoubleAnimation(objControl, null, objAction.Opacity, 
															new PropertyPath(UIElement.OpacityProperty),
															objAction);
			else
				objControl.Opacity = objAction.Opacity;
		}

		/// <summary>
		///		Cambia el tamaño de un control
		/// </summary>
		private void ExecuteResize(TimeLineModel objTimeLine, FrameworkElement objControl, 
															 ResizeActionModel objAction)
		{ if (CheckMustAnimate(objAction))
				{ if (objAction.Width != null)
						CreateDoubleAnimation(objControl, null, objAction.Width ?? 100,
																	new PropertyPath(ComicPageView.PageWidthProperty), objAction);
					if (objAction.Height != null)
						CreateDoubleAnimation(objControl, null, objAction.Height ?? 100,
																	new PropertyPath(ComicPageView.PageHeightProperty), objAction);
				}
			else
				{ if (objAction.Width != null)
						ComicPageView.SetPageWidth(objControl, objAction.Width ?? 100);
					if (objAction.Height != null)
						ComicPageView.SetPageHeight(objControl, objAction.Height ?? 100);
				}
		}

		/// <summary>
		///		Zoom de una imagen
		/// </summary>
		private void ExecuteZoom(FrameworkElement objControl, ZoomActionModel objAction)
		{ TransformGroup objGroup = (TransformGroup) objControl.RenderTransform;
			int intIndexScale = -1;

				// Obtiene la transformación de rotación
					for (int intIndex = 0; intIndex < objGroup.Children.Count; intIndex++)
						if (objGroup.Children[intIndex] is ScaleTransform)
							intIndexScale = intIndex;
				// Crea la transformación si no estaba en el grupo
					if (intIndexScale < 0)
						{ objGroup.Children.Add(new ScaleTransform(1, 1, 0, 0));
							intIndexScale = objGroup.Children.Count - 1;
						}
				// Añade la animación
					if (CheckMustAnimate(objAction))
						{ CreateDoubleAnimation(objControl, null, objAction.ScaleX,
																		new PropertyPath($"(UIElement.RenderTransform).(TransformGroup.Children)[{intIndexScale}].(ScaleTransform.ScaleX)"),
																		objAction);
							CreateDoubleAnimation(objControl, null, objAction.ScaleY,
																		new PropertyPath($"(UIElement.RenderTransform).(TransformGroup.Children)[{intIndexScale}].(ScaleTransform.ScaleY)"),
																		objAction);
						}
					else
						{ // Elimina la escala
								objGroup.Children.RemoveAt(intIndexScale);
							// Añade la escala
								objGroup.Children.Add(new ScaleTransform(objAction.ScaleX, objAction.ScaleY, objAction.OriginX, objAction.OriginY));
						}
		}

		/// <summary>
		///		Ejecuta la traslación de un elemento
		/// </summary>
		private void ExecuteTranslate(FrameworkElement objControl, TranslateActionModel objAction)
		{ TransformGroup objGroup = (TransformGroup) objControl.RenderTransform;
			int intIndexTranslate = -1;

				// Crea la transformación de rotación si no existe
					foreach (Transform objTransform in objGroup.Children)
						if (objTransform is TranslateTransform)
							intIndexTranslate = objGroup.Children.IndexOf(objTransform);
				// Crea la animación
					if (intIndexTranslate < 0)
						{ objGroup.Children.Add(new TranslateTransform(0, 0));
							intIndexTranslate = objGroup.Children.Count - 1;
						}
				// Añade la traslación del elemento
					if (CheckMustAnimate(objAction))
						{	if (objAction.Top != null)
								CreateDoubleAnimation(objControl, null, objAction.Top,
																			new PropertyPath(ComicPageView.PageTopProperty),
																			objAction);
							if (objAction.Left != null)
								CreateDoubleAnimation(objControl, null, objAction.Left,
																			new PropertyPath(ComicPageView.PageLeftProperty),
																			objAction);
						} 
					else
						{ // Elimina la traslación
								objGroup.Children.RemoveAt(intIndexTranslate);
							// Añade la traslación
								if (objAction.Left != null)
									ComicPageView.SetPageLeft(objControl, objAction.Left ?? 0);
								if (objAction.Top != null)
									ComicPageView.SetPageTop(objControl, objAction.Top ?? 0);
						}
		}

		/// <summary>
		///		Ejecuta el cambio de ZIndex de un control
		/// </summary>
		private void ExecuteZIndexAnimation(FrameworkElement objControl, SetZIndexModel objAction)
		{ if (CheckMustAnimate(objAction))
				CreateIntAnimation(objControl, null, objAction.ZIndex, 
													 new PropertyPath(System.Windows.Controls.Grid.ZIndexProperty),
													 objAction);
			else
				System.Windows.Controls.Grid.SetZIndex(objControl, objAction.ZIndex);
		}

		/// <summary>
		///		Ejecuta el cambio de ViewBox de una animación
		/// </summary>
		private void ExecuteViewBoxAnimation(FrameworkElement objControl, BrushViewBoxActionModel objAction)
		{ if (objAction.ViewBox != null || objAction.ViewPort != null)
				{ Brush objBrushControl = GetBrushFromControl(objControl);

						if (objBrushControl != null && objBrushControl is TileBrush)
							{	TileBrush objBrush = objBrushControl as TileBrush;

									if (objBrush != null)
										{ // Anima el viewbox
												if (objAction.ViewBox != null)
													{ // Inicializa el viewBox
															if (objBrush.Viewbox == null)
																{ objBrush.Viewbox = new Rect(0, 0, 1, 1);
																	objBrush.ViewboxUnits = BrushMappingMode.RelativeToBoundingBox;
																}
														// Genera la animación
															//if (CheckMustAnimate(objAction))
															//	CreateRectangleAnimation(ctlControl, null, ViewTools.ConvertToRect(objAction.ViewBox),
															//													 new PropertyPath("(ComicFrameView.Border).Fill.(ImageBrush.Viewbox)"),
															//													 objAction);
															//else
																objBrush.Viewbox = ViewTools.ConvertToRect(objAction.ViewBox);
													}
											// Anima el viewport
												if (objAction.ViewPort != null)
													{ // Inicializa el ViewPort
															if (objBrush.Viewport == null)
																{ objBrush.Viewport = new Rect(0, 0, 1, 1);
																	objBrush.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
																}
														// Asigna el modo de relleno
															objBrush.TileMode = ViewTools.Convert(objAction.TileMode);
														// Genera la animación
															//if (CheckMustAnimate(objAction))
															//	CreateRectangleAnimation(ctlControl, null, ViewTools.ConvertToRect(objAction.ViewPort),
															//													 new PropertyPath("(ComicFrameView.Border).Fill.(ImageBrush.Viewport)"),
															//													 objAction);
															//else
																objBrush.Viewport = ViewTools.ConvertToRect(objAction.ViewPort);
													}
										}
							}


												// ((ctlControl.Border.Fill) as ImageBrush).Viewbox
												//if (CheckMustAnimate(objAction))
												//	CreateRectangleAnimation(ctlControl, null, ViewTools.ConvertToRect(objAction.ViewBox),
												//													 // new PropertyPath(TileBrush.ViewportProperty),
												//													 // new PropertyPath("(UIElement.Fill.ImageBrush).(ViewBox)"),
												//													 // new PropertyPath("(Grid.Background.ImageBrush).(ViewBox)"),
												//													 // new PropertyPath("(Background).(ViewBox)"),
												//													 // new PropertyPath("(UIElement.Border.Fill).(ViewBox)"),
												//													 // new PropertyPath("(ComicFrameView.Border.Fill).(ViewBox)"),
												//													 // new PropertyPath("(ComicFrameView.Border.ImageBrush).(ViewBox)"),
												//													 // new PropertyPath("(ComicFrameView.Border.Fill).(ImageBrush).(ViewBox)"),
												//													 // new PropertyPath("(ComicFrameView.Border.Fill.ImageBrush).(ViewBox)"),
												//													 // new PropertyPath("(ComicFrameView.Border.Fill).(ImageBrush.ViewBox)"),
												//													 // new PropertyPath("(Grid.Background).(ImageBrush.ViewBox)"),
												//													 // new PropertyPath("(Grid.Background).(ImageBrush).(ViewBox)"),
												//													 // new PropertyPath("(Grid.Background).(Fill).(ViewBox)"),
												//													 // new PropertyPath("(ComicFrameView.Border.Fill).ViewBox"),
												//													 // new PropertyPath("(ComicFrameView.Border.Fill).(ImageBrush.ViewBox)"),
												//													 // new PropertyPath("(ComicFrameView.Border).(Fill).(ImageBrush.ViewBox)"),
												//													 // new PropertyPath("(ComicFrameView.Border).(Fill).ViewBox"),
												//													 // (Rectangle.Fill).(SolidColorBrush.Color)
												//													 // new PropertyPath("(ComicFrameView.Border).(ImageBrush.ViewBox)"),
												//													 // new PropertyPath("(ComicFrameView.Border).(Fill.ImageBrush).ViewBox"),
												//													 // new PropertyPath("(ComicFrameView.Border).(Fill).(ImageBrush).ViewBox"),
												//													 // new PropertyPath("(ComicFrameView.Border).ViewBox"),
												//													 // new PropertyPath("(Grid.Border.Fill).(ImageBrush.ViewBox)"),
												//													 // new PropertyPath("(Grid.Border.Fill).(ImageBrush).ViewBox"),
												//													 // (objectType.propertyName).propertyName2
												//													 // new PropertyPath("(ComicFrameView.Border).Fill.(ImageBrush.ViewBox)"),
												//													 // new PropertyPath("(ComicFrameView).Border.Fill.(ImageBrush).ViewBox"),
												//													 new PropertyPath("(ComicFrameView.Border.Fill.ImageBrush.ViewBox)"),
												//													 objAction);
												//else
												//if (CheckMustAnimate(objAction))
												//	{ CreateDoubleAnimation(objBrush, null, objAction.ViewBox.TopDefault,
												//													new PropertyPath("(ImageBrush.Viewbox).(Top)"), objAction);
												//	}
												//else

				}
		}

		/// <summary>
		///		Ejecuta una animación sobre un fondo radial
		/// </summary>
		private void ExecuteBrushRadial(FrameworkElement objControl, BrushRadialActionModel objAction)
		{ if (objAction.Center != null || objAction.Origin != null || objAction.RadiusX != null || objAction.RadiusY != null)
				{ Brush objBrushControl = GetBrushFromControl(objControl);

						if (objBrushControl != null && objBrushControl is RadialGradientBrush)
							{	RadialGradientBrush objBrush = objBrushControl as RadialGradientBrush;

									if (objBrush != null)
										{ // Anima el centro
												if (objAction.Center != null)
													{ // Genera la animación
															//if (CheckMustAnimate(objAction))
															//	CreateRectangleAnimation(ctlControl, null, ViewTools.ConvertToRect(objAction.ViewBox),
															//													 new PropertyPath("(ComicFrameView.Border).Fill.(ImageBrush.Viewbox)"),
															//													 objAction);
															//else
																objBrush.Center = ViewTools.Convert(objAction.Center);
													}
											// Anima el origen
												if (objAction.Origin != null)
													{ // Genera la animación
															//if (CheckMustAnimate(objAction))
															//	CreateRectangleAnimation(ctlControl, null, ViewTools.ConvertToRect(objAction.ViewPort),
															//													 new PropertyPath("(ComicFrameView.Border).Fill.(ImageBrush.Viewport)"),
															//													 objAction);
															//else
																objBrush.GradientOrigin = ViewTools.Convert(objAction.Origin);
													}
											// Anima el radio X
												if (objAction.RadiusX != null && objAction.RadiusX != objBrush.RadiusX)
													{ // Genera la animación
															//if (CheckMustAnimate(objAction))
															//	CreateRectangleAnimation(ctlControl, null, ViewTools.ConvertToRect(objAction.ViewPort),
															//													 new PropertyPath("(ComicFrameView.Border).Fill.(ImageBrush.Viewport)"),
															//													 objAction);
															//else
																objBrush.RadiusX = objAction.RadiusX ?? objBrush.RadiusX;
													}
											// Anima el radio Y
												if (objAction.RadiusY != null && objAction.RadiusY != objBrush.RadiusY)
													{ // Genera la animación
															//if (CheckMustAnimate(objAction))
															//	CreateRectangleAnimation(ctlControl, null, ViewTools.ConvertToRect(objAction.ViewPort),
															//													 new PropertyPath("(ComicFrameView.Border).Fill.(ImageBrush.Viewport)"),
															//													 objAction);
															//else
																objBrush.RadiusY = objAction.RadiusY ?? objBrush.RadiusY;
													}
										}
							}
				}
		}

		/// <summary>
		///		Ejecuta una animación sobre un fondo lineal
		/// </summary>
		private void ExecuteBrushLinear(FrameworkElement objControl, BrushLinearActionModel objAction)
		{ if (objAction.Start != null || objAction.End != null)
				{ Brush objBrushControl = GetBrushFromControl(objControl);

						if (objBrushControl != null && objBrushControl is LinearGradientBrush)
							{	LinearGradientBrush objBrush = objBrushControl as LinearGradientBrush;

									if (objBrush != null)
										{ // Cambia el modo de Spread
												objBrush.SpreadMethod = ViewTools.Convert(objAction.SpreadMethod);
											// Anima el centro
												if (objAction.Start != null)
													{ // Genera la animación
															//if (CheckMustAnimate(objAction))
															//	CreateRectangleAnimation(ctlControl, null, ViewTools.ConvertToRect(objAction.ViewBox),
															//													 new PropertyPath("(ComicFrameView.Border).Fill.(ImageBrush.Viewbox)"),
															//													 objAction);
															//else
																objBrush.StartPoint = ViewTools.Convert(objAction.Start);
													}
											// Anima el origen
												if (objAction.End != null)
													{ // Genera la animación
															//if (CheckMustAnimate(objAction))
															//	CreateRectangleAnimation(ctlControl, null, ViewTools.ConvertToRect(objAction.ViewPort),
															//													 new PropertyPath("(ComicFrameView.Border).Fill.(ImageBrush.Viewport)"),
															//													 objAction);
															//else
																objBrush.EndPoint = ViewTools.Convert(objAction.End);
													}
										}
							}
				}
		}

		/// <summary>
		///		Obtiene la brocha del control
		/// </summary>
		private Brush GetBrushFromControl(FrameworkElement ctlControl)
		{ if (ctlControl is ComicFrameView)
				return (ctlControl as ComicFrameView)?.Border.Fill;
			else if (ctlControl is ComicPageView)
				return (ctlControl as ComicPageView)?.Background;
			else
				return null;
		}

		/// <summary>
		///		Rota una imagen
		/// </summary>
		private void ExecuteRotation(FrameworkElement objControl, RotateActionModel objAction)
		{ TransformGroup objGroup = (TransformGroup) objControl.RenderTransform;
			int intIndexRotation = -1;

				// Cambia el origen de la transformación
					objControl.RenderTransformOrigin = new Point(objAction.OriginX, objAction.OriginY);
				// Crea la transformación de rotación si no existe
					for (int intIndex = 0; intIndex < objGroup.Children.Count; intIndex++)
						if (objGroup.Children[intIndex] is RotateTransform)
							intIndexRotation = intIndex;
				// Crea la animación
					if (intIndexRotation < 0)
						{ objGroup.Children.Add(new RotateTransform(0, 0.5, 0.5));
							intIndexRotation = objGroup.Children.Count - 1;
						}
				// Añade la rotación
					if (CheckMustAnimate(objAction))
						CreateDoubleAnimation(objControl, null, objAction.Angle,
																	new PropertyPath($"(UIElement.RenderTransform).(TransformGroup.Children)[{intIndexRotation}].(RotateTransform.Angle)"),
																	objAction);
					else
						{ // Elimina la rotación
								objGroup.Children.RemoveAt(intIndexRotation);
							// Añade una nueva rotación
								objGroup.Children.Add(new RotateTransform(objAction.Angle, objAction.OriginX, objAction.OriginY));
						}
		}

		/// <summary>
		///		Ejecuta una animación sobre una ruta
		/// </summary>
		private void ExecutePathAnimation(FrameworkElement objControl, PathActionModel objAction)
		{	if (CheckMustAnimate(objAction))
				{ PathGeometry objPathGeometry = new PathGeometry();

						// Añade la ruta a la geometría
							objPathGeometry.AddGeometry(Geometry.Combine(Geometry.Empty,
																														Geometry.Parse(objAction.Path),
																														GeometryCombineMode.Union, null));
						// Asigna las animaciones
							CreateDoubleAnimationUsingPath(objControl, objAction, objPathGeometry, PathAnimationSource.X,
																							new PropertyPath(ComicPageView.PageTopProperty));
							CreateDoubleAnimationUsingPath(objControl, objAction, objPathGeometry, PathAnimationSource.Y,
																							new PropertyPath(ComicPageView.PageLeftProperty));
				}
		}

		/// <summary>
		///		Obtiene una animación que utiliza una ruta
		/// </summary>
		private DoubleAnimationUsingPath CreateDoubleAnimationUsingPath(UIElement objControl, ActionBaseModel objAction, PathGeometry objPathGeometry,
																																		PathAnimationSource intSource, PropertyPath objPropertyPath)
		{ DoubleAnimationUsingPath objAnimation = new DoubleAnimationUsingPath();

				// Asigna las propiedades de la animación
					objAnimation.PathGeometry = objPathGeometry;
					objAnimation.Source = intSource;
				// Añade la animación al storyboard
					AddAnimationToStoryBoard(objControl, objAnimation, objAction, objPropertyPath);
				// Devuelve la animación
					return objAnimation;
		}

		/// <summary>
		///		Crea una animación Double sobre un control
		/// </summary>
		private void CreateDoubleAnimation(DependencyObject objControl, double? dblFrom, double? dblTo, PropertyPath objPropertyPath,
																			 ActionBaseModel objAction)
		{	DoubleAnimation objAnimation = new DoubleAnimation();

				// Asigna las propiedades
					if (dblFrom != null)
						objAnimation.From = dblFrom;
					if (dblTo != null)
						objAnimation.To = dblTo;
				// Asigna las funciones
					objAnimation.EasingFunction = AssignEasingFuntion(objAction);
				// Añade la animación al storyBoard
					AddAnimationToStoryBoard(objControl, objAnimation, objAction, objPropertyPath);
		}

		/// <summary>
		///		Crea una animación Int sobre un control
		/// </summary>
		private void CreateIntAnimation(UIElement objControl, int? intFrom, int? intTo, PropertyPath objPropertyPath,
																		ActionBaseModel objAction)
		{	Int32Animation objAnimation = new Int32Animation();

				// Asigna las propiedades
					if (intFrom != null)
						objAnimation.From = intFrom;
					if (intTo != null)
						objAnimation.To = intTo;
				// Asigna las funciones
					objAnimation.EasingFunction = AssignEasingFuntion(objAction);
				// Añade la animación al storyBoard
					AddAnimationToStoryBoard(objControl, objAnimation, objAction, objPropertyPath);
		}

		/// <summary>
		///		Crea la animación de punto sobre un control
		/// </summary>
		private void CreatePointAnimation(UIElement objControl, Point? pntFrom, Point? pntTo, PropertyPath objPropertyPath,
																			ActionBaseModel objAction)
		{	PointAnimation objAnimation = new PointAnimation();

				// Asigna las propiedades From y To de la animación
					if (pntFrom != null)
						objAnimation.From = pntFrom;
					if (pntTo != null)
						objAnimation.To = pntTo;
				// Asigna las funciones
					objAnimation.EasingFunction = AssignEasingFuntion(objAction);
				// Asigna la animación al storyBoard
					AddAnimationToStoryBoard(objControl, objAnimation, objAction, objPropertyPath);
		}

		/// <summary>
		///		Crea la animación de rectángulor sobre un control
		/// </summary>
		private void CreateRectangleAnimation(DependencyObject objControl, Rect? rctFrom, Rect rctTo, 
																					PropertyPath objPropertyPath, BrushViewBoxActionModel objAction)
		{ RectAnimation objAnimation = new RectAnimation();

				// Asigna las propiedades From y To de la animación
					if (rctFrom != null)
						objAnimation.From = rctFrom;
					if (rctTo != null)
						objAnimation.To = rctTo;
				// Asigna las funciones
					objAnimation.EasingFunction = AssignEasingFuntion(objAction);
				// Asigna la animación al storyboard
					AddAnimationToStoryBoard(objControl, objAnimation, objAction, objPropertyPath);
		}

		/// <summary>
		///		Asigna la función asociada a la animación
		/// </summary>
		private IEasingFunction AssignEasingFuntion(ActionBaseModel objAction)
		{	if (objAction != null && objAction.Eases.Count > 0)
				return GetEasingFunction(objAction.Eases[0]);
			else
				return null;
		}

		/// <summary>
		///		Obtiene la función asociada con la animación
		/// </summary>
		private IEasingFunction GetEasingFunction(EaseBaseModel objEase)
		{ if (objEase is BounceEaseModel)
				return GetBounceEase(objEase as BounceEaseModel);
			else if (objEase is BackEaseModel)
				return GetBackEase(objEase as BackEaseModel);
			else if (objEase is CircleEaseModel)
				return GetCircleEase(objEase as CircleEaseModel);
			else if (objEase is CubicEaseModel)
				return GetCubicEase(objEase as CubicEaseModel);
			else if (objEase is ElasticEaseModel)
				return GetElasticEase(objEase as ElasticEaseModel);
			else if (objEase is QuadraticEaseModel)
				return GetQuadraticEase(objEase as QuadraticEaseModel);
			else if (objEase is QuarticEaseModel)
				return GetCuarticeEase(objEase as QuarticEaseModel);
			else if (objEase is SineEaseModel)
				return GetSineEase(objEase as SineEaseModel);
			else if (objEase is ExponentialEaseModel)
				return GetExponentialEase(objEase as ExponentialEaseModel);
			else if (objEase is PowerEaseModel)
				return GetPowerEase(objEase as PowerEaseModel);
			else
				return null;
		}

		/// <summary>
		///		Obtiene una función Bounce
		/// </summary>
		private BounceEase GetBounceEase(BounceEaseModel objBounceEase)
		{ BounceEase objEase = new BounceEase();

				// Asigna las propiedades
					objEase.Bounces = objBounceEase.Bounces;
					objEase.Bounciness = objBounceEase.Bounciness;
					objEase.EasingMode = ConvertEaseMode(objBounceEase.EaseMode);
				// Devuelve la función
					return objEase;
		}

		/// <summary>
		///		Obtiene una función Back
		/// </summary>
		private BackEase GetBackEase(BackEaseModel objBackEase)
		{ BackEase objEase = new BackEase();

				// Asigna las propiedades
					objEase.Amplitude = objBackEase.Amplitude;
					objEase.EasingMode = ConvertEaseMode(objBackEase.EaseMode);
				// Devuelve la función
					return objEase;
		}

		/// <summary>
		///		Obtiene una función Circle
		/// </summary>
		private CircleEase GetCircleEase(CircleEaseModel objCircleEase)
		{ CircleEase objEase = new CircleEase();

				// Asigna las propiedades
					objEase.EasingMode = ConvertEaseMode(objCircleEase.EaseMode);
				// Devuelve la función
					return objEase;
		}

		/// <summary>
		///		Obtiene una función cúbica
		/// </summary>
		private CubicEase GetCubicEase(CubicEaseModel objCubicEase)
		{ CubicEase objEase = new CubicEase();

				// Asigna las propiedades
					objEase.EasingMode = ConvertEaseMode(objCubicEase.EaseMode);
				// Devuelve la función
					return objEase;
		}

		/// <summary>
		///		Obtiene una función de tipo muelle para la animación
		/// </summary>
		private ElasticEase GetElasticEase(ElasticEaseModel objElasticEase)
		{ ElasticEase objEase = new ElasticEase();

				// Asigna las propiedades
					objEase.EasingMode = ConvertEaseMode(objElasticEase.EaseMode);
					objEase.Oscillations = objElasticEase.Oscillations;
					objEase.Springiness = objElasticEase.Springiness;
				// Devuelve la función
					return objEase;
		}

		/// <summary>
		///		Obtiene una función cuadrática
		/// </summary>
		private QuadraticEase GetQuadraticEase(QuadraticEaseModel objQuadraticEase)
		{ QuadraticEase objEase = new QuadraticEase();

				// Asigna las propiedades
					objEase.EasingMode = ConvertEaseMode(objQuadraticEase.EaseMode);
				// Devuelve la función
					return objEase;
		}

		/// <summary>
		///		Obtiene una función quartic
		/// </summary>
		private QuarticEase GetCuarticeEase(QuarticEaseModel objQuarticEase)
		{ QuarticEase objEase = new QuarticEase();

				// Asigna las propiedades
					objEase.EasingMode = ConvertEaseMode(objQuarticEase.EaseMode);
				// Devuelve la función
					return objEase;
		}

		/// <summary>
		///		Obtiene una función seno
		/// </summary>
		private SineEase GetSineEase(SineEaseModel objSineEase)
		{ SineEase objEase = new SineEase();

				// Asigna las propiedades
					objEase.EasingMode = ConvertEaseMode(objSineEase.EaseMode);
				// Devuelve la función
					return objEase;
		}

		/// <summary>
		///		Obtiene una función exponencial
		/// </summary>
		private ExponentialEase GetExponentialEase(ExponentialEaseModel objExponentialEase)
		{ ExponentialEase objEase = new ExponentialEase();

				// Asigna las propiedades
					objEase.EasingMode = ConvertEaseMode(objExponentialEase.EaseMode);
					objEase.Exponent = objExponentialEase.Exponent;
				// Devuelve la función
					return objEase;
		}

		/// <summary>
		///		Obtiene una potencia
		/// </summary>
		private PowerEase GetPowerEase(PowerEaseModel objPowerEase)
		{ PowerEase objEase = new PowerEase();

				// Asigna las propiedades
					objEase.EasingMode = ConvertEaseMode(objPowerEase.EaseMode);
					objEase.Power = objPowerEase.Power;
				// Devuelve la función
					return objEase;
		}

		/// <summary>
		///		Obtiene el modo de aplicación de una función ease
		/// </summary>
		private EasingMode ConvertEaseMode(EaseBaseModel.Mode intMode)
		{ switch (intMode)
				{	case EaseBaseModel.Mode.EaseIn:
						return EasingMode.EaseIn;
					case EaseBaseModel.Mode.EaseOut:
						return EasingMode.EaseOut;
					default:
						return EasingMode.EaseInOut;
				}
		}

		/// <summary>
		///		Añade una animación al storyBoard
		/// </summary>
		private void AddAnimationToStoryBoard(DependencyObject objControl, AnimationTimeline objAnimation, 
																					ActionBaseModel objAction, PropertyPath objPropertyPath)
		{	// Asigna las propiedades de inicio y duración
				objAnimation.BeginTime = TimeSpan.FromSeconds(objAction.Parameters.Start);
				objAnimation.Duration = TimeSpan.FromSeconds(objAction.Parameters.Duration);
			// Asigna las propiedades de velocidad
				if (objAction.Parameters.AccelerationRatio != null)
					objAnimation.AccelerationRatio = objAction.Parameters.AccelerationRatio ?? 0;
				else if (objAction.TimeLine.Parameters.AccelerationRatio != null)
					objAnimation.AccelerationRatio = objAction.TimeLine.Parameters.AccelerationRatio ?? 0;
				if (objAction.Parameters.DecelerationRatio != null)
					objAnimation.DecelerationRatio = objAction.Parameters.DecelerationRatio ?? 0;
				else if (objAction.TimeLine.Parameters.DecelerationRatio != null)
					objAnimation.DecelerationRatio = objAction.TimeLine.Parameters.DecelerationRatio ?? 0;
				if (objAction.Parameters.SpeedRatio != null)
					objAnimation.SpeedRatio = objAction.Parameters.SpeedRatio ?? 0;
				else if (objAction.TimeLine.Parameters.SpeedRatio != null)
					objAnimation.SpeedRatio = objAction.TimeLine.Parameters.SpeedRatio ?? 0;
			// Añade los datos a la animación
				Storyboard.SetTarget(objAnimation, objControl);
				Storyboard.SetTargetProperty(objAnimation, objPropertyPath);
			// Añade la animación al storyboard
				sbStoryBoard.Children.Add(objAnimation);
		}

		/// <summary>
		///		Página de la vista
		/// </summary>
		private ComicPageView PageView { get; }

		/// <summary>
		///		Indica si se debe utilizar una animación
		/// </summary>
		private bool UseAnimation { get; }
	}
}