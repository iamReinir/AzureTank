using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
class Speeter_AI : MonoBehaviour
{
    enum State
    {
        idle,
        chase,
        shoot,
        run,
        wait
    }

    public int max_shot = 5;
    public float shootCoolDown = 0.3f;
    public int radar_range = 20;
    public int shooting_range = 7;
    public int moving_speed = 1500;
    public int bullet_speed = 1200;
    public float ally_comfort_dist = 1;
    public float wait_time = 1;
    
    
    float shootCD = 0;
    int shoot_count = 0;
    float cur_wait_time = 1;
    State current_state = State.idle;
    Vector2 run_target = Vector2.zero;
    

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
    void Update()
    {

        // Deal with other enemies
        List<GameObject> toDel = new();
        foreach (var ally in allies)
        {
            if(ally.IsDestroyed())
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
        
        foreach (var del in toDel)
            allies.Remove(del);

        if (shootCD > 0)
        {
            shootCD -= Time.deltaTime;
        }

        Radar();        
        switch (current_state)
        {
            case State.idle:
                break;
            case State.chase:
                Chase();
                break;
            case State.shoot:
                Shoot();
                break;
            case State.run:
                Run();
                break;
            case State.wait:
                Wait();
                break;
        }
    }
    void Wait()
    {
        cur_wait_time -= Time.deltaTime;
        if(cur_wait_time <= 0)
        {
            cur_wait_time = 0;
            current_state = State.idle;
        }
    }

    void Shoot()
    {        
        if (shootCD <= 0)
        {            
            turret.Shoot_at(target.position);
            shootCD = shootCoolDown;
            ++shoot_count;
        }
        if(shoot_count >= max_shot)
        {
            shoot_count = 0;
            run_target = - 1.5f * (target.position - rig.position) + rig.position;
            current_state = State.run;
            cur_wait_time = wait_time * 2;
        }
    }

    void Chase()
    {
        // Chase
        var dist = Vector2.Distance(rig.position, target.position);
        if (dist > shooting_range)
            body.HeadingToward(target.position);
        else
        {            
            current_state = State.shoot;
        }
    }

    void Run()
    {
        cur_wait_time -= Time.deltaTime;
        if (Vector2.Distance(rig.position, run_target) > 0.1f)
        {
            body.HeadingToward(run_target);
            if (cur_wait_time > 0)            
                return;
        }
        
        current_state= State.idle;
        target = null;
        cur_wait_time = 0;
    }
    void Radar()
    {
        var results = new List<Collider2D>();
        radar.OverlapCollider(new ContactFilter2D { }, results);        
        foreach (var obj in results)
        {
            if (target == null)
            {
                if (obj.CompareTag(Const.Tag.player))
                {
                    target = obj.gameObject.GetComponent<Rigidbody2D>();
                    current_state = State.chase;
                }
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