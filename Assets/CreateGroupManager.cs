using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateGroupManager : MonoBehaviour
{
    
    [SerializeField] private Button newGroupButton;
    public TMP_InputField newGroupNameInput;
    public TMP_InputField newGroupDescriptionInput;
    public Button buttonSlider;
    public Animator buttonSliderAnimator;
    public bool isPrivate = false;
    
    [DllImport("__Internal")]
    private static extern void JSCreateGroup(string json);
    
    void Start()
    {
        buttonSlider.onClick.AddListener(() => { TooglePrivate(); });
        newGroupButton.onClick.AddListener(() => { CreateGroup(); });
    }
    
    public void TooglePrivate()
    {
        if (isPrivate)
        {
            buttonSliderAnimator.Play("ToLeft");
        }
        else
        {
            buttonSliderAnimator.Play("ToRight");
        }
        isPrivate = !isPrivate;
    }

    public void CreateGroup(){
        if(newGroupNameInput.text != ""){
            CanvasPopup.Instance.OpenPopup(() => {
                CanvasPopup.Instance.OpenLoadingPanel();
                string json = "{\"namegroup\":\"" + newGroupNameInput.text + "\", \"description\":\"" 
                              + newGroupDescriptionInput.text + "\", \"isPrivate\":\""+ isPrivate +"\"}" ;
                JSCreateGroup(json);
            }, null, "Create", "Cancel", "Do you want create this Group?", newGroupNameInput.text, null, null);
        }
    }
    
    
}
