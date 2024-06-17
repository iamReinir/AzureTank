using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameStatic : MonoBehaviour
{
    public TMP_Text totalPlayTime;

    void Start()
    {
        totalPlayTime.text  = TimeCounter.GameDuration;
        TimeCounter.StopTimer();
    }
}
