using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameStatic : MonoBehaviour
{
    public TMP_Text totalPlayTime;
    public TMP_Text endGameTitle;


    void Start()
    {
        totalPlayTime.text  = TimeCounter.GameDuration;
        TimeCounter.StopTimer();
    }
}
