using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenCover : MonoBehaviour
{
    public Image coverLeft;
    public Image coverRight;

    public IEnumerator SetCoverValue(float coverValue, float coverTime)
    {
        float timer = 0;
        float fillLeft = coverLeft.fillAmount;
        float fillRight = coverRight.fillAmount;

        while (timer < coverTime)
        {
            coverLeft.fillAmount = Mathf.Lerp(fillLeft, coverValue, timer/coverTime);
            coverRight.fillAmount = Mathf.Lerp(fillRight, coverValue, timer/coverTime);
            timer += Time.deltaTime;
            yield return null;
        }

        coverLeft.fillAmount = coverValue;
        coverRight.fillAmount = coverValue;
    }
}
