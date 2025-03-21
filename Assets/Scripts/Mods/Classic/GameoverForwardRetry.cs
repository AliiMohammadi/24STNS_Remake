using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverForwardRetry : MonoBehaviour 
{
    public void Retry()
    {
        FindObjectOfType<ClassicMode>().Restart();
    }
}
