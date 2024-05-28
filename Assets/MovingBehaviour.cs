using UnityEngine;

public class MovingBehaviour : MonoBehaviour
{
    public const string toMove = "Moving";

    Rigidbody2D rig;
    public void Moving(Vector2 direction)
    {       
        var facing_dir = rig.velocity;
        var angle = Mathf.Atan2(facing_dir.y, facing_dir.x) * Mathf.Rad2Deg;
        rig.MoveRotation(Quaternion.Euler(new Vector3(0, 0, angle)));
        rig.AddForce(direction);        
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }
}

