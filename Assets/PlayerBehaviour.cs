using Const;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For reloading the scene

public class PlayerBehaviour : MonoBehaviour
{

    // Dependencies
    public GameObject bullet;
    public GameObject hp_overlay;
    public GameObject gun;

    // Player's stats
    const int mov_speed = 15;
    const int bullet_speed = 600;
    int Max_HP { get; set; } = 600;
    int HP { get; set; } = 600;

    // Helper variables
    Rigidbody2D rig;

    // UI elements for Game Over
    public GameObject gameOverPanel;
    public Text gameOverText;


    // Game state
    public bool isGameOver = false;

    // Start is called before the first frame update
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
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
        hp_overlay.SendMessage("display", $"HP : {HP}/{Max_HP}");
        hp_overlay.transform.position = this.transform.position + new Vector3(0, 1, -5);
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
        Vector2 mov_dir = new Vector2();
        if (Input.GetKey(Key.LEFT))
        {
          mov_dir += new Vector2
            {
                x = -mov_speed,
                y = 0
            };
        }
        if (Input.GetKey(Key.RIGHT))
        {
            mov_dir += (new Vector2
            {
                x = mov_speed,
                y = 0
            });
        }
        if (Input.GetKey(Key.UP))
        {
            mov_dir += (new Vector2
            {
                x = 0,
                y = mov_speed
            });
        }
        if (Input.GetKey(Key.DOWN))
        {
            mov_dir += (new Vector2
            {
                x = 0,
                y = -mov_speed
            });
        }
        if(mov_dir.x != 0 || mov_dir.y != 0)
            this.SendMessage(MovingBehaviour.toMove, mov_dir / mov_dir.magnitude * mov_speed);
    }

    void Shooting_check()
    {
        if (isGameOver) return;
        if (Input.GetMouseButtonDown(0))
        {
            var argument = new TurretBehaviour.Arg
            {
                body = this.gameObject,
                bullet_prefab = bullet,
                direction = ShootingDirection() * bullet_speed
            };
            this.SendMessage(TurretBehaviour.toShoot, argument);
        }
    }
    public void apply_dmg(int amount)
    {
        HP -= amount;
        if(HP <= 0)
        {
            Dead();
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
        if (HP == 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        // Display "Game Over" message
        gameOverPanel.SetActive(true);
        gameOverText.text = "Game Over";
        isGameOver = true;

        // Stop the game
        Time.timeScale = 0;
    }
}
