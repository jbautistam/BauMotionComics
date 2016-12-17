using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Bau.Libraries.MVVM.Tools
{
	/// <summary>
	///		Métodos de ayuda para Wpf
	/// </summary>
	public class ToolsWpf
	{
		/// <summary>
		///		Obtener la ventana padre de un control
		/// </summary>
		public Window GetParentWindow(DependencyObject ctlControl)
		{ DependencyObject ctlParent = VisualTreeHelper.GetParent(ctlControl);

				// Busca recursivamente la ventana padre
					if (ctlParent == null)
						return null;
					else if (ctlParent is Window)
						return ctlParent as Window;
					else
						return GetParentWindow(ctlParent);
		}

		/// <summary>
		///		Busca en el árbol visual el primer control de un tipo que sea padre del pasado como parámetro
		/// </summary>
		public TypeControl FindAncestor<TypeControl>(DependencyObject objSource) where TypeControl : DependencyObject
		{ // Recorre el árbol de controles buscando el primer control padre del tipo o hasta que se
			// encuentra el nodo raíz
				do
					{	// Si estamos en un objeto del tipo buscado, lo devolvemos, si no, comprobamos el padre
							if (objSource is TypeControl)
								return objSource as TypeControl;
							else
								objSource = VisualTreeHelper.GetParent(objSource);
					}
				while (objSource != null);
			// Si ha llegado hasta aquí es porque no ha encontrado nada
				return null;
		}

		/// <summary>
		///		Obtiene una imagen a partir de un Uri
		/// </summary>
		/// <param name="strUri">La cadena Uri debe ser del tipo "pack://application:,,,/BauControls;component/Themes/Images/Solution.png"</param>
		public Image GetImage(string strUri)
		{ Image objImage = new Image();
			
				// Asigna el origen de la imagen
					if (strUri != null && !string.IsNullOrEmpty(strUri))
						try
							{ objImage.Source = new ImageSourceConverter().ConvertFromString(strUri) as ImageSource;
							}
						catch {}
				// Devuelve la imagen
					return objImage;
		}
	}
}
