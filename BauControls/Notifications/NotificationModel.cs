using System;
using System.ComponentModel;

namespace Bau.Controls.Notifications
{
	/// <summary>
	///		Clase con los datos de una notificación
	/// </summary>
	public class NotificationModel : INotifyPropertyChanged
	{	// Eventos públicos
			public event PropertyChangedEventHandler PropertyChanged;
		// Enumerados públicos
			/// <summary>
			///		Tipo de notificación
			/// </summary>
			public enum NotificationType
				{ Error,
					Warning,
					Info,
					Other
				}
		// Variables privadas
			private NotificationType intType;
			private string strID, strMessage, strImageUrl, strTitle;

		/// <summary>
		///		Tratamiento del evento PropertyChanged
		/// </summary>
		protected virtual void OnPropertyChanged(string strProperty)
		{	if (PropertyChanged != null) 
				PropertyChanged(this, new PropertyChangedEventArgs(strProperty));
		}

		/// <summary>
		///		ID de la notificación
		/// </summary>
		public string ID
		{ get 
				{ // Obtiene el ID
						if (string.IsNullOrEmpty(strID))
							strID = Guid.NewGuid().ToString();
					// Devuelve el ID
						return strID; 
				}
			set
				{	if (strID != value)
						{ strID = value;
							OnPropertyChanged("ID");
						}
				}
		}

		/// <summary>
		///		Tipo de notificación
		/// </summary>
		public NotificationType IDType
		{ get { return intType; }
			set
				{ if (intType != value)
						{ intType = value;
							OnPropertyChanged("IDType");
						}
				}
		}

		/// <summary>
		///		Título de la notificación
		/// </summary>
		public string Title
		{ get { return strTitle; }
			set
				{	if (strTitle != value)
						{ strTitle = value;
							OnPropertyChanged("Title");
						}
				}
		}

		/// <summary>
		///		Mensaje de la notificación
		/// </summary>
		public string Message
		{ get { return strMessage; }
			set
				{	if (strMessage != value)
						{	strMessage = value;
							OnPropertyChanged("Message");
						}
				}
		}

		/// <summary>
		///		Url de la imagen
		/// </summary>
		public string ImageUrl
		{	get { return strImageUrl; }
			set
				{	if (strImageUrl != value)
						{ strImageUrl = value;
							OnPropertyChanged("ImageUrl");
						}
				}
		}
	}
}