using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMotionComic.Model.Components;
using Bau.Libraries.LibMotionComic.Model.Components.Actions;
using Bau.Libraries.LibMotionComic.Model.Components.PageItems;

namespace Bau.Controls.ComicControls.Controls
{
	/// <summary>
	///		Control para mostrar una página de un cómic
	/// </summary>
	internal class ComicPageView : Grid
	{ // Propiedades
			public static readonly DependencyProperty UseAnimationProperty = 
								DependencyProperty.Register("UseAnimation", typeof(bool), typeof(ComicPageView),
																						new FrameworkPropertyMetadata(true, 
																																					FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
			public static readonly DependencyProperty ShowAdornersProperty = 
								DependencyProperty.Register("ShowAdorners", typeof(bool), typeof(ComicPageView),
																						new FrameworkPropertyMetadata(false, 
																																					FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
			public static readonly DependencyProperty AbsoluteWidthProperty = 
								DependencyProperty.Register("AbsoluteWidth", typeof(double), typeof(ComicPageView),
																						new FrameworkPropertyMetadata(1500.0, 
																																					FrameworkPropertyMetadataOptions.AffectsArrange));
			public static readonly DependencyProperty AbsoluteHeightProperty = 
								DependencyProperty.Register("AbsoluteHeight", typeof(double), typeof(ComicPageView),
																						new FrameworkPropertyMetadata(3000.0, 
																																					FrameworkPropertyMetadataOptions.AffectsArrange));
		// Propiedades de idioma
			public static readonly DependencyProperty LanguageSelectedProperty = 
								DependencyProperty.Register("LanguageSelected", typeof(string), typeof(ComicPageView));
			public static readonly DependencyProperty LanguageDefaultProperty = 
								DependencyProperty.Register("LanguageDefault", typeof(string), typeof(ComicPageView));
		// Propiedades adjuntas
			public static readonly DependencyProperty PageTopProperty = 
								DependencyProperty.RegisterAttached("PageTop", typeof(double), typeof(ComicPageView), 
																										new FrameworkPropertyMetadata((double) double.NaN, 
																																									FrameworkPropertyMetadataOptions.AffectsMeasure | 
																																									FrameworkPropertyMetadataOptions.AffectsArrange |
																																									FrameworkPropertyMetadataOptions.AffectsParentMeasure |
																																									FrameworkPropertyMetadataOptions.AffectsParentArrange));
			public static readonly DependencyProperty PageLeftProperty = 
								DependencyProperty.RegisterAttached("PageLeft", typeof(double), typeof(ComicPageView), 
																										new FrameworkPropertyMetadata((double) double.NaN, 
																																									FrameworkPropertyMetadataOptions.AffectsMeasure | 
																																									FrameworkPropertyMetadataOptions.AffectsArrange |
																																									FrameworkPropertyMetadataOptions.AffectsParentMeasure |
																																									FrameworkPropertyMetadataOptions.AffectsParentArrange));
			public static readonly DependencyProperty PageWidthProperty = 
								DependencyProperty.RegisterAttached("PageWidth", typeof(double), typeof(ComicPageView), 
																										new FrameworkPropertyMetadata((double) double.NaN, 
																																									FrameworkPropertyMetadataOptions.AffectsMeasure | 
																																									FrameworkPropertyMetadataOptions.AffectsArrange |
																																									FrameworkPropertyMetadataOptions.AffectsParentMeasure |
																																									FrameworkPropertyMetadataOptions.AffectsParentArrange));
			public static readonly DependencyProperty PageHeightProperty = 
								DependencyProperty.RegisterAttached("PageHeight", typeof(double), typeof(ComicPageView), 
																										new FrameworkPropertyMetadata((double) double.NaN, 
																																									FrameworkPropertyMetadataOptions.AffectsMeasure | 
																																									FrameworkPropertyMetadataOptions.AffectsArrange |
																																									FrameworkPropertyMetadataOptions.AffectsParentMeasure |
																																									FrameworkPropertyMetadataOptions.AffectsParentArrange));
		// Eventos públicos
			public event EventHandler StartAnimation;
			public event EventHandler EndAnimation;
		// Variables privadas
			private TimeLineProcessor objAnimationProcessor = null;

		/// <summary>
		///		Carga los datos de una página
		/// </summary>
		public void LoadPage(PageModel objPage)
		{	// Guarda la página
				AbsoluteWidth = objPage.Comic.Width;
				AbsoluteHeight = objPage.Comic.Height;
			// Comienza el proceso
				BeginInit();
			// Limpia las imágenes
				Clear();
			// Asigna el fondo
				if (objPage.Brush != null)
					Background = ViewTools.GetBrush(objPage, objPage.Brush);
				else
					Background = null;
			// Añade las imágenes de la página
				foreach (AbstractPageItemModel objPageContent in objPage.Content)
					if (objPageContent is ImageModel)
						AddImage(objPageContent as ImageModel);
					else if (objPageContent is FrameModel)
						AddFrame(objPageContent as FrameModel);
					else if (objPageContent is TextModel)
						AddText(objPageContent as TextModel);
			// Finaliza el proceso
				EndInit();
		}

		/// <summary>
		///		Limpia el control
		/// </summary>
		public void Clear()
		{	Children.Clear();
		}

		/// <summary>
		///		Ejecuta las acciones de una línea de tiempo
		/// </summary>
		internal void Execute(TimeLineModel objTimeLine)
		{ // Si no existía ninguna animación se crea
				if (objAnimationProcessor == null)
					{ // Crea el objeto
							objAnimationProcessor = new TimeLineProcessor(this, UseAnimation);
						// Asigna los manejadores de eventos
							objAnimationProcessor.AnimationStart += (objSender, objEventArgs) =>
										{	StartAnimation?.Invoke(this, EventArgs.Empty);
											IsPlayingAnimation = true;
										};
							objAnimationProcessor.AnimationEnd += (objSender, objEventArgs) =>
										{ EndAnimation?.Invoke(this, EventArgs.Empty);
											IsPlayingAnimation = false;
										};
					}
			// Ejecuta la animación
				objAnimationProcessor.Execute(objTimeLine);
		}

		/// <summary>
		///		Añade un control de imagen
		/// </summary>
		private void AddImage(ImageModel objImage)
		{	Image objView = new Image();
			
				// Carga la imagen
					objView.Source = ViewTools.LoadImage(objImage);
					objView.Stretch = Stretch.Fill;
				// Asigna las dimensiones a la imagen
					if (objImage.Dimensions.Width == null)
						objImage.Dimensions.Width = objView.Source.Width;
					if (objImage.Dimensions.Height == null)
						objImage.Dimensions.Height = objView.Source.Height;
				// Añade el control
					AddControl(objView, objImage);
		}

		/// <summary>
		///		Añade un frame a la página
		/// </summary>
		private void AddFrame(FrameModel objShape)
		{ ComicFrameView objView = new ComicFrameView();

				// Carga la figura
					objView.InitShape(objShape, LanguageSelected, LanguageDefault);
				// Añade el control
					AddControl(objView, objShape);
		}

		/// <summary>
		///		Añade un control con un texto
		/// </summary>
		private void AddText(TextModel objText)
		{ TextFormattedGeometry objView = new TextFormattedGeometry();

				// Carga el texto
					objView.InitView(objText, objText.Texts.GetText(LanguageSelected, LanguageDefault));
				// Añade el control
					AddControl(objView, objText);
		}

		/// <summary>
		///		Añade un control
		/// </summary>
		private void AddControl(UIElement objControl, AbstractPageItemModel objPageItem)
		{	// Le asigna la clave al control
				(objControl as FrameworkElement).Tag = objPageItem.Key;
			// Cambia la visibilidad y opacidad del control
				if (objPageItem.Visible)
					objControl.Opacity = 1;
				else
					objControl.Opacity = 0;
				if (objPageItem.Opacity != 0)
					objControl.Opacity = objPageItem.Opacity;
			// Inicializa un grupo de transformaciones
				objControl.RenderTransform = new TransformGroup();
			// Asigna las propiedades
				ComicPageView.SetPageTop(objControl, objPageItem.Dimensions.TopDefault);
				ComicPageView.SetPageLeft(objControl, objPageItem.Dimensions.LeftDefault);
				ComicPageView.SetPageWidth(objControl, objPageItem.Dimensions.WidthDefault);
				ComicPageView.SetPageHeight(objControl, objPageItem.Dimensions.HeightDefault);
				Grid.SetZIndex(objControl, objPageItem.ZIndex);
			// Añade el control a la vista
				Children.Add(objControl);
			// Añade el adorno si es necesario
				if (ShowAdorners)
					AdornerLayer.GetAdornerLayer(objControl)?.Add(new FourCornersAdorner(objControl));
		}

		/// <summary>
		///		Obtiene un control
		/// </summary>
		internal FrameworkElement GetPageControl(string strKey)
		{ // Busca la imagen
				if (strKey.EqualsIgnoreCase("Page"))
					return this;
				else
					foreach (UIElement objControl in Children)
						if (((objControl as FrameworkElement)?.Tag as string) == strKey)
							return objControl as FrameworkElement;
			// Si ha llegado hasta aquí es porque no ha encontrado nada
				return null;
		}

		/// <summary>
		///		Sobrescribe el método que mide el tamaño deseado
		/// </summary>
		protected override Size MeasureOverride(Size objAvailableSize)
		{	Size objDesiredSize = new Size(0, 0);
 
				// Recorre los elementos hijo calculando el tamaño máximo deseado
					foreach (UIElement objChild in Children)
						{	Rect rctScaled = GetScaledRectangle(objChild, objAvailableSize.Width, objAvailableSize.Height);

								// Mide el tamaño
									objChild.Measure(new Size(rctScaled.Width, rctScaled.Height));
								// Obtiene el ancho máximo hasta ahora
									objDesiredSize.Width = Math.Max(objDesiredSize.Width, objChild.DesiredSize.Width);
									objDesiredSize.Height = Math.Max(objDesiredSize.Height, objChild.DesiredSize.Height);
						}
				// Normaliza el ancho máximo 
					if (double.IsPositiveInfinity(objAvailableSize.Width))
						objDesiredSize.Width = objDesiredSize.Width;
					else
						objDesiredSize.Width = objAvailableSize.Width;
				// Normaliza el alto máximo
					if (double.IsPositiveInfinity(objAvailableSize.Height))
						objDesiredSize.Height = objDesiredSize.Height;
					else
						objDesiredSize.Height = objAvailableSize.Height;
				// Devuelve el tamaño obtenido
					return objDesiredSize;
		}

		/// <summary>
		///		Sobrescribe el método que coloca los controles
		/// </summary>
		protected override Size ArrangeOverride(Size objFinalSize)
		{ // Coloca los controles
				foreach (UIElement objChild in Children)
					{ Rect rctNewRectangle = GetScaledRectangle(objChild, objFinalSize.Width, objFinalSize.Height);

							// Coloca el control
								objChild.Arrange(rctNewRectangle);
					}
			// Devuelve el tamaño
				return objFinalSize;
		}

		/// <summary>
		///		Obtiene un rectángulo escalado
		/// </summary>
		private Rect GetScaledRectangle(UIElement objControl, double dblViewPortWidth, double dblViewPortHeight)
		{ double dblControlTop = GetPageTop(objControl);
			double dblControlLeft = GetPageLeft(objControl);
			double dblControlWidth = GetPageWidth(objControl);
			double dblControlHeight = GetPageHeight(objControl);

				// Obtiene el rectángulo
					if (!double.IsNaN(dblControlTop) && !double.IsNaN(dblControlLeft) && 
							!double.IsNaN(dblControlWidth) && !double.IsNaN(dblControlHeight))
						return new Rect(GetScaledX(dblControlLeft, dblViewPortWidth),
														GetScaledY(dblControlTop, dblViewPortHeight),
														GetScaledX(dblControlWidth, dblViewPortWidth),
														GetScaledY(dblControlHeight, dblViewPortHeight));
					else
						return new Rect(0, 0, dblViewPortWidth, dblViewPortHeight);
		}

		/// <summary>
		///		Obtiene una X escalada
		/// </summary>
		private double GetScaledX(double dblX, double dblXViewPort)
		{ return (dblX / AbsoluteWidth) * dblXViewPort;
		}

		/// <summary>
		///		Obtiene una Y escalada
		/// </summary>
		private double GetScaledY(double dblY, double dblYViewPort)
		{ return (dblY / AbsoluteHeight) * dblYViewPort;
		}

		/// <summary>
		///		Obtiene el valor de la propiedad adjunta "PageTop"
		/// </summary>
		public static double GetPageTop(DependencyObject objDependency)
		{	return (double) objDependency.GetValue(PageTopProperty);
		}

		/// <summary>
		///		Asigna el valor de la propiedad adjunta "PageTop"
		/// </summary>
		public static void SetPageTop(DependencyObject objDependency, double dblValue)
		{	objDependency.SetValue(PageTopProperty, dblValue);
		}

		/// <summary>
		///		Obtiene el valor de la propiedad adjunta "PageLeft"
		/// </summary>
		public static double GetPageLeft(DependencyObject objDependency)
		{	return (double) objDependency.GetValue(PageLeftProperty);
		}

		/// <summary>
		///		Asigna el valor de la propiedad adjunta "PageLeft"
		/// </summary>
		public static void SetPageLeft(DependencyObject objDependency, double dblValue)
		{	objDependency.SetValue(PageLeftProperty, dblValue);
		}

		/// <summary>
		///		Obtiene el valor de la propiedad adjunta "PageWidth"
		/// </summary>
		public static double GetPageWidth(DependencyObject objDependency)
		{	return (double) objDependency.GetValue(PageWidthProperty);
		}

		/// <summary>
		///		Asigna el valor de la propiedad adjunta "PageWidth"
		/// </summary>
		public static void SetPageWidth(DependencyObject objDependency, double dblValue)
		{	objDependency.SetValue(PageWidthProperty, dblValue);
		}

		/// <summary>
		///		Obtiene el valor de la propiedad adjunta "PageHeight"
		/// </summary>
		public static double GetPageHeight(DependencyObject objDependency)
		{	return (double) objDependency.GetValue(PageHeightProperty);
		}

		/// <summary>
		///		Asigna el valor de la propiedad adjunta "PageHeight"
		/// </summary>
		public static void SetPageHeight(DependencyObject objDependency, double dblValue)
		{	objDependency.SetValue(PageHeightProperty, dblValue);
		}

		/// <summary>
		///		Ancho absoluto total
		/// </summary>
		public double AbsoluteWidth
		{	get { return (double) GetValue(AbsoluteWidthProperty); }
			set { SetValue(AbsoluteWidthProperty, value); }
		}

		/// <summary>
		///		Alto absoluto total
		/// </summary>
		public double AbsoluteHeight
		{	get { return (double) GetValue(AbsoluteHeightProperty); }
			set { SetValue(AbsoluteHeightProperty, value); }
		}

		/// <summary>
		///		Idioma seleccionado
		/// </summary>
		public string LanguageSelected
		{ get { return (string) GetValue(LanguageSelectedProperty); }
			set { SetValue(LanguageSelectedProperty, value); }
		}
		
		/// <summary>
		///		Idioma predeterminado
		/// </summary>
		public string LanguageDefault
		{ get { return (string) GetValue(LanguageDefaultProperty); }
			set { SetValue(LanguageDefaultProperty, value); }
		}

		/// <summary>
		///		Indica si se deben mostrar o no los ardorners sobre los controles
		/// </summary>
		public bool ShowAdorners 
		{ get { return (bool) GetValue(ShowAdornersProperty); }
			set { SetValue(ShowAdornersProperty, value); }
		}

		/// <summary>
		///		Indica si se deben animar las páginas
		/// </summary>
		public bool UseAnimation
		{	get { return (bool) GetValue(UseAnimationProperty); }
			set { SetValue(UseAnimationProperty, value); }
		}

		/// <summary>
		///		Indica si está ejecutando la animación
		/// </summary>
		public bool IsPlayingAnimation { get; private set; } = false;
	}
}
