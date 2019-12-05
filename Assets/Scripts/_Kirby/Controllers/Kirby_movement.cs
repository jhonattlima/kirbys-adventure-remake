using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Kirby_movement : NetworkBehaviour
{
    private float moveSpeed = 2f;
    private float flyHeight = 0.8f;
    private float jumpHeight = 3f;
    private float _turnSpeed;
    private float _verticalSpeed;
    private float _flySpeed;
    private float _jumpSpeed;
    private float _maxflyCooldown;
    private float _inputHorizontal;
    private float _inputVertical;
    private Quaternion _endRotation;
    private float cooldownAction;
    private Kirby_actor _kirby;

    // Used to handle animationsS
    public bool isFlying;
    public bool isWalking;
    public bool isJumping;
    public bool isFalling;

    void Start()
    {
        _kirby = GetComponent<Kirby_actor>();
        _endRotation = transform.rotation;
        _flySpeed = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * flyHeight);
        _jumpSpeed = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);
        _turnSpeed = moveSpeed * 10; 
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        if(!_kirby.isParalyzed && !_kirby.isSucking)
        {
            fly();
            move();
            jump();
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, _endRotation, Time.deltaTime * _turnSpeed);
        applyGravity();
    }

    // If kirby is not sucking
    // Then move
    private void move()
    {            
        _inputHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = _kirby.directionRight * _inputHorizontal * moveSpeed * Time.deltaTime;
        turn(_inputHorizontal);
        if(_kirby.characterController.isGrounded &&  movement.x != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        _kirby.characterController.Move(movement);
    }

    // If kirby is not fullOfEnemy
    // and is not sucking
    // and presses fly button
    // becomes fullOfAir
    private void fly()
    {
        if(!_kirby.isFullOfEnemy
            && Input.GetKey(KirbyConstants.KEY_FLY))
        {
            if(_kirby.characterController.isGrounded)
            {
                _verticalSpeed = _flySpeed;
            }
            else
            {
                _verticalSpeed = _flySpeed / 1.8f;
            }  
            _kirby.isFullOfAir = true;
            isFlying = true;
        }
    }

    private void jump()
    {
        if(!_kirby.isFullOfEnemy
            && !_kirby.isFullOfAir
            && Input.GetKeyDown(KirbyConstants.KEY_JUMP))
        {
            if(_kirby.characterController.isGrounded)
            {
                _verticalSpeed = _jumpSpeed;
                isJumping = true;
            }
        }
    }

    private void turn(float movement)
    {
        if(movement < -0.01f)
        {
            _endRotation = Quaternion.LookRotation(_kirby.directionLeft +_kirby.directionBack * 0.0001f);
            if(_kirby.isLookingRight) _kirby.isLookingRight = !_kirby.isLookingRight;
        }
        else if(movement > 0.01f)
        {
            _endRotation = Quaternion.LookRotation(_kirby.directionRight);
            if(!_kirby.isLookingRight) _kirby.isLookingRight = !_kirby.isLookingRight;
        }
    }

    private void applyGravity()
    {
        if(!_kirby.characterController.isGrounded)
        {
            _verticalSpeed += Physics.gravity.y * Time.deltaTime;
        }
        Vector3 movement = Vector3.zero;
        movement.y = _verticalSpeed * Time.deltaTime;

        if(_kirby.isFullOfAir)
        {
            isFlying = true;
            isJumping = false;
            isFalling = false;
        }
        else 
        {
            isFlying = false;
            if(movement.y < 0 && !_kirby.characterController.isGrounded)
            {
                isJumping = false;
                isFalling = true;
            } 
            else if (movement.y > 0 )
            {
                isJumping = true;
                isFalling = false;
            } 
        }

        _kirby.characterController.Move(movement);

        if (_kirby.characterController.isGrounded)
        {
            isJumping = false;
            isFalling = false;   
            isFlying = false;
        }
    }
}
