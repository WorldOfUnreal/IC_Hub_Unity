using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User_ContextualMenu : ContextualMenu
{

    [HideInInspector] public string principalID;
    [HideInInspector] public string username;
    
    public void StartChatWithUser()
    {
        Debug.Log("StartChatWithUser");
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
        CanvasReport.Instance.OpenPopupReport(principalID);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
}
