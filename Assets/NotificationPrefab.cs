using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPrefab : MonoBehaviour
{
    public ImageDownloadManager imageDownloadManager;
    
    public TMP_Text title;
    public TMP_Text description;
    public CanvasGroup canvasGroup;
    public float timeAnimationClose = 2f;
    public float timeWait = 7f;


    private void Awake()
    {
        StartCoroutine(WaitCoroutine());
    }

    public void CloseNotification()
    {
        StopAllCoroutines();
        StartCoroutine(AnimationCoRoutine());
    }
    
    private IEnumerator AnimationCoRoutine()
    {
        canvasGroup.interactable = false;
        // animate over 2 second
        for (float accumTime = Time.deltaTime; accumTime <= timeAnimationClose; accumTime += Time.deltaTime)
        {
            canvasGroup.alpha = 1 - (accumTime / timeAnimationClose);
            yield return null;
        }
        canvasGroup.alpha = 0;
        Destroy(this.gameObject);
    }
    
    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(timeWait);
        CloseNotification();
    }
}
