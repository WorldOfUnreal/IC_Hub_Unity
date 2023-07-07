using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CanvasSendCrypto : MonoBehaviour
{
        public static CanvasSendCrypto Instance { get; private set; }
        public class TransactionData
        {
            public string idToken;
            public string quantityToken;
            public string web3Adress;
            public bool isToken;
        }
        private void Awake() 
        {
            if (Instance != null && Instance != this) { Destroy(this); } 
            else { Instance = this;} 
        }
        [DllImport("__Internal")]
        private static extern void JSSendCrypto(string json);
        
        public GameObject panelParent;
        
        [Header("Info´s Panel : ")] 
        public ImageDownloadManager iconNft;
        public TMP_Text title;
        public TMP_InputField web3Address;
        public TMP_InputField quantityField;
        [Header("Buttons Panel : ")] 
        public Button buttonCancel;
        public Button buttonSend;
    
        public void ClosePopupSendCrypto()
        {
            panelParent.SetActive(false);
        }

        public void OpenPopupSendCrypto(string ID, bool isToken, string urlImage)
        {
            iconNft.ChangeUrlImage(urlImage);
            title.text = "Do you want to transfer this NFT?";
            if (isToken) { title.text = "Do you want to transfer this Token?";}
            quantityField.gameObject.SetActive(isToken);
            
            buttonCancel.onClick.RemoveAllListeners();
            buttonCancel.onClick.AddListener(ClosePopupSendCrypto);
            
            buttonSend.onClick.RemoveAllListeners();
            buttonSend.onClick.AddListener(() => SendCrypto(ID, isToken, urlImage) );
            
            panelParent.SetActive(true);
        }

        public void SendCrypto( string ID, bool isToken, string urlImage)
        {
            TransactionData transactionData = new TransactionData();
            transactionData.idToken = ID;
            transactionData.quantityToken = quantityField.text;
            transactionData.web3Adress = web3Address.text;
            transactionData.isToken = isToken;
            
            CanvasPopup.Instance.OpenPopup(() => {
                    CanvasPopup.Instance.OpenLoadingPanel();
                    JSSendCrypto( JsonUtility.ToJson(transactionData) );
                }, null, "Send", "Cancel", "Do you want send to this User?", 
                transactionData.idToken, web3Address.text, urlImage);
        }


       


        




}
