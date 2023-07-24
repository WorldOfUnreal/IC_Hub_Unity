using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class AppIconPrefab : MonoBehaviour
{
    public int id;
    public string nameApp;
    public string description;
    public int favAppOrder;
    public TMP_Text nameTMP;
    public ImageDownloadManager imageDownloadManager;
    public Image bgSelected;
    public GameObject barSelected;
    public ClickableObject clickableObject;
    public AppBrowserController.AppCategory appCategory;

    

}
