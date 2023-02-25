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
        
        public TMP_Text usernameTMP;
        public TMP_InputField descriptionPopup;
        public TMP_Text principalID;
        public TMP_Text memberSinceTMP;
        
        private InfoPopupPlayer infoPopupPlayer;
        
        [DllImport("__Internal")]
        private static extern void JSCallToUser(string principalID);
        
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
            principalID.text = infoPopupPlayer.principalID;
            memberSinceTMP.text = infoPopupPlayer.memberSince;
        }

        public class InfoPopupPlayer { 
            public string username;
            public string principalID;
            public string avatar;
            public string description;
            public string memberSince;
            public RolePlayerProfile rolePlayerProfile;
        }
        
        public enum RolePlayerProfile { Owner, Nofriend, Requested, Friend }

       
    
        public void StopSearchIconAnim() { iconSearchAnimator.Play("Searching_Stop"); }
        public void StartSearchIconAnim() { iconSearchAnimator.Play("Searching");}
}
