using Unity.VisualScripting;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    public const string toShoot = "Shoot";

    public class Arg
    {
        public GameObject bullet_prefab;
        public GameObject body; 
        public Vector2 direction;
    }
    private void Shoot(Arg arg)
    {
        var radius = arg.body.GetComponent<CircleCollider2D>().radius 
            + arg.bullet_prefab.GetComponent<CircleCollider2D>().radius + 1f;
        var cur_pos = arg.body.GetComponent<Rigidbody2D>().position;
        var dir = arg.direction / arg.direction.magnitude;
        var bullet = Instantiate(arg.bullet_prefab, cur_pos + dir * radius, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(arg.direction);
    }
}