using Const;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

class ScoreBoard : MonoBehaviour
{
	public TMP_Text top1_score;
	public TMP_Text top2_score;
	public TMP_Text top3_score;
	public TMP_Text top4_score;
	public TMP_Text top5_score;
	public TMP_Text top1_date;
	public TMP_Text top2_date;
	public TMP_Text top3_date;
	public TMP_Text top4_date;
	public TMP_Text top5_date;
	private void Start()
	{
		ScoreBoardItems scores;
		string data = PlayerPrefs.GetString(ScoreBoardItems.All);
		Debug.Log("get- \n" + data);
		if (string.IsNullOrEmpty(data))
		{
			scores = new ScoreBoardItems();
		}
		else
		{
			scores = JsonUtility.FromJson<ScoreBoardItems>(data);
		}
		top1_date.text = scores.Playtime1?.ToString() ?? "<empty>";
		top1_score.text = scores.Score1.ToString() ?? "<empty>"; 
		top2_date.text = scores.Playtime2?.ToString() ?? "<empty>";
		top2_score.text = scores.Score2.ToString() ?? "<empty>"; 
		top3_date.text = scores.Playtime3?.ToString() ?? "<empty>";
		top3_score.text = scores.Score3.ToString() ?? "<empty>";
		top4_date.text = scores.Playtime4?.ToString() ?? "<empty>";
		top4_score.text = scores.Score4.ToString() ?? "<empty>";
		top5_date.text = scores.Playtime5?.ToString() ?? "<empty>";
		top5_score.text = scores.Score5.ToString() ?? "<empty>";
	}
}