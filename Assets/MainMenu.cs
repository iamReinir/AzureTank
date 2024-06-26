using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject scoreBoard;

	private void Start()
	{
		scoreBoard.SetActive(false);
	}
	public void NewGame()
    {
        SceneManager.LoadSceneAsync(Const.Scence.CHAP1_1);
        Time.timeScale = 1f;
    }

    public void viewScoreBoard()
    {
        scoreBoard.SetActive(true);
    }
    public void closeScoreBoard()
    {
        scoreBoard.SetActive(false);
    }
}
