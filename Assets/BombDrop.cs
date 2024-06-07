using UnityEngine;
using System;
using Const;

public class BombDrop : MonoBehaviour
{
    // Components
    public GameObject bomb_prefab;
    public GameObject bomb_target;
    private CommonHelper helper;

    const float drop_radius = 6;
    const int cd = 500;
    int cur_cd = cd;

    Vector3 RotateAroundAxis(Vector3 vector, Vector3 axis, float angle)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);
        return rotation * vector;
    }
    Vector3 axis = new Vector3(0,0,1);

    private void Start()
    {
        helper = new CommonHelper();
    }

    // Update is called once per frame
    void Update()
    {
        if (bomb_target == null ) return;
        cur_cd--;
        if (cur_cd < 0)
        {
            Vector3 pos = bomb_target.transform.position 
                        + helper.Rand() * drop_radius * Vector3.right;
            Vector3 rotatedVector = RotateAroundAxis(pos, axis, 360f * helper.Rand());
            Instantiate(bomb_prefab, rotatedVector, Quaternion.identity);
            cur_cd = (int)(helper.Rand() * (float)cd);
        }
    }
}
