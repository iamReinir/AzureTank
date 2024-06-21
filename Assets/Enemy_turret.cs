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
    //float reset_shootCD = 1f;
    // float shootCD = 1f;
    //int moving_speed = 1000;
    int hp = 3;

    public GameObject? bombPickupPrefab; // Prefab of bomb
    public GameObject? healthPickupPrefab; // Prefab of health
    public GameObject? rocketPickupPrefab; // Prefab of rocket

    private PlayerBehaviour? player;
    private CommonHelper? helper;
    SpriteRenderer? spriteRenderer;
    HeadUpDisplay hub;

	[System.Obsolete]
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        helper = new CommonHelper();
        hub = FindAnyObjectByType<HeadUpDisplay>();
        GetComponent<SpriteRenderer>().sortingOrder = Const.Layer.body;
	}

    void Update()
    {
        if (player != null && player.isGameOver) return;
    }    

    public void Hit(int dame)
    {
        ApplyDame(dame);
    }

    public void ApplyDame(int dame)
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
            case 0: 
                DropItem();
                Destroy(gameObject);
                hub?.AddKill();
                hub?.AddExp(20);
                break;
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
