using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class User_ContextualMenu : ContextualMenu
{

    [HideInInspector] public string principalID;
    [HideInInspector] public string username;
    
    [DllImport("__Internal")]
    private static extern void JSSendMessageToUser(string principalID);
    
    public void StartChatWithUser()
    {
        Debug.Log("StartChatWithUser");
        JSSendMessageToUser(principalID);
        Hub_Manager.Instance.OpenSection(0);
        ChatManager.Instance.WaitFromGroupCreated();
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void GoToProfile()
    {
        CanvasPlayerProfile.Instance.OpenPopupPlayerProfile(principalID, username);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void ReportUser()
    {
        Debug.Log("ReportUser");
        CanvasReport.Instance.OpenPopupReport(principalID, 0);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
}
