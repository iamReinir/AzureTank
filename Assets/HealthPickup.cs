using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 50; // Amount of HP to restore

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Entity"))
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