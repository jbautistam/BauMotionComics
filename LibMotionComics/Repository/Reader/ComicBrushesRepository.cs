using System;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMotionComic.Model.Components.PageItems.Brushes;

namespace Bau.Libraries.LibMotionComic.Repository.Reader
{
	/// <summary>
	///		Clase para carga de brochas
	/// </summary>
	internal class ComicBrushesRepository
	{
		internal ComicBrushesRepository(ComicReaderMediator objMediator)
		{ Mediator = objMediator;
		}

		/// <summary>
		///		Carga la primera brocha de una serie de nodos
		/// </summary>
		internal AbstractBaseBrushModel LoadFirstBrush(MLNode objMLNode)
		{ AbstractBaseBrushModel objBrush = null;

				// Carga los datos de la brocha (sólo recoge la primera de todas las que puede haber)
					foreach (MLNode objMLChild in objMLNode.Nodes)
						if (objBrush == null)
							objBrush = LoadBrush(objMLChild);
				// Devuelve la brocha
					return objBrush;
		}

		/// <summary>
		///		Carga las datos de una brocha
		/// </summary>
		internal AbstractBaseBrushModel LoadBrush(MLNode objMLNode)
		{ // Carga los datos de la brocha
				switch (objMLNode.Name)
					{	case ComicRepositoryConstants.cnstStrTagSolidBrush:
							return LoadSolidBrush(objMLNode);
						case ComicRepositoryConstants.cnstStrTagImageBrush:
							return LoadImageBrush(objMLNode);
						case ComicRepositoryConstants.cnstStrTagRadialBrush:
							return LoadRadialBrush(objMLNode);
						case ComicRepositoryConstants.cnstStrTagLinearBrush:
							return LoadLinearBrush(objMLNode);
					}
			// Devuelve la brocha
				return null;
		}

		/// <summary>
		///		Carga los datos de una brocha sólida
		/// </summary>
		private SolidBrushModel LoadSolidBrush(MLNode objMLNode)
		{ return new SolidBrushModel(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value,
																 objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagResourceKey].Value,
																 objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagColor].Value);
		}

		/// <summary>
		///		Carga los datos de una brocha de imagen
		/// </summary>
		private ImageBrushModel LoadImageBrush(MLNode objMLNode)
		{ return new ImageBrushModel(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value,
																 objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagResourceKey].Value,
																 objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagFileName].Value,
																 Mediator.CommonRepository.GetRectangle(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagViewBox].Value),
																 Mediator.CommonRepository.GetRectangle(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagViewPort].Value),
																 Mediator.CommonRepository.ConvertTile(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagTileMode].Value),
																 Mediator.CommonRepository.GetStretchMode(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagStretch].Value));
		}

		/// <summary>
		///		Carga un gradiante circular
		/// </summary>
		private RadialGradientBrushModel LoadRadialBrush(MLNode objMLNode)
		{ RadialGradientBrushModel objRadial = new RadialGradientBrushModel(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value,
																																				objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagResourceKey].Value);

				// Carga los datos del gradiante
					objRadial.Center = Mediator.CommonRepository.GetPoint(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagCenter].Value);
					objRadial.RadiusX = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagRadiusX].Value.GetDouble(1);
					objRadial.RadiusY = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagRadiusY].Value.GetDouble(1);
					objRadial.Origin = Mediator.CommonRepository.GetPoint(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagOrigin].Value);
					objRadial.SpreadMethod = Mediator.CommonRepository.GetSpreadMethod(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagSpread].Value);
				// Asigna los puntos de parada
					objRadial.Stops.AddRange(LoadStops(objMLNode));
				// Devuelve el gradiante
					return objRadial;
		}

		/// <summary>
		///		Carga un gradiante lineal
		/// </summary>
		private LinearGradientBrushModel LoadLinearBrush(MLNode objMLNode)
		{ LinearGradientBrushModel objLinear = new LinearGradientBrushModel(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value,
																																				objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagResourceKey].Value);

				// Carga los datos del gradiante
					objLinear.Start = Mediator.CommonRepository.GetPoint(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagStart].Value);
					objLinear.End = Mediator.CommonRepository.GetPoint(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagEnd].Value);
					objLinear.SpreadMethod = Mediator.CommonRepository.GetSpreadMethod(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagSpread].Value);
				// Añade los puntos de parada
					objLinear.Stops.AddRange(LoadStops(objMLNode));
				// Devuelve el gradiante
					return objLinear;
		}

		/// <summary>
		///		Carga los puntos de parada
		/// </summary>
		private System.Collections.Generic.List<GradientStopModel> LoadStops(MLNode objMLNode)
		{ System.Collections.Generic.List<GradientStopModel> objColStops = new System.Collections.Generic.List<GradientStopModel>();

				// Añade los puntos de parada
					foreach (MLNode objMLChild in objMLNode.Nodes)
						objColStops.Add(new GradientStopModel(Mediator.CommonRepository.GetColor(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagColor].Value),
																									objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagOffset].Value.GetDouble(0)));
				// Devuelve la colección de puntos
					return objColStops;
		}

		/// <summary>
		///		Mediator
		/// </summary>
		private ComicReaderMediator Mediator { get; }
	}
}
