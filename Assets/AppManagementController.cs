using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class AppManagementController : MonoBehaviour
{
    
    [DllImport("__Internal")]
    private static extern void JSSendDataApp(string json);
    
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
    public TMP_InputField nameInput;
    public TMP_InputField categoryInput;
    public TMP_InputField linkDappInput;
    public TMP_InputField nftCollections;
    public TMP_InputField DSCVRPortalInput;
    public TMP_InputField DistriktInput;
    public TMP_InputField OpenChatInput;
    public TMP_InputField CatalyzeInput;
    public TMP_InputField TwitterInput;
    public TMP_InputField AppVersionInput;
    public string BannerSlot;
    public string LogoSlot;

    public void SubmitApp()
    {
        AppData appData = new AppData();
        appData.category = Hub_Manager.AppCategory.Games;
        appData.linkDapp = linkDappInput.text;
        appData.nftCollections = new List<string>();
        appData.DSCVRPortal = DSCVRPortalInput.text;
        appData.Distrikt = DistriktInput.text;
        appData.OpenChat = OpenChatInput.text;
        appData.Catalyze = CatalyzeInput.text;
        appData.Twitter = TwitterInput.text;
        appData.AppVersion = AppVersionInput.text;
        appData.Banner = "aaaa";
        appData.Logo = "bbbb";
        
        string json = JsonUtility.ToJson(appData);
        JSSendDataApp(json);
    }

    private void Start()
    {
        AppData appData = new AppData();
        appData.category = Hub_Manager.AppCategory.Games;
        appData.linkDapp = "linkDappInput.text";
        appData.nftCollections = new List<string>();
        appData.nftCollections.Add("Collection1");appData.nftCollections.Add("Collection2");
        appData.DSCVRPortal = "DSCVRPortalInput.text";
        appData.Distrikt = "DistriktInput.text";
        appData.OpenChat = "OpenChatInput.text";
        appData.Catalyze = "CatalyzeInput.text";
        appData.Twitter = "TwitterInput.text";
        appData.AppVersion = "AppVersionInput.text";
        appData.Banner = "aaaa";
        appData.Logo = "bbbb";
        
        string json = JsonUtility.ToJson(appData);
        JSSendDataApp(json);
    }
}
