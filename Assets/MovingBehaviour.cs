using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovingBehaviour : MonoBehaviour
{
    public float rotation_speed = 10f;
    public float moving_speed = 1000;


    Rigidbody2D rig;
    float currentAngle = 0f;
    public void Moving(Vector2 direction, bool heading = true)
    {
        var desired_angle = Vector2.SignedAngle(Vector2.right, direction);
        if(heading)
            Facing(desired_angle);
        var dir = direction / (direction.magnitude == 0 ? 1 : direction.magnitude);
        rig.AddForce(dir * rig.mass * moving_speed * Time.deltaTime);
    }
       
    public void HeadingToward(Vector2 to, bool heading = true)
    {
        var move_dir = (to - rig.position);
        var dir = move_dir / (move_dir.magnitude == 0 ? 1 : move_dir.magnitude);
        Moving(moving_speed * rig.mass * dir,heading);
    }

    public void MovingAwayFrom(Vector2 target, bool heading = true)
    {
        var move_dir = - (target - rig.position);
        var dir = move_dir / (move_dir.magnitude == 0 ? 1 : move_dir.magnitude);
        Moving(moving_speed * rig.mass * dir,heading);
    }

    public void Facing(float desired_angle)
    {       
        var result_angle = (desired_angle - currentAngle);
        if (result_angle > 180f) result_angle -= 360f;
        if (result_angle < -180f) result_angle += 360f;
        currentAngle += result_angle * rotation_speed * Time.deltaTime;
        rig.MoveRotation(currentAngle);
    }

    public void Facing(Vector2 target)
    {
        var dir_vect = target - rig.position;
        var desired_angle = Vector2.SignedAngle(Vector2.right, dir_vect);
        Facing(desired_angle);
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }
}

