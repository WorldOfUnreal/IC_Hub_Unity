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
        public string StringSearch;
        public List<Group_data> group_data;
    }
    public class Group_data
    {
        public bool joined;
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
   
    public GameObject panelSliderGroup;
    public GameObject panelNotFound;
    public Animator searching_Anim;
    
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
        
        SearchGroupInfo _searchGroupInfo = JsonUtility.FromJson<SearchGroupInfo>(json);

        if (searchGroupInput.text == _searchGroupInfo.StringSearch)
        {
            searching_Anim.Play("Searching_Stop");
            groupsList.Clear();
            foreach (Transform t in panelSliderGroup.transform) { GameObject.Destroy(t.gameObject); }
            
            if (!_searchGroupInfo.group_exists)
            {
                panelNotFound.SetActive(true);
                panelSliderGroup.SetActive(false);
            }
            else
            {
                panelNotFound.SetActive(false);
                panelSliderGroup.SetActive(true);
                foreach(Group_data g in _searchGroupInfo.group_data){
                    AddGroupToList(g.id, g.name, g.is_private);
                }
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
            searching_Anim.Play("Searching");
            JSSearchGroup(searchGroupInput.text);
        }
        else
        {
            panelSliderGroup.SetActive(false);
            panelNotFound.SetActive(true);
            searching_Anim.Play("Searching_Stop");
        }
    }
    
    public void JoinRequestGroup(int id){
        JSRequestJoinGroup("Pruebas");
    }
    public void JoinGroup(int id){
        JSJoinGroup("Pruebas");
    }
    
    
    
}
