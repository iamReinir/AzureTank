using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndPointBehavior : MonoBehaviour
{
    [SerializeField]
    public GameObject gameOverPanel;

    [SerializeField]
    public GameObject intervention;

    [SerializeField]
    public TMP_Text endGameTitle;

    public static bool isEndGame = false;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Entity"))
        {
            Endgame(true);
        }
    }
    public void Endgame(Boolean isWin)
    {
        
        intervention.SetActive(true);
        gameOverPanel.SetActive(true);

        isEndGame = true;
        endGameTitle.text = isWin ? "COMPLETE" : "FALSE";
        Time.timeScale = 0;
    }
}
