using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public int damage;
    private PlayerHealth playerHealth;
    private PlayerMovement _PlayerMovement;

    private void Awake()
    {
        _PlayerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
    }
    public void Feur(GameObject collision)
    {
        _PlayerMovement.KBCounter = _PlayerMovement.KBTotalTime;
        if(collision.transform.position.x <= transform.position.x)
        {
            _PlayerMovement.KnockFromRight = true;
        }
        if(collision.transform.position.x > transform.position.x)
        {
            _PlayerMovement.KnockFromRight = false;
        }
        playerHealth.TakeDamage(damage);
    }
}
