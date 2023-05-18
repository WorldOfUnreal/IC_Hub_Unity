using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContextualMenu : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouseIsOver = false;
    
    public void OnDeselect(BaseEventData eventData) {
        //Close the Window on Deselect only if a click occurred outside this panel
        if (!mouseIsOver && ContextualMenuManager.Instance.contextualMenuOpened == this.gameObject)
        {
            ContextualMenuManager.Instance.CloseContextualMenu();
        }
    }
    public void OnPointerEnter(PointerEventData eventData) {
        mouseIsOver = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
        Debug.Log("Entre");
    }
    public void OnPointerExit(PointerEventData eventData) {
        mouseIsOver = false;
        EventSystem.current.SetSelectedGameObject(gameObject);
        Debug.Log("Salí");
    }
    
}
