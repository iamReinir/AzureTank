using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HeadUpDisplay : MonoBehaviour
{
	// Start is called before the first frame update
	PlayerBehaviour player;
    TimeCounter counter;
    public int enemiesKilled = 0;
    public int exp = 0;
	void Start()
    {
        player = FindFirstObjectByType<PlayerBehaviour>();
		counter =FindAnyObjectByType<TimeCounter>();
	}

    // Update is called once per frame
    void Update()
    {
        var text = GetComponent<Text>();
        StringBuilder displayText = new StringBuilder();
		if (player == null) return;
		displayText.AppendLine($"HP: {player.HP}/{player.Max_HP}");
        displayText.AppendLine($"Enemies: {GameObject.FindGameObjectsWithTag(Const.Tag.enemy).Length}");
        displayText.AppendLine($"Rocket: {player.rocketCount}");
        displayText.AppendLine($"Time: {counter.gameDuration}");
        text.text = displayText.ToString();
    }
    public void AddKill()
    {
        enemiesKilled++;
    }
    public void AddExp(int x)
    {
        exp += x;
    }
}
