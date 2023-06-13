using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection_ContextualMenu : ContextualMenu
{
    [HideInInspector] public Hub_Manager.Collection collection;
    [HideInInspector] public string groupName;
    
    public void GoToCollection()
    {
        Hub_Manager.Instance.OpenSection(4); 
        CollectionSectionController.Instance.UpdateInfo(collection);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void OpenUrl()
    {
        Application.OpenURL(collection.avatar);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    
}
