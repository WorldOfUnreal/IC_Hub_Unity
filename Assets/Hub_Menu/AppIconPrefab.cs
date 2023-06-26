using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AppIconPrefab : MonoBehaviour
{
    public int id;
    public int favAppOrder;
    public Image logoImage;
    public ImageDownloadManager imageDownloadManager;
    public Image bgSelected;
    public GameObject barSelected;
    public Button buttonApp;
    public ClickableObject clickableObject;
    public AppBrowserController.AppCategory appCategory;

    

}
