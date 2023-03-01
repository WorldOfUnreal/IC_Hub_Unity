using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationsPanelManager : MonoBehaviour
{
    
    public TMP_Text allNotificationsNumber;
    
    [Header("Request Panel : ")]
    public GameObject prefabFriendRequest;
    public GameObject contentFriendRequests;
    public TMP_Text requestFriendNumber;

    [Header("Notifications Panel : ")]
    public GameObject prefabNotification;
    public GameObject contentNotification;
    public TMP_Text unreadNumber;

    public string stringJson;
    
    [DllImport("__Internal")]
    private static extern void JSAcceptFriendRequest(string principalID);
    [DllImport("__Internal")]
    private static extern void JSDenyFriendRequest(string principalID);
    
    [System.Serializable]
    public class Notification {
        public string title;
        public string avatar;
        public string description;
    }
    [System.Serializable]
    public class RequestsFriend {
        public string username;
        public string principalID;
        public string avatarUser;
    }
    [System.Serializable]
    public class InfoNotificationPanel { 
        public List<Notification> notifications;
        public List<RequestsFriend> requests;
    }

    private void Start()
    {
        //GetInfoNotificationPanel(stringJson);
    }

    public void GetInfoNotificationPanel(string json)
    {
        InfoNotificationPanel infoNotificationPanel = JsonUtility.FromJson<InfoNotificationPanel>(json);
        
    /*//Fill notification
        foreach (Transform t in contentNotification.transform) { GameObject.Destroy(t.gameObject); }
        foreach (Notification g in infoNotificationPanel.notifications)
        {
           
        }*/
    //Fill request
        foreach (Transform t in contentFriendRequests.transform) { GameObject.Destroy(t.gameObject); }
        foreach (RequestsFriend g in infoNotificationPanel.requests)
        {
            AddRequestToList(g.principalID, g.username);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentFriendRequests.GetComponent<RectTransform>());
        requestFriendNumber.text = infoNotificationPanel.requests.Count.ToString();

        allNotificationsNumber.text = infoNotificationPanel.requests.Count.ToString();
    }
    
    public void AddRequestToList(string principalID, string username){
        GameObject newRequest = Instantiate(prefabFriendRequest, contentFriendRequests.transform);
        RequestFriendPrefab requestFriendPrefab = newRequest.GetComponent<RequestFriendPrefab>();

        requestFriendPrefab.userNameText.text = username;
        requestFriendPrefab.principalID.text = principalID.Substring(0, 4)+"..."+principalID.Substring(principalID.Length - 4);
        
        requestFriendPrefab.buttonAccept.onClick.AddListener(() =>
        {
            CanvasPopup.Instance.OpenPopup(() =>
            {
                CanvasPopup.Instance.OpenLoadingPanel();
                JSAcceptFriendRequest(principalID);
            }, null, "Accept", "Cancel", "Do you want accept this User?", username, principalID);
        });
        requestFriendPrefab.buttonDeny.onClick.AddListener(() =>
        {
            CanvasPopup.Instance.OpenPopup(() =>
            {
                CanvasPopup.Instance.OpenLoadingPanel();
                JSDenyFriendRequest(principalID);
            }, null, "Deny", "Cancel", "Do you want deny this User?", username, principalID);
        });
    }
}
