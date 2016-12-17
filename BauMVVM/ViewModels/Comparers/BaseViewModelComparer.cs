using System;
using System.Collections.Generic;

namespace Bau.Libraries.MVVM.ViewModels.Comparer
{
	/// <summary>
	///		Clase base para los comparadores
	/// </summary>
	public abstract class BaseViewModelComparer<TypeData> : IComparer<TypeData> where TypeData : BaseViewModel
	{
		public BaseViewModelComparer(bool blnAscending)
		{ IsAscending = blnAscending;
		}

		/// <summary>
		///		Compara dos elementos
		/// </summary>
		public int Compare(TypeData objFirst, TypeData objSecond)
		{ if (objFirst == null && objSecond == null)
				return 0;
			else
				return GetSortFactor() * CompareData(objFirst, objSecond);
		}

		/// <summary>
		///		Compara dos elementos
		/// </summary>
		protected abstract int CompareData(TypeData objFirst, TypeData objSecond);

		/// <summary>
		///		Factor para la ordenación ascendente / descendente
		/// </summary>
		private int GetSortFactor()
		{ if (IsAscending)
				return 1;
			else
				return -1;
		}

		/// <summary>
		///		Indica si la ordenación es ascendente / descendente
		/// </summary>
		private bool IsAscending { get; set; }
	}
}
