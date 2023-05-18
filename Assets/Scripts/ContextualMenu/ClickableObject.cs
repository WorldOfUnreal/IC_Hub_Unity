using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
 
public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
    public UnityAction callLeftClick = (() =>  Debug.Log("Left click") );
    public UnityAction callRightClick = (() =>  Debug.Log("Right click") );
        
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (callLeftClick != null) callLeftClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (callRightClick != null) callRightClick();
        }
    }
}