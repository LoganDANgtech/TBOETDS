using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Ennemyshooting : MonoBehaviour
{
    public GameObject bullet;
    public Animator animator;
    public Transform bulletPos;
    public int health;
    public int maxHealth = 5;
    public PlayerDamage playerDamage;
    public bool bumped = false;
    public bool friendly = false;

    private Rigidbody2D rb;
    private bool bulletBool;
    private GameObject _player;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        System.Random random = new System.Random();
        timer = random.Next(0,150) / 100f;
        _player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
        if (timer > 2)
        {
            timer = 0;
            animator.SetBool("shooting", true);
        }
        Vector3 rotation = _player.transform.position - transform.position;
        if (rotation[0] < 0)
        {
            if (transform.rotation[1] != 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            if (transform.rotation[1] == 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        if (bumped)
        {
            Vector3 direction = (_player.transform.position - transform.position) * -1;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * 11;
        }
    }
    public void AlertObservers(string message)
    {
        if (message.Equals("shootingended"))
        {
            animator.SetBool("shooting", false);
            Shoot();
        }
    }
    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" && friendly)
        {
            TakeDamage(5);
        }
        if (collision.gameObject.tag == "ennemy" && friendly)
        {
            Destroy(collision.gameObject);
            TakeDamage(5);
        }
        if (collision.gameObject.tag == "barrel" && friendly)
        {
            Destroy(collision.gameObject);
            TakeDamage(5);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Playerbatte"))
        {
            bumped = true;
            friendly = true;
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            bulletBool = other.gameObject.GetComponent<Bulletscript>().friendly;
            if (bulletBool)
            {
                TakeDamage(5);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        // Vérifie si la santé est inférieure ou égale à zéro et déclenche la division
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
