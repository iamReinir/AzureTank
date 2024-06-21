using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Const.Tag.player).transform;
        transform.position = player.position + new Vector3(0, 1, -5);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {        
        var camera = Camera.main;
        var targetPos = player.transform.position + (camera.ScreenToWorldPoint(Input.mousePosition) - player.transform.position) * 0.5f;
        targetPos += new Vector3(0, 1, -5); // put the camera on top of target
		var vectorToTarget = targetPos - camera.transform.position;
        transform.position = transform.position + vectorToTarget * Time.deltaTime * 1.3f;			
    }
    public void BackToPlayer()
    {
		transform.position = player.position + new Vector3(0, 1, -5);
	}
}
