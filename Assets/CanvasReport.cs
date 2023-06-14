using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasReport : MonoBehaviour
{
    public static CanvasReport Instance { get; private set; }
    private void Awake() 
    {
        if (Instance != null && Instance != this) { Destroy(this); } 
        else { Instance = this;} 
    }
    
    public class ReportData
    {
        public string idReport;
        public int dropdown;
        public string description;
    }
    [DllImport("__Internal")]
    private static extern void JSSendReport(string json);
    
    public GameObject panelParent;
    public TMP_Dropdown dropdown;
    public TMP_InputField descriptionReport;
    [Header("Buttons Panel : ")] 
    public Button buttonCancel;
    public Button buttonSend;

    public void ClosePopupReport()
    {
        panelParent.SetActive(false);
    }

    public void OpenPopupReport(string ID)
    {
        buttonCancel.onClick.RemoveAllListeners();
        buttonCancel.onClick.AddListener(ClosePopupReport);
        
        buttonSend.onClick.RemoveAllListeners();
        buttonSend.onClick.AddListener(() => SendReport(ID) );
        
        panelParent.SetActive(true);
    }

    public void SendReport(string ID)
    {
        ReportData reportData = new ReportData();
        reportData.idReport = ID;
        reportData.dropdown = dropdown.value;
        reportData.description = descriptionReport.text;
        
        CanvasPopup.Instance.OpenPopupInLoading();
        JSSendReport( JsonUtility.ToJson(reportData) );
        
    }
     
        
}
