using System;
using System.Collections.Generic;

namespace Bau.Libraries.LibMotionComic.Model.Components
{
	/// <summary>
	///		Diccionario de componentes
	/// </summary>
	public class ResourcesDictionary : Dictionary<string, AbstractComponentModel>
	{
		/// <summary>
		///		Añade un elemento con la clave normalizada
		/// </summary>
		public new void Add(string strKey, AbstractComponentModel objComponent)
		{ base.Add(ComputeKey(strKey), objComponent);
		}

		/// <summary>
		///		Obtiene un componente del diccionario
		/// </summary>
		public AbstractComponentModel Search(string strKey)
		{ AbstractComponentModel objComponent;

				if (TryGetValue(ComputeKey(strKey), out objComponent))
					return objComponent;
				else
					return null;
		}

		/// <summary>
		///		Calcula una clave normalizada
		/// </summary>
		private string ComputeKey(string strKey)
		{ return strKey?.ToUpperInvariant();
		}
	}
}
