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
    
    public Animator panelPopupAnimator;
    public Animator iconSearchAnimator;
    public Button buttonYes;
    public TMP_Text buttonYesTMP;
    public Button buttonNo;
    public TMP_Text buttonNoTMP;
    public TMP_Text titlePopup;
    public TMP_Text descriptionPopup;
    public ImageDownloadManager avatar;
    public TMP_Text middleTMP1;
    public TMP_Text middleTMP2;

    public void ClosePopupFromConfirm()
    {
        panelPopupAnimator.Play("Confirm_To_Closed");
    }
    public void ClosePopupFromSuccess()
    {
        panelPopupAnimator.Play("Success_To_Closed");
    }
    public void OpenPopup(UnityAction callYes,UnityAction callNo,string buttonYes_Txt,string buttonNo_Txt,string description,string middleText1,string middleText2, string avatarURL)
    {
        buttonYes.onClick.RemoveAllListeners();
        buttonYes.onClick.AddListener(callYes);
        buttonYesTMP.text = buttonYes_Txt;
        
        buttonNo.onClick.RemoveAllListeners();
        if (callNo == null) { buttonNo.onClick.AddListener(ClosePopupFromConfirm); }
        else { buttonNo.onClick.AddListener(callNo); }
        buttonNoTMP.text = buttonNo_Txt;

        descriptionPopup.text = description;
        middleTMP1.gameObject.SetActive(middleText1 != null); middleTMP1.text = middleText1;
        middleTMP2.gameObject.SetActive(middleText2 != null); middleTMP2.text = middleText2;
        avatar.gameObject.SetActive(avatarURL != null); if(avatarURL != null){avatar.ChangeUrlImage(avatarURL);}
        
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
    public void OpenPopupInLoading()
    {
        string middleText1 = "Loading";
        string middleText2 = null;
        
        panelPopupAnimator.Play("Open_Loading_Panel");
        descriptionPopup.text = "Loading";
        middleTMP1.gameObject.SetActive(middleText1 != null); middleTMP1.text = middleText1;
        middleTMP2.gameObject.SetActive(middleText2 != null); middleTMP2.text = middleText2;
    }
    

    public void ChangeTitleNameToConfirm() { titlePopup.text = "CONFIRM"; }
    public void ChangeTitleNameToProcessing() { titlePopup.text = "PROCESSING"; }
    public void ChangeTitleNameToSuccess() { titlePopup.text = "SUCCESS"; }
    public void ChangeTitleNameToFailed() { titlePopup.text = "FAILED"; }

    public void StopSearchIconAnim() { iconSearchAnimator.Play("Searching_Stop"); }
    public void StartSearchIconAnim() { iconSearchAnimator.Play("Searching");}
}
