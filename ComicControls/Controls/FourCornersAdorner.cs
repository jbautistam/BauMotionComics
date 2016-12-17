using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Bau.Controls.ComicControls.Controls
{
	/// <summary>
	///		Adorner para mostrar cuatro esquinas sobre un control
	/// </summary>
	internal class FourCornersAdorner : Adorner
	{
		public FourCornersAdorner(UIElement objControl) : base(objControl)
		{
		}

		/// <summary>
		///		Dibuja el control
		/// </summary>
		protected override void OnRender(DrawingContext dwContext)
		{	dwContext.DrawRectangle(Brushes.Red, null, new Rect(0, 0, 5, 5));
			dwContext.DrawRectangle(Brushes.Gray, null, new Rect(0, ActualHeight - 5, 5, 5));
			dwContext.DrawRectangle(Brushes.Olive, null, new Rect(ActualWidth - 5, 0, 5, 5));
			dwContext.DrawRectangle(Brushes.Yellow, null, new Rect(ActualWidth - 5, ActualHeight - 5, 5, 5));
		}
	}
}
