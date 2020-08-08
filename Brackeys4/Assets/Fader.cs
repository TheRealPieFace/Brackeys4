using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public CanvasGroup uiElement;

    public void Fade(float end)
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, end));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup panel, float start, float end, float lerpTime = 0.5f)
    {
        if(end == 1)
        {
            lerpTime = 0.001f;
        }
        float timeStartLerp = Time.time;
        float timeSinceStart = Time.time - timeStartLerp;
        float percentageComplete = timeSinceStart / lerpTime;

        while (true)
        {
            timeSinceStart = Time.time - timeStartLerp;
            percentageComplete = timeSinceStart / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            uiElement.alpha = currentValue;

            if(percentageComplete >= 1)
            {
                break;
            }

            yield return new WaitForEndOfFrame();
        }
        if (end == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
