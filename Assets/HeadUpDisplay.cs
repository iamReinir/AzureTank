using Const;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HeadUpDisplay : MonoBehaviour
{	
	PlayerBehaviour player;
    TimeCounter counter;
    public int enemiesKilled = 0;
    public int exp = 0;

	State state = State.unfade;
	GameObject blackPanel;
	SpriteRenderer sprite;
    Action fadeCallback;
	enum State
	{
		idle,
		fade,
		change,
		unfade
	}

	void Start()
    {
		blackPanel = Resources.Load("PreFab/BlackPanel") as GameObject;
		player = FindFirstObjectByType<PlayerBehaviour>();
		counter = FindAnyObjectByType<TimeCounter>();
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

		if (sprite == null || fadeCallback == null)
			return;
        // Fade check
        switch (state)
		{
			case State.fade:				
				FadeToBlack();
				break;
			case State.unfade:
				Unfade();
				break;
			case State.change:
				fadeCallback();
				state = State.unfade;
				break;
			case State.idle:
			default:
				break;
		}
	}
    public void AddKill()
    {
        enemiesKilled++;
    }
    public void AddExp(int x)
    {
        exp += x;
    }

	void FadeToBlack()
	{		
		sprite.color += new Color(0, 0, 0, 0.05f);
		if (sprite.color.a >= 1f)
		{
			state = State.change;
		}
	}

	void Unfade()
	{
		// Delta time will fuck this up
		sprite.color -= new Color(0, 0, 0, 0.05f);
		if (sprite.color.a <= 0f)
		{
			Time.timeScale = 1f;
			Destroy(sprite.gameObject);
			sprite = null;
			state = State.idle;
		}
	}  
	public void FadeThenUnfade(Action callBack)
    {
		Time.timeScale = 0;
		fadeCallback = callBack;
		sprite = Instantiate(blackPanel, player.gameObject.transform.position, Quaternion.identity)
				.GetComponent<SpriteRenderer>();
		DontDestroyOnLoad(sprite.gameObject);
		state = State.fade;		
	}
	public int Score()
	{
		bool win = player.HP > 0;
		return (win ? ((int)Mathf.Round(600f / counter.playTime) * 200) : 0) + exp;
	}

	public void SaveScore()
	{
		ScoreBoardItems scores;
		string data = PlayerPrefs.GetString(ScoreBoardItems.All);
		if(string.IsNullOrEmpty(data)) {
			scores = new ScoreBoardItems();
		}
		else
		{
			scores = JsonUtility.FromJson<ScoreBoardItems>(data);
		}
		scores.Add(counter.gameDuration,Score());
		data = JsonUtility.ToJson(scores);		
		PlayerPrefs.SetString(ScoreBoardItems.All,data);
	}
}
