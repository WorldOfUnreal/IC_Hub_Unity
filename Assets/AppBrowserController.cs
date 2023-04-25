using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppBrowserController : MonoBehaviour
{
    
    [Serializable]
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
    [Serializable]
    public class ListApps {
        public List<AppInfo> data;
    }
    [Serializable]
    public class NewsApp{
        public string imageNews;
        public string title;
        public string content;
        public string textButton;
        public string linkButton;
    }
    public enum AppCategory { Games, Markets, Defi, Social, New }

    [Header("UI Categorys: ")] 
    public string categoryActual = "ALL";
    public List<AppIconPrefab> listAppIconPrefab = new List<AppIconPrefab>();
    
    [Header("Scroll Content & Prefab: ")] 
    public GameObject prefabAppIcon;
    public GameObject contentApps;
    
    [Header("List Apps: ")] 
    public ListApps listApps = new ListApps();
    
    private bool firstTimeGetAppsInfo = true;
    
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
       
        foreach(AppInfo appInfo in listApps.data){
            GameObject newAppIcon = Instantiate(prefabAppIcon, contentApps.transform);
            AppIconPrefab appIcon = newAppIcon.GetComponent<AppIconPrefab>();
            appIcon.imageDowloadManager.ChangeUrlImage(appInfo.logo);
            appIcon.buttonApp.onClick.AddListener(() => { OnClickAppIcon(appInfo.id); });
            appIcon.appCategory = appInfo.appCategoryIndex;
            listAppIconPrefab.Add(appIcon);
        }
        ChangeCategory(categoryActual);
      
        if (firstTimeGetAppsInfo)
        {
            AppSectionController.Instance.UpdateInfo(listApps.data[0]);
            firstTimeGetAppsInfo = false;
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentApps.GetComponent<RectTransform>());  //Update UI
    }
    
    public void OnClickAppIcon(int id)
    {
        Hub_Manager.Instance.OpenSection(3);
        AppInfo appInfo = listApps.data.Find(appInfo => appInfo.id == id );
        AppSectionController.Instance.UpdateInfo(appInfo);
    }
    
}
