using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Kirby_movement : NetworkBehaviour
{
    public float moveSpeed = 2f;
    public float jumpHeight = 0.8f;
    private float _turnSpeed;
    private float _verticalSpeed;
    private float _jumpSpeed;
    private float _maxJumpCooldown;
    private float _inputHorizontal;
    private float _inputVertical;
    private Quaternion _endRotation;
    private float cooldownAction;
    private Kirby_actor _kirby;

    void Start()
    {
        _kirby = GetComponent<Kirby_actor>();
        _endRotation = transform.rotation;
        _jumpSpeed = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);
        _turnSpeed = moveSpeed * 10; 
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        if(!_kirby.isParalyzed && !_kirby.isSucking)
        {
            jump();
            move();
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
        
        if(movement.x < 0 && _kirby.isLookingRight
            || movement.x > 0 && !_kirby.isLookingRight)
        {
            turn();
        }  
        _kirby.characterController.Move(movement);
    }

    // If kirby is not fullOfEnemy
    // and is not sucking
    // and presses jump button
    // becomes fullOfAir
    private void jump()
    {
        if(!_kirby.isFullOfEnemy
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
        if(_kirby.isLookingRight)
        {
            _endRotation = Quaternion.LookRotation(_kirby.directionLeft +_kirby. directionBack * 0.0001f);
        }
        else 
        {
            _endRotation = Quaternion.LookRotation(_kirby.directionRight);
        }
        _kirby.isLookingRight = !_kirby.isLookingRight;
    }

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
