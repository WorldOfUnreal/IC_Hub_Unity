using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SearchGroupManager : MonoBehaviour
{
    public class SearchGroupInfo
    {
        public bool group_exists;
        public bool joined;
        public List<Group_data> group_data;
    }
    public class Group_data
    {
        public int id;
        public string name;
        public bool is_private;
    }
    public class GroupSearchObject{
        public int id;
        public string name;
        public Button groupObject;
    }
    
    public TMP_InputField searchGroupInput;
    public Button searchGroupButton;

    public GameObject panelSliderGroup;
    public GameObject panelNotFound;
    public GameObject panelLoading;


    public GameObject prefabGroupItem;
    
    [SerializeField]
    List<GroupSearchObject> groupsList = new List<GroupSearchObject>();
    
    
    [DllImport("__Internal")]
    private static extern void JSSearchGroup(string text);
    [DllImport("__Internal")]
    private static extern void JSJoinGroup(string text);
    [DllImport("__Internal")]
    private static extern void JSRequestJoinGroup(string text);
    
    public void GetGroups(string json){
        searchGroupButton.interactable = true;
        searchGroupInput.interactable = true;
        panelLoading.SetActive(false);
        
        groupsList.Clear();
        foreach (Transform t in panelSliderGroup.transform) { GameObject.Destroy(t.gameObject); }
        
        SearchGroupInfo _searchGroupInfo = JsonUtility.FromJson<SearchGroupInfo>(json);

        if (!_searchGroupInfo.group_exists)
        {
            panelNotFound.SetActive(true);
        }
        else
        {
            foreach(Group_data g in _searchGroupInfo.group_data){
                AddGroupToList(g.id, g.name, g.is_private);
            }
        }
        
    }
    public void AddGroupToList(int id, string name, bool isPrivate){
        GroupSearchObject g = new GroupSearchObject();
        g.id    = id;
        g.name  = name;
        GameObject newGroup = Instantiate(prefabGroupItem, panelSliderGroup.transform);
        SearchGroupPrefab groupPrefab = newGroup.GetComponent<SearchGroupPrefab>();

        groupPrefab.tittle.text = name;
        
        if (isPrivate)
        {
            groupPrefab.buttonText.text = "Request Join";
            groupPrefab.button.onClick.AddListener(() => { JoinRequestGroup(id); });
        }
        else
        {
            groupPrefab.buttonText.text = "Join";
            groupPrefab.button.onClick.AddListener(() => { JoinGroup(id); });
        }
        groupsList.Add(g);
    }
    
    public void SearchGroups(){
        if(!string.IsNullOrEmpty(searchGroupInput.text))
        {
            searchGroupButton.interactable = false;
            searchGroupInput.interactable = false;
            JSSearchGroup(searchGroupInput.text);
            
            panelNotFound.SetActive(false);
            panelSliderGroup.SetActive(false);
            panelLoading.SetActive(true);
        }
        else
        {
            panelNotFound.SetActive(true);
            panelLoading.SetActive(false);
            panelSliderGroup.SetActive(false);
        }
    }
    
    public void JoinRequestGroup(int id){
        JSRequestJoinGroup("Pruebas");
    }
    public void JoinGroup(int id){
        JSJoinGroup("Pruebas");
    }
    
    
    
}
