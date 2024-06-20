using TMPro;
using UnityEngine;

class GetTime : MonoBehaviour
{
	private void Start()
	{
		GetComponent<TMP_Text>().text = FindAnyObjectByType<TimeCounter>()
			.gameDuration;
	}
}