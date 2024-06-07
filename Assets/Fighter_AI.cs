using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fighter_AI : MonoBehaviour
{

    int radar_range = 15;
    int comfortable_range = 5;
    int shooting_range = 12;
    int moving_speed = 1000;
    int bullet_speed = 1200;
    float shootCD = 1;
    public float shootCoolDown = 1;
    float ally_comfort_dist = 5;

    Rigidbody2D target = null;
    List<GameObject> allies = new();
    CircleCollider2D radar;
    MovingBehaviour body;
    CannonMounted turret;
    Rigidbody2D rig;
    
    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<MovingBehaviour>();
        body.moving_speed = moving_speed;
        rig = gameObject.GetComponent<Rigidbody2D>();
        radar = gameObject.AddComponent<CircleCollider2D>();        
        turret = gameObject.GetComponent<CannonMounted>();
        turret.bullet_speed = bullet_speed;
        radar.radius = radar_range;
        radar.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        Radar();
        // Deal with other enemies
        List<GameObject> toDel = new();
        foreach (var ally in allies)
        {
            if (ally.IsDestroyed())
            {
                toDel.Add(ally);
                continue;
            }
            var ally_dist = Vector2.Distance(transform.position, ally.transform.position);
            if (ally_dist > radar_range)
                toDel.Add(ally);
            if (ally_dist < ally_comfort_dist)
            {                
                body.MovingAwayFrom(ally.transform.position, false);
            }
        }
        foreach(var del in toDel)
            allies.Remove(del);
        if (target == null)
            return;
        // Chase
        var dist = Vector2.Distance(rig.position, target.position);
        if (dist > comfortable_range)
            body.HeadingToward(target.position);
        else
            body.Facing(target.position);
        
        // Shoot
        if(shootCD > 0)
        {
            shootCD -= Time.deltaTime;
        }
        if (dist < shooting_range && shootCD < 0)
        {            
            turret.Shoot_at(target.position);
            shootCD = shootCoolDown;
        }
                       
    }

    void Radar()
    {
        var results = new List<Collider2D>();
        radar.OverlapCollider(new ContactFilter2D { }, results);        
        foreach (var obj in results)
        {
            if (target == null)
            {               
                if(obj.CompareTag(Const.Tag.player))
                    target = obj.gameObject.GetComponent<Rigidbody2D>();
            }
            if (obj.CompareTag(Const.Tag.enemy))
            {
                if (!allies.Contains(obj.gameObject))
                {
                    allies.Add(obj.gameObject);
                }
            }
        }
    }
}
