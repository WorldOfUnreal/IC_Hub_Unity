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
