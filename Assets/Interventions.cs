using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        if (Input.GetKeyDown(KeyCode.Escape) && EndGameBehavior.isEndGame == false)
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
        var player = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();
        Destroy(player);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        EndGameBehavior.isEndGame = false;
        Time.timeScale = 1f;
        interventionPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        foreach (GameObject gameObject in Object.FindObjectsOfType<GameObject>())
        {
            Destroy(gameObject);
        }
        /*
        var player = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();
        var gun = GameObject.FindFirstObjectByType<TurretBehaviour>();
        var HP = GameObject.FindGameObjectsWithTag("UI").FirstOrDefault();
        Destroy(player);
        Destroy(HP);
        Destroy(gun);*/

        SceneManager.LoadSceneAsync(Const.Scence.CHAP1_1);
        EndGameBehavior.isEndGame = false;
        Time.timeScale = 1f;
        interventionPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public static Interventions Instance;

    private void Awake()
    {

            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(interventionPanel);

        
    }
}
