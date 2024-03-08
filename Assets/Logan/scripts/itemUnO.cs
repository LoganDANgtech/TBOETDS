using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemUnO : MonoBehaviour
{
    private PlayerMovement _playM;
    // Start is called before the first frame update
    void Awake()
    {
        _playM = GameObject.FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playM.UnO = true;
            Destroy(gameObject);
        }
    }

}
