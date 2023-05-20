using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Hub_Manager : MonoBehaviour
{
    [DllImport("__Internal")] 
    private static extern void JSCurrentSection(int id);
        
    public static Hub_Manager Instance { get; private set; }
        
    private void Awake() 
    {
        if (Instance != null && Instance != this) { Destroy(this); } 
        else { Instance = this;} 
    }

    [System.Serializable]
    public class Token{
        public string avatar;
        public string name;
        public string value;
        public int id;
    }
    [System.Serializable]
    public class ListTokens {
        public List<Token> data;
    }
    [System.Serializable]
    public class Friend{
        public string avatar;
        public string name;
        public string status;
        public string principalID;
    }
    [System.Serializable]
    public class ListFriends {
        public List<Friend> data;
    }
    [System.Serializable]
    public class Group{
        public string avatar;
        public string name;
        public int id;
    }
    [System.Serializable]
    public class ListGroups {
        public List<Group> data;
    }
    
    [System.Serializable]
    public class UserProfileInfo {
        public string username;
        public string userState;
        public string principalID;
        public string avatar;
    }

    [Header("GameObjects Section Center: ")]
    public GameObject[] sectionsCenter;
    
    [Header("UI User Info: ")] 
    public ImageDowloadManager avatarUser;
    public TMP_Text userName;
    public TMP_Text userState;
    public Button buttonGoToUser;
    /*public Button buttonGoToUserCornerLeftDown1;
    public Button buttonGoToUserCornerLeftDown2;*/
    
    [Header("UI Tokens, Friends, Groups: ")] 
    public GameObject contentTokens;
    public GameObject prefabToken;
    public TMP_Text separatorTokenNumber;
    public GameObject contentFriends;
    public GameObject prefabFriend;
    public TMP_Text separatorFriendNumber;
    public GameObject contentGroups;
    public GameObject prefabGroup;
    public TMP_Text separatorGroupNumber;
    
    [DllImport("__Internal")]
    private static extern void JSOnHubScene();

    private void Start()
    {
       JSOnHubScene();
    }
    public void GetTokensInfo(string json)
    {
        foreach (Transform t in contentTokens.transform) { GameObject.Destroy(t.gameObject); }
        
        ListTokens listTokens = JsonUtility.FromJson<ListTokens>(json);

        foreach (Token g in listTokens.data)
        {
            GameObject newToken = Instantiate(prefabToken, contentTokens.transform);
            Hub_TokenPrefab tokenPrefab = newToken.GetComponent<Hub_TokenPrefab>();
            tokenPrefab.nameToken.text = g.name;
            tokenPrefab.valueToken.text = g.value;
            tokenPrefab.iconToken.ChangeUrlImage(g.avatar);
            tokenPrefab.clickableObject.callLeftClick= () => { Debug.Log("ClickToken"); };
            tokenPrefab.clickableObject.callRightClick= () => { ContextualMenuManager.Instance.OpenToken_ContextualMenu(newToken, g.id, g.name); };
        }
        separatorTokenNumber.text = "- " + listTokens.data.Count;
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentTokens.GetComponent<RectTransform>());  //Update UI
    }
    public void GetFriendsInfo(string json)
    {
        foreach (Transform t in contentFriends.transform) { GameObject.Destroy(t.gameObject); }
        
        ListFriends listFriends = JsonUtility.FromJson<ListFriends>(json);

        foreach (Friend g in listFriends.data)
        {
            GameObject newFriend = Instantiate(prefabFriend, contentFriends.transform);
            Hub_FriendPrefab friendPrefab = newFriend.GetComponent<Hub_FriendPrefab>();
            friendPrefab.nameFriend.text = g.name;
            friendPrefab.statusTMP.text = g.status;
            friendPrefab.iconFriend.ChangeUrlImage(g.avatar);
            friendPrefab.clickableObject.callLeftClick = () => { CanvasPlayerProfile.Instance.OpenPopupPlayerProfile(g.principalID, g.name); };
            friendPrefab.clickableObject.callRightClick = () => { ContextualMenuManager.Instance.OpenUser_ContextualMenu(newFriend, g.principalID, g.name); };
            //friendPrefab.button.onClick.AddListener(() => { CanvasPlayerProfile.Instance.OpenPopupPlayerProfile(g.principalID, g.name); });
        }

        separatorFriendNumber.text = "- " + listFriends.data.Count;
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentFriends.GetComponent<RectTransform>());  //Update UI
    }
    
    public void GetGroupsInfo(string json)
    {
        foreach (Transform t in contentGroups.transform) { GameObject.Destroy(t.gameObject); }
        
        ListGroups listGroups = JsonUtility.FromJson<ListGroups>(json);
        
        foreach (Group g in listGroups.data)
        {
            GameObject newGroup = Instantiate(prefabGroup, contentGroups.transform);
            Hub_GroupPrefab groupPrefab = newGroup.GetComponent<Hub_GroupPrefab>();
            groupPrefab.nameGroup.text = g.name;
            groupPrefab.iconGroup.ChangeUrlImage(g.avatar);
            groupPrefab.clickableObject.callLeftClick= () => { ChatManager.Instance.SetGroupSelected(g.id); };
            groupPrefab.clickableObject.callRightClick= () => { ContextualMenuManager.Instance.OpenGroup_ContextualMenu(newGroup, g.id, g.name); };
        }   
        separatorGroupNumber.text = "- " + listGroups.data.Count;
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentGroups.GetComponent<RectTransform>());  //Update UI
    }
    public void GetUserInfo(string json)
    {
       UserProfileInfo userProfileInfo = JsonUtility.FromJson<UserProfileInfo>(json);
       avatarUser.ChangeUrlImage(userProfileInfo.avatar);
       userName.text = userProfileInfo.username;
       userState.text = userProfileInfo.userState;
       buttonGoToUser.onClick.RemoveAllListeners();
       buttonGoToUser.onClick.AddListener((() =>
       {
           CanvasPlayerProfile.Instance.OpenPopupPlayerProfile(userProfileInfo.principalID, userProfileInfo.username);
       }));
      
    }
    public void OpenSection(int id)
    {
        foreach (GameObject section in sectionsCenter) { section.SetActive(false); }
        sectionsCenter[id].SetActive(true);
        JSCurrentSection(id);
        
    }
}
