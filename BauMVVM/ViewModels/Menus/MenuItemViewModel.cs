using System;

namespace Bau.Libraries.MVVM.ViewModels.Menus
{
	/// <summary>
	///		ViewModel para un menú
	/// </summary>
	public class MenuItemViewModel
	{
		public MenuItemViewModel(string strText = null, string strIcon = null, BaseCommand objCommand = null)
		{ Text = strText;
			Icon = strIcon;
			Command = objCommand;
		}

		/// <summary>
		///		Texto
		/// </summary>
		public string Text { get; private set; }

		/// <summary>
		///		Icono del menú
		/// </summary>
		public string Icon { get; private set; }

		/// <summary>
		///		Comando
		/// </summary>
		public BaseCommand Command { get; private set; }

		/// <summary>
		///		Indica si el menú es un separador
		/// </summary>
		public bool IsSeparator 
		{ get { return string.IsNullOrEmpty(Text); }
		}
	}
}
