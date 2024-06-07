using UnityEngine;
using UnityEngine.UI;

public class EndPointBehavior : MonoBehaviour
{
    // UI elements for Game Over
    public GameObject gameOverPanel;
    public Text gameOverText;

    public bool isWinGame = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Const.Tag.player))
        {
            Endgame("You win");
        }
    }
    public void Endgame(string text)
    {
        // Display "Game Over" message
        gameOverPanel.SetActive(true);
        gameOverText.text = text;
        isWinGame = false;

        // Stop the game
        Time.timeScale = 0;
    }
}
