using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFT_ContextualMenu : MonoBehaviour
{
    [HideInInspector] public Hub_Manager.UserNFTs userNFT;
    
    
    public void GoToSendNFT()
    {
        //Hub_Manager.Instance.OpenSection(4; 
        //OPen Panel Send NFT
    }
    public void OpenUrl()
    {
        Application.OpenURL(userNFT.nftUrl);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }

}
