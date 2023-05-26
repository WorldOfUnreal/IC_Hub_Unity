using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionSectionController : MonoBehaviour
{
    
    public static CollectionSectionController Instance { get; private set; }
        
    private void Awake() 
    {
        if (Instance != null && Instance != this) { Destroy(this); } 
        else { Instance = this;} 
    }
    
    [Header("UI Collection: ")] 
    public TMP_Text nameCollection;
    public ImageDowloadManager iconCollection;

    [Header("Scroll Content & Prefab: ")] 
    public GameObject nftPrefab;
    public GameObject contentNFTs;
    
    public void UpdateInfo(Hub_Manager.Collection collection)
    {
        //iconCollection.ChangeUrlImage(collection.avatar);
        nameCollection.text = collection.colectionName;
        
        foreach (Transform t in contentNFTs.transform) { GameObject.Destroy(t.gameObject); }
        
        foreach(Hub_Manager.UserNFTs n in collection.userNFTs){
            GameObject newNFT = Instantiate(nftPrefab, contentNFTs.transform);
            NftIconPrefab nftIconPrefab = newNFT.GetComponent<NftIconPrefab>();
            nftIconPrefab.imageDowloadManager.ChangeUrlImage(n.nftAvatar);
            
        }
        
    }
}
