using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search_ContextualMenu : ContextualMenu
{
   
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
    public void SearchTokens()
    {
        Debug.Log("SearchTokens");
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void SearchNFTs()
    {
        Debug.Log("SearchNFTs");
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
}
