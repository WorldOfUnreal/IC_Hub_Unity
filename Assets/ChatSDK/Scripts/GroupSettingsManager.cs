using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GroupSettingsManager : MonoBehaviour
{

    public GameObject panelAdmin;
    public GameObject panelUser;

    [Header("Admin Panel : ")]
    public GameObject prefabRequest;
    public GameObject prefabMember;
    public GameObject contentRequests;
    public GameObject contentMembers_owner;
    public GameObject contentMembers_admin;
    public GameObject contentMembers_user;
    public Image iconGroup;
    public TMP_InputField tittleGroup;
    public Button editTitleGroup;
    public Button editConfirmTitleGroup;
    public Button editCancelTitleGroup;
    public TMP_InputField descriptionGroup;
    public Button editDescriptionGroup;
    public Button editConfirmDescriptionGroup;
    public Button editCancelDescriptionGroup;
    public TMP_Text numberUsers;
    public TMP_Text numberInvites;
    public Animator buttonSliderStates;
    public Button buttonSlider;
    public GameObject transferOwnership_Gameobject;
    
    [Header("User Panel : ")]
    public GameObject prefabMember_User;
    public GameObject contentMembersPanelUser;
    public Image iconGroup_User;
    public TMP_Text tittleGroup_User;
    public TMP_Text descriptionGroup_User;

    private string originalTitle;
    private string originalDescription;
    private InfoPanelSetting infoPanelSetting;

    
    [DllImport("__Internal")]
    private static extern void JSAcceptRequest(string json);
    [DllImport("__Internal")]
    private static extern void JSDenyRequest(string json);
    [DllImport("__Internal")]
    private static extern void JSKickUser(string json);
    [DllImport("__Internal")]
    private static extern void JSMakeAdmin(string json);
    [DllImport("__Internal")]
    private static extern void JSRemoveAdmin(string json);
    [DllImport("__Internal")]
    private static extern void JSSetGroupPrivate(int id);
    [DllImport("__Internal")]
    private static extern void JSSetGroupPublic(int id);
    [DllImport("__Internal")]
    private static extern void JSChangeTitle(string json);
    [DllImport("__Internal")]
    private static extern void JSChangeDescription(string json);
    
    public void GetInfoPanelSettings(string json)
    {
        infoPanelSetting = JsonUtility.FromJson<InfoPanelSetting>(json);
        originalTitle = infoPanelSetting.nameGroup;
        originalDescription = infoPanelSetting.descriptionGroup;
        CancelEditDescription(); 
        CancelEditTitle();
        
        if (infoPanelSetting.roleuser == RoleUser.User)
        {
            panelUser.SetActive(true); panelAdmin.SetActive(false);
        //Fill overview
            //iconGroup_User.sprite = infoPanelSetting.avatarGroup;
            tittleGroup_User.text = infoPanelSetting.nameGroup;
            descriptionGroup_User.text = infoPanelSetting.descriptionGroup;
        //Fill members
            foreach (Transform t in contentMembersPanelUser.transform) { GameObject.Destroy(t.gameObject); }
            foreach (MembersGroup g in infoPanelSetting.members)
            {
                AddMemberToContent_User(g.principalID, g.username, g.avatarUser, g.roleuser);
            }
        }
        else if (infoPanelSetting.roleuser == RoleUser.Admin || infoPanelSetting.roleuser == RoleUser.Owner)
        {
            panelUser.SetActive(false); panelAdmin.SetActive(true);
        //Fill overview
            //iconGroup.sprite = infoPanelSetting.avatarGroup;
            tittleGroup.text = infoPanelSetting.nameGroup; 
            descriptionGroup.text = infoPanelSetting.descriptionGroup; 
            if (infoPanelSetting.isPrivate)
            {
                buttonSliderStates.Play("InRight");
                buttonSlider.onClick.RemoveAllListeners();
                buttonSlider.onClick.AddListener(() => { SetGroupPublic(infoPanelSetting.nameGroup, infoPanelSetting.idGroup);});
            }
            else
            {
                buttonSliderStates.Play("InLeft");
                buttonSlider.onClick.RemoveAllListeners();
                buttonSlider.onClick.AddListener(() => { SetGroupPrivate(infoPanelSetting.nameGroup, infoPanelSetting.idGroup);});
            }
            transferOwnership_Gameobject.SetActive(infoPanelSetting.roleuser == RoleUser.Owner);
        //Fill members
            foreach (Transform t in contentMembers_owner.transform) { GameObject.Destroy(t.gameObject); }
            foreach (Transform t in contentMembers_admin.transform) { GameObject.Destroy(t.gameObject); }
            foreach (Transform t in contentMembers_user.transform) { GameObject.Destroy(t.gameObject); }
            foreach (MembersGroup g in infoPanelSetting.members)
            {
                AddMemberToContent_Admin(g.principalID, g.username, g.avatarUser, g.roleuser, infoPanelSetting.idGroup);
            }   
            LayoutRebuilder.ForceRebuildLayoutImmediate(contentMembers_owner.GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(contentMembers_admin.GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(contentMembers_user.GetComponent<RectTransform>());
        //Fill request
            foreach (Transform t in contentRequests.transform) { GameObject.Destroy(t.gameObject); }
            foreach (RequestsGroup g in infoPanelSetting.requests)
            {
                AddRequestToList(g.principalID, g.username, g.timeStamp, infoPanelSetting.idGroup);
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(contentRequests.GetComponent<RectTransform>());
        }
        numberUsers.text = infoPanelSetting.members.Count.ToString();
        numberInvites.text = infoPanelSetting.requests.Count.ToString();
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(panelAdmin.GetComponent<RectTransform>()); //Update UI
        LayoutRebuilder.ForceRebuildLayoutImmediate(panelUser.GetComponent<RectTransform>());
    }
    public void AddMemberToContent_User(string principalID, string username, string iconSprite, RoleUser roleUser){
        
        GameObject newMember = Instantiate(prefabMember_User, contentMembersPanelUser.transform);
        MemberPrefabToUser memberPrefab = newMember.GetComponent<MemberPrefabToUser>();
        
        //memberPrefab.icon.sprite = iconSprite;
        memberPrefab.userNameText.text = username;

        switch (roleUser)
        {
            case RoleUser.User:
                memberPrefab.userRoleText.text = "User";
                memberPrefab.userRoleDot.color = new Color(1f, 1f, 1f, 1f);
                break;
            case RoleUser.Admin:
                memberPrefab.userRoleText.text = "Admin";
                memberPrefab.userRoleDot.color = new Color(0f, 0.7607843f, 1f, 1f);
                break;
            case RoleUser.Owner:
                memberPrefab.userRoleText.text = "Owner";
                memberPrefab.userRoleDot.color = new Color(1f, 0.4713461f, 0f, 1f);
                break;
        }
        memberPrefab.buttonToUser.onClick.AddListener(() => { CallGoToUser(principalID);});
        
    }
    public void AddMemberToContent_Admin(string principalID, string username, string iconSprite, RoleUser roleUser, int idGroup){
        
        GameObject newMember = Instantiate(prefabMember, contentMembers_user.transform);
        MemberPrefabToAdmin memberPrefab = newMember.GetComponent<MemberPrefabToAdmin>();

        //memberPrefab.icon.sprite = iconSprite;
        memberPrefab.userNameText.text = username;
        
        switch (roleUser)
        {
            case RoleUser.User:
                memberPrefab.userRoleDot.color = new Color(1f, 1f, 1f, 1f);
                memberPrefab.buttonText1.text = "Make Admin";
                memberPrefab.button1.onClick.AddListener(() => { MakeAdmin(principalID, idGroup, username);} );
                memberPrefab.buttonText2.text = "Kick";
                memberPrefab.button2.onClick.AddListener(() => { KickUser(principalID, idGroup, username);} );
                newMember.transform.SetParent(contentMembers_user.transform);
                break;
            case RoleUser.Admin:
                memberPrefab.userRoleDot.color = new Color(0f, 0.7607843f, 1f, 1f);
                memberPrefab.buttonText1.text = "Remove Admin";
                memberPrefab.button1.onClick.AddListener(() => { RemoveAdmin(principalID, idGroup, username);} );
                memberPrefab.buttonText2.text = "Kick";
                memberPrefab.button2.onClick.AddListener(() => { KickUser(principalID, idGroup, username);} );
                newMember.transform.SetParent(contentMembers_admin.transform);
                break;
            case RoleUser.Owner:
                memberPrefab.userRoleDot.color = new Color(1f, 0.4713461f, 0f, 1f);
                memberPrefab.button1.gameObject.SetActive(false);
                memberPrefab.button2.gameObject.SetActive(false);
                newMember.transform.SetParent(contentMembers_owner.transform);
                break;
        }
        //Modificar esto luego
        memberPrefab.buttonToUser.onClick.AddListener(() => { CallGoToUser(principalID);});
    }
    public void AddRequestToList(string principalID, string username, string timestamp, int idGroup){
        GameObject newRequest = Instantiate(prefabRequest, contentRequests.transform);
        RequestPrefab requestPrefab = newRequest.GetComponent<RequestPrefab>();

        requestPrefab.userNameText.text = username;
        requestPrefab.timestampText.text = timestamp;
        
        requestPrefab.buttonAccept.onClick.AddListener(() =>
        {
            CanvasPopup.Instance.OpenPopup(() =>
            {
                CanvasPopup.Instance.OpenLoadingPanel();
                string json = "{\"userPrincipalID\":\"" + principalID + "\", \"idGroup\": " + idGroup + "}" ;
                JSAcceptRequest(json);
            }, null, "Accept", "Cancel", "Do you want accept this User?", username, principalID);
        });
        requestPrefab.buttonDeny.onClick.AddListener(() =>
        {
            CanvasPopup.Instance.OpenPopup(() =>
            {
                CanvasPopup.Instance.OpenLoadingPanel();
                string json = "{\"userPrincipalID\":\"" + principalID + "\", \"idGroup\": " + idGroup + "}";
                JSDenyRequest(json);
            }, null, "Deny", "Cancel", "Do you want deny this User?", username, principalID);
        });
    }
    private void KickUser(string principalID, int idGroup, string username)
    {
        CanvasPopup.Instance.OpenPopup(() => {
            CanvasPopup.Instance.OpenLoadingPanel();
            string json = "{\"userPrincipalID\":\"" + principalID + "\", \"idGroup\": " + idGroup + "}";
            JSKickUser(json);
        }, null, "Kick", "Cancel", "Do you want kick this User?", username, principalID);
    }
    private void MakeAdmin(string principalID, int idGroup, string username)
    {
        CanvasPopup.Instance.OpenPopup(() => {
            CanvasPopup.Instance.OpenLoadingPanel();
            string json = "{\"userPrincipalID\":\"" + principalID + "\", \"idGroup\": " + idGroup + "}";
            JSMakeAdmin(json);
        }, null, "Make admin", "Cancel", "Do you want make admin this User?", username, principalID);
    }
    private void RemoveAdmin(string principalID, int idGroup, string username)
    {
        CanvasPopup.Instance.OpenPopup(() => {
            CanvasPopup.Instance.OpenLoadingPanel();
            string json = "{\"userPrincipalID\":\"" + principalID + "\", \"idGroup\": " + idGroup + "}";
            JSRemoveAdmin(json);
        },null, "Remove admin", "Cancel", "Do you want remove admin this User?", username, principalID);
    }
    private void SetGroupPrivate(string nameGroup, int idGroup)
    {
        buttonSliderStates.Play("ToRight");
        CanvasPopup.Instance.OpenPopup(() => {
            CanvasPopup.Instance.OpenLoadingPanel();
            JSSetGroupPrivate(idGroup);
        }, () =>
        {
            buttonSliderStates.Play("ToLeft");
            CanvasPopup.Instance.ClosePopupFromConfirm();
        }, "Set Private", "Cancel", "Do you want set private this group?", nameGroup, null);
    }
    private void SetGroupPublic(string nameGroup, int idGroup)
    {
        buttonSliderStates.Play("ToLeft");
        CanvasPopup.Instance.OpenPopup(() => {
            CanvasPopup.Instance.OpenLoadingPanel();
            JSSetGroupPublic(idGroup);
        }, () =>
        {
            buttonSliderStates.Play("ToRight");
            CanvasPopup.Instance.ClosePopupFromConfirm();
        }, "Set Public", "Cancel", "Do you want set public this group?", nameGroup, null);
    }
    
    public void EditTitle()
    {
        editTitleGroup.gameObject.SetActive(false);
        editConfirmTitleGroup.gameObject.SetActive(true);
        editCancelTitleGroup.gameObject.SetActive(true);
        tittleGroup.interactable = true;
        tittleGroup.Select();
    }
    public void ConfirmEditTitle()
    {
        tittleGroup.interactable = false;
        CanvasPopup.Instance.OpenPopup(() => {
            editTitleGroup.gameObject.SetActive(true);
            editConfirmTitleGroup.gameObject.SetActive(false);
            editCancelTitleGroup.gameObject.SetActive(false);
            CanvasPopup.Instance.OpenLoadingPanel();
            string json = "{\"text\":\"" + tittleGroup.text + "\", \"idGroup\": " + infoPanelSetting.idGroup + "}";
            JSChangeTitle(json);
        }, () =>
        {
            tittleGroup.interactable = true;
            CanvasPopup.Instance.ClosePopupFromConfirm();
        },"Change Title","Cancel","Do you want change title of this group?", infoPanelSetting.nameGroup, null);
    }
    public void CancelEditTitle()
    {
        tittleGroup.interactable = false;
        tittleGroup.text = originalTitle;
        editTitleGroup.gameObject.SetActive(true);
        editConfirmTitleGroup.gameObject.SetActive(false);
        editCancelTitleGroup.gameObject.SetActive(false);
    }
    public void EditDescription()
    {
        editDescriptionGroup.gameObject.SetActive(false);
        editConfirmDescriptionGroup.gameObject.SetActive(true);
        editCancelDescriptionGroup.gameObject.SetActive(true);
        descriptionGroup.interactable = true;
        descriptionGroup.Select();
    }
    public void ConfirmEditDescription()
    {
        descriptionGroup.interactable = false;
        CanvasPopup.Instance.OpenPopup(() => {
            editDescriptionGroup.gameObject.SetActive(true);
            editConfirmDescriptionGroup.gameObject.SetActive(false);
            editCancelDescriptionGroup.gameObject.SetActive(false);
            CanvasPopup.Instance.OpenLoadingPanel();
            string json = "{\"text\":\"" + descriptionGroup.text + "\", \"idGroup\": " + infoPanelSetting.idGroup + "}";
            JSChangeDescription(json);
        }, () =>
        {
            descriptionGroup.interactable = true;
            CanvasPopup.Instance.ClosePopupFromConfirm();
        },"Change Description","Cancel","Do you want change description of this group?", infoPanelSetting.nameGroup, null);
    }
    public void CancelEditDescription()
    {
        descriptionGroup.interactable = false;
        descriptionGroup.text = originalDescription;
        editDescriptionGroup.gameObject.SetActive(true);
        editConfirmDescriptionGroup.gameObject.SetActive(false);
        editCancelDescriptionGroup.gameObject.SetActive(false);
    }
    private void CallGoToUser(string principalID)
    {
        CanvasPopup.Instance.OpenPopup(() => {
            CanvasPopup.Instance.OpenLoadingPanel();
            /*string json = "{\"userPrincipalID\":\"" + principalID + "\", \"idGroup\": " + idGroup + "}";
            JSRemoveAdmin(json);*/
        }, null, null, null, null, null, null);
    }
 
    [System.Serializable]
    public class MembersGroup {
        public RoleUser roleuser;
        public string username;
        public string principalID;
        public string avatarUser;
    }
    [System.Serializable]
    public class RequestsGroup {
        public string username;
        public string principalID;
        public string avatarUser;
        public string timeStamp;
    }
    [System.Serializable]
    public class InfoPanelSetting { 
        public RoleUser roleuser;
        public int idGroup;
        public string nameGroup;
        public string avatarGroup;
        public string descriptionGroup;
        public bool isPrivate;
        public List<MembersGroup> members;
        public List<RequestsGroup> requests;
    }
    
    
}
