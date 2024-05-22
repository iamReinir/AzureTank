using System;
using UnityEngine;
using UnityEngine.UIElements;

public class KeyboardControll : MonoBehaviour
{
    const KeyCode left = KeyCode.A;
    const KeyCode right = KeyCode.D;
    const KeyCode up = KeyCode.W;
    const KeyCode down = KeyCode.S;
    const int force = 3;
    Rigidbody2D rig;
    public GameObject myPrefab;
    public GameObject hp_overlay;
    const int bullet_speed = 600;
    public int Max_HP { get; set; } = 600;
    public int HP { get; set; } = 600;

    
    // Start is called before the first frame update
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    public void apply_dmg(int amount)
    {
        HP -= amount;
    }
    void Update()
    {
        hp_overlay.SendMessage("display", $"HP : {Max_HP}/{HP}");
        if (Input.GetKey(left))
        {
            rig.AddForce(new Vector3
            {
                x = -force,
                y = 0                
            });
        }
        if (Input.GetKey(right))
        {
            rig.AddForce(new Vector3
            {
                x = force,
                y = 0                
            });
        }
        if (Input.GetKey(up))
        {
            rig.AddForce(new Vector3
            {
                x = 0,
                y = force                
            });
        }
        if (Input.GetKey(down))
        {
            rig.AddForce(new Vector3
            {
                x = 0,
                y = -force                
            });
        }
        if(Input.GetMouseButtonDown(0))
        {
            var shoot_dir = ShootingDirection();                   
            var bullet = Instantiate(myPrefab, Pos() + shoot_dir * 3, Quaternion.identity);
            var bullet_rigid = bullet.GetComponent<Rigidbody2D>();
            bullet_rigid.AddForce(shoot_dir * bullet_speed);
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
}
