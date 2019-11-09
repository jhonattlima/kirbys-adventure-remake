using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_movement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float turnSpeed = 200f;
    public float jumpHeight = 2f;
    private float _verticalSpeed;
    private float _jumpSpeed;
    private float _maxJumpCooldown;
    private float _inputHorizontal;
    private float _inputVertical;
    private Vector3 movement;
    private Kirby_actor _kirby;
    private bool _isLookingRight;

    private Quaternion _endRotation;

    void Start()
    {
        _endRotation = transform.rotation;
        _jumpSpeed = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);
        _kirby = GetComponent<Kirby_actor>();
        _isLookingRight = true;
    }

    void Update()
    {
        jump();
        move();
        applyGravity();
        transform.rotation = Quaternion.Lerp(transform.rotation, _endRotation, Time.deltaTime * moveSpeed);
    }

    private void jump()
    {
        if(Input.GetKeyDown(KirbyConstants.keyJump))
        {
            if(_kirby.characterController.isGrounded)
            {
                _verticalSpeed = _jumpSpeed;
            }
            else
            {
                _verticalSpeed = _jumpSpeed / 2;
            }  
        }
    }

    private void move()
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
    }

    private void turn()
    {
        _isLookingRight = !_isLookingRight;

         transform.Rotate(0, 180, 0);
        _endRotation = Quaternion.Euler(0,transform.rotation.y - 180,0);
    }

    private void applyGravity()
    {
        if(!_kirby.characterController.isGrounded)
        {
            _verticalSpeed += Physics.gravity.y * Time.deltaTime;
        }
    }
}
