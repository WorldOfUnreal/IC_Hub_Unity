using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionAppPrefab : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void JSDeleteCollection(string canisterid);
    [DllImport("__Internal")]
    private static extern void JSSetAvatarImageToCollection(string nameObject);

    private static int IDUploadAvatar = 0;
    
    public TMP_InputField collectionNameInput;
    public TMP_InputField canisterIDInput;
    public TMP_Dropdown nftStandardDropdown;
    public TMP_InputField linkToMarketplaceInput;
    public ImageDownloadManager avatarCollection;

    public Button removeCollectionBtn;

    public void FillCollectionAppData(AppManagementController.CollectionAppData collectionAppData)
    {
        collectionNameInput.text = collectionAppData.collectionName;
        canisterIDInput.text = collectionAppData.canisterID;
        nftStandardDropdown.value = collectionAppData.nftStandard;
        linkToMarketplaceInput.text = collectionAppData.linkToMarketplace;
        avatarCollection.ChangeUrlImage(collectionAppData.avatarUrl);

        removeCollectionBtn.onClick.RemoveAllListeners();
        removeCollectionBtn.onClick.AddListener(() =>
        {
            CanvasPopup.Instance.OpenPopup(() =>
            {
                CanvasPopup.Instance.OpenLoadingPanel();
                JSDeleteCollection(collectionAppData.canisterID);
            },null, "Delete", "Cancel", "Do you want delete this collection?", collectionAppData.collectionName, null, collectionAppData.avatarUrl);
        });

    }
    
    public void SetAvatarImageToCollection()
    {
        this.gameObject.name = "Collection_Prefab_" + IDUploadAvatar;
        IDUploadAvatar++;
        JSSetAvatarImageToCollection(this.gameObject.name);
    }
    public void OnAvatarUploadLoading()
    {
        CanvasPopup.Instance.OpenPopupInLoading();
    }
    public void OnAvatarUploadReady(string url)
    {
        avatarCollection.ChangeUrlImage(url);
        CanvasPopup.Instance.OpenSuccessPanel();
        
    }
    
    
}
