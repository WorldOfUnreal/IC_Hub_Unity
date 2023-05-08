using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageDowloadManager : MonoBehaviour
{ 
    public string urlImage;
    public List<Image> listImages;

    private void Awake()
    {
        if (listImages.Count == 0)
        {
            listImages.Add(this.GetComponent<Image>());
        }
    }

    public void ChangeUrlImage(string url)
    {
        if (urlImage != url)
        {
            urlImage = url;
            DownloadTexture.Instance.StartGetTexture(this);
        }
    }

    
    
}
