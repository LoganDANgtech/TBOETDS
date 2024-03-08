using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Ennemyshooting : MonoBehaviour
{
    public GameObject bullet;
    public Animator animator;
    public Transform bulletPos;

    private GameObject _player;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
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
}
