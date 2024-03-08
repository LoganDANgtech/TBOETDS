using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    public GameObject Player;
    public Vector3 decalage;

    void Start()
    {
        decalage = new Vector3((float)0.7, 0, 0);
    }

    void Update()
    {
        transform.position = Player.transform.position + decalage;
    }
}
