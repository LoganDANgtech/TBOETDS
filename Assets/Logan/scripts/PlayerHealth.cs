using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 6;
    public int health;
    public GameObject Player;
    public GameObject Weapon;
    public Animator Animator;
    private bool invincible = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!invincible)
        {
            health -= 1;
            StartCoroutine(waitInvincibility());
        }

        if (health <= 0)
        {
            Destroy(Player);
            Destroy(Weapon);
        }
    }
    public IEnumerator waitInvincibility()
    {
        invincible = true;
        Animator.SetBool("invincible",true);
        yield return new WaitForSeconds(1);
        invincible = false;
        Animator.SetBool("invincible",false);
    }
}
