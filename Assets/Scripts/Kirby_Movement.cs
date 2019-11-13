using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_movement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float jumpHeight = 0.8f;
    public bool canFall;
    private float _turnSpeed;
    private float _verticalSpeed;
    private float _jumpSpeed;
    private float _maxJumpCooldown;
    private float _inputHorizontal;
    private float _inputVertical;
    private Kirby_actor _kirby;
    public bool _isLookingRight;
    private Quaternion _endRotation;
    private float cooldownAction;

    // Things to check:
    // - How do I rotate Smoothly;
    // - 

    void Start()
    {
        _endRotation = transform.rotation;
        _jumpSpeed = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);
        _turnSpeed = moveSpeed * 10; 
        _isLookingRight = true;
        canFall = true;
        _kirby = GetComponent<Kirby_actor>();
    }

    void Update()
    {
        jump();
        move();
        applyGravity();
    }

    // If kirby is not sucking
    // Then move
    private void move()
    {            
        if(!_kirby.isSucking)
        {
            _inputHorizontal = Input.GetAxis("Horizontal");
            Vector3 movement = _kirby.directionRight * _inputHorizontal * moveSpeed;
            movement = movement * Time.deltaTime;

            //Debug.Log(movement * 10000);
            
            if(movement.x < 0 && _isLookingRight
                || movement.x > 0 && !_isLookingRight)
            {
                turn();
            }  

            _kirby.characterController.Move(movement);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, _endRotation, Time.deltaTime * _turnSpeed);
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

    private void turn()
    {
        if(_isLookingRight)
        {
            _endRotation = Quaternion.LookRotation(_kirby.directionLeft + _kirby.directionBack * 0.0001f);
        }
        else 
        {
            _endRotation = Quaternion.LookRotation(_kirby.directionRight);
        }
        _isLookingRight = !_isLookingRight;
    }

   
    // Then apply Gravity
    private void applyGravity()
    {
        if(!_kirby.characterController.isGrounded)
        {
            _verticalSpeed += Physics.gravity.y * Time.deltaTime;
        }
        Vector3 movement = Vector3.zero;
        movement.y = _verticalSpeed * Time.deltaTime;
        _kirby.characterController.Move(movement);
    }
}
