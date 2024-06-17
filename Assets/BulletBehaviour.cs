using Const;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    float live_time = 0.8f;
    public int damage = 100;
    void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Const.Layer.bullet;
        Destroy(gameObject, live_time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger == true)
            return;
        if (collision.gameObject.GetComponent<StopBullet>() != null)
        {
            Destroy(gameObject);
            return;
        }
        var player = collision.gameObject.GetComponent<PlayerBehaviour>();
        if (player != null)
        {
            player.apply_dmg(damage);
            Destroy(gameObject);
            return;
        }
        var enemy = collision.gameObject.GetComponent<Enemy_turret>();
        if (enemy != null)
        {
            enemy.ApplyDame(1);
            Destroy(gameObject);
            return;

        }
    }
}
