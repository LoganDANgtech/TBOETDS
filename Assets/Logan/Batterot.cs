using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batterot : MonoBehaviour
{
    private Vector3 _mousePos;
    private Camera _mainCam;
    private bool inverted;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = _mousePos - transform.position;
        float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotz); 
        if (rotation[0] > 0)
        {
            if (spriteRenderer.flipY)
            {
                spriteRenderer.flipY = false;
            }
        }
        else
        {
            if (!spriteRenderer.flipY)
            {
                spriteRenderer.flipY = true;
            }
        }

    }
}
