using System;
using System.Linq;
using UnityEngine;

namespace Const
{
	public static class Key
	{
		public const KeyCode LEFT = KeyCode.A;
		public const KeyCode RIGHT = KeyCode.D;
		public const KeyCode UP = KeyCode.W;
		public const KeyCode DOWN = KeyCode.S;
		public const KeyCode ROCKET = KeyCode.B;
	}
	public class CommonHelper
	{
		public float dropChance = 0.5f; // Drop chance is 50%

		// Ultility
		public System.Random random = new System.Random();
		public float Rand()
		{
			return ((float)random.NextDouble());
		}
		public GameObject GetRandomPickup(GameObject[] pickups)
		{
			if (pickups.Count() == 0) return null;
			int index = random.Next(pickups.Length);
			return pickups[index];
		}
	}

	public static class Tag
	{
		public const string player = "Player";
		public const string entity = "Entity";
		public const string enemy = "Enemy";
	}

	public class Layer
	{
		public const int data = 0;
		public const int background = 1;
		public const int subBackground = 2;
		public const int bullet = 6;
		public const int body = 6;
		public const int turret = 7;
	}

	public static class Scence
	{
		public const string CHAP1_1 = "Chapter1.1";
		public const string CHAP1_2 = "Chapter1.2";
		public const string CHAP1_3 = "Chapter1.3";
		public const string CHAP1_4 = "Chapter1.4";
		public const string CHAP1_5 = "Chapter1.5";
		public const string CHAP1_6 = "Chapter1.6";
	}

	[Serializable]
	public class ScoreBoardItems
	{
		public const string All = "scoreboard";
		public string? Playtime1;
		public int Score1;

		public string? Playtime2;
		public int Score2;

		public string? Playtime3;
		public int Score3;

		public string? Playtime4;
		public int Score4;

		public string? Playtime5;
		public int Score5;

		public void Add(string time, int score)
		{
			var result = new ScoreBoardItems();
			if (score > Score1)
			{
				Score5 = Score4;
				Playtime5 = Playtime4;
				Score4 = Score3;
				Playtime4 = Playtime3;
				Score3 = Score2;
				Playtime3 = Playtime2;
				Score2 = Score1;
				Playtime2 = Playtime1;
				Score1 = score;
				Playtime1 = time;
			}
			else if (score > Score2)
			{
				Score5 = Score4;
				Playtime5 = Playtime4;
				Score4 = Score3;
				Playtime4 = Playtime3;
				Score3 = Score2;
				Playtime3 = Playtime2;
				Score2 = score;
				Playtime2 = time;
			}
			else if (score > Score3)
			{
				Score5 = Score4;
				Playtime5 = Playtime4;
				Score4 = Score3;
				Playtime4 = Playtime3;
				Score3 = score;
				Playtime3 = time;
			}
			else if (score > Score4)
			{
				Score5 = Score4;
				Playtime5 = Playtime4;
				Score4 = score;
				Playtime4 = time;
			}
			else if (score > Score5)
			{
				Score5 = score;
				Playtime5 = time;
			}
		}
	}
}
