using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Status_ContextualMenu : ContextualMenu
{
    [DllImport("__Internal")]
    private static extern void JSChangeStatus(int state);
    
    public void SetState(int state)
    {
        CanvasPopup.Instance.OpenPopupInLoading();
        JSChangeStatus(state);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    
}
