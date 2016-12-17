using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Entities
{
	/// <summary>
	///		Clase con los datos de un rectángulo
	/// </summary>
	public class RectangleModel
	{
		public RectangleModel(double? dblTop = null, double? dblLeft = null, 
													double? dblWidth = null, double? dblHeight = null)
		{ Top = dblTop;
			Left = dblLeft;
			Width = dblWidth;
			Height = dblHeight;
		}

		/// <summary>
		///		Obtiene un rectángulo escalado
		/// </summary>
		public RectangleModel Scale(double dblComicWidth, double dblComicHeight, double dblViewPortWidth, double dblViewPortHeight)
		{	
			//double dblRatioX = dblViewPortWidth / WidthDefault;
			//double dblRatioY = dblViewPortHeight / HeightDefault;
			//double dblRatio;
			//double dblWidth, dblHeight;
			//double dblLeft, dblTop;

			//	// Calcula el ratio
			//		if (dblRatioX < dblRatioY)
			//			dblRatio = dblRatioX;
			//		else
			//			dblRatio = dblRatioY;
			//	// Calcula el ancho y alto manteniendo el aspecto
			//		dblWidth = WidthDefault * dblRatio;
			//		dblHeight = HeightDefault * dblRatio;
			//	// Calcula la posición superior iquierda
			//		dblTop = TopDefault * dblRatio;
			//		dblLeft = LeftDefault * dblRatio;
			//	// Devuelve el rectángulo escalado
			//		return new RectangleModel(dblTop, dblLeft, dblWidth, dblHeight);
		
			return new RectangleModel((TopDefault / dblComicHeight) * dblViewPortHeight,
																(LeftDefault / dblComicWidth) * dblViewPortWidth,
																(WidthDefault / dblComicWidth) * dblViewPortWidth,
																(HeightDefault / dblComicHeight) * dblViewPortHeight);
		}

		/// <summary>
		///		Restringe un valor superior
		/// </summary>
		public double GetNotNullTop(double? dblTop, double dblDefault = 0)
		{ return Top ?? dblTop ?? dblDefault;
		}

		/// <summary>
		///		Restringe el valor de izquierda
		/// </summary>
		public double GetNotNullLeft(double? dblLeft, double dblDefault = 0)
		{ return Left ?? dblLeft ?? dblDefault;
		}

		/// <summary>
		///		Restringe el ancho
		/// </summary>
		public double GetNotNullWidth(double? dblWidth, double dblDefault = 0)
		{ return Width ?? dblWidth ?? dblDefault;
		}

		/// <summary>
		///		Restringe la altura
		/// </summary>
		public double GetNotNullHeight(double? dblHeight, double dblDefault = 0)
		{ return Height ?? dblHeight ?? dblDefault;
		}

		/// <summary>
		///		Superior
		/// </summary>
		public double? Top { get; set; }

		/// <summary>
		///		Valor predeterminado para el punto superior
		/// </summary>
		public double TopDefault
		{ get { return Top ?? 0; }
		}

		/// <summary>
		///		Izquierda
		/// </summary>
		public double? Left { get; set; }

		/// <summary>
		///		Valor predeterminado para el punto izquierdo
		/// </summary>
		public double LeftDefault
		{ get { return Left ?? 0; }
		}

		/// <summary>
		///		Ancho
		/// </summary>
		public double? Width { get; set; }

		/// <summary>
		///		Valor predeterminado para el ancho
		/// </summary>
		public double WidthDefault
		{ get { return Width ?? 100; }
		}

		/// <summary>
		///		Alto
		/// </summary>
		public double? Height { get; set; }

		/// <summary>
		///		Valor predeterminado para el alto
		/// </summary>
		public double HeightDefault
		{ get { return Height ?? 100; }
		}
	}
}
