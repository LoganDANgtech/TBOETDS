using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public int damage = 1;
    private PlayerHealth playerHealth;
    private PlayerMovement _PlayerMovement;

    private void Awake()
    {
        _PlayerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
    }
    public void Feur(GameObject collision)
    {
        _PlayerMovement.Knockback(collision.transform.position);
        playerHealth.TakeDamage(damage);
    }
}
