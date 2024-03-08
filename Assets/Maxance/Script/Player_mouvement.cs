using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float _playerSpeed;

    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;

    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;

    public bool RUBBERBALL = false;
    public bool UnO = false;
    public bool KnockFromRight;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(
            _smoothedMovementInput,
            _movementInput,
            ref _movementInputSmoothVelocity,
            0.1f);
        if (KBCounter <=0)
        {
            _rigidbody.velocity = _smoothedMovementInput * _playerSpeed;
        }else{
            if(KnockFromRight == true){
                _rigidbody.velocity = new Vector2(-KBForce, 0);
            }if(KnockFromRight == false){
                _rigidbody.velocity = new Vector2(KBForce, 0);
            }
            KBCounter -= Time.deltaTime;
        }
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();

    }
}