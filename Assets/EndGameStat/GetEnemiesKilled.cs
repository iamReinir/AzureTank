using TMPro;
using UnityEngine;

class GetEnemiesKilled : MonoBehaviour
{
	private void Start()
	{
		GetComponent<TMP_Text>().text = FindAnyObjectByType<HeadUpDisplay>()
			.enemiesKilled.ToString();
	}
}