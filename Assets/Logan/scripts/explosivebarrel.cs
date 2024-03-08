using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class explosivebarrel : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private Animator animator;
    public float force = 10;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }
    void Boom()
    {
        animator.SetBool("bumped", true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Playerbatte"))
        {
            force = 8;
            Vector3 direction = (player.transform.position - transform.position) * -1;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

            float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot - 180);
            Boom();
        }
        if (other.gameObject.CompareTag("Wall"))
        {

            Vector2 contactPoint = other.ClosestPoint(transform.position);
            Vector2 contactNormal = ((Vector2)transform.position - contactPoint).normalized;

            rb.velocity = Vector2.Reflect(rb.velocity, contactNormal).normalized * force;

            float rotz = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotz);

        }
    }
    public void AlertObservers(string message)
    {
        if (message.Equals("explode"))
        { 
            Destroy(gameObject);
        }

    }
}

