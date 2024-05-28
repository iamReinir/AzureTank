using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
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
        collision.gameObject.SendMessage("apply_dmg", 100);
        Destroy(gameObject);
    }
}
