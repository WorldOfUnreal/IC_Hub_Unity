using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToURLButton : MonoBehaviour
{

    public string url = "https://www.cosmicrafts.com/";
    
    public void OpenURL()
    {
        Application.OpenURL(url);
    }

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => { OpenURL(); });
    }
}
