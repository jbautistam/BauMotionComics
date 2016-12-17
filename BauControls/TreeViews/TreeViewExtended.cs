using System;
using System.Windows;
using System.Windows.Controls;

namespace Bau.Controls.TreeViews
{
	/// <summary>
	///		Control extendido para <see cref="TreeView"/>
	/// </summary>
	public class TreeViewExtended : TreeView
	{ // Propiedades dependientes
			public static readonly DependencyProperty SelectedItemMVVMProperty = DependencyProperty.Register("SelectedItemMVVM", typeof(object), 
																																																			 typeof(TreeViewExtended), 
																																																			 new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, 
																																																																		 null, null, true, 
																																																																		 System.Windows.Data.UpdateSourceTrigger.PropertyChanged));
		public TreeViewExtended() : base()
    {
    }

		/// <summary>
		///		Sobrescribe el tratamiento del evento SelectedItemChanged
		/// </summary>
		protected override void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<object> e)
		{ // Asigna el elemento seleccionado
				SelectedItemMVVM = SelectedItem;
			// Llama al evento base
				base.OnSelectedItemChanged(e);
		}

		/// <summary>
		///		Elemento seleccionado (para MVVM)
		/// </summary>
    public object SelectedItemMVVM
    { get { return (object) GetValue(SelectedItemMVVMProperty); }
			set { SetValue(SelectedItemMVVMProperty, value); }
    }
	}
}
