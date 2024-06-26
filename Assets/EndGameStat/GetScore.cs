using TMPro;
using UnityEngine;

class GetScore : MonoBehaviour
{
	private void Start()
	{
		GetComponent<TMP_Text>().text = FindAnyObjectByType<HeadUpDisplay>()
			.Score().ToString();
	}
}