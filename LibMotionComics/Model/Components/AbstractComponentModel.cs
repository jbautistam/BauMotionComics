using System;

namespace Bau.Libraries.LibMotionComic.Model.Components
{	
	/// <summary>
	///		Clase base para los componentes
	/// </summary>
	public class AbstractComponentModel
	{
		public AbstractComponentModel(string strKey)
		{ Key = strKey;
			if (string.IsNullOrEmpty(Key))
				Key = Guid.NewGuid().ToString();
		}

		/// <summary>
		///		Clave del elemento
		/// </summary>
		public string Key { get; }
	}
}
