using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Hub_Manager : MonoBehaviour
{
    [System.Serializable]
    public class AppInfo
    {
        public int id;
        public string name;
        public string logo;
        public string banner;
        [SerializeField]
        public AppCategory appCategoryIndex;
        
        public string dscvrPortal;
        public string distrikt;
        public string openChat;
        public string catalyze;
        public string twitter;
        public string nftCollections;
        public string newVersion;
        public string patchNotes;
        
        public string blockchain;
        public string currentVersion;
        public string launchLink;
        public List<NewsApp> listNews = new List<NewsApp>();
        
    }
    public enum AppCategory { Games, Markets, Defi, Social, New }
    
    [System.Serializable]
    public class ListApps {
        public List<AppInfo> data;
    }
    [System.Serializable]
    public class NewsApp{
        public string imageNews;
        public string title;
        public string content;
        public string textButton;
        public string linkButton;
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
        public int id;
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

    public ListApps listApps = new ListApps();
    [Header("Scroll Contents & Prefabs: ")] 
    public GameObject prefabAppIcon;
    public GameObject prefabNews;
    public GameObject contentApps;
    public GameObject contentNews;

    [Header("UI App Content: ")] 
    public Sprite loadingSprite;
    public Image bannerApp;
    public string bannerAppString;
    public TMP_Text blockchainTxt;
    public TMP_Text versionInfoTxt;
    
    [Header("UI App Buttons: ")] 
    public GoToURLButton dscvrPortalButton;
    public GoToURLButton distriktButton;
    public GoToURLButton openChatButton;
    public GoToURLButton catalyzeButton;
    public GoToURLButton twitterButton;
    public GoToURLButton nftCollectionsButton;
    public GoToURLButton newVersionButton;
    public GoToURLButton patchNotesButton;
    public GoToURLButton launchButton;
    
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
    
    [Header("UI Categorys: ")] 
    public string categoryActual = "ALL";
    public List<AppIconPrefab> listAppIconPrefab = new List<AppIconPrefab>();

    [DllImport("__Internal")]
    private static extern void JSOnHubScene();

    private void Start()
    {
        JSOnHubScene();
    }

    public void ChangeCategory(string category)
    {
        if (category == "ALL")
        {
            foreach(AppIconPrefab icon in listAppIconPrefab){
                icon.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach(AppIconPrefab icon in listAppIconPrefab){
                if ( (int)icon.appCategory == int.Parse(category) )
                {
                    icon.gameObject.SetActive(true);
                }
                else
                {
                    icon.gameObject.SetActive(false);
                }
            }
        }
        categoryActual = category;
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentApps.GetComponent<RectTransform>());//Update UI
    }

    
    public void GetAppsInfo(string json){
        foreach (Transform t in contentApps.transform) { GameObject.Destroy(t.gameObject); }
        listAppIconPrefab.Clear();
        
        listApps = JsonUtility.FromJson<ListApps>(json);
       
        foreach(AppInfo m in listApps.data){
            GameObject newAppIcon = Instantiate(prefabAppIcon, contentApps.transform);
            AppIconPrefab appIcon = newAppIcon.GetComponent<AppIconPrefab>();
            appIcon.urlImage = m.logo;
            appIcon.buttonApp.onClick.AddListener(() => { OnClickAppIcon(m.id); });
            appIcon.appCategory = m.appCategoryIndex;
            listAppIconPrefab.Add(appIcon);
        }
        ChangeCategory(categoryActual);
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentApps.GetComponent<RectTransform>());  //Update UI
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
        }
        separatorTokenNumber.text = "- " + listTokens.data.Count;
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
        }   
        separatorFriendNumber.text = "- " + listFriends.data.Count;
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
        }   
        separatorGroupNumber.text = "- " + listGroups.data.Count;
    }
    
    public void OnClickAppIcon(int id)
    {
        bannerApp.sprite = loadingSprite;
        AppInfo selectedApp = listApps.data[id];
        bannerAppString = selectedApp.banner;
        StartCoroutine(GetTexture(selectedApp.banner));
        
        dscvrPortalButton.UrlChange(selectedApp.dscvrPortal);
        distriktButton.UrlChange(selectedApp.distrikt);
        openChatButton.UrlChange(selectedApp.openChat);
        catalyzeButton.UrlChange(selectedApp.catalyze);
        twitterButton.UrlChange(selectedApp.twitter);
        nftCollectionsButton.UrlChange(selectedApp.nftCollections);
        newVersionButton.UrlChange(selectedApp.newVersion);
        patchNotesButton.UrlChange(selectedApp.patchNotes);
        launchButton.url = selectedApp.launchLink;
        blockchainTxt.text = selectedApp.blockchain;
        versionInfoTxt.text = selectedApp.currentVersion;
        
        foreach (Transform t in contentNews.transform) { GameObject.Destroy(t.gameObject); }
        
        foreach(NewsApp m in selectedApp.listNews){
            GameObject newNews = Instantiate(prefabNews, contentNews.transform);
            NewsPrefab newsPrefab = newNews.GetComponent<NewsPrefab>();
            newsPrefab.imageNewsUrl = m.imageNews;
            newsPrefab.title.text = m.title;
            newsPrefab.content.text = m.content;
            newsPrefab.textButton.text = m.textButton;
            newsPrefab.linkButton = m.linkButton;
        }
        
    }
  
    IEnumerator GetTexture(string url) {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            bannerApp.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
        }
    }


}
