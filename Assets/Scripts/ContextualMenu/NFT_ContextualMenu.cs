using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFT_ContextualMenu : ContextualMenu
{
    [HideInInspector] public Hub_Manager.UserNFTs userNFT;
    [HideInInspector] public string marketplace;
    
    public void OpenSectionNFT()
    {
        Hub_Manager.Instance.OpenSection(6); 
        NftSectionController.Instance.UpdateInfo(userNFT, marketplace);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void GoToSendNFT()
    {
        CanvasSendCrypto.Instance.OpenPopupSendCrypto(userNFT.nftID, false, userNFT.nftAvatar);
        ContextualMenuManager.Instance.CloseContextualMenu();
        //Hub_Manager.Instance.OpenSection(4; 
        //OPen Panel Send NFT
    }
    public void OpenUrl()
    {
        Application.OpenURL(userNFT.nftUrl);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void OpenMarketPlace()
    {
        Application.OpenURL(marketplace);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }

   


}
