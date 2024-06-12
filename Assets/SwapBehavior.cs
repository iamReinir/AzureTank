using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerBehaviour>()?.EnterSwamp();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerBehaviour>()?.ExitSwamp();
        }
    }
}
