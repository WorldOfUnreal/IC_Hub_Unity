using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Text;
using System;
using System.Runtime.InteropServices;
using TMPro;

//For the InputField to work in WebGL go to Project Settings
// then Go in "Player" Show HTML5/WebGL settings
// Set the Active Input Handling to "Both" instead of "Input System Package (New)"
// if you don't have an EventSystem, create it by right click on Inspector->UI->EventSystem

public class ChatManager : MonoBehaviour
{
    public string username;

    public int maxMessages = 240;
    private int lastMessage = -1;
    public int idGroupSelected = 0;
    [SerializeField] float itemSpacing = .5f;

    private string pasteTxt = "";

    public GameObject chatPanel, textObject;
    public TMP_InputField chatBox;
    //set the colors in the inspector
    public Color playerMessage, info;
    public TMP_Text groupName;
    public TMP_Text groupAvatar;
    public ImageDowloadManager groupAvatarImage;
    
    [Header("Side Panel : ")]
    public GameObject sidePanel;
    public GameObject groupObject;
    
    [SerializeField] private Button addButton;
    public GameObject popupPanel;
    [SerializeField] private Button newGroupButton;
    public TMP_InputField newGroupNameInput;
    public TMP_InputField newGroupDescriptionInput;
    public Button buttonSlider;
    public Animator buttonSliderAnimator;
    public bool isPrivate = false;
    [SerializeField] private Button closePopupButton;

    public GameObject popupMoreSettings;
    
    private bool openAddPopup = false;

    [SerializeField]
    List<Message> messageList = new List<Message>();
    //This doesn't need to be public, no need to have access outside the script

    [SerializeField]
    List<Group> groupsList = new List<Group>();

    /// WebGL
    [DllImport("__Internal")]
    private static extern void JSSendMessage(string text);
    [DllImport("__Internal")]
    private static extern void JSAddUserToGroup(string json);
    [DllImport("__Internal")]
    private static extern void JSCreateGroup(string json);
    [DllImport("__Internal")]
    private static extern void JSSelectChatGroup(int id);
    [DllImport("__Internal")]
    private static extern void JSLeaveGroup(int id);
    
    void Start()
    {
        username = "";
        popupPanel.SetActive(false);
        
        addButton.onClick.AddListener(() => { ToggleAddPopup(); });
        buttonSlider.onClick.AddListener(() => { TooglePrivate(); });
        newGroupButton.onClick.AddListener(() => { CreateGroup(); });
        /// Popup -> Return
        closePopupButton.onClick.AddListener(() => { ToggleAddPopup(); });
        
    }

    void Update()
    {
        if(openAddPopup == false){
            if(chatBox.text != ""){ 
                //Press Enter to send the message to the chatBox
                if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
                    //SendMessageToChat(username + ": " + chatBox.text, Message.MessageType.playerMessage);
                    JSSendMessage(chatBox.text);
                    //This is going to reset the chatBox to empty
                    chatBox.text = "";
                    /// Close the New Popup
                    popupPanel.SetActive(false);
                    openAddPopup = false;
                }
            } else {
                //if the chatBox is !not focused, it can 
                if(!chatBox.isFocused && (Input.GetKeyDown("t") || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))){
                    chatBox.ActivateInputField();
                    //still needs code to deactivate with another enter
                }
            }
            if(chatBox.isFocused && Input.GetKeyDown(KeyCode.Escape)){
                //chatBox.ActivateInputField();
                //still needs code to deactivate with another enter
            }
        }

        if(pasteTxt != ""){
            if(chatBox.isFocused == true){
                /// Chat input
                chatBox.text = pasteTxt;
                pasteTxt = "";
            } else {
                if(newGroupNameInput.isFocused == true){
                    /// Create group
                    newGroupNameInput.text = pasteTxt;
                    pasteTxt = "";
                } else {
                    if(newGroupDescriptionInput.isFocused == true){
                        /// Add user to group
                        newGroupDescriptionInput.text = pasteTxt;
                        pasteTxt = "";
                    } else {
                        /*if(newUserInput.isFocused == true){
                            /// Create user
                            newUserInput.text = pasteTxt;
                            pasteTxt = "";
                        }*/
                    }
                }
            }
        }
    }
    
    public void SendMessageToBlockchain(string text){
        if(text != ""){
            JSSendMessage(text);
        }
    }

    public void ClearMessages(){
        for(int i = 0; i < messageList.Count; i++) {
            Destroy(messageList[i].gameObject);
        }
        messageList.Clear();
        lastMessage = -1;
    }

    public void GetChatMessages(string json)
    {
        ClearMessages();
        
        MessagesTexts messagesTexts = JsonUtility.FromJson<MessagesTexts>(json);
        groupName.text = messagesTexts.nameGroup;
        groupAvatar.text = messagesTexts.nameGroup.Substring(0,2);
        groupAvatarImage.ChangeUrlImage(messagesTexts.avatarGroup);
        foreach(MessageText m in messagesTexts.data){
            if(lastMessage < m.id){
                SendMessageToChat(m,  Message.MessageType.playerMessage);
                lastMessage = m.id;
            }
        }
        //Update UI
        LayoutRebuilder.ForceRebuildLayoutImmediate(chatPanel.GetComponent<RectTransform>());
    }

    public void SendMessageToChat(MessageText m, Message.MessageType messageType) {
        //This will clear the last Message after the maximum allowed
        if(messageList.Count >= maxMessages) {
            //This is to destroy only the text, but not the Object
            Destroy(messageList[0].gameObject);
            messageList.Remove(messageList[0]);
        }
        
        //Create a new game object to instantiate the text Prefab for new Messages
        GameObject newText = Instantiate(textObject, chatPanel.transform);
        Message newMessage = newText.GetComponent<Message>();
        newMessage.text = m.text;
        newMessage.contentMessage.text = newMessage.text;
        newMessage.contentMessage.color = MessageTypeColor(messageType);
        newMessage.nameUser.text = m.username;
        newMessage.avatarUser.ChangeUrlImage(m.avatarUser);
        newMessage.button.onClick.AddListener((() =>
        {
            CanvasPlayerProfile.Instance.OpenPopupPlayerProfile(m.principalID, m.username);
        }));
        
        messageList.Add(newMessage);
    }

    public void GetGroups(string json){
        groupsList.Clear();
        foreach (Transform t in sidePanel.transform) { GameObject.Destroy(t.gameObject); }
        
        GroupsList _groupsList = JsonUtility.FromJson<GroupsList>(json);
        
        foreach(GroupData g in _groupsList.data){
            AddGroupToList(g.id, g.name, g.avatar);
            
        }
    }
    
    public void AddGroupToList(int id, string name, string avatar){
        Group g = new Group();
        g.id    = id;
        g.name  = name;
        GameObject newGroup = Instantiate(groupObject, sidePanel.transform);
        GroupPrefab groupPrefab = newGroup.GetComponent<GroupPrefab>();
        groupPrefab.buttonGroup.onClick.AddListener(() => { SetGroupSelected(id); });
        groupPrefab.groupName.text = name;
        groupPrefab.iconGroup.ChangeUrlImage(avatar);
        groupsList.Add(g);
    }

    public void SetGroupSelected(int id){
        idGroupSelected = id;
        JSSelectChatGroup(id);
    }
    public void LeaveGroup(){
        popupMoreSettings.SetActive(false);
        JSLeaveGroup(idGroupSelected);
    }

    public void ToggleAddPopup(){
        if(openAddPopup == false){
            openAddPopup = true;
            popupPanel.SetActive(true);
        } else {
            openAddPopup = false;
            popupPanel.SetActive(false);
        }
    }

    

    /*
    public void AddUserToGroupButton(){
        /// Add new user to currently selected group
        if(addUserToGroupInput.text != ""){
            string jsn = "{\"userId\":\"" + addUserToGroupInput.text + "\", \"groupId\": " + idGroupSelected + "}" ;
            Debug.Log("Add user");
            Debug.Log(jsn);
            JSAddUserToGroup(jsn);
        }
    }
    */

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
            }, null, "Create", "Cancel", "Do you want create this Group?", newGroupNameInput.text, null);
        }
    }

    public void getPaste(string s){
        pasteTxt = s;
    }

    public void UserAdded(bool result){
        if(result == true){
            ToggleAddPopup();
        } else {
            ToggleAddPopup();
        }
    }

    Color MessageTypeColor(Message.MessageType messageType)
    {
        Color color = info;
        //check on different cases
        switch(messageType)
        {
            //you can have many different cases instead of "playerMessage"
            case Message.MessageType.playerMessage:
            color = playerMessage;
            break;
            //its an if statement where you 
            //check against one variable that can be set
            //to be many different things 
            //this can also be used for ints and floats
        }

        return color;
    }
}

[System.Serializable]
public class MessageText {
    public int id;
    public string text;
    public string username;
    public string timeStamp;
    public string avatarUser;
    public string principalID;
}
[System.Serializable]
public class MessagesTexts {
    public List<MessageText> data;
    public int idGroup;
    public string nameGroup;
    public string avatarGroup;
}

[System.Serializable]
public class Group{
    public int id;
    public string name;
    public Button groupObject;
}
[System.Serializable]
public class GroupData{
    public int id;
    public string name;
    public string avatar;
    public RoleUser roleuser;
}
public enum RoleUser { Owner, Admin, User}

[System.Serializable]
public class GroupsList{
    public List<GroupData> data;
}

//For more information on how this script was done check out
// https://www.youtube.com/watch?v=IRAeJgGkjHk
// Thanks to Soupertrooper for this great tutorial