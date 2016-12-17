using System;
using System.Collections.ObjectModel;

namespace Bau.Controls.Notifications
{
	/// <summary>
	///		Colección de <see cref="NotificationModel"/>
	/// </summary>
	public class NotificationsModelCollection : ObservableCollection<NotificationModel>
	{
		///// <summary>
		/////		Añade una notificación
		///// </summary>
		//public NotificationModel Add(string strTitle, string strMesage, string strUrlImage)
		//{ NotificationModel objNotification = new NotificationModel { Title = strTitle,
		//																															Message = strMesage,
		//																															ImageUrl = strUrlImage
		//																														};

		//		// Añade la notificación
		//			Add(objNotification);
		//		// Devuelve la notificación
		//			return objNotification;
		//}

		/// <summary>
		///		Elimina un elemento por su ID
		/// </summary>
		public void RemoveByID(string strID)
		{ for (int intIndex = Count - 1; intIndex >= 0; intIndex--)
				if (this[intIndex].ID == strID)
					RemoveAt(intIndex);
		}
	}
}
