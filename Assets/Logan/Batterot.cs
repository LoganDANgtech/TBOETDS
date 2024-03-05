using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Batterot : MonoBehaviour
{
    private Vector3 _mousePos;
    private Camera _mainCam;
    private SpriteRenderer spriteRenderer;
    private Transform hitboxTransform;
    private void Awake()
    {
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hitboxTransform = transform.Find("battehitbox");
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
                hitboxTransform.Rotate(-180,0,0);
                spriteRenderer.flipY = false;
            }
        }
        else
        {
            if (!spriteRenderer.flipY)
            {
                hitboxTransform.Rotate(-180, 0, 0);
                spriteRenderer.flipY = true;
            }
        }

    }
}
