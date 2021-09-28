using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image ProgressBarImage;

    public void UpdateProgressBar(float maxValue, float value)
    {
        ProgressBarImage.fillAmount = Mathf.Clamp(value / maxValue, 0, 1f);
    }
}
