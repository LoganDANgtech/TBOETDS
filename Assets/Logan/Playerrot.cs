using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerrot : MonoBehaviour
{
    public Sprite personnage_0;
    public Sprite personnage_1;
    public Sprite personnage_2;
    public Sprite personnage_3;

    private Vector3 _mousePos;
    private Camera _mainCam;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //CHANGE GETCOMPONENT INTO GAMEOBJ
        if (spriteRenderer.sprite == null) 
            spriteRenderer.sprite = personnage_0; 
    }


    private void Awake()
    {
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {

        _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition); 
        Vector3 rotation = _mousePos - transform.position;
        if (rotation[1] < 0){
            spriteRenderer.sortingOrder = 1;
            if (rotation[0] > 0){
                spriteRenderer.sprite = personnage_0;
            }
            else{
                spriteRenderer.sprite = personnage_1;
            }
        }else{
            spriteRenderer.sortingOrder = 3;
            if (rotation[0] > 0){
                spriteRenderer.sprite = personnage_2;
            }
            else{
                spriteRenderer.sprite = personnage_3;
            }
        }
    }
}
