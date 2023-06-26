using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageDownloadManager : MonoBehaviour
{ 
    public string urlImage;
    
    public List<RawImage> rawImages;
    
    private void Awake()
    {
        if (rawImages.Count == 0) { rawImages.Add(this.GetComponent<RawImage>()); }
    }

    public void ChangeUrlImage(string url)
    {
        if (urlImage != url)
        {
            urlImage = url;
            Pool_DownloadTexture.Instance.DownloadTextureFromIDM(this);
            
        }
    }

    public void ChangeImage(Texture2D texture2D)
    {
        foreach (RawImage rawImage in rawImages)
        {
            rawImage.texture = texture2D;
            AspectRatioFitter aspectRatioFitter = rawImage.GetComponent<AspectRatioFitter>();
            if (aspectRatioFitter)
            {
                aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
                aspectRatioFitter.aspectRatio = (float)rawImage.texture.width / rawImage.texture.height;
            }
        }
    }

    
    
}
