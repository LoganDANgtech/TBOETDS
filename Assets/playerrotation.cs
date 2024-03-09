using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerrot : MonoBehaviour
{

    private Vector3 _mousePos;
    private Camera _mainCam;
    private SpriteRenderer spriteRenderer;
    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }


    private void Awake()
    {
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {

        _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = _mousePos - transform.position;
        if (rotation[1] < 0)
        {
            animator.SetBool("back", false);
            if (rotation[0] > 0)
            {
                if (spriteRenderer.flipX)
                { spriteRenderer.flipX = false; }
            }
            else
            {
                if (!spriteRenderer.flipX)
                { spriteRenderer.flipX = true; }
            }
        }
        else
        {
            animator.SetBool("back", true);

            if (rotation[0] > 0)
            {
                if (spriteRenderer.flipX)
                { spriteRenderer.flipX = false; }
            }
            else
            {
                if (!spriteRenderer.flipX)
                { spriteRenderer.flipX = true; }
            }
        }
    }
}

