using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletscript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer _spriteRenderer;
    private float timer;

    public int bounce = 1;
    public float force = 5;
    public bool friendly = false;

    private PlayerMovement _playM;
    private MonsterDamage _monsterDamage;


    private void Awake()
    {
        _playM = GameObject.FindObjectOfType<PlayerMovement>();
        _monsterDamage = GameObject.FindObjectOfType<MonsterDamage>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 15)
        {
            timer = 0;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Playerbatte"))
        {
            if (_playM.RUBBERBALL)
            {
                bounce += 3;
            }
            if (_playM.UnO)
            {
                gameObject.transform.localScale = new Vector3(5, 5, 1);
            }
            force = 15;
            Vector3 direction = (player.transform.position - transform.position) * -1;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
            _spriteRenderer.color = new Color(0,255,255);
            friendly = true;

            

            float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot);
        }

        if (other.gameObject.CompareTag("Player") && !friendly)
        {
            _monsterDamage.Feur(gameObject);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("ennemy") && friendly) {
            
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            if (bounce > 0)
            {
                bounce--;

                Vector2 contactPoint = other.ClosestPoint(transform.position);
                Vector2 contactNormal = ((Vector2)transform.position - contactPoint).normalized;

                rb.velocity = Vector2.Reflect(rb.velocity, contactNormal).normalized * force;

                float rotz = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotz -180f);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}