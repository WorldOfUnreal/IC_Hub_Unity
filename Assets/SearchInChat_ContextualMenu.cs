using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchInChat_ContextualMenu : ContextualMenu
{
    public GameObject createGroupPanel;
    
    public void SearchUsers()
    {
        SearchUserManager.Instance.OpenPopup();
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void SearchGroups()
    {
        SearchGroupManager.Instance.OpenPopup();
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void CreateGroup()
    {
        createGroupPanel.SetActive(true);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
   
}
