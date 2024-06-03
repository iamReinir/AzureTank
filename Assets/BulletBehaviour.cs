using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    float live_time = 0.8f;
    void Start()
    {
        Destroy(gameObject, live_time);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Entity"))
        {
            collision.gameObject.SendMessage("apply_dmg", 100, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SendMessage("Hit", 1, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
}
