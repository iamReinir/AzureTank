#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy_turret : MonoBehaviour
{
    public GameObject bullet_prefab;
    public GameObject? target;
    const int bullet_speed = 600;
    const int shoot_speed = 60;
    const float give_up_dist = 15;
    const float comfort_dist = 5;
    int reset_shootCD = 8000;
    int shootCD = 2000;
    int moving_speed = 7;
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
        if (target == null) return;
        Shooting();
        Chase();  
        
    }

    void Chase()
    {
        var body = GetComponent<Rigidbody2D>();
        if (target == null) return;
        if (Vector2.Distance(transform.position, target.transform.position) > comfort_dist)
        {
            var force = Shoot_dir(target.transform.position) * moving_speed;
            body.AddForce(force);           
        }              
    }
    void Shooting()
    {
        if (shootCD > 0)
        {
            shootCD -= shoot_speed;
            return;
        }
        var shoot_dir = Shoot_dir(target.transform.position);
        var bullet = Instantiate(bullet_prefab, Pos() + shoot_dir * (transform.localScale.x * 1.2f), Quaternion.identity);
        var bullet_rigid = bullet.GetComponent<Rigidbody2D>();
        bullet_rigid.AddForce(shoot_dir * bullet_speed);
        shootCD = reset_shootCD;
        if(Vector2.Distance(transform.position, target.transform.position) > give_up_dist)
        {
            give_up();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if(collision.gameObject.tag == "Entity")
            target = collision.gameObject;        
    }

    void give_up()
    {
        target = null;
    }
}
