using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContextualMenuManager : MonoBehaviour
{
    public static ContextualMenuManager Instance { get; private set; }
    private void Awake() 
    {
        if (Instance != null && Instance != this) { Destroy(this); } 
        else { Instance = this;} 
    }

    [HideInInspector]
    public GameObject contextualMenuOpened;

    public Search_ContextualMenu search_contextualmenu;
    public App_ContextualMenu app_contextualmenu;
    public User_ContextualMenu user_contextualmenu;
    public Group_ContextualMenu group_ContextualMenu;
    public Token_ContextualMenu token_ContextualMenu;
    public Collection_ContextualMenu collection_ContextualMenu;
    public NFT_ContextualMenu nft_ContextualMenu;
    
    public void OpenSearch_ContextualMenu(GameObject pos)
    {
        search_contextualmenu.transform.position = pos.transform.position;
        OpenContextualMenu(search_contextualmenu.gameObject);
    }
    public void OpenApp_ContextualMenu(GameObject pos, int idApp)
    {
        app_contextualmenu.transform.position = pos.transform.position;
        app_contextualmenu.idApp = idApp;
        OpenContextualMenu(app_contextualmenu.gameObject);
    }
    public void OpenUser_ContextualMenu(GameObject pos, string principalID, string username)
    {
        user_contextualmenu.transform.position = pos.transform.position;
        user_contextualmenu.principalID = principalID;
        user_contextualmenu.username = username;
        OpenContextualMenu(user_contextualmenu.gameObject);
    }
    public void OpenGroup_ContextualMenu(GameObject pos, int groupID, string groupName)
    {
        group_ContextualMenu.transform.position = pos.transform.position;
        group_ContextualMenu.groupID = groupID;
        group_ContextualMenu.groupName = groupName;
        OpenContextualMenu(group_ContextualMenu.gameObject);
    }
    public void OpenToken_ContextualMenu(GameObject pos, Hub_Manager.Token token)
    {
        token_ContextualMenu.transform.position = pos.transform.position;
        token_ContextualMenu.tokenID = token.id;
        token_ContextualMenu.tokenName = token.name;
        token_ContextualMenu.tokenAvatar = token.avatar;
        OpenContextualMenu(token_ContextualMenu.gameObject);
    }
    public void OpenCollection_ContextualMenu(GameObject pos, Hub_Manager.Collection collection)
    {
        collection_ContextualMenu.transform.position = pos.transform.position;
        collection_ContextualMenu.collection = collection;
        OpenContextualMenu(collection_ContextualMenu.gameObject);
    }
    public void OpenNFT_ContextualMenu(GameObject pos, Hub_Manager.UserNFTs userNFT, string marketplace)
    {
        nft_ContextualMenu.transform.position = pos.transform.position;
        nft_ContextualMenu.userNFT = userNFT;
        nft_ContextualMenu.marketplace = marketplace;
        OpenContextualMenu(nft_ContextualMenu.gameObject);
    }
    //OpenBase Global
    public void OpenContextualMenu(GameObject go_ContextualMenu)
    {
        if(contextualMenuOpened){ contextualMenuOpened.SetActive(false);}
        contextualMenuOpened = go_ContextualMenu;
        contextualMenuOpened.SetActive(true);
        EventSystem.current.SetSelectedGameObject(contextualMenuOpened);
    }
    public void CloseContextualMenu()
    {
        if(contextualMenuOpened){ contextualMenuOpened.SetActive(false);}
       // EventSystem.current.SetSelectedGameObject(null);
    }
    



    
}
