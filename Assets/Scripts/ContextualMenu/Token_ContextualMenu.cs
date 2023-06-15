using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token_ContextualMenu : ContextualMenu
{
    [HideInInspector] public int tokenID;
    [HideInInspector] public string tokenName;
    [HideInInspector] public string tokenAvatar;

    
    public void SendToken()
    {
        //Open panel sendToken
        CanvasSendCrypto.Instance.OpenPopupSendCrypto(tokenID.ToString(), true, tokenAvatar);
        ContextualMenuManager.Instance.CloseContextualMenu();
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void ViewToken()
    {
        //Open panel sendToken
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
   
}
