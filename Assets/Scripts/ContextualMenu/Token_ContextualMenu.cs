using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token_ContextualMenu : MonoBehaviour
{
    [HideInInspector] public int tokenID;
    [HideInInspector] public string tokenName;

    
    public void SendToken()
    {
        //Open panel sendToken
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void ViewToken()
    {
        //Open panel sendToken
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
   
}
