using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppBrowserController : MonoBehaviour
{
    public static AppBrowserController Instance { get; private set; }
    private void Awake() 
    {
        if (Instance != null && Instance != this) { Destroy(this); } 
        else { Instance = this;} 
    }
    
    [Serializable]
    public class AppInfo
    {
        public int id;
        public string name;
        public string logo;
        public string banner;
        [SerializeField]
        public AppCategory appCategoryIndex;
        public bool isFavorite;
        public int favAppOrder;
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
    public TMP_InputField searchInputField;
    public string categoryActual = "ALL";
    public List<AppIconPrefab> listAppIconPrefab = new List<AppIconPrefab>();
    public List<AppIconPrefab> listAppIconPrefabFavs = new List<AppIconPrefab>();
    
    [Header("Scroll Content & Prefab: ")] 
    public GameObject prefabAppIcon;
    public GameObject contentApps;
    public GameObject contentFavApps;
    
    [Header("List Apps: ")] 
    public ListApps listApps = new ListApps();
    private bool firstTimeGetAppsInfo = true;
    
    public TMP_Text[] slotsCategories;
    int[] idsFeatured = {1, 2, 3, 4, 5};
    int[] idsHot = {6, 7, 8, 9, 10};
    
    [Serializable] public class IntFavs { public List<int> data; }
    public IntFavs intFavs = new IntFavs();


    private void Start()
    {
        Debug.Log(PlayerPrefs.GetString("idFavs"));
        if (PlayerPrefs.HasKey("idFavs"))
        {
            intFavs = JsonUtility.FromJson< IntFavs >(PlayerPrefs.GetString("idFavs"));
        }
    }

    public void ChangeCategoryActual(string category)
    {
        categoryActual = category;
        FilterAppsByCategoryAndText();
    }
    public void FilterAppsByCategoryAndText()
    {
        foreach(AppIconPrefab icon in listAppIconPrefab){ icon.gameObject.SetActive(true); }
        ChangeCategory(categoryActual);
        FilterApps(searchInputField.text);
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentApps.GetComponent<RectTransform>());//Update UI
    }
    public void ChangeCategory(string category)
    {
        if (category == "ALL")
        {
            
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
                icon.gameObject.SetActive( (int)icon.appCategory == int.Parse(category) );
            }
        }
    }
    public void FilterApps(string searchText)
    {
        if(!string.IsNullOrEmpty(searchText))
        {
            foreach (Transform app in contentApps.transform)
            {
                if (app.GetComponent<AppIconPrefab>().nameApp.ToLower().Contains(searchText.ToLower())) { }
                else { app.gameObject.SetActive(false); }
            }
        }
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
       
        foreach (Transform t in contentApps.transform)    { GameObject.Destroy(t.gameObject); } listAppIconPrefab.Clear();
        foreach (Transform t in contentFavApps.transform) { GameObject.Destroy(t.gameObject); } listAppIconPrefabFavs.Clear();
        
        listApps = JsonUtility.FromJson<ListApps>(json);
       
        foreach(AppInfo appInfo in listApps.data){
            GameObject newAppIcon = Instantiate(prefabAppIcon, contentApps.transform);
            AppIconPrefab appIcon = newAppIcon.GetComponent<AppIconPrefab>();
            appIcon.imageDownloadManager.ChangeUrlImage(appInfo.logo);
            appIcon.id = appInfo.id;
            appIcon.nameApp = appInfo.name;
            appIcon.clickableObject.callLeftClick = () => { OnClickAppIcon(appInfo.id); };
            appIcon.clickableObject.callRightClick = () => { ContextualMenuManager.Instance.OpenApp_ContextualMenu(newAppIcon, appInfo.id, false); };
            //appIcon.buttonApp.onClick.AddListener(() => { OnClickAppIcon(appInfo.id); });
            appIcon.appCategory = appInfo.appCategoryIndex;
            listAppIconPrefab.Add(appIcon);

            //++++++++++++++++++++++++++++++++
            //Favorite AppList Code Section
            if ( intFavs.data.Contains(appInfo.id) )
            {
                InstanceFavoriteApp(appInfo);
            }
            //Favorite AppList Code Section
            //++++++++++++++++++++++++++++++++
        }
        
        FilterAppsByCategoryAndText();
        UpdateCategoryNumbers();
        
        if (firstTimeGetAppsInfo)
        {
            AppSectionController.Instance.UpdateInfo(listApps.data[0]);
            firstTimeGetAppsInfo = false;
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentApps.GetComponent<RectTransform>());  //Update UI
    }
    
    public void InstanceFavoriteApp(AppBrowserController.AppInfo appInfo)
    {
        GameObject newAppFavIcon = Instantiate(prefabAppIcon, contentFavApps.transform);
        AppIconPrefab appFavIcon = newAppFavIcon.GetComponent<AppIconPrefab>();
        appFavIcon.imageDownloadManager.ChangeUrlImage(appInfo.logo);
        appFavIcon.id = appInfo.id;
        appFavIcon.clickableObject.callLeftClick = () => { OnClickAppIcon(appInfo.id); };
        appFavIcon.clickableObject.callRightClick = () => { ContextualMenuManager.Instance.OpenApp_ContextualMenu(newAppFavIcon, appInfo.id, true); };
        appFavIcon.appCategory = appInfo.appCategoryIndex;
        appFavIcon.favAppOrder = appInfo.favAppOrder;
        listAppIconPrefabFavs.Add(appFavIcon);
    }
    public void AddAppToFavorite(int idApp)
    {
        AppInfo appToAdd = listApps.data.Find(appInfo => appInfo.id == idApp);
        if (appToAdd != null)
        {
            intFavs.data.Add(idApp);
            InstanceFavoriteApp(appToAdd);
            Debug.Log(JsonUtility.ToJson(intFavs));
            PlayerPrefs.SetString("idFavs", JsonUtility.ToJson(intFavs));
        }
    }
    public void RemoveFromFavorite(int id)
    {
        AppIconPrefab appToDelete = listAppIconPrefabFavs.Find(appIconPrefab => appIconPrefab.id == id);
        if (appToDelete != null)
        {
            intFavs.data.Remove(appToDelete.id);
            Destroy(appToDelete.gameObject);
            Debug.Log(JsonUtility.ToJson(intFavs));
            PlayerPrefs.SetString("idFavs", JsonUtility.ToJson(intFavs));
        }
    }
    
    public void OnClickAppIcon(int id)
    {
        
        AppInfo appInfo = listApps.data.Find(appInfo => appInfo.id == id );
        if (appInfo != null)
        {
            AppSectionController.Instance.UpdateInfo(appInfo);
            Hub_Manager.Instance.OpenSection(3);
        }
        
    }
    
}
