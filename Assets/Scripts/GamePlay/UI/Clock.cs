using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class Clock : MonoBehaviour 
{
    public Image BigArrow;
    public Image SmallArrow;

    public TimeSpan CurrentTime;

    public void SetTime(TimeSpan time)
    {
        float BigArrowangle = -((time.Minutes/60) * 360);
        float SmallArrowangle = -((time.Hours/12) * 360);
        BigArrow.rectTransform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, BigArrowangle);
        SmallArrow.rectTransform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, SmallArrowangle);
    }
}
