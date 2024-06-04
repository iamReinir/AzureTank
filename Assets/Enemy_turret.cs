#nullable enable
using Const;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_turret : MonoBehaviour
{
    public GameObject? bullet_prefab;
    public GameObject? target;
    const int bullet_speed = 1000;
    const int shoot_speed = 60;
    const float give_up_dist = 15;
    const float comfort_dist = 5;
    int reset_shootCD = 8000;
    int shootCD = 2000;
    int moving_speed = 7;
    int hp = 3;

    public GameObject? bombPickupPrefab; // Prefab of bomb
    public GameObject? healthPickupPrefab; // Prefab of health
    public GameObject? rocketPickupPrefab; // Prefab of rocket

    private PlayerBehaviour? player;
    private CommonHelper? helper;
    SpriteRenderer? spriteRenderer;

    [System.Obsolete]
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        helper = new CommonHelper();
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

    void Update()
    {
        if (player != null && player.isGameOver) return;
        if (target == null) return;

        Shooting();
        Chase();
    }

    void Chase()
    {
        if (target == null || player != null && player.isGameOver) return;

        var body = GetComponent<Rigidbody2D>();
        if (Vector2.Distance(transform.position, target.transform.position) > comfort_dist)
        {
            var force = Shoot_dir(target.transform.position) * moving_speed;
            body.AddForce(force);
        }
    }

    void Shooting()
    {
        if (player != null && player.isGameOver) return;
        if (shootCD > 0)
        {
            shootCD -= shoot_speed;
            return;
        }

        if (target == null) return;

        var shoot_dir = Shoot_dir(target.transform.position);
        var bullet = Instantiate(bullet_prefab, Pos() + shoot_dir * (transform.localScale.x * 1.2f), Quaternion.identity);
        var bullet_rigid = bullet?.GetComponent<Rigidbody2D>();
        bullet_rigid?.AddForce(shoot_dir * bullet_speed);
        shootCD = reset_shootCD;

        if (Vector2.Distance(transform.position, target.transform.position) > give_up_dist)
        {
            give_up();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Entity")
            target = collision.gameObject;
    }

    void give_up()
    {
        target = null;
    }

    public void Hit(int dame)
    {
        ApplyDame(dame);
    }

    private void ApplyDame(int dame)
    {
        if (spriteRenderer == null)
        {
            return;
        }
        else if(hp > 0)
        {
            hp -= dame;
        }
        switch (hp)
        {
            case 0: { DropItem(); Destroy(gameObject); break; }
            case 1: spriteRenderer.color = Color.gray; break;
            case 2: spriteRenderer.color = Color.yellow; break;
        }
    }

    private void DropItem()
    {
        List<GameObject> validPickups = new List<GameObject>();
        if (healthPickupPrefab != null)
        {
            validPickups.Add(healthPickupPrefab);
        }

        if (bombPickupPrefab != null)
        {
            validPickups.Add(bombPickupPrefab);
        }
        if (rocketPickupPrefab != null)
        {
            validPickups.Add(rocketPickupPrefab);
        }

        if (validPickups.Count > 0 && helper?.Rand() < helper?.dropChance)
        {
            GameObject pickup = helper.GetRandomPickup(validPickups.ToArray());
            Instantiate(pickup, transform.position, Quaternion.identity);
        }
    }
}
