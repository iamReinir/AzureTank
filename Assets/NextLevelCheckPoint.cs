using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

class NextLevelCheckPoint : MonoBehaviour
{
    public string next_level = Const.Scence.CHAP1_2;
    public Vector3 next_level_position = Vector3.zero;
    void Start()
    {        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Const.Tag.player)) {
            collision.gameObject.transform.SetLocalPositionAndRotation(next_level_position, Quaternion.identity);
            SceneManager.LoadSceneAsync(next_level, LoadSceneMode.Single);
            
        }       
    }
    private void Update()
    {

    }
}
