using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interventions : MonoBehaviour
{
    [SerializeField]
    private GameObject interventionPanel;

    [SerializeField]
    private GameObject pausePanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && EndPointBehavior.isEndGame == false)
        {
            if (pausePanel.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        interventionPanel.SetActive(true);
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        interventionPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        EndPointBehavior.isEndGame = false;
        Time.timeScale = 1f;

    }
}
