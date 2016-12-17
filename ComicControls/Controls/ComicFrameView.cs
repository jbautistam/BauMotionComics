using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

using Bau.Libraries.LibMotionComic.Model.Components.PageItems;

namespace Bau.Controls.ComicControls.Controls
{
	/// <summary>
	///		View para mostrar un frame 
	/// </summary>
	internal class ComicFrameView : Grid
	{
		/// <summary>
		///		Inicializa la figura
		/// </summary>
		public void InitShape(FrameModel objFrame, string strLanguageSelected, string strLanguageDefault)
		{ // Asigna el borde del control
				if (objFrame.Shape != null)
					Border = new ComicShapeView(objFrame.Shape);
				else
					{ Border = new Rectangle();
						if (objFrame.RadiusX != null)
							(Border as Rectangle).RadiusX = objFrame.RadiusX ?? 0;
						if (objFrame.RadiusY != null)
							(Border as Rectangle).RadiusY = objFrame.RadiusY ?? 0;
					}
				Border.Stretch = Stretch.Fill;
			// Asigna el fondo
				Border.Fill = ViewTools.GetBrush(objFrame, objFrame.Brush);
			// Asigna el borde
				ViewTools.AssignPen(Border, objFrame.Pen);
			// Añade la figura al canvas
				Children.Add(Border);
			// Asigna la posición a la figura
				Grid.SetRow(Border, 0);
				Grid.SetColumn(Border, 0);
			// Añade los textos
				InitTexts(objFrame, strLanguageSelected, strLanguageDefault);
		}

		/// <summary>
		///		Inicializa los textos
		/// </summary>
		private void InitTexts(FrameModel objFrame, string strLanguageSelected, string strLanguageDefault)
		{ foreach (TextModel objText in objFrame.Texts)
				{ TextFormattedGeometry objTextGeometry = new TextFormattedGeometry();

						// Asigna las propiedades
							objTextGeometry.InitView(objText, objText.Texts.GetText(strLanguageSelected, strLanguageDefault));
						// Añade el texto al control
							Children.Add(objTextGeometry);
				}
		}

		/// <summary>
		///		Mueve el fondo
		/// </summary>
		internal void MoveBrush(double dblTop, double dblLeft)
		{ ImageBrush objBrush = Border.Fill as ImageBrush;

				if (objBrush != null)
					{ objBrush.Transform = new TranslateTransform(dblLeft, dblTop);
					}
		}

		/// <summary>
		///		Figura del borde
		/// </summary>
		public Shape Border { get; set; }
	}
}
