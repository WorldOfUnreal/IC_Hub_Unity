using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DownloadTexture : MonoBehaviour
{
    public static DownloadTexture Instance { get; private set; }
        
    private void Awake() 
    {
        if (Instance != null && Instance != this) { Destroy(this); } 
        else { Instance = this;} 
    }

    public void StartGetTexture(ImageDowloadManager idm)
    {
        StartCoroutine(GetTexture(idm));
    }

    public IEnumerator GetTexture(ImageDowloadManager idm) {
        foreach (Image image in idm.listImages) { image.sprite = Resources.Load<Sprite>( "Images/Loading" ); }
        
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(idm.urlImage);
        Debug.Log("Antes yield");
        yield return www.SendWebRequest();
        Debug.Log("despues yield");

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
            foreach (Image image in idm.listImages) { image.sprite = Resources.Load<Sprite>( "Images/Error" ); }
        }
        else {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            foreach (Image image in idm.listImages) { image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());}
        }
    }
}
