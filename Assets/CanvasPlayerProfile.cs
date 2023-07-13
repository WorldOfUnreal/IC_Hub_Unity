using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CanvasPlayerProfile : MonoBehaviour
{
        public static CanvasPlayerProfile Instance { get; private set; }
        
        private void Awake() 
        {
            if (Instance != null && Instance != this) { Destroy(this); } 
            else { Instance = this;} 
        }
        
        public Animator panelPlayerProfileAnimator;
        public Animator iconSearchAnimator;
        public GameObject panelParent;
        public GameObject panelLoading;
        public GameObject panelMiddle;
        [Header("Info´s Panel : ")] 
        public ImageDownloadManager userIcon;
        public TMP_Text usernameTMP;
        public TMP_InputField descriptionPopup;
        public Button editDescriptionUser;
        public Button editConfirmDescriptionUser;
        public Button editCancelDescriptionUser;
        public TMP_Text principalID;
        public string principalIDComplete;
        public TMP_Text memberSinceTMP;
        [Header("Buttons Panel : ")]
        public GameObject buttonUploadAvatar;
        public GameObject parentButtons;
        public Button button1;
        public TMP_Text buttonText1;
        public Button button2;
        public TMP_Text buttonText2;
        [Header("Buttons Logout : ")] 
        public GameObject logoutButton;
        
        private InfoPopupPlayer infoPopupPlayer;
        private string originalDescription;
        
        [DllImport("__Internal")]
        private static extern void JSCallToUser(string principalID);
        [DllImport("__Internal")]
        private static extern void JSSendFriendRequest(string principalID);
        [DllImport("__Internal")]
        private static extern void JSSendMessageToUser(string principalID);
        [DllImport("__Internal")]
        private static extern void JSLogoutFromProfile();
        [DllImport("__Internal")]
        private static extern void JSSetAvatarImageFromProfile();
        [DllImport("__Internal")]
        private static extern void JSChangeDescriptionUser(string text);
        [DllImport("__Internal")]
        public static extern void JSCopyToClipboard(string accountName);
        
       
        public void ClosePopupPlayerProfile()
        {
            panelParent.SetActive(false);
        }
        public void OpenPopupPlayerProfile(string principalID, string username)
        {
            usernameTMP.text = username;
            userIcon.rawImages[0].texture = Resources.Load<Texture>( "Images/Loading" );
            panelParent.SetActive(true);
            panelLoading.SetActive(true);
            panelMiddle.SetActive(false);
            StartSearchIconAnim();
            JSCallToUser(principalID);
        }
        
        public void GetInfoPopupPlayer(string json)
        {
            panelLoading.SetActive(false);
            panelMiddle.SetActive(true);
            StopSearchIconAnim();
            
            infoPopupPlayer = JsonUtility.FromJson<InfoPopupPlayer>(json);

            usernameTMP.text = infoPopupPlayer.username;
            userIcon.ChangeUrlImage(infoPopupPlayer.avatar);
            descriptionPopup.text = infoPopupPlayer.description;
            originalDescription = infoPopupPlayer.description;
            principalID.text =  infoPopupPlayer.principalID.Substring(0, 4)+"..."+infoPopupPlayer.principalID.Substring(infoPopupPlayer.principalID.Length - 4);
            principalIDComplete = infoPopupPlayer.principalID;
            memberSinceTMP.text = infoPopupPlayer.memberSince;
            
            editDescriptionUser.gameObject.SetActive(false);
            editConfirmDescriptionUser.gameObject.SetActive(false);
            editCancelDescriptionUser.gameObject.SetActive(false);
            logoutButton.SetActive(false);
            parentButtons.SetActive(true);
            buttonUploadAvatar.SetActive(false);
            button1.onClick.RemoveAllListeners();
            button1.gameObject.SetActive(true);
            button1.interactable = true;
            button2.onClick.RemoveAllListeners();
            button2.gameObject.SetActive(true);
            
            switch (infoPopupPlayer.rolePlayerProfile)
            {
                case RolePlayerProfile.Owner:
                    logoutButton.SetActive(true);
                    buttonUploadAvatar.SetActive(true);
                    parentButtons.SetActive(false);
                    CancelEditDescription();
                    break;
                case RolePlayerProfile.Nofriend:
                    buttonText1.text = "Friend Request";
                    button1.onClick.AddListener(() => { SendFriendRequest(infoPopupPlayer); });
                    buttonText2.text = "Message";
                    button2.onClick.AddListener(() => { SendMessageToUser(infoPopupPlayer.principalID); });
                    break;
                case RolePlayerProfile.Requested:
                    buttonText1.text = "Requested";
                    button1.interactable = false;
                    buttonText2.text = "Message";
                    button2.onClick.AddListener(() => { SendMessageToUser(infoPopupPlayer.principalID); });
                    break;
                case RolePlayerProfile.Friend:
                    buttonText1.text = "Friends";
                    button1.interactable = false;
                    buttonText2.text = "Message";
                    button2.onClick.AddListener(() => { SendMessageToUser(infoPopupPlayer.principalID); });
                    break;
            }
            
        }

    
        public void SendFriendRequest(InfoPopupPlayer _infoPopupPlayer)
        {
            CanvasPopup.Instance.OpenPopup(() => {
                CanvasPopup.Instance.OpenLoadingPanel();
                JSSendFriendRequest(_infoPopupPlayer.principalID);
            }, null, "Send Request", "Cancel", "Do you want add this User?", 
                _infoPopupPlayer.username, _infoPopupPlayer.principalID, _infoPopupPlayer.avatar);
        }
        public void SendMessageToUser(string principalID)
        {
            JSSendMessageToUser(principalID);
            Hub_Manager.Instance.OpenSection(0);
            ChatManager.Instance.WaitFromGroupCreated();
            ClosePopupPlayerProfile();
        }
        public void LogoutFromProfile()
        {
            JSLogoutFromProfile();
        }
        public void SetAvatarImageFromProfile()
        {
            JSSetAvatarImageFromProfile();
        }
        public void OnAvatarUploadLoading()
        {
            CanvasPopup.Instance.OpenPopupInLoading();
        }
        public void OnAvatarUploadReady()
        {
            CanvasPopup.Instance.OpenSuccessPanel();
            OpenPopupPlayerProfile(this.principalIDComplete,this.usernameTMP.text );
        }
        
        public void EditDescription()
        {
            editDescriptionUser.gameObject.SetActive(false);
            editConfirmDescriptionUser.gameObject.SetActive(true);
            editCancelDescriptionUser.gameObject.SetActive(true);
            descriptionPopup.interactable = true;
            descriptionPopup.Select();
        }
        public void ConfirmEditDescription()
        {
            descriptionPopup.interactable = false;
            CanvasPopup.Instance.OpenPopup(() => {
                editDescriptionUser.gameObject.SetActive(true);
                editConfirmDescriptionUser.gameObject.SetActive(false);
                editCancelDescriptionUser.gameObject.SetActive(false);
                CanvasPopup.Instance.OpenLoadingPanel();
                JSChangeDescriptionUser(descriptionPopup.text);
            }, () =>
            {
                descriptionPopup.interactable = true;
                CanvasPopup.Instance.ClosePopupFromConfirm();
            },"Change Description","Cancel","Do you want change you description?", usernameTMP.text, null, userIcon.urlImage);
        }
        public void CancelEditDescription()
        {
            descriptionPopup.interactable = false;
            descriptionPopup.text = originalDescription;
            editDescriptionUser.gameObject.SetActive(true);
            editConfirmDescriptionUser.gameObject.SetActive(false);
            editCancelDescriptionUser.gameObject.SetActive(false);
        }
        
        public enum RolePlayerProfile { Owner, Nofriend, Requested, Friend };
        public void StopSearchIconAnim() { iconSearchAnimator.Play("Searching_Stop"); }
        public void StartSearchIconAnim() { iconSearchAnimator.Play("Searching");}
        
        public class InfoPopupPlayer { 
            public string username;
            public string principalID;
            public string avatar;
            public string description;
            public string memberSince;
            public RolePlayerProfile rolePlayerProfile;
        }
        
        public void CopyTextToClipBoard(TMP_Text tmpText)
        {
            JSCopyToClipboard(principalIDComplete);
        }

        
}
