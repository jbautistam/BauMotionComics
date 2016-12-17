using System;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMotionComic.Model.Components;
using Bau.Libraries.LibMotionComic.Model.Components.Entities;
using Bau.Libraries.LibMotionComic.Model.Components.PageItems;

namespace Bau.Libraries.LibMotionComic.Repository.Reader
{
	/// <summary>
	///		Repository para funciones comunes
	/// </summary>
	internal class ComicCommonRepository
	{
		/// <summary>
		///		Obtiene el modo de ajuste
		/// </summary>
		internal PageModel.StretchMode GetStretchMode(string strValue)
		{ if (strValue.EqualsIgnoreCase("None"))
				return PageModel.StretchMode.None;
			else if (strValue.EqualsIgnoreCase("Uniform"))
				return PageModel.StretchMode.Uniform;
			else if (strValue.EqualsIgnoreCase("UniformToFill"))
				return PageModel.StretchMode.UniformToFill;
			else
				return PageModel.StretchMode.Fill;
		}

		/// <summary>
		///		Carga los datos de un punto
		/// </summary>
		internal PointModel GetPoint(string strValue, double dblDefaultX = 0, double dblDefaultY = 0)
		{ PointModel objPoint = new PointModel(0, 0);

				// Carga los datos
					if (!strValue.IsEmpty())
						{ string [] arrStrValue = strValue.Split(',');

								if (arrStrValue.Length > 0)
									objPoint.X = arrStrValue[0].GetDouble(dblDefaultX);
								if (arrStrValue.Length > 1)
									objPoint.Y = arrStrValue[1].GetDouble(dblDefaultY);
						}
				// Devuelve el punto
					return objPoint;
		}

		/// <summary>
		///		Carga los atributos de un elemento
		/// </summary>
		internal void AssignAttributesPageItem(MLNode objMLNode, AbstractPageItemModel objItem)
		{	objItem.Dimensions = AssignDimensions(objMLNode);
			objItem.Visible = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagVisible].Value.GetBool(true);
			objItem.Opacity = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagOpacity].Value.GetDouble(1);
			objItem.ZIndex = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagZIndex].Value.GetInt(1);
			if (!objItem.Visible)
				objItem.Opacity = 0;
		}

		/// <summary>
		///		Asigna las dimensiones
		/// </summary>
		internal RectangleModel AssignDimensions(MLNode objMLNode)
		{ return new RectangleModel(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagTop].Value.GetDouble(),
																objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagLeft].Value.GetDouble(),
																objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagWidth].Value.GetDouble(),
																objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagHeight].Value.GetDouble());
		}

		/// <summary>
		///		Obtiene un rectángulo a partir de una cadena de coordenada Top, Left, Width, Height
		/// </summary>
		internal RectangleModel GetRectangle(string strValue)
		{ RectangleModel rctRectangle = null;

				// Obtiene el rectángulo
					if (!strValue.IsEmpty())
						{ string [] arrStrValues = strValue.Split(',');

								if (arrStrValues.Length == 4)
									rctRectangle = new RectangleModel(arrStrValues[0].GetDouble(), 
																										arrStrValues[1].GetDouble(),
																										arrStrValues[2].GetDouble(),
																										arrStrValues[3].GetDouble());
						}
				// Devuelve el rectángulo
					return rctRectangle;
		}

		/// <summary>
		///		Convierte el modo de relleno
		/// </summary>
		internal Model.Components.PageItems.Brushes.ImageBrushModel.TileType ConvertTile(string strValue)
		{ if (strValue.EqualsIgnoreCase("FlipX"))
				return Model.Components.PageItems.Brushes.ImageBrushModel.TileType.FlipX;
			else if (strValue.EqualsIgnoreCase("FlipXY"))
				return Model.Components.PageItems.Brushes.ImageBrushModel.TileType.FlipXY;
			else if (strValue.EqualsIgnoreCase("FlipY"))
				return Model.Components.PageItems.Brushes.ImageBrushModel.TileType.FlipY;
			else if (strValue.EqualsIgnoreCase("Tile"))
				return Model.Components.PageItems.Brushes.ImageBrushModel.TileType.Tile;
			else
				return Model.Components.PageItems.Brushes.ImageBrushModel.TileType.None;
		}

		/// <summary>
		///		Obtiene el método de relleno
		/// </summary>
		internal Model.Components.PageItems.Brushes.AbstractBaseBrushModel.Spread GetSpreadMethod(string strValue)
		{ if (strValue.EqualsIgnoreCase("Reflect"))
				return Model.Components.PageItems.Brushes.AbstractBaseBrushModel.Spread.Reflect;
			else if (strValue.EqualsIgnoreCase("Repeat"))
				return Model.Components.PageItems.Brushes.AbstractBaseBrushModel.Spread.Repeat;
			else
				return Model.Components.PageItems.Brushes.AbstractBaseBrushModel.Spread.Pad;
		}

		/// <summary>
		///		Carga los datos de un color
		/// </summary>
		internal ColorModel GetColor(string strColor)
		{ return new ColorModel(strColor);
		}

		/// <summary>
		///		Normaliza una cadena
		/// </summary>
		internal string Normalize(string strValue)
		{	// Normaliza una cadena
				if (!strValue.IsEmpty())
					{ strValue = strValue.Replace('\n', ' ');
						strValue = strValue.Replace('\r', ' ');
						strValue = strValue.Replace('\t', ' ');
						strValue = strValue.ReplaceWithStringComparison("  ", " ");
						strValue = strValue.TrimIgnoreNull();
					}
			// Devuelve la cadena
				return strValue;
		}
	}
}
