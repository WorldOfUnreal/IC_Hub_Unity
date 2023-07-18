using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateGroupManager : MonoBehaviour
{
    
    public class GroupData
    {
        public string namegroup;
        public string description;
        public string isPrivate;
        public string avatarURL;
    }
    
    [SerializeField] private Button newGroupButton;
    public TMP_InputField newGroupNameInput;
    public TMP_InputField newGroupDescriptionInput;
    public Button buttonSlider;
    public ImageDownloadManager groupAvatar;
    public Animator buttonSliderAnimator;
    public bool isPrivate = false;
    public GameObject contentCG;
    
    [DllImport("__Internal")]
    private static extern void JSCreateGroup(string json);
    [DllImport("__Internal")]
    private static extern void JSSetAvatarToGroup();
    
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
    public void SetAvatarImageFromGroup() { JSSetAvatarToGroup(); }
    public void OnAvatarUploadLoading()
    {
        CanvasPopup.Instance.OpenPopupInLoading();
    }
    public void OnAvatarUploadReady(string avatarURL)
    {
        CanvasPopup.Instance.OpenSuccessPanel();
        groupAvatar.ChangeUrlImage(avatarURL);
    }

    public void CreateGroup(){
        if(newGroupNameInput.text != ""){
            CanvasPopup.Instance.OpenPopup(() => {
                CanvasPopup.Instance.OpenLoadingPanel();
                
                contentCG.SetActive(false);
                GroupData groupData = new GroupData();
                groupData.namegroup = newGroupNameInput.text;
                groupData.description = newGroupDescriptionInput.text;
                groupData.isPrivate = isPrivate.ToString();
                groupData.avatarURL = groupAvatar.urlImage;
                JSCreateGroup(JsonUtility.ToJson(groupData));
            }, null, "Create", "Cancel", "Do you want create this Group?", newGroupNameInput.text, null, groupAvatar.urlImage);
        }
    }
    
    
}
