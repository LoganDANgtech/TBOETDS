using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Batteatk : MonoBehaviour
{
    public GameObject[] chargeup;
    public Animator animator;
    public CapsuleCollider2D battecollider;
    private bool charged = false;

    private void Start()
    {
        battecollider = GetComponent<CapsuleCollider2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("ischarging", true);
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (charged){
                    animator.SetBool("released", true);
                }
                else{
                    charged = false;
                    animator.SetBool("ischarging", false);
                }
                
            }
        }
    }
    public void AlertObservers(string message)
    {
        if (message.Equals("ChargeAnimationEnded"))
        {
            charged = true;
            animator.SetBool("charged", true);
        }
        if (message.Equals("animended"))
        {
            charged = false;
            animator.SetBool("released", false);
            animator.SetBool("charged", false);
            animator.SetBool("ischarging", false);
        }
    }
}
