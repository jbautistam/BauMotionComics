using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ICSharpCode.AvalonEdit.Highlighting;
using Bau.Libraries.LibHelper.Extensors;

namespace Bau.Controls.CodeEditor
{
	/// <summary>
	///		Control de usuario para el editor
	/// </summary>
	public partial class ctlEditor : UserControl
	{ // Eventos públicos
			public event EventHandler TextChanged;
			//public event EventHandler<DragEventArgs> DragEnter;
			//public event EventHandler<DragEventArgs> Drop;
		// Variables privadas
			private string strFileName;

		public ctlEditor()
		{ InitializeComponent();
		}

		/// <summary>
		///		Cambia el modo de resalte a partir del nombre de archivo
		/// </summary>
		private void ChangeHighLight()
		{ string strExtension = ".cs";

				// Obtiene la extensión de archivo
					if (!FileName.IsEmpty())
						strExtension = System.IO.Path.GetExtension(FileName);
				// Cambia el modo de resalte del archivo
					ChangeHighLightByExtension(strExtension);
		}

		/// <summary>
		///		Cambia el modo de resalte a partir del nombre de archivo
		/// </summary>
		public void ChangeHighLightByExtension(string strExtension)
		{ if (!strExtension.IsEmpty())
				{ // Quita los espacios
						strExtension = strExtension.TrimIgnoreNull();
					// Añade el punto a la extensión
						if (!strExtension.StartsWith("."))
							strExtension = "." + strExtension;
					// Cambia el modo de resalte
						if (strExtension.EqualsIgnoreCase(".sql"))
							ChangeHighlightByResource("pack://application:,,,/CodeEditor;component/Resources/SqlHighLight.xml");
						else
							txtEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(strExtension);
				}
		}

		/// <summary>
		///		Cambia el modo de resalte de un recurso
		/// </summary>
		public bool ChangeHighlightByResource(string strResource)
		{	bool blnChanged = false;

				// Carga el recurso
					try
						{ using (System.IO.Stream stm = Application.GetResourceStream(new Uri(strResource)).Stream)
								{ LoadHighLight(stm);
								}
						}
					catch (Exception objException)
						{ System.Diagnostics.Debug.WriteLine("Error al cambiar el modo de resalte. " + objException.Message);
						}
				// Devuelve el valor que indica que se ha cargado
					return blnChanged;
		}

		/// <summary>
		///		Cambia el modo de resalte desde un stream
		/// </summary>
		public void LoadHighLight(System.IO.Stream stmFile)
		{	using (System.Xml.XmlTextReader rdrXml = new System.Xml.XmlTextReader(stmFile)) 
				{	// Cambia el modo de resalte
						txtEditor.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load(rdrXml,
																																																						HighlightingManager.Instance);
					// Cierra el lector
						rdrXml.Close();
				}
		}

		/// <summary>
		///		Inserta un texto en el documento
		/// </summary>
		public void InsertText(string strText)
		{ txtEditor.Document.Insert(txtEditor.CaretOffset, strText);
		}

		/// <summary>
		///		Texto
		/// </summary>
		public string Text
		{ get { return txtEditor.Text; }
			set { txtEditor.Text = value; }
		}

		/// <summary>
		///		Nombre del archivo
		/// </summary>
		public string FileName
		{ get { return strFileName; }
			set 
				{ strFileName = value;
					ChangeHighLight();
				}
		}

		private void txtEditor_TextChanged(object sender, EventArgs e)
		{ TextChanged?.Invoke(this, EventArgs.Empty);
		}

		private void txtEditor_DragEnter(object sender, DragEventArgs e)
		{ OnDragEnter(e);
		}

		private void txtEditor_Drop(object sender, DragEventArgs e)
		{ OnDrop(e);
		}
	}
}
