using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchUserManager : MonoBehaviour
{
    public static SearchUserManager Instance { get; private set; }
    private void Awake() 
    {
        if (Instance != null && Instance != this) { Destroy(this); } 
        else { Instance = this;} 
    }
    
     [System.Serializable]
    public class SearchUserInfo
    {
        public bool user_exists;
        public string StringSearch;
        public List<User_data> user_data;
    }
    [System.Serializable]
    public class User_data
    {
        public string userName;
        public string principalID;
        public string avatarUser;
    }
    
    public GameObject panelParent;
    public TMP_InputField searchUserInput;
   
    public GameObject panelSliderUser;
    public GameObject panelNotFound;
    public Animator searching_Anim;
    public GameObject prefabUserItem;
    
    [DllImport("__Internal")]
    private static extern void JSSearchUser(string text);
    
    public void ClosePopup() { panelParent.SetActive(false); }
    public void OpenPopup() { panelParent.SetActive(true); }
    
    public void GetUser(string json){
        
        SearchUserInfo searchUserInfo = JsonUtility.FromJson<SearchUserInfo>(json);

        if (searchUserInput.text == searchUserInfo.StringSearch)
        {
            searching_Anim.Play("Searching_Stop");
            foreach (Transform t in panelSliderUser.transform) { GameObject.Destroy(t.gameObject); }
            
            if (!searchUserInfo.user_exists)
            {
                panelNotFound.SetActive(true);
                panelSliderUser.SetActive(false);
            }
            else
            {
                panelNotFound.SetActive(false);
                panelSliderUser.SetActive(true);
                foreach(User_data g in searchUserInfo.user_data){
                    AddUserToList(g.principalID, g.userName, g.avatarUser);
                }
            }
        }
    }
    
    public void AddUserToList(string principalID, string userName, string avatar){
       
        GameObject newUser = Instantiate(prefabUserItem, panelSliderUser.transform);
        SearchUserPrefab userPrefab = newUser.GetComponent<SearchUserPrefab>();

        userPrefab.userName.text = userName;
        userPrefab.icon.ChangeUrlImage(avatar);
        userPrefab.button.onClick.AddListener(() =>
        {
            CanvasPlayerProfile.Instance.OpenPopupPlayerProfile(principalID, userName);
            ClosePopup();
        });
    }
    public void SearchUsers(){
        if(!string.IsNullOrEmpty(searchUserInput.text))
        { 
            searching_Anim.Play("Searching");
            JSSearchUser(searchUserInput.text);
        }
        else
        {
            panelSliderUser.SetActive(false);
            panelNotFound.SetActive(true);
            searching_Anim.Play("Searching_Stop");
        }
    }
    
}
