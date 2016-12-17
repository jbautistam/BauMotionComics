using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

using Bau.Libraries.LibHelper.Extensors;

namespace Bau.Controls.WebExplorers
{
	/// <summary>
	///		Control de usuario para extensión del explorador Web
	/// </summary>
	public partial class WebExplorerExtended : UserControl
	{ // Propiedades de dependencia
			public static readonly DependencyProperty HiddenScriptErrorsProperty = DependencyProperty.Register("HiddenScriptErrors", typeof(bool), typeof(WebExplorerExtended), 
																																																				 new FrameworkPropertyMetadata() { DefaultValue = true } );
			public static readonly DependencyProperty HtmlContentProperty = DependencyProperty.Register("HtmlContent", typeof(string), typeof(WebExplorerExtended), 
																																																	new FrameworkPropertyMetadata("",
																																																																FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
		// Eventos públicos
			public event EventHandler<WebExplorerFunctionEventArgs> FunctionExecute;

		public WebExplorerExtended()
		{ // Inicializa el componente
				InitializeComponent();
			// Inicializa el objeto que atiende las llamadas de JavaScript
				wbExplorer.ObjectForScripting = new WebExplorerScriptingHelper(this);
		}

		/// <summary>
		///		Muestra una URL
		/// </summary>
		public void ShowUrl(string strUrl)
		{ try
				{ HideScriptErrors(HiddenScriptErrors);
					wbExplorer.Navigate(new Uri(strUrl, UriKind.RelativeOrAbsolute));
				}
			catch (Exception objException)
				{ System.Diagnostics.Debug.WriteLine(objException.Message);
				}
		}

		/// <summary>
		///		Muestra una cadena HTML
		/// </summary>
		public void ShowHtml(string strHtml)
		{ try
				{ HideScriptErrors(HiddenScriptErrors);
					wbExplorer.NavigateToString(RemoveIframe(strHtml));
				}
			catch (Exception objException)
				{ System.Diagnostics.Debug.WriteLine(objException.Message);
				}
		}

		/// <summary>
		///		Elimina el contenido de las etiquetas iFrame que pueden bloquear el explorador
		/// </summary>
		private string RemoveIframe(string strText)
		{ string strResult = strText;

				// Quita la etiqueta "iframe"
					while (!strResult.IsEmpty() && strResult.IndexOf("<iframe") >= 0)
						strResult = System.Text.RegularExpressions.Regex.Replace(strResult, "<iframe(.|\n)*?</iframe>", string.Empty);
				// Devuelve el resultado
					return strResult;
		}

		/// <summary>
		///		Llama a un método de JavaScript
		/// </summary>
		public void InvokeJavaScript(string strMethod, string strArguments)
		{ wbExplorer.InvokeScript(strMethod, new object[] { strArguments });
		}

		/// <summary>
		///		Oculta los errores de script
		/// </summary>
		private void HideScriptErrors(bool blnHide) 
		{	Dispatcher.Invoke(new Action(() =>
														{ var fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);

																	if (fiComWebBrowser != null)
																		{	var objComWebBrowser = fiComWebBrowser.GetValue(wbExplorer);

																				if (objComWebBrowser == null) // ... en este caso aún no se ha cargado el explorador
																					wbExplorer.Loaded += (o, s) => HideScriptErrors(blnHide);
																				else
																					objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object [] { blnHide });
																		}
														}
											), null);
		}

		/// <summary>
		///		Va una página atrás
		/// </summary>
		public void GoBack()
		{ if (CanGoBack)
				wbExplorer.GoBack();
		}

		/// <summary>
		///		Va una página adelante
		/// </summary>
		public void GoForward()
		{	if (CanGoForward)
				wbExplorer.GoForward();
		}

		/// <summary>
		///		Lanza los argumentos de una función javaScript
		/// </summary>
		internal void RaiseScriptArguments(string strJavaScriptArguments)
		{ if (FunctionExecute != null)
				FunctionExecute(this, new WebExplorerFunctionEventArgs(strJavaScriptArguments));
		}

		/// <summary>
		///		Indica si se deben mostrar o no los errores de JavaScript
		/// </summary>
		public bool HiddenScriptErrors
		{ get { return (bool) GetValue(HiddenScriptErrorsProperty); }
			set { SetValue(HiddenScriptErrorsProperty, value); }
		}

		/// <summary>
		///		Texto HTML a mostrar en el navegador
		/// </summary>
		public string HtmlContent
		{ get { return (string) GetValue(HtmlContentProperty); }
			set 
				{ // Asigna el valor
						SetValue(HtmlContentProperty, value); 
					// Muestra el HTML
						ShowHtml(value);
				}
		}

		/// <summary>
		///		Comprueba si puede ir una página hacia atrás
		/// </summary>
		public bool CanGoBack
		{ get { return wbExplorer.CanGoBack; }
		}

		/// <summary>
		///		Comprueba si puede ir una página hacia delante
		/// </summary>
		public bool CanGoForward
		{ get { return wbExplorer.CanGoForward; }
		}
	}
}
