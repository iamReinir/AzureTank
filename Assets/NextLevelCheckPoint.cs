using UnityEngine;
using UnityEngine.SceneManagement;

class NextLevelCheckPoint : MonoBehaviour
{
    public string next_level = Const.Scence.CHAP1_2;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Const.Tag.player)) {
            SceneManager.LoadSceneAsync(next_level);
        }
    }
}
