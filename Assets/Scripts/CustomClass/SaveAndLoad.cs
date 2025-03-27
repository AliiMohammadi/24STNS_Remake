using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class SaveAndLoad 
{
    public static int TopRecord
    {
        get 
        {
            return PlayerPrefs.GetInt("TopRecord",0);
        }
        set
        {
            PlayerPrefs.SetInt("TopRecord",value);
        }
    }
}
