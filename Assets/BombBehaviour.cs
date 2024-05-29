using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int healthAmount = 100; // Amount of HP to restore

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Entity"))
        {
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                player.DecreaseHP(healthAmount);
                Destroy(gameObject); // Destroy the pickup after use
            }
        }
    }
}
