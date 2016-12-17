using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BauMotionComics.Views
{
	/// <summary>
	///		Ventana para presentar una imagen
	/// </summary>
	public partial class ImageView : Window
	{
		public ImageView(string strFileName)
		{ // Inicializa los componentes
				InitializeComponent();
			// Carga el archivo
				LoadImage(strFileName);
				Title = System.IO.Path.GetFileName(strFileName);
			// Cambia el zoom
				pnlZoom.ZoomMode = Bau.Controls.Graphical.ImageZoomBoxPanel.eZoomMode.ActualSize;
		}

		/// <summary>
		///		Carga una imagen
		/// </summary>
		private void LoadImage(string strFileName)
		{ BitmapImage bmpImage = new BitmapImage();

				// Lee el archivo sobre la imagen
					bmpImage.BeginInit();
					bmpImage.StreamSource = new System.IO.FileStream(strFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
					bmpImage.CacheOption = BitmapCacheOption.OnLoad;
					bmpImage.EndInit();
				// Libera el stream para evitar excepciones de acceso al archivo cuando se intenta borrar la imagen
					bmpImage.StreamSource.Dispose();
				// Asigna la imagen
					imgImage.Source = bmpImage;
		}
	}
}
