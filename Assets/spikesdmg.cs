using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikesdmg : MonoBehaviour
{
    private GameObject player;
    private MonsterDamage _monsterDamage;
    private BoxCollider2D boxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        _monsterDamage = GameObject.FindObjectOfType<MonsterDamage>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _monsterDamage.Feur(gameObject);
        }
    }
}
