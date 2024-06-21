using UnityEngine;
using UnityEngine.SceneManagement;

class NextLevelCheckPoint : MonoBehaviour
{
    public string next_level = Const.Scence.CHAP1_2;
    public Vector3 next_level_position = Vector3.zero;
	
	private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.CompareTag(Const.Tag.player)) {
			var player = collision.gameObject.transform;
			FindAnyObjectByType<HeadUpDisplay>().FadeThenUnfade(() =>
			{
				SceneManager.LoadScene(next_level, LoadSceneMode.Single);
				player.SetLocalPositionAndRotation(next_level_position, Quaternion.identity);
				FindAnyObjectByType<FollowPlayer>().BackToPlayer();
			});							
        }
    }
}
