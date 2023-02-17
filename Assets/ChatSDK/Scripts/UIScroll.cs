using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScroll : MonoBehaviour
{
    private float Ref = -411.39f;
    public float timeAnimation = 0.5f;

    private int column = 0;
    public RectTransform content;
    
    public void MoveTo(int columnNumber)
    {
        column = columnNumber;
        Debug.Log("Column: " + column );
        StopAllCoroutines();
        StartCoroutine(AnimationCoRoutine(column));
    }

    private IEnumerator AnimationCoRoutine(int column)
    {
        Vector2 start = content.anchoredPosition;
        float targetX = Ref * column;
        Vector2 targetPosition = new Vector2(targetX, content.anchoredPosition.y);
        // animate over 1/2 second
        for (float accumTime = Time.deltaTime; accumTime <= timeAnimation; accumTime += Time.deltaTime)
        {
            content.anchoredPosition = Vector2.Lerp(start, targetPosition, accumTime / timeAnimation);
            yield return null;
        }
        
        content.anchoredPosition = targetPosition;
    }

    

}