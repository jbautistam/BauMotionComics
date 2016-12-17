using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Bau.Libraries.LibMotionComic.Model;
using Bau.Libraries.LibMotionComic.Model.Components;
using Bau.Libraries.LibMotionComic.Model.Components.Entities;
using Bau.Libraries.LibMotionComic.Model.Components.PageItems;
using Bau.Libraries.LibMotionComic.Model.Components.PageItems.Brushes;

namespace Bau.Controls.ComicControls.Controls
{
	/// <summary>
	///		Herramientas para las vistas
	/// </summary>
	internal static class ViewTools
	{
		/// <summary>
		///		Convierte un ajuste
		/// </summary>
		internal static Stretch Convert(PageModel.StretchMode intStretch)
		{	switch (intStretch)
				{	case PageModel.StretchMode.None:
						return Stretch.None;
					case PageModel.StretchMode.Uniform:
						return Stretch.Uniform;
					case PageModel.StretchMode.UniformToFill:
						return Stretch.UniformToFill;
					default:
						return Stretch.Fill;
				}
		}

		/// <summary>
		///		Asigna el lápiz a una figura
		/// </summary>
		internal static void AssignPen(Shape objView, PenModel objPen)
		{	if (objPen != null)
				{ // Asigna los datos del lápiz
						objView.Stroke = new SolidColorBrush(Color.FromArgb(objPen.Color.Alpha, objPen.Color.Red, 
																																objPen.Color.Green, objPen.Color.Blue));
						objView.StrokeThickness = objPen.Thickness;
					// Añade los puntos
						foreach (double dblDot in objPen.Dots)
							objView.StrokeDashArray.Add(dblDot);
						objView.StrokeDashCap = Convert(objPen.CapDots);
						if (objPen.DashOffset != null)
							objView.StrokeDashOffset = objPen.DashOffset ?? 0;
						if (objPen.MiterLimit != null)
							objView.StrokeMiterLimit = objPen.MiterLimit ?? 1;
						objView.StrokeStartLineCap = Convert(objPen.StartLineCap);
						objView.StrokeEndLineCap = Convert(objPen.EndLineCap);
						objView.StrokeLineJoin = Convert(objPen.JoinMode);
				}
		}

		/// <summary>
		///		Convierte el modo de la línea
		/// </summary>
		private static PenLineCap Convert(PenModel.LineCap intCap)
		{ switch (intCap)
				{	case PenModel.LineCap.Round:
						return PenLineCap.Round;
					case PenModel.LineCap.Square:
						return PenLineCap.Square;
					case PenModel.LineCap.Triangle:
						return PenLineCap.Triangle;
					default:
						return PenLineCap.Flat;
				}
		}

		/// <summary>
		///		Convierte el modo de unión de las líneas
		/// </summary>
		private static PenLineJoin Convert(PenModel.LineJoin intJoin)
		{ switch (intJoin)
				{	case PenModel.LineJoin.Bevel:
						return PenLineJoin.Bevel;
					case PenModel.LineJoin.Round:
						return PenLineJoin.Round;
					default:
						return PenLineJoin.Miter;
				}
		}

		/// <summary>
		///		Convierte un modelo de brocha en una brocha de WPF
		/// </summary>
		internal static Brush GetBrush(AbstractPageItemModel objPageItem, AbstractBaseBrushModel objBrush)
		{ if (objPageItem.Page == null)
				return null;
			else
				return GetBrush(objPageItem.Page, objBrush);
		}

		/// <summary>
		///		Convierte un modelo de brocha en una brocha de WPF
		/// </summary>
		internal static Brush GetBrush(PageModel objPage, AbstractBaseBrushModel objBrush)
		{ if (objBrush == null)
				return null;
			else if (objBrush is SolidBrushModel)
				return ConvertSolidBrush(objPage, objBrush as SolidBrushModel);
			else if (objBrush is ImageBrushModel)
				return ConvertImageBrush(objPage, objBrush as ImageBrushModel);
			else if (objBrush is RadialGradientBrushModel)
				return ConvertRadialBrush(objPage, objBrush as RadialGradientBrushModel);
			else if (objBrush is LinearGradientBrushModel)
				return ConvertLinearBrush(objPage, objBrush as LinearGradientBrushModel);
			else
				return null;
		}

		/// <summary>
		///		Convierte una definición de brocha
		/// </summary>
		private static AbstractBaseBrushModel GetBrushDefinition(PageModel objPage, string strResourceKey)
		{ AbstractBaseBrushModel objResource = objPage.Comic.Resources.Search(strResourceKey) as AbstractBaseBrushModel;

				if (objResource != null && objResource is AbstractBaseBrushModel)
					return objResource;
				else
					return null;
		}


		/// <summary>
		///		Obtiene una brocha de un color sólido
		/// </summary>
		private static SolidColorBrush ConvertSolidBrush(PageModel objPage, SolidBrushModel objBrush)
		{	// Obtiene la brocha a partir del nombre del recurso
				if (!string.IsNullOrEmpty(objBrush.ResourceKey))
					objBrush = GetBrushDefinition(objPage, objBrush.ResourceKey) as SolidBrushModel;
			// Obtiene la brocha
				if (objBrush?.Color != null && !objBrush.Color.IsEmpty)
					return new SolidColorBrush(Color.FromArgb(objBrush.Color.Alpha, objBrush.Color.Red, 
																										objBrush.Color.Green, objBrush.Color.Blue));
				else
					return null;
		}

		/// <summary>
		///		Obtiene una brocha de imagen
		/// </summary>
		private static Brush ConvertImageBrush(PageModel objPage, ImageBrushModel objBrush)
		{	ImageBrush brsBackground = null;
			string strFileName = GetFileName(objPage.Comic, objBrush.ResourceKey, objBrush.FileName);

				// Asigna las propiedades al fondo
					if (!string.IsNullOrEmpty(strFileName) && System.IO.File.Exists(strFileName))
						{ // Genera el fondo
								brsBackground = new ImageBrush();
							// Asigna la imagen
								brsBackground.ImageSource = LoadImage(strFileName);
								brsBackground.Stretch = Convert(objBrush.Stretch);
								brsBackground.TileMode = Convert(objBrush.TileMode);
							// Asigna el viewport de la imagen
								if (objBrush.ViewPort != null)
									{ brsBackground.Viewport = ConvertToRect(objBrush.ViewPort);
										brsBackground.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
									}
							// Asigna el ViewBox de la imagen
								if (objBrush.ViewBox != null)
									{ brsBackground.Viewbox = ConvertToRect(objBrush.ViewBox);
										brsBackground.ViewboxUnits = BrushMappingMode.RelativeToBoundingBox;
									}
						}
				// Asigna el fondo
					return brsBackground;
		}

		/// <summary>
		///		Convierte un rectángulo
		/// </summary>
		internal static Rect ConvertToRect(RectangleModel rctRectangle)
		{	return new Rect(rctRectangle.LeftDefault, rctRectangle.TopDefault,
											rctRectangle.WidthDefault, rctRectangle.HeightDefault);
		}

		/// <summary>
		///		Convierte el modo de relleno
		/// </summary>
		internal static TileMode Convert(ImageBrushModel.TileType intTileMode)
		{ switch (intTileMode)
				{	case ImageBrushModel.TileType.FlipX:
						return TileMode.FlipX;
					case ImageBrushModel.TileType.FlipXY:
						return TileMode.FlipXY;
					case ImageBrushModel.TileType.FlipY:
						return TileMode.FlipY;
					case ImageBrushModel.TileType.Tile:
						return TileMode.Tile;
					default:
						return TileMode.None;
				}
		}

		/// <summary>
		///		Carga una imagen
		/// </summary>
		internal static BitmapImage LoadImage(ImageModel objImage)
		{	return LoadImage(GetFileName(objImage.Page.Comic, objImage.ResourceKey, objImage.FileName));
		}

		/// <summary>
		///		Carga una imagen
		/// </summary>
		private static BitmapImage LoadImage(string strFileName)
		{	BitmapImage bmpSource = null;

				// Carga la imagen
					if (!string.IsNullOrEmpty(strFileName) && System.IO.File.Exists(strFileName))
						{ // Crea un nuevo bitmap
								bmpSource = new BitmapImage();
							// Carga la imagen
								bmpSource.BeginInit();
								bmpSource.UriSource = new Uri(strFileName, UriKind.Relative);
								bmpSource.CacheOption = BitmapCacheOption.OnLoad; // ... carga la imagen en este momento, no la deja en caché
								bmpSource.EndInit();
						}
					else
						{ // Crea un nuevo bitmap
								bmpSource = new BitmapImage();
							// Carga la imagen
								bmpSource.BeginInit();
								bmpSource.UriSource = new Uri("pack://application:,,,/ComicControls;component/Assets/DefaultCover.png", UriKind.Absolute);
								bmpSource.CacheOption = BitmapCacheOption.OnLoad; // ... carga la imagen en este momento, no la deja en caché
								bmpSource.EndInit();
						}
				// Devuelve la imagen cargada
					return bmpSource;
		}

		/// <summary>
		///		Obtiene el nombre de archivo
		/// </summary>
		private static string GetFileName(ComicModel objComic, string strResourceKey, string strFileName)
		{	if (!string.IsNullOrEmpty(strResourceKey))
				return (objComic.Resources.Search(strResourceKey) as ImageModel)?.FileName;
			else
				return strFileName;
		}

		/// <summary>
		///		Crea una brocha con un gradiante lineal
		/// </summary>
		private static LinearGradientBrush ConvertLinearBrush(PageModel objPage, LinearGradientBrushModel objBrush)
		{ LinearGradientBrush brsBackground = new LinearGradientBrush();

				// Obtiene la brocha a partir del nombre del recurso
					if (!string.IsNullOrEmpty(objBrush.ResourceKey))
						objBrush = GetBrushDefinition(objPage, objBrush.ResourceKey) as LinearGradientBrushModel;
				// Si realmente se ha encontrado una brocha ...
					if (objBrush != null)
						{ // Asigna los datos de la brocha
								brsBackground.StartPoint = Convert(objBrush.Start);
								brsBackground.EndPoint = Convert(objBrush.End);
								brsBackground.SpreadMethod = Convert(objBrush.SpreadMethod);
							// Asigna los puntos
								AddGradientStops(brsBackground, objBrush.Stops);
						}
				// Devuelve la brocha
					return brsBackground;
		}

		/// <summary>
		///		Crea una brocha con un gradiante circular
		/// </summary>
		private static RadialGradientBrush ConvertRadialBrush(PageModel objPage, RadialGradientBrushModel objBrush)
		{ RadialGradientBrush brsBackground = new RadialGradientBrush();

				// Obtiene la brocha a partir del nombre del recurso
					if (!string.IsNullOrEmpty(objBrush.ResourceKey))
						objBrush = GetBrushDefinition(objPage, objBrush.ResourceKey) as RadialGradientBrushModel;
				// Si realmente se ha encontrado una brocha
					if (objBrush != null)
						{ // Asigna los datos de la brocha
								brsBackground.Center = Convert(objBrush.Center);
								brsBackground.RadiusX = objBrush.RadiusX;
								brsBackground.RadiusY = objBrush.RadiusY;
								brsBackground.GradientOrigin = Convert(objBrush.Origin);
							// Asigna los puntos
								AddGradientStops(brsBackground, objBrush.Stops);
						}
				// Devuelve la brocha
					return brsBackground;
		}

		/// <summary>
		///		Añade los puntos de parada del gradiante
		/// </summary>
		private static void AddGradientStops(GradientBrush brsBackground, List<GradientStopModel> objColStops)
		{	foreach (GradientStopModel objStop in objColStops)
				brsBackground.GradientStops.Add(new GradientStop(Convert(objStop.Color), objStop.Offset));
		}

		/// <summary>
		///		Convierte el método de relleno
		/// </summary>
		internal static GradientSpreadMethod Convert(AbstractBaseBrushModel.Spread intMethod)
		{ switch (intMethod)
				{	case AbstractBaseBrushModel.Spread.Reflect:
						return GradientSpreadMethod.Reflect;
					case AbstractBaseBrushModel.Spread.Repeat:
						return GradientSpreadMethod.Repeat;
					default:
						return GradientSpreadMethod.Pad;
				}
		}

		/// <summary>
		///		Convierte una regla de relleno de una geometría
		/// </summary>
		internal static FillRule Convert(ShapeModel.FillRule intFillMode)
		{ switch (intFillMode)
				{	case ShapeModel.FillRule.NonZero:
						return FillRule.Nonzero;
					default:
						return FillRule.EvenOdd;
				}
		}

		/// <summary>
		///		Convierte un color
		/// </summary>
		internal static Color Convert(ColorModel objColor)
		{ if (objColor == null || objColor.IsEmpty)
				return Color.FromArgb(255, 255, 255, 255);
			else
				return Color.FromArgb(objColor.Alpha, objColor.Red, objColor.Green, objColor.Blue);
		}

		/// <summary>
		///		Convierte un punto
		/// </summary>
		internal static Point Convert(PointModel objPoint)
		{ return new Point(objPoint.X, objPoint.Y);
		}
	}
}
