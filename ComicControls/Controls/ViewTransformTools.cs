using System;
using System.ComponentModel;
using System.Windows.Media;

using Bau.Libraries.LibMotionComic.Model.Components.Transforms;

namespace Bau.Controls.ComicControls.Controls
{
	/// <summary>
	///		Herramientas para transformaciones de vistas / controles
	/// </summary>
	internal static class ViewTransformTools
	{
		/// <summary>
		///		Obtiene las transformaciones de la figura
		/// </summary>
		internal static Transform GetTransforms(TransformModelCollection objColTransforms)
		{ Transform objTransform = new TranslateTransform(0, 0);

				// Obtiene las transformaciones de la figura
					if (objColTransforms != null && objColTransforms.Count > 0)
						{ // Crea un grupo de transformaciones
								objTransform = new TransformGroup();
							// Añade las transformaciones de la figura
								foreach (AbstractTransformModel objFigureTransform in objColTransforms)
									if (objFigureTransform is TranslateTransformModel)
										(objTransform as TransformGroup).Children.Add(ConvertTransform(objFigureTransform as TranslateTransformModel));
									else if (objFigureTransform is MatrixTransformModel)
										(objTransform as TransformGroup).Children.Add(ConvertTransform(objFigureTransform as MatrixTransformModel));
									else if (objFigureTransform is RotateTransformModel)
										(objTransform as TransformGroup).Children.Add(ConvertTransform(objFigureTransform as RotateTransformModel));
									else if (objFigureTransform is ScaleTransformModel)
										(objTransform as TransformGroup).Children.Add(ConvertTransform(objFigureTransform as ScaleTransformModel));
						}
				// Devuelve la transformación
					return objTransform;
		}

		/// <summary>
		///		Convierte una transformación de escala
		/// </summary>
		private static Transform ConvertTransform(ScaleTransformModel objTransform)
		{ return new ScaleTransform(objTransform.ScaleX, objTransform.ScaleY, 
																objTransform.Center.X, objTransform.Center.Y);
		}

		/// <summary>
		///		Convierte una rotación
		/// </summary>
		private static Transform ConvertTransform(RotateTransformModel objTransform)
		{ return new RotateTransform(objTransform.Angle, objTransform.Origin.X, objTransform.Origin.Y);
		}

		/// <summary>
		///		Convierte una transformación de traslación
		/// </summary>
		private static Transform ConvertTransform(TranslateTransformModel objTransform)
		{ return new TranslateTransform(objTransform.Left, objTransform.Top);
		}

		/// <summary>
		///		Convierte una transformación de matriz
		/// </summary>
		private static Transform ConvertTransform(MatrixTransformModel objTransform)
		{	TypeConverter objConverter = TypeDescriptor.GetConverter(typeof(Matrix));

				// Obtiene una transformación de matriz
					return new MatrixTransform((Matrix) objConverter.ConvertFrom(objTransform.Data));
		}
	}
}
