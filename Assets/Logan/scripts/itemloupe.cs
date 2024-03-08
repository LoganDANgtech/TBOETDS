using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemloupe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameObject.Find("Playerweapon").transform.localScale = new Vector3(7,7,1);
        }
    }
}
