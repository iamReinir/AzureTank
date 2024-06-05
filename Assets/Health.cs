using UnityEngine;

// 
class Health : MonoBehaviour
{
    public int health;
    public int max_health;
    public bool isDead = false;

    GameObject HP_bar;

    static Sprite CreateSquareSprite(int width, int height, Color color)
    {
        Texture2D texture = new Texture2D(width, height);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        Rect rect = new Rect(0, 0, width, height);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        return Sprite.Create(texture, rect, pivot);
    }
    static GameObject healthBar()
    {
        var healthBar = new GameObject("healthBar");
        healthBar.transform.localScale = Vector3.one;
        var sprite = healthBar.AddComponent<SpriteRenderer>();
        // can turn this into a suitable sprite
        sprite.sprite = CreateSquareSprite(150, 20, Color.blue);        
        sprite.sortingOrder = 3;
        return healthBar;
    }

    private void Start()
    {
        HP_bar = healthBar();
        max_health = GetComponent<PlayerBehaviour>().HP;
    }

    private void Update()
    {
        // Not supposed to do this, but need a value to work with.
        // Will need to refactor the HP behaviour
        health = GetComponent<PlayerBehaviour>().HP;
        HP_bar.transform.position = gameObject.transform.position + Vector3.down;
        HP_bar.transform.localScale = new Vector3(health / (float)max_health,1,1);
        Debug.Log(HP_bar.transform.localScale);

    }

    public void DealDmg(int dmg)
    {        
        health -= dmg;
        if(health < 0)
        {
            isDead = true;
            health = 0;
        }
    }
}