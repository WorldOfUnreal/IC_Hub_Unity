using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CanvasPopup : MonoBehaviour
{

    public static CanvasPopup Instance { get; private set; }
    private void Awake() 
    {
        if (Instance != null && Instance != this) { Destroy(this); } 
        else { Instance = this; } 
    }

    public GameObject popupBG;
    public GameObject panelQuestion;
    public Button buttonYes;
    public GameObject panelLoading;
    public GameObject panelSuccess;


    public void ClosePopup()
    {
        popupBG.SetActive(false);
        panelQuestion.SetActive(false);
        panelLoading.SetActive(false);
        panelSuccess.SetActive(false);
    }
    public void OpenPopup(UnityAction call)
    {
        popupBG.SetActive(true);
        panelQuestion.SetActive(true);
        panelLoading.SetActive(false);
        panelSuccess.SetActive(false);
        buttonYes.onClick.AddListener(call);
    }
    public void OpenLoadingPanel()
    {
        popupBG.SetActive(true);
        panelQuestion.SetActive(false);
        panelLoading.SetActive(true);
        panelSuccess.SetActive(false);
    }
    public void OpenSuccessPanel()
    {
        popupBG.SetActive(true);
        panelQuestion.SetActive(false);
        panelLoading.SetActive(false);
        panelSuccess.SetActive(true);
    }

}
