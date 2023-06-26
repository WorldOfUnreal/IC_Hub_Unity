using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppSectionController : MonoBehaviour
{
    
    public static AppSectionController Instance { get; private set; }
        
    private void Awake() 
    {
        if (Instance != null && Instance != this) { Destroy(this); } 
        else { Instance = this;} 
    }

    [HideInInspector] 
    public AppBrowserController.AppInfo selectedAppInfo;
    
    [Header("UI App Content: ")] 
    public ImageDownloadManager bannerApp;
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
    
    [Header("Scroll Content & Prefab: ")]
    public GameObject prefabNews;
    public GameObject contentNews;


    public void UpdateInfo(AppBrowserController.AppInfo appInfo)
    {
        selectedAppInfo = appInfo;
        
        bannerApp.ChangeUrlImage(selectedAppInfo.banner);
        dscvrPortalButton.UrlChange(selectedAppInfo.dscvrPortal);
        distriktButton.UrlChange(selectedAppInfo.distrikt);
        openChatButton.UrlChange(selectedAppInfo.openChat);
        catalyzeButton.UrlChange(selectedAppInfo.catalyze);
        twitterButton.UrlChange(selectedAppInfo.twitter);
        nftCollectionsButton.UrlChange(selectedAppInfo.nftCollections);
        newVersionButton.UrlChange(selectedAppInfo.newVersion);
        patchNotesButton.UrlChange(selectedAppInfo.patchNotes);
        launchButton.url = selectedAppInfo.launchLink;
        blockchainTxt.text = selectedAppInfo.blockchain;
        versionInfoTxt.text = selectedAppInfo.currentVersion;
        
        foreach (Transform t in contentNews.transform) { GameObject.Destroy(t.gameObject); }
        
        foreach(AppBrowserController.NewsApp m in selectedAppInfo.listNews){
            GameObject newNews = Instantiate(prefabNews, contentNews.transform);
            NewsPrefab newsPrefab = newNews.GetComponent<NewsPrefab>();
            newsPrefab.imageNews.ChangeUrlImage(m.imageNews);
            newsPrefab.title.text = m.title;
            newsPrefab.content.text = m.content;
            newsPrefab.textButton.text = m.textButton;
            newsPrefab.linkButton = m.linkButton;
        }
        
    }
}
