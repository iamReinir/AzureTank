using UnityEngine;

class CannonMounted : MonoBehaviour
{
    public GameObject bullet;
    public int bullet_speed = 10;
    GameObject body;

    private void Start()
    {
        body = gameObject;
        if (bullet == null)
            bullet = GameObject.Find("Bullet");
    }
    public void Shoot_toward(Vector2 direction)
    {        
        var radius = body.GetComponent<CircleCollider2D>().radius 
            + bullet.GetComponent<CircleCollider2D>().radius + 1f;
        var cur_pos = body.GetComponent<Rigidbody2D>().position;
        var dir = direction / direction.magnitude;
        var bul = Instantiate(bullet, cur_pos + dir * radius, Quaternion.identity);
        bul.GetComponent<Rigidbody2D>().AddForce(direction);
    }

    public void Shoot_at(Vector2 target)
    {
        Vector2 body_pos = body.transform.position;
        var dir = target - body_pos;
        var weight = body.GetComponent<Rigidbody2D>()?.mass ?? 1f;
        Shoot_toward(dir / dir.magnitude * weight * bullet_speed);
    }
}