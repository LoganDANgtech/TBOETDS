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
    private Vector2 _smoothedMovementInputKBed;
    private Vector2 _movementInputKBedSmoothVelocity;
    private Vector2 _kb;
    private float timer;
    private float décompte = 0;

    public bool RUBBERBALL = false;
    public bool UnO = false;
    public bool KnockFromRight;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput,_movementInput,ref _movementInputSmoothVelocity,0.1f);
        _smoothedMovementInputKBed = Vector2.SmoothDamp(_smoothedMovementInput, _kb, ref _movementInputKBedSmoothVelocity, 0.1f);
        _rigidbody.velocity = _smoothedMovementInputKBed * _playerSpeed;
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }
    public void Knockback(Vector3 collisioneur)
    {
        Vector3 direction = (transform.position - collisioneur) * 1;
        décompte = 1;
        _kb = new Vector2(direction.x, direction.y).normalized * 4 * décompte;
    }
    private void Update()
    {
        if (décompte > 0)
        {
            timer += Time.deltaTime;
            décompte = 1 - timer;
            _kb *= décompte;
        }
        else
        {
            timer = 0;
        }
    }
}