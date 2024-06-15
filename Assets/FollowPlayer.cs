using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Const.Tag.player).transform;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 1, -5);
    }
}
