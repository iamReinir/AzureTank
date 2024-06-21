using Const;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    // Dependencies
    public GameObject bullet;
    public GameObject rocket;
    public GameObject hp_overlay;
    public GameObject gun;

    // Player's stats
    const int mov_speed = 1200;
    const int bullet_speed = 1000;
    public int Max_HP { get; set; } = 3000;
    public int HP { get; set; } = 3000;

    // Enemy count: if enemy number == 0 => win game
    int enemyCount = 0;

    // Rocket count
    public int rocketCount = 0;

    // Helper variables
    Rigidbody2D rig;
    MovingBehaviour mov;

    // Game state
    public bool isGameOver = false;
    EndGameBehavior endGameBehavior;
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<CannonMounted>().bullet_speed = 1000;
        GetComponent<SpriteRenderer>().sortingOrder = Layer.body;
        gun.GetComponent<SpriteRenderer>().sortingOrder= Layer.turret;
        rig = GetComponent<Rigidbody2D>();
        mov = GetComponent<MovingBehaviour>();
        mov.moving_speed = mov_speed;
        gameObject.tag = Tag.player;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(gun);
        endGameBehavior = GameObject.FindAnyObjectByType<EndGameBehavior>(); ;
    }

    void Update()
    {
        Update_camera();
        Update_gun();
        Moving_check();
        Shooting_check();
    }

    void Update_camera()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        //hp_overlay.transform.position = this.transform.position + new Vector3(0, 1, -5);
    }

    void Update_gun()
    {
        var mouse_pos = ShootingDirection();
        var angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        gun.transform.position = this.transform.position;
    }
    void Moving_check()
    {
        Vector2 mov_dir = rig.position;
        bool moved = false;
        if (Input.GetKey(Key.LEFT))
        {
            moved = true;
            mov_dir += new Vector2
            {
                x = -mov_speed,
                y = 0
            };
        }
        if (Input.GetKey(Key.RIGHT))
        {
            moved = true;
            mov_dir += (new Vector2
            {
                x = mov_speed,
                y = 0
            });
        }
        if (Input.GetKey(Key.UP))
        {
            moved = true;
            mov_dir += (new Vector2
            {
                x = 0,
                y = mov_speed
            });
        }
        if (Input.GetKey(Key.DOWN))
        {
            moved = true;
            mov_dir += (new Vector2
            {
                x = 0,
                y = -mov_speed
            });
        }

        if (moved)
            mov.HeadingToward(mov_dir);     
    }

    void Shooting_check()
    {
        if (isGameOver) return;
        if (Input.GetMouseButtonDown(0))
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetComponent<CannonMounted>().Shoot_at(pos);
        }
        if (Input.GetKey(Key.ROCKET))
        {
            ShootRocket();
        }
    }
    public void apply_dmg(int amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            endGameBehavior.Endgame(false);
        }
    }
    Vector2 ShootingDirection()
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        var mouse = new Vector2
        {
            x = mouseWorldPos.x,
            y = mouseWorldPos.y
        };
        Vector2 character = rig.position;
        var dir = (mouse - character);
        return (dir / dir.magnitude);
    }

    Vector2 Pos()
    {
        return this.transform.position;
    }
    public void IncreaseHP(int amount)
    {
        HP += amount;
        if (HP > Max_HP)
        {
            HP = Max_HP;
        }
    }

    public void DecreaseHP(int amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            endGameBehavior.Endgame(false);
        }
    }

    //Add rocket function
    public void AddRocket(int quanity)
    {
        rocketCount += quanity;
    }

    private void ShootRocket()
    {
        if (rocketCount == 0) { return; }
        rocketCount -= 1;
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Enemy_turret enemyTurret = enemy.GetComponent<Enemy_turret>();
            if (enemyTurret != null)
            {
                enemyTurret.Hit(1);
            }
        }
        var argument = new TurretBehaviour.Arg
        {
            body = this.gameObject,
            bullet_prefab = bullet,
            rocket_prefab = rocket,
            direction = ShootingDirection() * bullet_speed
        };
        this.SendMessage(TurretBehaviour.toShootRocket, argument);

    }
}
