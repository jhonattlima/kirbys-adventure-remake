using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_movement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float turnSpeed;
    public float jumpHeight = 0.8f;
    public bool canFall;
    private float _turnSpeed;
    private float _verticalSpeed;
    private float _jumpSpeed;
    private float _maxJumpCooldown;
    private float _inputHorizontal;
    private float _inputVertical;
    private Vector3 movement;
    private Kirby_actor _kirby;
    private bool _isLookingRight;
    private Quaternion _endRotation;

    // Things to check:
    // - How do I rotate Smoothly;
    // - 

    void Start()
    {
        _endRotation = transform.rotation;
        _jumpSpeed = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);
        _kirby = GetComponent<Kirby_actor>();
        turnSpeed = moveSpeed * 5; 
        _isLookingRight = true;
        canFall = true;
    }

    void Update()
    {
        jump();
        move();
        applyGravity();
    }

    // If kirby is not fullOfEnemy
    // and is not sucking
    // and presses jump button
    // becomes fullOfAir
    private void jump()
    {
        if(!_kirby.isFullOfEnemy
            && !_kirby.isSucking
            && Input.GetKeyDown(KirbyConstants.KEY_JUMP))
        {
            if(_kirby.characterController.isGrounded)
            {
                _verticalSpeed = _jumpSpeed;
            }
            else
            {
                _verticalSpeed = _jumpSpeed / 1.8f;
            }  
            _kirby.isFullOfAir = true;
        }
    }

    // If kirby is not sucking
    // Then move
    private void move()
    {
        if(!_kirby.isSucking)
        {
            _inputHorizontal = Input.GetAxis("Horizontal");
            movement = Vector3.right * _inputHorizontal * moveSpeed;
            movement.y = _verticalSpeed;
            movement = movement * Time.deltaTime;
            
            if(movement.x < 0 && _isLookingRight
                || movement.x > 0 && !_isLookingRight)
            {
                turn();
            }

            _kirby.characterController.Move(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, _endRotation, Time.deltaTime * _turnSpeed);
        }
    }

    private void turn()
    {
        //Debug.Log("entered here");
        _endRotation = Quaternion.LookRotation(Vector3.left);
        _isLookingRight = !_isLookingRight;
    }

    // If kirby is in the air
    // Then apply Gravity
    private void applyGravity()
    {
        if(!_kirby.characterController.isGrounded)
        {
            _verticalSpeed += Physics.gravity.y * Time.deltaTime;
        }
    }
}
