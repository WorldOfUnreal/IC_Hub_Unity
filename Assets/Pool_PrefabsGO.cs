using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool_PrefabsGO : MonoBehaviour
{
    
    [System.Serializable]
    public class PoolInfo {
        public GameObject prefab;
        public Transform content;
        public int initialPoolSize = 10;
        [HideInInspector] public List<GameObject> availableObjects = new List<GameObject>();
        [HideInInspector] public List<GameObject> inUseObjects = new List<GameObject>();
    }
    
    [SerializeField] public PoolInfo poolHubToken;
    [SerializeField] public PoolInfo poolHubFriend;
    [SerializeField] public PoolInfo poolHubGroup;
    [SerializeField] public PoolInfo poolHubCollection;
    [SerializeField] public PoolInfo poolNotificationRequests;
    [SerializeField] public PoolInfo poolMessagesChat;
    public static Pool_PrefabsGO Instance { get; private set; }
    private void Awake() { if (Instance != null && Instance != this) { Destroy(this); } else { Instance = this;} }
    private void Start()
    {
        InitializePool(poolHubToken); InitializePool(poolHubFriend);
        InitializePool(poolHubGroup); InitializePool(poolHubCollection);
        InitializePool(poolNotificationRequests); InitializePool(poolMessagesChat); 
    }
    private void InitializePool(PoolInfo poolInfo)
    {
        for (int i = 0; i < poolInfo.initialPoolSize; i++)
        {
            GameObject newObj = Create_NewObj(poolInfo);
            poolInfo.availableObjects.Add(newObj);
        }
    }
    private GameObject Create_NewObj(PoolInfo poolInfo)
    {
        GameObject newObj = Instantiate(poolInfo.prefab, poolInfo.content);
        newObj.SetActive(false);
        return newObj;
    }
    public GameObject Get_ObjFromPool(PoolInfo poolInfo)
    {
        GameObject obj = null;

        if (poolInfo.availableObjects.Count > 0)
        {
            obj = poolInfo.availableObjects[0];
            poolInfo.availableObjects.RemoveAt(0);
        }
        else { obj = Create_NewObj(poolInfo); }

        obj.gameObject.SetActive(true);
        obj.transform.SetAsLastSibling();
        poolInfo.inUseObjects.Add(obj);
        return obj;
    }
    public void Release_AllObjsInPool(PoolInfo poolInfo)
    {
        foreach (GameObject obj in poolInfo.inUseObjects)
        {
            obj.SetActive(false);
            poolInfo.availableObjects.Add(obj);
        }
        poolInfo.inUseObjects.Clear();
    }
  
}
