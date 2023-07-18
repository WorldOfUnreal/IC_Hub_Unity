using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class App_ContextualMenu : ContextualMenu
{

    public int idApp = 0;
    public AppBrowserController appBrowserController;
    public bool isFavoriteIcon;

    public TMP_Text AddRemoveFavoriteTmp;
    
    [DllImport("__Internal")]
    private static extern void JSAddAppToFavorite(int id);

    public void GoToApp()
    {
        appBrowserController.OnClickAppIcon(idApp);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void AddAppToFavorites()
    {
        if (!isFavoriteIcon)
        {
            //JSAddAppToFavorite(idApp);
            //CanvasPopup.Instance.OpenPopupInLoading();
            appBrowserController.AddAppToFavorite(idApp);
            ContextualMenuManager.Instance.CloseContextualMenu();
        }
        else
        {
            //JSAddAppToFavorite(idApp);
            //CanvasPopup.Instance.OpenPopupInLoading();
            appBrowserController.RemoveFromFavorite(idApp);
            ContextualMenuManager.Instance.CloseContextualMenu();
            
        }
        
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
