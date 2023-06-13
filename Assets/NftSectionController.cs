using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NftSectionController : MonoBehaviour
{
    
    public static NftSectionController Instance { get; private set; }
        
    private void Awake() 
    {
        if (Instance != null && Instance != this) { Destroy(this); } 
        else { Instance = this;} 
    }
    
    [Header("UI NFT Content: ")] 
    public ImageDowloadManager logoNFT;
    public TMP_Text titleNFT;
    public TMP_Text ownerNFT;
    public TMP_Text linkNFT;
    public TMP_Text marketplaceNFT;
    
    [Header("UI App Buttons: ")] 
    public GoToURLButton linkNFTButton;
    public GoToURLButton marketplaceButton;
    
    public void UpdateInfo(Hub_Manager.UserNFTs userNfTs, string marketPlace)
    {
        logoNFT.ChangeUrlImage(userNfTs.nftAvatar);
        titleNFT.text = userNfTs.nftName;
        ownerNFT.text = userNfTs.nftOwner;
        linkNFT.text = userNfTs.nftUrl;     linkNFTButton.UrlChange(userNfTs.nftUrl);
        marketplaceNFT.text = marketPlace;  marketplaceButton.UrlChange(marketPlace);
    }
}
