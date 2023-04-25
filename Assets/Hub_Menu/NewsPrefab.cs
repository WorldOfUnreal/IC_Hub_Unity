using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NewsPrefab : MonoBehaviour
{
    public ImageDowloadManager imageNews;
    public TMP_Text title;
    public TMP_Text content;
    public TMP_Text textButton;
    public string linkButton = "https://www.cosmicrafts.com/";
    public Button buttonNews;
    
    void Start() {
        buttonNews.onClick.AddListener(() => { OpenURL(); });
    }
    
    public void OpenURL()
    {
        Application.OpenURL(linkButton);
    }
    
    
}
