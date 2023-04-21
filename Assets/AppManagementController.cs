using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppManagementController : MonoBehaviour
{
    
    [DllImport("__Internal")]
    private static extern void JSSendDataApp(string json);
    [DllImport("__Internal")]
    private static extern void JSSendDataVersions(string json);
    
    public class AppData
    {
        public string name;
        public Hub_Manager.AppCategory category;
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
    public class VersionsData
    {
        public List<VersionAppPrefab.VersionAppData> versionAppDatas;
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
    public string BannerSlot;
    public string LogoSlot;

    [Header("VersionPanel: ")] 
    public GameObject contentVersions;
    public GameObject versionAppPrefab;
    [Header("NewsPanel: ")] 
    public string LogoSlot3;
    
    public void SubmitApp()
    {
        AppData appData = new AppData();
        appData.name = nameInput.text;
        appData.category = (Hub_Manager.AppCategory)categoryInput.value;
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
        appData.Banner = BannerSlot;
        appData.Logo = LogoSlot;
        
        string json = JsonUtility.ToJson(appData);
        CanvasPopup.Instance.OpenPopup(() =>
        {
            CanvasPopup.Instance.OpenLoadingPanel();
            JSSendDataApp(json);
        },null, "Update InfoApp", "Cancel", "Do you want update info App?", null, null);
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
        BannerSlot = appData.Banner;
        LogoSlot= appData.Logo;
        
        //LayoutRebuilder.ForceRebuildLayoutImmediate(contentTokens.GetComponent<RectTransform>());  //Update UI
    }

    public void SubmitInfoVersions()
    {
        VersionsData versionsData = new VersionsData();
        versionsData.versionAppDatas = new List<VersionAppPrefab.VersionAppData>();

        foreach (Transform t in contentVersions.transform)
        {
            VersionAppPrefab versionAppPrefab = t.gameObject.GetComponent<VersionAppPrefab>();
            
            VersionAppPrefab.VersionAppData versionAppData = new VersionAppPrefab.VersionAppData();
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
        },null, "Update Info Versions", "Cancel", "Do you want update info versions?", null, null);

    }
    public void GetInfoVersions(string json)
    {
        VersionsData versionsData = JsonUtility.FromJson<VersionsData>(json);

        foreach (Transform t in contentVersions.transform) { GameObject.Destroy(t.gameObject); }
        foreach (VersionAppPrefab.VersionAppData version in versionsData.versionAppDatas)
        {
            GameObject newVersion = Instantiate(versionAppPrefab, contentVersions.transform);
            VersionAppPrefab versionPrefab = newVersion.GetComponent<VersionAppPrefab>();
            versionPrefab.fillVersionAppData(version);
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
    
    
}