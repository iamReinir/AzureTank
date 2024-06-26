using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameBehavior : MonoBehaviour
{

    [SerializeField]
    public GameObject gameOverPanel;

    [SerializeField]
    public GameObject intervention;

    [SerializeField]
    public TMP_Text endGameTitle;


    public static bool isEndGame = false;


    public void Endgame(Boolean isWin)
    {
        intervention.SetActive(true);
        gameOverPanel.SetActive(true);
        isEndGame = true;
        endGameTitle.text = isWin ? "VICTORY" : "DEFEATED";
        FindAnyObjectByType<TimeCounter>().StopTimer();
        Time.timeScale = 0;
        FindAnyObjectByType<HeadUpDisplay>().SaveScore();
    }
}
