using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserStatus_ContextualMenu : ContextualMenu
{
    [HideInInspector] public string principalID;
    [HideInInspector] public string username;
    [HideInInspector] public int hasApp;

    public GameObject appManagement;
    public GameObject appRegistration;
    public GameObject versionControl;
    public GameObject uploadNews;
    public GameObject nftCollection;
    
    public Button btn_appRegistration;
    public Button btn_versionControl;
    public Button btn_uploadNews;
    public Button btn_nftCollection;

    public TMP_Text TMP_PrincipalID;
    public TMP_Text TMP_Username;
    public TMP_Text TMP_OpenAppManagement;

    [DllImport("__Internal")]
    private static extern void JSChangeStatus(int state);
    [DllImport("__Internal")]
    private static extern void JSLogoutFromProfile();
    [DllImport("__Internal")]
    public static extern void JSCopyToClipboard(string accountName);
    
    public void SetState(int state)
    {
        CanvasPopup.Instance.OpenPopupInLoading();
        JSChangeStatus(state);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void GoToProfile()
    {
        CanvasPlayerProfile.Instance.OpenPopupPlayerProfile(principalID, username);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void GoToAppManagement()
    {
        Hub_Manager.Instance.OpenSection(1);
        appManagement.SetActive(false);
        appRegistration.SetActive(false);
        versionControl.SetActive(false);
        uploadNews.SetActive(false);
        nftCollection.SetActive(false);
        if (hasApp != 0)
        {
            appManagement.SetActive(true);
            btn_versionControl.interactable = true;
            btn_uploadNews.interactable = true;
            btn_nftCollection.interactable = true;
        }
        else
        {
            appRegistration.SetActive(true);
            btn_versionControl.interactable = false;
            btn_uploadNews.interactable = false;
            btn_nftCollection.interactable = false;
        }
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void CopyTextToClipBoard() { JSCopyToClipboard(principalID); }
    public void LogoutFromProfile() { JSLogoutFromProfile(); }
}
