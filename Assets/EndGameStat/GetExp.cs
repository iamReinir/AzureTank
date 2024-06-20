using TMPro;
using UnityEngine;

class GetExp : MonoBehaviour
{
	private void Start()
	{
		GetComponent<TMP_Text>().text = FindAnyObjectByType<HeadUpDisplay>()
			.exp.ToString();
	}
}