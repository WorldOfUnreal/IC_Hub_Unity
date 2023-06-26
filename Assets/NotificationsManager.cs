using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NotificationsManager : MonoBehaviour
{
    public GameObject notificationPrefab;
    public GameObject notificationContent;
    
    public void GetNotification(string json)
    {
        StartCoroutine(GetTexture(json));
    }
     [System.Serializable]
     public class Notification {
         public string title;
         public string avatar;
         public string description;
     }
     
     IEnumerator GetTexture(string json)
     {
         Sprite sprite;
         Texture2D texture = null;
         Notification notification = JsonUtility.FromJson<Notification>(json);
         UnityWebRequest www = UnityWebRequestTexture.GetTexture(notification.avatar);
         yield return www.SendWebRequest();
         
         if (www.result != UnityWebRequest.Result.Success) {
             sprite = Resources.Load<Sprite>( "Images/Error" );
             Debug.Log("Error... " + notification.avatar);
         }
         else {
              texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
             sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
         }
         
         GameObject newNotification = Instantiate(notificationPrefab, notificationContent.transform);
         NotificationPrefab newNotificationPrefab = newNotification.GetComponent<NotificationPrefab>();
         newNotificationPrefab.title.text = notification.title;
         newNotificationPrefab.description.text = notification.description;
         newNotificationPrefab.imageDownloadManager.ChangeImage(texture); 
     }
     
     /*private void Awake()
     {
         StartCoroutine(WaitCoroutine());
     }
     private IEnumerator WaitCoroutine()
     {
         yield return new WaitForSeconds(1f);
         GetNotification(MakeJson("Noti1", "https://images.squarespace-cdn.com/content/v1/62c32effd8601c2f49f1728b/424f58fc-0901-4272-a81b-01c1347a1d01/20220303_Omage-11.jpg", "Descrip1"));
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
