using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatusPanelManager : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Button buttonAvaliable;
    public Button buttonDonotdistur;
    public Button buttonAway;
    public Button buttonOffline;

    public GameObject statusPanelContent;

    [DllImport("__Internal")]
    private static extern void JSChangeStatus(int state);
    
    public static StatusPanelManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this); } 
        else { Instance = this;} 
    }
    
    private bool mouseIsOver = false;
    
    public void OpenStatus()
    {
        statusPanelContent.SetActive(true);
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    public void OnDeselect(BaseEventData eventData) {
        //Close the Window on Deselect only if a click occurred outside this panel
        if (!mouseIsOver) { statusPanelContent.SetActive(false); }
    }
    public void OnPointerEnter(PointerEventData eventData) {
        mouseIsOver = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    public void OnPointerExit(PointerEventData eventData) {
        mouseIsOver = false;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    public void SetState(int state)
    {
        CanvasPopup.Instance.OpenPopupInLoading();
        CloseStatusPanel();
        JSChangeStatus(state);
    }
    public void CloseStatusPanel()
    {
        statusPanelContent.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
    
}
