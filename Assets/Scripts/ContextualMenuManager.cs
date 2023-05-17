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
