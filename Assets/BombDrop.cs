using UnityEngine;
using System;

public class BombDrop : MonoBehaviour
{
    // Components
    public GameObject? bomb_prefab;
    public GameObject? bomb_target;

    const float drop_radius = 6;
    const int cd = 500;
    int cur_cd = cd;


    // Ultility
    System.Random random = new System.Random();
    float Rand()
    {
        return ((float)random.NextDouble());
    }
    Vector3 RotateAroundAxis(Vector3 vector, Vector3 axis, float angle)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);
        return rotation * vector;
    }
    Vector3 axis = new Vector3(0,0,1);

    // Update is called once per frame
    void Update()
    {
        if (bomb_target == null ) return;
        cur_cd--;
        if (cur_cd < 0)
        {
            Vector3 pos = bomb_target.transform.position + Vector3.right * drop_radius * Rand();
            Vector3 rotatedVector = RotateAroundAxis(pos, axis, 360f * Rand());
            Instantiate(bomb_prefab, rotatedVector, Quaternion.identity);
            cur_cd = (int)(Rand() * (float)cd);
        }
    }
}
