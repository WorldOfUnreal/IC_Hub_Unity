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
            StartCoroutine(GetTexture());
        }
    }

    IEnumerator GetTexture() {
        foreach (Image image in listImages) { image.sprite = Resources.Load<Sprite>( "Images/Loading" ); }
        
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(urlImage);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
            foreach (Image image in listImages) { image.sprite = Resources.Load<Sprite>( "Images/Error" ); }
        }
        else {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            foreach (Image image in listImages) { image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());}
        }
    }
    
}
