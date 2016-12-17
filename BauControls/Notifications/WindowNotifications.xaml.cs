using System;
using System.Windows;
using System.Windows.Controls;

namespace Bau.Controls.Notifications
{
	/// <summary>
	///		Ventana para mostrar notificaciones
	/// </summary>
	public partial class WindowNotifications
	{	// Constantes privadas
			private const int cnstIntMaximum = 4;
		// Variables privadas
			private NotificationsModelCollection objColNotifications = new NotificationsModelCollection();
			private readonly NotificationsModelCollection objColBuffer = new NotificationsModelCollection();

		public WindowNotifications()
		{	// Inicializa los componentes
				InitializeComponent();
			// Asigna el dataContext
				NotificationsControl.DataContext = objColNotifications;
		}

		/// <summary>
		///		Añade una notificación
		/// </summary>
		public NotificationModel AddNotification(NotificationModel.NotificationType intIDType, string strTitle, string strMessage,
																						 string strUrlImage = null)
		{ // Asigna la Url de la imagen
				if (string.IsNullOrWhiteSpace(strUrlImage))
					switch (intIDType)
						{ case NotificationModel.NotificationType.Error:
									strUrlImage = "pack://application:,,,/BauControls;component/Themes/Images/Notifications/facebook-button.png";
								break;
							case NotificationModel.NotificationType.Warning:
									strUrlImage = "pack://application:,,,/BauControls;component/Themes/Images/Notifications/Radiation_warning_symbol.png";
								break;
							default:
									strUrlImage = "pack://application:,,,/BauControls;component/Themes/Images/Notifications/notification-icon.png";
								break;
						}
			// Añade la notificación
				return AddNotification(strTitle, strMessage, strUrlImage);
		}

		/// <summary>
		///		Añade una notificación
		/// </summary>
		public NotificationModel AddNotification(string strTitle, string strMessage, string strUrl)
		{	NotificationModel objNotification;
		
				// Añade la notificación
					objNotification = new NotificationModel { Title = strTitle, Message = strMessage, ImageUrl = strUrl };
				// Añade la notificación al buffer o a la colección de notificaciones a mostrar
					if (objColNotifications.Count + 1 > cnstIntMaximum)
						objColBuffer.Add(objNotification);
					else
						objColNotifications.Add(objNotification);
			// Muestra la ventana si hay notificaciones
				if (objColNotifications.Count > 0 && !IsActive)
					Show();
			// Devuelve la notificación
				return objNotification;
		}

		/// <summary>
		///		Elimina una notificación
		/// </summary>
		public void RemoveNotification(NotificationModel objNotification)
		{ RemoveNotification(objNotification.ID);
		}

		/// <summary>
		///		Elimina una notificación
		/// </summary>
		public void RemoveNotification(string strID)
		{	// Elimina la notificación
				objColNotifications.RemoveByID(strID);
			// Si queda algo en el buffer, lo añade a la colección a mostrar
				if (objColBuffer.Count > 0)
					{	objColNotifications.Add(objColBuffer[0]);
						objColBuffer.RemoveAt(0);
					}
			// Oculta la ventana si no queda nada para mostrar
				if (objColNotifications.Count < 1)
					Hide();
		}

		/// <summary>
		///		Trata el evento de cambio de tamaño
		/// </summary>
		private void NotificationWindowSizeChanged(object sender, SizeChangedEventArgs e)
		{	if (e.NewSize.Height == 0.0)
				{ Grid grdNotifications = sender as Grid;

						if (grdNotifications != null && grdNotifications.Tag != null)
							RemoveNotification(grdNotifications.Tag.ToString());
				}
		}
	}
}
