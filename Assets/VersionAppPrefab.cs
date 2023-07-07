using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VersionAppPrefab : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void JSDeleteVersion(int id);
    
    public class VersionAppData
    {
        public int versionID;
        public string projectName;
        public string linkDapp;
        public string currentVersion;
        public string blockChain;
    }
    
    public TMP_InputField projectNameInput;
    public TMP_InputField linkDappInput;
    public TMP_InputField currentVersionInput;
    public TMP_InputField blockChainInput;
    public int versionID;

    public Button removeVersionBtn;

    public void fillVersionAppData(VersionAppData versionAppData)
    {
        projectNameInput.text = versionAppData.projectName;
        linkDappInput.text = versionAppData.linkDapp;
        currentVersionInput.text = versionAppData.currentVersion;
        blockChainInput.text = versionAppData.blockChain;
        versionID = versionAppData.versionID;

        removeVersionBtn.onClick.RemoveAllListeners();
        removeVersionBtn.onClick.AddListener(() =>
        {
            CanvasPopup.Instance.OpenPopup(() =>
            {
                CanvasPopup.Instance.OpenLoadingPanel();
                JSDeleteVersion(versionAppData.versionID);
            },null, "Delete Version", "Cancel", "Do you want delete this version?", versionAppData.currentVersion, null, null);
        });

    }
    
    
   
    
    


}
