using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static IEnumerator IEFade(CanvasGroup group, float desireValue, float fadeTime)
    {
        float timer = 0.01f;
        float initialValue = group.alpha;
        
        while(timer < fadeTime)
        {
            group.alpha = Mathf.Lerp(initialValue, desireValue, timer / fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }
        group.alpha = desireValue;
    }
}
