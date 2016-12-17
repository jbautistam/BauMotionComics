using System;
using System.Windows;

namespace Bau.Controls.WebExplorers
{
	[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
	[System.Runtime.InteropServices.ComVisible(true)]
	public class WebExplorerScriptingHelper
	{ 
    public WebExplorerScriptingHelper(WebExplorerExtended udtWebExplorer)
    {	WebExplorer = udtWebExplorer;
    }

		/// <summary>
		///		Función a la que se llama cuando se invoca a una función externa desde JavaScript
		/// </summary>
    public void ExternalFunction(string strJavaScriptArguments)
		{ WebExplorer.RaiseScriptArguments(strJavaScriptArguments);
		}

		/// <summary>
		///		Explorador al que se asocia el objeto
		/// </summary>
		public WebExplorerExtended WebExplorer { get; private set; }
	}
}
