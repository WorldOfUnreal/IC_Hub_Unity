using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    
    public Button buttonYes;
    public Animator panelPopupAnimator;
    public Animator iconSearchAnimator;
    public TMP_Text TitlePopup;

    public void ClosePopupFromConfirm()
    {
        panelPopupAnimator.Play("Confirm_To_Closed");
    }
    public void ClosePopupFromSuccess()
    {
        panelPopupAnimator.Play("Success_To_Closed");
    }
    public void OpenPopup(UnityAction call)
    {
        buttonYes.onClick.RemoveAllListeners();
        buttonYes.onClick.AddListener(call);
        panelPopupAnimator.Play("Open_Confirm_Panel");
       
    }
    public void OpenLoadingPanel()
    {
        panelPopupAnimator.Play("Confirm_To_Loading");
    }
    public void OpenSuccessPanel()
    {
        panelPopupAnimator.Play("Loading_To_Success");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OpenSuccessPanel();
        }
    }

    public void ChangeTitleNameToConfirm() { TitlePopup.text = "CONFIRM"; }
    public void ChangeTitleNameToProcessing() { TitlePopup.text = "PROCESSING"; }
    public void ChangeTitleNameToSuccess() { TitlePopup.text = "SUCCESS"; }
    public void ChangeTitleNameToFailed() { TitlePopup.text = "FAILED"; }

    public void StopSearchIconAnim() { iconSearchAnimator.Play("Searching_Stop"); }
    public void StartSearchIconAnim() { iconSearchAnimator.Play("Searching");}
}
