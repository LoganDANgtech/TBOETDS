using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    public float _playerSpeed;

    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    private Camera _mainCam;
    private Vector3 _mousePos;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        /*_camera = Camera.main;
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.LookAt(mouseWorldPosition);*/
        
        _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = _mousePos - transform.position;
        float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, rotz);

        /*_mousePos.x = 0f;
        _mousePos.y = 0f;
        transform.LookAt(_mousePos);*/


        _smoothedMovementInput = Vector2.SmoothDamp(
            _smoothedMovementInput,
            _movementInput,
            ref _movementInputSmoothVelocity,
            0.1f);

        _rigidbody.velocity = _smoothedMovementInput * _playerSpeed;


    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();

    }
}