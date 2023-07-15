using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionAppPrefab : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void JSDeleteCollection(int id);
    
    public TMP_InputField collectionNameInput;
    public TMP_InputField canisterIDInput;
    public TMP_InputField nftStandardInput;
    public TMP_InputField linkToMarketplaceInput;
    public ImageDownloadManager avatarCollection;
    public int collectionID;

    public Button removeCollectionBtn;

    public void FillCollectionAppData(AppManagementController.CollectionAppData collectionAppData)
    {
        collectionNameInput.text = collectionAppData.collectionName;
        canisterIDInput.text = collectionAppData.canisterID;
        nftStandardInput.text = collectionAppData.nftStandard;
        linkToMarketplaceInput.text = collectionAppData.linkToMarketplace;
        avatarCollection.ChangeUrlImage(collectionAppData.avatarUrl);
        collectionID = collectionAppData.collectionID;

        removeCollectionBtn.onClick.RemoveAllListeners();
        removeCollectionBtn.onClick.AddListener(() =>
        {
            CanvasPopup.Instance.OpenPopup(() =>
            {
                CanvasPopup.Instance.OpenLoadingPanel();
                JSDeleteCollection(collectionAppData.collectionID);
            },null, "Delete", "Cancel", "Do you want delete this collection?", collectionAppData.collectionName, null, collectionAppData.avatarUrl);
        });

    }
}
