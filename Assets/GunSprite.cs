using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GunSprite : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject body;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mouse_pos = Input.mousePosition;        
        var object_pos = Camera.main.WorldToScreenPoint(body.transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        var angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.position = body.transform.position + Mouse_dir(new Vector2(mouse_pos.x, mouse_pos.y));    
    }

    Vector3 Pos()
    {
        return new Vector3(body.transform.position.x,body.transform.position.y);

    }
    Vector3 Mouse_dir(Vector3 target)
    {
        var self = Pos();
        var dir = (target - self);
        return dir / dir.magnitude;

    }


}
