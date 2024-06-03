using UnityEngine;

namespace Assets
{
    public class RocketBehaviour : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Entity"))
            {
                PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
                if (player != null)
                {
                    player.AddRocket(1);
                    Destroy(gameObject); // Destroy the pickup after use
                }
            }
        }
    }
}
