using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemchosur : MonoBehaviour
{
    private PlayerMovement _playerMov;
    // Start is called before the first frame update
    void Awake()
    {
        _playerMov = GameObject.FindObjectOfType<PlayerMovement>();
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
            _playerMov._playerSpeed = 10;
        }
    }

}
