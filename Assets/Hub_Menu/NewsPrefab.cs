using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NewsPrefab : MonoBehaviour
{
    public string imageNewsUrl;
    public Image imageNews;
    public TMP_Text title;
    public TMP_Text content;
    public TMP_Text textButton;
    public string linkButton = "https://www.cosmicrafts.com/";
    public Button buttonNews;
    
    void Start() {
       
        StartCoroutine(GetTexture());
        buttonNews.onClick.AddListener(() => { OpenURL(); });
    }
    
    public void OpenURL()
    {
        Application.OpenURL(linkButton);
    }
 
    IEnumerator GetTexture() {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageNewsUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            imageNews.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
        }
    }

    
}
