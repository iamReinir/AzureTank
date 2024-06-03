using UnityEngine;

public class BombDropBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    const int life_time = 500;
    int current_life = life_time;
    SpriteRenderer sprite;
    Vector3 currentScale;
    float strink_rate = 0.01f;

    void Start()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        currentScale = transform.localScale;
    }
    void Update()
    {
        if (current_life < 5)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            sprite.color = Color.red;
        }
        if (current_life < 0)
        {
            Destroy(gameObject);
            return;
        }
        currentScale.x -= strink_rate;
        currentScale.y -= strink_rate;
        currentScale.z -= strink_rate;
        transform.localScale = currentScale;
        --current_life;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Entity"))
        {
            collision.gameObject.SendMessage("apply_dmg", 200, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
}
