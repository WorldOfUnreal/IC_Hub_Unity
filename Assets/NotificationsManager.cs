using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class NotificationsManager : MonoBehaviour
{
    public GameObject notificationPrefab;
    public GameObject notificationContent;
    
    public void GetNotification(string json)
    {
        Notification notification = JsonUtility.FromJson<Notification>(json);
        GameObject newNotification = Instantiate(notificationPrefab, notificationContent.transform);
        NotificationPrefab newNotificationPrefab = newNotification.GetComponent<NotificationPrefab>();

        newNotificationPrefab.title.text = notification.title;
        newNotificationPrefab.description.text = notification.description;
    }
     [System.Serializable]
     public class Notification {
         public string title;
         public string avatar;
         public string description;
     }
     /*private void Awake()
     {
         StartCoroutine(WaitCoroutine());
     }
     private IEnumerator WaitCoroutine()
     {
         yield return new WaitForSeconds(1f);
         GetNotification(MakeJson("Noti1", "Descrip1", "Descrip1"));
         yield return new WaitForSeconds(1f);
         GetNotification(MakeJson("Noti2", "Descrip1", "Descrip2"));
         yield return new WaitForSeconds(1f);
         GetNotification(MakeJson("Noti3", "Descrip1", "Descrip31"));
         yield return new WaitForSeconds(1f);
         GetNotification(MakeJson("Noti4", "Descrip1", "Descrip4"));
         yield return new WaitForSeconds(1f);
         GetNotification(MakeJson("Noti5", "Descrip1", "Descrip5"));
     }
     public string MakeJson(string title, string avatar, string description)
     {
         return "{\"title\":\""+ title +"\", \"description\":\""+ description +"\",\"avatar\":\""+ avatar +"\"}";
     }*/
}
