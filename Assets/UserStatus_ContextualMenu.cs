using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class UserStatus_ContextualMenu : ContextualMenu
{
    [HideInInspector] public string principalID;
    [HideInInspector] public string username;

    public TMP_Text TMP_PrincipalID;
    public TMP_Text TMP_Username;

    [DllImport("__Internal")]
    private static extern void JSChangeStatus(int state);
    [DllImport("__Internal")]
    private static extern void JSLogoutFromProfile();
    [DllImport("__Internal")]
    public static extern void JSCopyToClipboard(string accountName);
    
    public void SetState(int state)
    {
        CanvasPopup.Instance.OpenPopupInLoading();
//        JSChangeStatus(state);
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
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void CopyTextToClipBoard() { JSCopyToClipboard(principalID); }
    public void LogoutFromProfile() { JSLogoutFromProfile(); }
}
