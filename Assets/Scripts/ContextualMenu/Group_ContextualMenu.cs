using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group_ContextualMenu : ContextualMenu
{
    [HideInInspector] public int groupID;
    [HideInInspector] public string groupName;

    public GameObject groupSettingsPanel;
    
    
    public void GoToChat()
    {
        Hub_Manager.Instance.OpenSection(0);
        ChatManager.Instance.SetGroupSelected(groupID);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void GoToGroupSettings()
    {
        Hub_Manager.Instance.OpenSection(0);
        ChatManager.Instance.SetGroupSelected(groupID);
        groupSettingsPanel.SetActive(true);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void LeaveGroup()
    {
        ChatManager.Instance.LeaveGroup(groupID);
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
    public void ReportGroup()
    {
        Debug.Log("ReportGroup");
        CanvasReport.Instance.OpenPopupReport(groupID.ToString());
        ContextualMenuManager.Instance.CloseContextualMenu();
    }
}
