using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndPointBehavior : MonoBehaviour
{
    private EndGameBehavior endGameBehavior;
    public GameObject endPoint;
    // Enemy count: if enemy number == 0 => win game
    int enemyCount = 0;

    void Start()
    {
        endGameBehavior = GameObject.FindAnyObjectByType<EndGameBehavior>();
    }
    private void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Const.Tag.player) && enemyCount == 0)
        {
            endGameBehavior.Endgame(true);
        }
    }

}
