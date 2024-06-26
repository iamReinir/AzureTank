using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    private const int damageAmount = 500; // Amount of bomb's damage
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Const.Tag.player))
        {
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                player.DecreaseHP(damageAmount);
                Destroy(gameObject); // Destroy the pickup after use
            }
        }
    }
}
