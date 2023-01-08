using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Hub_Manager : MonoBehaviour
{
    [System.Serializable]
    public class AppInfo
    {
        public int id;
        public string name;
        public string logo;
        public string banner;
        [SerializeField]
        public AppCategory appCategoryIndex;
        public enum AppCategory { Games, Markets, Defi, Social, New }
        public List<NewsApp> listNews = new List<NewsApp>();
        
    }
    [System.Serializable]
    public class ListApps {
        public List<AppInfo> data;
    }
    [System.Serializable]
    public class NewsApp{
        public string imageNews;
        public string title;
        public string content;
        public string textButton;
        public string linkButton;
    }

    public ListApps listApps = new ListApps();
    [Header("Scroll Contents & Prefabs: ")] 
    public GameObject prefabAppIcon;
    public GameObject prefabNews;
    public GameObject contentApps;
    public GameObject contentNews;

    [Header("UI App Content: ")] 
    public Sprite loadingSprite;
    public Image bannerApp;
    public string bannerAppString;

    
    public void GetAppsInfo(string json){
        listApps = JsonUtility.FromJson<ListApps>(json);
       
        foreach(AppInfo m in listApps.data){
            
            GameObject newAppIcon = Instantiate(prefabAppIcon, contentApps.transform);
            AppIconPrefab appIcon = newAppIcon.GetComponent<AppIconPrefab>();
            appIcon.urlImage = m.logo;
            appIcon.buttonApp.onClick.AddListener(() => { OnClickAppIcon(m.id); });
        }
        //Update UI
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentApps.GetComponent<RectTransform>());
    }

    public void OnClickAppIcon(int id)
    {
        bannerApp.sprite = loadingSprite;
        AppInfo selectedApp = listApps.data[id];
        bannerAppString = selectedApp.banner;
        StartCoroutine(GetTexture(selectedApp.banner));
        
        foreach (Transform t in contentNews.transform) { GameObject.Destroy(t.gameObject); }
        
        foreach(NewsApp m in selectedApp.listNews){
            GameObject newNews = Instantiate(prefabNews, contentNews.transform);
            NewsPrefab newsPrefab = newNews.GetComponent<NewsPrefab>();
            newsPrefab.imageNewsUrl = m.imageNews;
            newsPrefab.title.text = m.title;
            newsPrefab.content.text = m.content;
            newsPrefab.textButton.text = m.textButton;
            newsPrefab.linkButton = m.linkButton;
        }
        
    }
  
    IEnumerator GetTexture(string url) {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            bannerApp.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
        }
    }


}
