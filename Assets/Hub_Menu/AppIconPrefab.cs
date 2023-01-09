using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AppIconPrefab : MonoBehaviour
{
    public Image logoImage;
    public string urlImage;
    public Image bgSelected;
    public GameObject barSelected;
    public Button buttonApp;

   

    void Start() {
        StartCoroutine(GetTexture());
    }
 
    IEnumerator GetTexture() {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(urlImage);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            logoImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
        }
    }
    


}
