using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentSeparator_Controller : MonoBehaviour
{
    
    public GameObject arrow;  bool flagToken = false;
    public float tiempo = 0.01f;
    public GameObject content;
    public GameObject prefab;
    
    private float originalHeightPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        originalHeightPrefab = prefab.GetComponent<RectTransform>().sizeDelta.y;
    }
    public void ChangeContent()
    {
        StopCoroutine("ContentIEnumerator"); StartCoroutine("ContentIEnumerator");
    }
    IEnumerator ContentIEnumerator()
    {
        var t = 0f;
        var start = content.transform.localScale.y;
        Vector2 startPrefab = content.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
        var target = 0; var targetPrefab = 0f;
        if (flagToken) { target = 1; targetPrefab = originalHeightPrefab;
        }
        arrow.transform.localEulerAngles = new Vector3(0, 0, -90*target);
        flagToken = !flagToken;

        while (t < 1)
        {
            t += Time.deltaTime / tiempo;
            if (t > 1) t = 1;
            content.transform.localScale = new Vector3(content.transform.localScale.x, 
                Mathf.Lerp(start, target, t), content.transform.localScale.z);
            
            Vector2 newHeight = new Vector2 (startPrefab.x, Mathf.Lerp(startPrefab.y, targetPrefab, t));
            
            foreach (RectTransform token in content.transform) { token.sizeDelta = newHeight; }
            
            yield return null;
            LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());//Update UI
        }
    }
}
