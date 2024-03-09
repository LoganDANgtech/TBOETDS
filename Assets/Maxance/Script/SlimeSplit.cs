using UnityEngine;

public class SlimeSplit : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer _spriteRenderer;
    private bool bulletBool;
    private MonsterDamage _monsterDamage;

    public bool bumped = false;
    public bool friendly = false;
    public int health;
    public int canSplit = 1;

    public int maxHealth = 10;
    public PlayerDamage playerDamage;

    // Start is called before the first frame update
    void Start()
    {
        _monsterDamage = GameObject.FindObjectOfType<MonsterDamage>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
    }
    void Split()
    {
        if (canSplit > 0)
        {
            health = 5;
            // Instantiate new slime GameObjects
            GameObject newSlime1 = Instantiate(gameObject, transform.position + new Vector3(0.1f,0,0), Quaternion.identity);
            GameObject newSlime2 = Instantiate(gameObject, transform.position + new Vector3(-0.1f,0,0), Quaternion.identity);

            // Set canSplit to false to prevent further splitting
            canSplit -= 1;

            // Set canSplit to false for the newly instantiated slimes
            newSlime1.GetComponent<SlimeSplit>().canSplit = canSplit;
            newSlime2.GetComponent<SlimeSplit>().canSplit = canSplit;
            newSlime1.GetComponent<SlimeSplit>().maxHealth = 5;
            newSlime2.GetComponent<SlimeSplit>().maxHealth = 5;
            newSlime1.GetComponent<SlimeSplit>().friendly = false;
            newSlime2.GetComponent<SlimeSplit>().friendly = false;
            newSlime1.GetComponent<SlimeSplit>().bumped = false;
            newSlime2.GetComponent<SlimeSplit>().bumped = false;
            newSlime1.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
            newSlime2.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);

            // Adjust the scale of the new slimes to make them smaller
            float scaleFactor = 0.5f; // Adjust the scale factor as needed
            newSlime1.transform.localScale *= scaleFactor;
            newSlime2.transform.localScale *= scaleFactor;

            // Destroy the current slime GameObject
            Destroy(gameObject);
        }
        if (canSplit <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Wall" && friendly)
        {
            Split();
        }
        if (collision.gameObject.tag == "ennemy" && friendly)
        {
            Destroy(collision.gameObject);
            Split();
        }
        if (collision.gameObject.tag == "barrel" && friendly)
        {
            Destroy(collision.gameObject);
            TakeDamage(5);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _monsterDamage.Feur(gameObject);
        }
        if (other.gameObject.CompareTag("Playerbatte"))
        {
            _spriteRenderer.color = new Color(0, 255, 255);
            bumped = true;
            friendly = true;
        }
        if (other.gameObject.CompareTag("Bullet") )
        {
            bulletBool = other.gameObject.GetComponent<Bulletscript>().friendly;
            if (bulletBool)
            {
                TakeDamage(5);
            }
        }
    }
    private void Update()
    {
        if (bumped)
        {
            Vector3 direction = (player.transform.position - transform.position) * -1;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * 11;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        // Vérifie si la santé est inférieure ou égale à zéro et déclenche la division
        if (health <= 0)
        {
            Split();
        }
    }


    // Implement collision or damage handling methods as needed
}
