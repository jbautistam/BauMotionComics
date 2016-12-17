using System;
using System.Windows.Media;
using System.Windows.Shapes;

using Bau.Libraries.LibMotionComic.Model.Components;
using Bau.Libraries.LibMotionComic.Model.Components.PageItems;

namespace Bau.Controls.ComicControls.Controls
{
	/// <summary>
	///		Control para mostrar una figura de un cómic
	/// </summary>
	internal class ComicShapeView : Shape
	{ // Variables privadas
			private ShapeModel objShape = null;
			private Geometry objGeometry = null;
			private bool blnIsDirty = false;

		public ComicShapeView(ShapeModel objShape)
		{ this.objShape = objShape;
			objGeometry = CreateGeometry();
		}

		/// <summary>
		///		Crea la geometría
		/// </summary>
		private Geometry CreateGeometry()
		{ PathGeometry objPathGeometry = new PathGeometry();

				// Obtiene la figura de los recursos si es necesario
					objShape = GetShape(objShape);
				// Añade las figuras a la geometría
					foreach (FigureModel objFigure in objShape.Figures)
						objPathGeometry.AddGeometry(Geometry.Combine(Geometry.Empty,
																												 Geometry.Parse(objFigure.Data), 
																												 GeometryCombineMode.Union, 
																												 ViewTransformTools.GetTransforms(objFigure.Transforms)));
				// Asigna el método de relleno
					objPathGeometry.FillRule = ViewTools.Convert(objShape.FillMode);
				// Añade las geometrías a la geometría principal
				//! Esto no parece añadir nada en especial
					objPathGeometry.Transform = ViewTransformTools.GetTransforms(objShape.Transforms);
				// Devuelve la geometría creada
					return objPathGeometry;
		}

		/// <summary>
		///		Obtiene la definición de la figura
		/// </summary>
		private ShapeModel GetShape(ShapeModel objShape)
		{ // Obtiene las figuras buscando entre los recursos
				if (objShape != null && !string.IsNullOrEmpty(objShape.ComponentKey))
					{ AbstractComponentModel objResource = objShape.Page.Comic.Resources.Search(objShape.ComponentKey);

							// Obtiene los datos del recurso
								if (objResource != null && objResource is ShapeModel)
									{ ShapeModel objResourceShape = objResource as ShapeModel;

											// Obtiene el modo de relleno
												objShape.FillMode = objResourceShape.FillMode;
											// Obtiene las figuras
												objShape.Figures.Clear();
												objShape.Figures.AddRange(objResourceShape.Figures);
											// Añade las transformaciones
												objShape.Transforms.InsertRange(0, objResourceShape.Transforms);
									}
					}
			// Devuelve la figura
				return objShape;
		}

		/// <summary>
		///		Geometría de la figura
		/// </summary>
		protected override Geometry DefiningGeometry
		{ get
			{ // Crea la geometría si no existía
					if (objGeometry == null || blnIsDirty)
						objGeometry = CreateGeometry();
				// Devuelve la geometría
					return objGeometry;
			}
		}
	}
}