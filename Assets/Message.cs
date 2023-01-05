using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Message: MonoBehaviour
{
    public string text;
    public Image avatarUser;
    public TMP_Text nameUser;
    public TMP_Text timestamp;
    public TMP_Text contentMessage;
    public MessageType messageType;

    public enum MessageType
    {
        playerMessage,
        info
    }
    
}
