using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameStatic : MonoBehaviour
{
    public TMP_Text timeCounter;
    public TMP_Text endGameTitle;


    void Start()
    {
        timeCounter.text = TimeCounter.time;
        TimeCounter.StopTimer();
    }
}
