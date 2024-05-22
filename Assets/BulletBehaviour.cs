using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    CircleCollider2D collider;
    float live_time = 3;
    void Start()
    {
        Destroy(gameObject, live_time);
    }

    // Update is called once per frame
    void Update()
    {        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colliding");
        collision.gameObject.SendMessage("apply_dmg", 100);
        Destroy(gameObject);
    }
}
