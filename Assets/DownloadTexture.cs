using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DownloadTexture : MonoBehaviour
{


    public Texture2D texture_Loading;
    public Texture2D texture_Succe;


    private void Start()
    {
        //Texture2D texture2D = Texture2D.blackTexture;
       // sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2());
    }

    public void StartGetTexture(ImageDownloadManager idm)
    {
        StartCoroutine(GetTexture(idm));
    }

    public IEnumerator GetTexture(ImageDownloadManager idm) {
        foreach (RawImage rawImage in idm.rawImages) { rawImage.texture = Resources.Load<Texture2D>( "Images/Loading" ); }
        
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(idm.urlImage);
        Debug.Log("Antes yield");
        yield return www.SendWebRequest();
        Debug.Log("despues yield");

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
            foreach (RawImage rawImage in idm.rawImages) { rawImage.texture = Resources.Load<Texture2D>( "Images/Error" ); }
        }
        else {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            idm.ChangeImage(texture);
            if (!Pool_DownloadTexture.Instance.inventoryTextures.ContainsKey(idm.urlImage))
                { Pool_DownloadTexture.Instance.inventoryTextures.Add(idm.urlImage, texture); }
        }
        Pool_DownloadTexture.Instance.ReleaseObject(this);
    }
    
   
}
