using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WebGLInput = WebGLSupport.WebGLInput;

public class CopyPaste_Listener : MonoBehaviour
{
    public TMP_InputField[] tmpInputFields;
    
    void Start()
    {

        tmpInputFields = FindObjectsOfType<TMP_InputField>(true);
        //Debug.Log(tmpInputFields);

        foreach (TMP_InputField tmpInput in tmpInputFields)
        {
            //Debug.Log("Entre");
            tmpInput.gameObject.AddComponent<WebGLInput>().enabled = true;
            //Debug.Log(tmpInput.gameObject.GetComponent<WebGLInput>().enabled);
        }
    }

    
}
