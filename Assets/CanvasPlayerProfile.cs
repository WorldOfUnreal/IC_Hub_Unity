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
        public TMP_Text usernameTMP;
        public TMP_InputField descriptionPopup;
        public TMP_Text principalID;
        public TMP_Text memberSinceTMP;
        [Header("Buttons Panel : ")]
        public Button button1;
        public TMP_Text buttonText1;
        public Button button2;
        public TMP_Text buttonText2;
        
        private InfoPopupPlayer infoPopupPlayer;
        
        [DllImport("__Internal")]
        private static extern void JSCallToUser(string principalID);
        [DllImport("__Internal")]
        private static extern void JSSendFriendRequest(string principalID);
        [DllImport("__Internal")]
        private static extern void JSSendMessageToUser(string principalID);
        public void ClosePopupPlayerProfile()
        {
            panelParent.SetActive(false);
        }
        
        public void OpenPopupPlayerProfile(string principalID, string username)
        {
            usernameTMP.text = username;
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
            descriptionPopup.text = infoPopupPlayer.description;
            principalID.text =  infoPopupPlayer.principalID.Substring(0, 4)+"..."+infoPopupPlayer.principalID.Substring(infoPopupPlayer.principalID.Length - 4);
            memberSinceTMP.text = infoPopupPlayer.memberSince;
            
            button1.onClick.RemoveAllListeners();
            button1.gameObject.SetActive(true);
            button1.interactable = true;
            button2.onClick.RemoveAllListeners();
            button2.gameObject.SetActive(true);

            usernameTMP.text = usernameTMP.text + (int)infoPopupPlayer.rolePlayerProfile;
            
            switch (infoPopupPlayer.rolePlayerProfile)
            {
                case RolePlayerProfile.Owner:
                    button2.gameObject.SetActive(false);
                    buttonText1.text = "Edit Profile";
                    button1.onClick.AddListener(() => { EditProfile(); });
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

        public class InfoPopupPlayer { 
            public string username;
            public string principalID;
            public string avatar;
            public string description;
            public string memberSince;
            public RolePlayerProfile rolePlayerProfile;
        }

        public void EditProfile()
        {
            Debug.Log("SSSS");
        }
        public void SendFriendRequest(InfoPopupPlayer _infoPopupPlayer)
        {
            CanvasPopup.Instance.OpenPopup(() => {
                CanvasPopup.Instance.OpenLoadingPanel();
                JSSendFriendRequest(_infoPopupPlayer.principalID);
            }, null, "Send Request", "Cancel", "Do you want add this User?", 
                _infoPopupPlayer.username, _infoPopupPlayer.principalID);
        }
        public void SendMessageToUser(string principalID)
        {
            JSSendMessageToUser(principalID);
        }
        
        public enum RolePlayerProfile { Owner, Nofriend, Requested, Friend };
        public void StopSearchIconAnim() { iconSearchAnimator.Play("Searching_Stop"); }
        public void StartSearchIconAnim() { iconSearchAnimator.Play("Searching");}
}
