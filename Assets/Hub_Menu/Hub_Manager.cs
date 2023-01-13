using System;
using System.Collections;
using System.Collections.Generic;
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
    

    public string categoryActual = "ALL";
    public List<AppIconPrefab> listAppIconPrefab = new List<AppIconPrefab>();


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
