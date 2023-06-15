using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App_ContextualMenu : ContextualMenu
{

    public int idApp = 0;
    public AppBrowserController appBrowserController;

    public void GoToApp()
    {
        appBrowserController.OnClickAppIcon(idApp);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void AddAppToFavorites()
    {
        Debug.Log("AddAppToFavorites");
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void LaunchApp()
    {
        Debug.Log("LaunchApp");
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void ReportApp()
    {
        Debug.Log("ReportApp");
        CanvasReport.Instance.OpenPopupReport(idApp.ToString(), 1);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
}
