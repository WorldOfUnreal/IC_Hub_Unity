using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Text;
using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine.SceneManagement;

//For the InputField to work in WebGL go to Project Settings
// then Go in "Player" Show HTML5/WebGL settings
// Set the Active Input Handling to "Both" instead of "Input System Package (New)"
// if you don't have an EventSystem, create it by right click on Inspector->UI->EventSystem

public class LoginManager : MonoBehaviour
{
    public class UserData {
        public string User;
        public string Wallet;
    }
    [SerializeField] TMP_InputField inputNameField;
    [SerializeField] TMP_InputField inputHashField;
    [SerializeField] GameObject walletsPanel;
    [SerializeField] GameObject loadingPanel;
    [SerializeField] GameObject registrationPanel;
    [SerializeField] GameObject userInfoPanel;
    [SerializeField] GameObject avatarPanel;
    [SerializeField] TMP_InputField inputUrlAvatarField;
    [SerializeField] GameObject urlSection;
    [SerializeField] GameObject textAvatarSection;
    private ImageDowloadManager avatarImage;
    [SerializeField] Button acceptAvatarButton;
    [SerializeField] TMP_Text userInfo_NameHash;
    [SerializeField] TMP_Text userInfo_Account;
    [SerializeField] Toggle toggleKeepLogin;
    
    [SerializeField] string mainScene = "Menu";
    /// WebGL
    [DllImport("__Internal")]
    public static extern void JSWalletsLogin(string json);
    [DllImport("__Internal")]
    public static extern void JSSetNameLogin(string accountName);
    [DllImport("__Internal")]
    public static extern void JSSetAvatarURL(string url);
    [DllImport("__Internal")]
    public static extern void JSSetAvatarImage();
    [DllImport("__Internal")]
    public static extern void JSCopyToClipboard(string accountName);
    
    
    
    public void OnReceiveLoginData(string user)//ListenerReact
    {
        if (string.IsNullOrEmpty(user))
        {
            Debug.Log("String with Username is Empty");
            loadingPanel.SetActive(false);
            registrationPanel.SetActive(true);
        }
        else
        {
            Debug.Log("El usuario tiene registrado un nombre, se procede a la otra escena");
            SceneManager.LoadScene(mainScene);
        }

    }
    public void OnNamePlayerSet(string json)//ListenerReact
    {
        if (!string.IsNullOrEmpty(json))
        {
            UserData userData = JsonUtility.FromJson<UserData>(json);
            userInfo_NameHash.text = userData.User;
            userInfo_Account.text = userData.Wallet;
            loadingPanel.SetActive(false);
            userInfoPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Se llamó la función sin nombre de usuario, error");
        }
    }
    
    public void SetPlayerName()
    {
        if (!string.IsNullOrEmpty(inputNameField.text) && !string.IsNullOrEmpty(inputHashField.text))
        {
            string playerName = inputNameField.text+"#"+inputHashField.text;
            PlayerPrefs.SetString("AccountName", playerName);
            JSSetNameLogin(playerName);
            registrationPanel.SetActive(false);
            loadingPanel.SetActive(true);
        }
        else
        {
            Debug.Log("NameField or HashField are empty");
        }
    }
    
    //Calls to React WebGL
    public void StoicLogin()
    {
        JSWalletsLogin("{\"wallet\":\"StoicWallet\", \"KeepLogin\":\""+ toggleKeepLogin.isOn +"\"}");
        walletsPanel.SetActive(false);
        loadingPanel.SetActive(true);
    }
    public void IdentityLogin()
    {
        JSWalletsLogin("{\"wallet\":\"IdentityWallet\", \"KeepLogin\":\""+ toggleKeepLogin.isOn +"\"}");
        walletsPanel.SetActive(false);
        loadingPanel.SetActive(true);
    }
    public void InfinityLogin()
    {
        JSWalletsLogin("{\"wallet\":\"InfinityWallet\", \"KeepLogin\":\""+ toggleKeepLogin.isOn +"\"}");
        walletsPanel.SetActive(false);
        loadingPanel.SetActive(true);
    }
    public void PlugLogin()
    {
        JSWalletsLogin("{\"wallet\":\"PlugWallet\", \"KeepLogin\":\""+ toggleKeepLogin.isOn +"\"}");
        walletsPanel.SetActive(false);
        loadingPanel.SetActive(true);
    }
    
    public void SetAvatarURL()
    {
        JSSetAvatarURL(inputUrlAvatarField.text);
        avatarPanel.SetActive(false);
        loadingPanel.SetActive(true);
    }
    public void SetAvatarImage()
    {
        JSSetAvatarImage();
        avatarPanel.SetActive(false);
        loadingPanel.SetActive(true);
    }
    public void OnAvatarReady()
    {
        loadingPanel.SetActive(false);
        SceneManager.LoadScene(mainScene);
    }
    public void OnAvatarUploadReady(string url)
    {
        loadingPanel.SetActive(false);
        urlSection.SetActive(false);
        textAvatarSection.SetActive(true);
        avatarImage.ChangeUrlImage(url);
        acceptAvatarButton.onClick.RemoveAllListeners();
        acceptAvatarButton.onClick.AddListener(()=>{SceneManager.LoadScene(mainScene);});
    }
    
    public void CopyTextToClipBoard(TMP_Text tmpText)
    {
        JSCopyToClipboard(tmpText.text);
    }
    
    
}

