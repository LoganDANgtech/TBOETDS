using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batteatk : MonoBehaviour
{
    public GameObject[] chargeup;
    public Animator animator;
    private bool charging = false;
    private bool charged = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            charging = true;
            animator.SetBool("ischarging", true);
            ;
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Space) && charging)
            {
                charging = false;
                animator.SetBool("ischarging", false);
            }
        }
    }
    public void AlertObservers(string message)
    {
        if (message.Equals("ChargeAnimationEnded"))
        {
            charged = true;
            animator.SetBool("charged", true);
            print("shllacamarche");
        }
    }
}
