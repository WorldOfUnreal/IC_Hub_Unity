using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    public enum AppCategory { Games, Markets, Defi, Social, New, Communities}

    [Header("UI Categorys: ")] 
    public string categoryActual = "ALL";
    public List<AppIconPrefab> listAppIconPrefab = new List<AppIconPrefab>();
    
    [Header("Scroll Content & Prefab: ")] 
    public GameObject prefabAppIcon;
    public GameObject contentApps;
    
    [Header("List Apps: ")] 
    public ListApps listApps = new ListApps();
    
    private bool firstTimeGetAppsInfo = true;

    public TMP_Text[] slotsCategories;
    int[] idsFeatured = {1, 2, 3, 4, 5};
    int[] idsHot = {6, 7, 8, 9, 10};
    
    public void ChangeCategory(string category)
    {
        Debug.Log(category);
        if (category == "ALL")
        {
            foreach(AppIconPrefab icon in listAppIconPrefab){
                icon.gameObject.SetActive(true);
            }
        }
        else if (category == "HOT")
        {
            foreach(AppIconPrefab icon in listAppIconPrefab){
                icon.gameObject.SetActive( idsHot.Contains(icon.id) );
            }
        }
        else if (category == "FEATURED")
        {
            foreach(AppIconPrefab icon in listAppIconPrefab){
                icon.gameObject.SetActive( idsFeatured.Contains(icon.id) );
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
    public void UpdateCategoryNumbers()
    {
        slotsCategories[0].text = "" + listAppIconPrefab.Count;
        slotsCategories[1].text = "" + SearchQuantityInCategory(AppCategory.Games);
        slotsCategories[2].text = "" + SearchQuantityInCategory(AppCategory.Markets);
        slotsCategories[3].text = "" + SearchQuantityInCategory(AppCategory.Defi);
        slotsCategories[4].text = "" + SearchQuantityInCategory(AppCategory.Social);
        slotsCategories[5].text = "" + SearchQuantityInCategory(AppCategory.New);
        slotsCategories[6].text = "" + SearchQuantityInCategory(AppCategory.Communities);
        
        slotsCategories[7].text = "" + listAppIconPrefab.FindAll( appIconPrefab => idsFeatured.Contains(appIconPrefab.id)).Count;
        slotsCategories[8].text = "" + listAppIconPrefab.FindAll( appIconPrefab => idsHot.Contains(appIconPrefab.id)).Count;

        LayoutRebuilder.ForceRebuildLayoutImmediate(contentApps.GetComponent<RectTransform>());//Update UI
    }

    public int SearchQuantityInCategory(AppCategory category)
    {
        int counterItemsInCategory = 0;
        foreach(AppIconPrefab icon in listAppIconPrefab){
            if ( icon.appCategory == category )
            {
                counterItemsInCategory += 1;
            }
        }
        return counterItemsInCategory;
    }
    
    public void GetAppsInfo(string json){
        foreach (Transform t in contentApps.transform) { GameObject.Destroy(t.gameObject); }
        listAppIconPrefab.Clear();
        
        listApps = JsonUtility.FromJson<ListApps>(json);
       
        foreach(AppInfo appInfo in listApps.data){
            GameObject newAppIcon = Instantiate(prefabAppIcon, contentApps.transform);
            AppIconPrefab appIcon = newAppIcon.GetComponent<AppIconPrefab>();
            appIcon.imageDowloadManager.ChangeUrlImage(appInfo.logo);
            appIcon.id = appInfo.id;
            appIcon.buttonApp.onClick.AddListener(() => { OnClickAppIcon(appInfo.id); });
            appIcon.appCategory = appInfo.appCategoryIndex;
            listAppIconPrefab.Add(appIcon);
        }
        ChangeCategory(categoryActual);
        UpdateCategoryNumbers();
        
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
