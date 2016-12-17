using System;

namespace Bau.Libraries.MVVM.ViewModels.ComboItems
{
	/// <summary>
	///		Elemento de un combo
	/// </summary>
	public class ComboItem
	{
		public ComboItem(int? intID = null, string strText = null, object objTag = null)
		{ ID = intID;
			Text = strText;
			Tag = objTag;
		}

		/// <summary>
		///		ID del elemento
		/// </summary>
		public int? ID { get; set; }

		/// <summary>
		///		Texto del elemento
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		///		Objeto asociado al elemento
		/// </summary>
		public object Tag { get; set; }
	}
}
