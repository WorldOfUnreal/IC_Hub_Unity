using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search_ContextualMenu : ContextualMenu
{
    public GameObject searchUserPanel;
    public GameObject searchGroupPanel;
    
    public void SearchUsers()
    {
        searchUserPanel.SetActive(true);
        searchGroupPanel.SetActive(false);
        Hub_Manager.Instance.OpenSection(0);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void SearchGroups()
    {
        searchUserPanel.SetActive(false);
        searchGroupPanel.SetActive(true);
        Hub_Manager.Instance.OpenSection(0);
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
