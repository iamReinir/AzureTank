using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy_turret : MonoBehaviour
{
    public GameObject bullet_prefab;
    public GameObject player;
    const int bullet_speed = 600;
    const int shoot_speed = 60;
    int reset_shootCD = 2000;
    int shootCD = 2000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    Vector2 Pos()
    {
        return GetComponent<Rigidbody2D>().position;
        
    }
    Vector2 Shoot_dir(Vector2 target)
    {
        var self = Pos();
        var dir = (target - self);
        return dir / dir.magnitude;

    }
    // Update is called once per frame
    void Update()
    {
        if(shootCD > 0)
        {
            shootCD -= shoot_speed;
            return;
        }
        var shoot_dir = Shoot_dir(player.GetComponent<Rigidbody2D>().position);
        var bullet = Instantiate(bullet_prefab, Pos() + shoot_dir * 3, Quaternion.identity);
        var bullet_rigid = bullet.GetComponent<Rigidbody2D>();
        bullet_rigid.AddForce(shoot_dir * bullet_speed);
        shootCD = reset_shootCD;
    }
}
