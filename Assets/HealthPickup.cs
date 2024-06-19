using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private const int healthAmount = 200; // Amount of HP to restore

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Const.Tag.player))
        {
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                player.IncreaseHP(healthAmount);
                Destroy(gameObject); // Destroy the pickup after use
            }
        }
    }
}