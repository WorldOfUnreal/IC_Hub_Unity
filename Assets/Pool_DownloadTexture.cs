using UnityEngine;
using System.Collections.Generic;

public class Pool_DownloadTexture : MonoBehaviour
{
    public DownloadTexture objectPrefab;
    public int initialPoolSize = 10;

    private List<DownloadTexture> availableObjects = new List<DownloadTexture>();
    private List<DownloadTexture> inUseObjects = new List<DownloadTexture>();

    public Dictionary<string, Texture2D> inventoryTextures = new Dictionary<string, Texture2D> ();

    
    public static Pool_DownloadTexture Instance { get; private set; }
    private void Awake() 
    {
        if (Instance != null && Instance != this) { Destroy(this); } 
        else { Instance = this;} 
    }
    private void Start()
    {
        InitializePool();
    }
    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            DownloadTexture newObj = CreateNewObject();
            availableObjects.Add(newObj);
        }
    }
    private DownloadTexture CreateNewObject()
    {
        DownloadTexture newObj = Instantiate(objectPrefab, this.transform);
        newObj.gameObject.SetActive(false);
        return newObj;
    }
    public void DownloadTextureFromIDM(ImageDownloadManager imd)
    {
        DownloadTexture obj = null;

        if (availableObjects.Count > 0)
        {
            obj = availableObjects[0];
            availableObjects.RemoveAt(0);
        }
        else { obj = CreateNewObject(); }

        obj.gameObject.SetActive(true);
        inUseObjects.Add(obj);

        obj.StartGetTexture(imd);
    }
    public void ReleaseObject(DownloadTexture obj)
    {
        Debug.Log("Textures in Dictionary: " + inventoryTextures.Count);
        
        obj.gameObject.SetActive(false);
        inUseObjects.Remove(obj);
        availableObjects.Add(obj);
    }
    
    
}