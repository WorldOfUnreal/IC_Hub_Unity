using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AppManagementController : MonoBehaviour
{
    
    [DllImport("__Internal")]
    private static extern void JSSendDataApp(string json);
    [DllImport("__Internal")]
    private static extern void JSSendDataVersions(string json);
    [DllImport("__Internal")]
    private static extern void JSSendDataCollections(string json);
    [DllImport("__Internal")]
    private static extern void JSSendDataNews(string json);
    [DllImport("__Internal")]
    private static extern void JSSetImage(string json);
    public class AppData
    {
        public string name;
        public AppBrowserController.AppCategory category;
        public string linkDapp;
        public List<string> nftCollections = new List<string>();
        public string DSCVRPortal;
        public string Distrikt;
        public string OpenChat;
        public string Catalyze;
        public string Twitter;
        public string AppVersion;
        public string Banner;
        public string Logo;
    }
    [System.Serializable]
    public class VersionsData
    {
        public List<VersionAppData> versionAppDatas;
    }
    [System.Serializable]
    public class VersionAppData
    {
        public int versionID;
        public string projectName;
        public string linkDapp;
        public string currentVersion;
        public string blockChain;
    }
    public class NewsData
    {
        public string imageNews;
        public string titleNews;
        public string contentNews;
        public string textButtonNews;
        public string linkButtonNews;
    }
    [System.Serializable]
    public class CollectionsData
    {
        public List<CollectionAppData> collectionAppDatas;
    }
    [System.Serializable]
    public class CollectionAppData
    {
        public string collectionName;
        public string canisterID;
        public string nftStandard;
        public string linkToMarketplace;
        public string avatarUrl;
    }
    
    [Header("RegistrationPanel: ")] 
    public TMP_InputField nameInput;
    public TMP_Dropdown categoryInput;
    public TMP_InputField linkDappInput;
    public TMP_InputField nftCollection;
    public TMP_InputField nftCollection2;
    public TMP_InputField nftCollection3;
    public TMP_InputField DSCVRPortalInput;
    public TMP_InputField DistriktInput;
    public TMP_InputField OpenChatInput;
    public TMP_InputField CatalyzeInput;
    public TMP_InputField TwitterInput;
    public TMP_InputField AppVersionInput;
    public ImageDownloadManager BannerSlot;
    public ImageDownloadManager LogoSlot;

    [Header("VersionPanel: ")] 
    public GameObject contentVersions;
    public GameObject versionAppPrefab;
    [Header("CollectionPanel: ")] 
    public GameObject contentCollections;
    public GameObject collectionAppPrefab;
    [Header("NewsPanel: ")] 
    public ImageDownloadManager newsImage;
    public TMP_InputField newsTitle;
    public TMP_InputField newsContent;
    public TMP_InputField newsCallToActionText;
    public TMP_InputField newsCallToActionLink;
    public TMP_Text newsTitlePreview;
    public TMP_Text newsContentPreview;
    public TMP_Text newsCallToActionPreview;
    public void SubmitApp()
    {
        AppData appData = new AppData();
        appData.name = nameInput.text;
        appData.category = (AppBrowserController.AppCategory)categoryInput.value;
        appData.linkDapp = linkDappInput.text;
        appData.nftCollections = new List<string>();
        appData.nftCollections.Add(nftCollection.text);
        appData.nftCollections.Add(nftCollection2.text);
        appData.nftCollections.Add(nftCollection3.text);
        appData.DSCVRPortal = DSCVRPortalInput.text;
        appData.Distrikt = DistriktInput.text;
        appData.OpenChat = OpenChatInput.text;
        appData.Catalyze = CatalyzeInput.text;
        appData.Twitter = TwitterInput.text;
        appData.AppVersion = AppVersionInput.text;
        appData.Banner = BannerSlot.urlImage;
        appData.Logo = LogoSlot.urlImage;
        
        string json = JsonUtility.ToJson(appData);
        CanvasPopup.Instance.OpenPopup(() =>
        {
            CanvasPopup.Instance.OpenLoadingPanel();
            JSSendDataApp(json);
        },null, "Submit", "Cancel", "Do you want update info App?", null, null, appData.Logo);
    }
    public void GetInfoApp(string json)
    {
        AppData appData = JsonUtility.FromJson<AppData>(json);

        nameInput.text = appData.name;
        categoryInput.value = (int)appData.category;
        linkDappInput.text = appData.linkDapp;
        nftCollection.text = appData.nftCollections[0];
        nftCollection2.text = appData.nftCollections[1];
        nftCollection3.text = appData.nftCollections[2];
        DSCVRPortalInput.text = appData.DSCVRPortal;
        DistriktInput.text = appData.Distrikt;
        OpenChatInput.text = appData.OpenChat;
        CatalyzeInput.text = appData.Catalyze;
        TwitterInput.text = appData.Twitter;
        AppVersionInput.text = appData.AppVersion;
        BannerSlot.ChangeUrlImage(appData.Banner); 
        LogoSlot.ChangeUrlImage(appData.Logo);
        
        //LayoutRebuilder.ForceRebuildLayoutImmediate(contentTokens.GetComponent<RectTransform>());  //Update UI
    }

    public void SubmitInfoVersions()
    {
        VersionsData versionsData = new VersionsData();
        versionsData.versionAppDatas = new List<VersionAppData>();
        
        foreach (Transform t in contentVersions.transform)
        {
            VersionAppPrefab versionAppPrefab = t.gameObject.GetComponent<VersionAppPrefab>();
            
            VersionAppData versionAppData = new VersionAppData();
            versionAppData.projectName = versionAppPrefab.projectNameInput.text;
            versionAppData.currentVersion = versionAppPrefab.currentVersionInput.text;
            versionAppData.linkDapp = versionAppPrefab.linkDappInput.text;
            versionAppData.blockChain = versionAppPrefab.blockChainInput.text;
            versionAppData.versionID = versionAppPrefab.versionID;
            
            versionsData.versionAppDatas.Add(versionAppData);
        }
        
        string json = JsonUtility.ToJson(versionsData);
        
        CanvasPopup.Instance.OpenPopup(() =>
        {
            CanvasPopup.Instance.OpenLoadingPanel();
            JSSendDataVersions(json);
        },null, "Update Info Versions", "Cancel", "Do you want update info versions?", null, null, LogoSlot.urlImage);

    }
    public void GetInfoVersions(string json)
    {
        VersionsData versionsData = JsonUtility.FromJson<VersionsData>(json);

        foreach (Transform t in contentVersions.transform) { GameObject.Destroy(t.gameObject); }
        foreach (VersionAppData version in versionsData.versionAppDatas)
        {
            GameObject newVersion = Instantiate(versionAppPrefab, contentVersions.transform);
            VersionAppPrefab versionPrefab = newVersion.GetComponent<VersionAppPrefab>();
            versionPrefab.FillVersionAppData(version);
        }
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentVersions.GetComponent<RectTransform>());  //Update UI
    }
    public void AddSlotVersion()
    {
        GameObject newVersion = Instantiate(versionAppPrefab, contentVersions.transform);
        VersionAppPrefab versionPrefab = newVersion.GetComponent<VersionAppPrefab>();
        versionPrefab.removeVersionBtn.onClick.AddListener(() => { GameObject.Destroy(newVersion); });
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentVersions.GetComponent<RectTransform>());  //Update UI
    }
    public void SubmitInfoCollections()
    {
        CollectionsData collectionsData = new CollectionsData();
        collectionsData.collectionAppDatas = new List<CollectionAppData>();
        
        foreach (Transform t in contentVersions.transform)
        {
            CollectionAppPrefab collectionAppPrefab = t.gameObject.GetComponent<CollectionAppPrefab>();
            
            CollectionAppData collectionAppData = new CollectionAppData();
            collectionAppData.collectionName = collectionAppPrefab.collectionNameInput.text;
            collectionAppData.canisterID = collectionAppPrefab.canisterIDInput.text;
            collectionAppData.nftStandard = collectionAppPrefab.nftStandardInput.text;
            collectionAppData.linkToMarketplace = collectionAppPrefab.linkToMarketplaceInput.text;
            collectionAppData.avatarUrl = collectionAppPrefab.avatarCollection.urlImage;
            
            collectionsData.collectionAppDatas.Add(collectionAppData);
        }
        
        string json = JsonUtility.ToJson(collectionsData);
        
        CanvasPopup.Instance.OpenPopup(() =>
        {
            CanvasPopup.Instance.OpenLoadingPanel();
            JSSendDataCollections(json);
        },null, "Update Info Collections", "Cancel", "Do you want update info collections?", null, null, null);
    }
    public void GetInfoCollections(string json)
    {
        CollectionsData collectionsData = JsonUtility.FromJson<CollectionsData>(json);

        foreach (Transform t in contentCollections.transform) { GameObject.Destroy(t.gameObject); }
        foreach (CollectionAppData collection in collectionsData.collectionAppDatas)
        {
            GameObject newCollection = Instantiate(collectionAppPrefab, contentCollections.transform);
            CollectionAppPrefab collectionPrefab = newCollection.GetComponent<CollectionAppPrefab>();
            collectionPrefab.FillCollectionAppData(collection);
        }
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentCollections.GetComponent<RectTransform>());  //Update UI
    }
    public void AddSlotCollection()
    {
        GameObject newCollection = Instantiate(collectionAppPrefab, contentCollections.transform);
        CollectionAppPrefab collectionPrefab = newCollection.GetComponent<CollectionAppPrefab>();
        collectionPrefab.removeCollectionBtn.onClick.AddListener(() => { GameObject.Destroy(newCollection); });
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentVersions.GetComponent<RectTransform>());  //Update UI
    }
    public void SubmitNews()
    {
        NewsData newsData = new NewsData();

        newsData.imageNews = newsImage.urlImage;
        newsData.titleNews = newsTitle.text;
        newsData.contentNews = newsContent.text;
        newsData.textButtonNews = newsCallToActionText.text;
        newsData.linkButtonNews = newsCallToActionLink.text;
        
        string json = JsonUtility.ToJson(newsData);
        
        CanvasPopup.Instance.OpenPopup(() =>
        {
            CanvasPopup.Instance.OpenLoadingPanel();
            JSSendDataNews(json);
        },null, "Create new News", "Cancel", "Do you want upload this news?", null, null, newsData.imageNews);

    }
    public void UpdateNewsPreview()
    {
        newsTitlePreview.text = newsTitle.text;
        newsContentPreview.text = newsContent.text;
        newsCallToActionPreview.text = newsCallToActionText.text;
    }
    public void OnSubmitProcessSuccess()
    {
        CanvasPopup.Instance.OpenSuccessPanel();
        ContextualMenuManager.Instance.userStatusContextualMenu.GoToAppManagement();
    }

    public void SetImageAppManagement(int id)
    {
        switch (id)
        {
            case 0: JSSetImage("logo"); break;
            case 1: JSSetImage("banner"); break;
            case 2: JSSetImage("imageNews"); break;
        }
    }
    public void GetImageLogo(string url) { LogoSlot.ChangeUrlImage(url); }
    public void GetImageBanner(string url) { BannerSlot.ChangeUrlImage(url); }
    public void GetImageNews(string url) { newsImage.ChangeUrlImage(url); }

    

}