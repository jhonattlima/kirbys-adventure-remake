using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_movement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float turnSpeed = 200f;
    public float jumpHeight = 2f;

    private CharacterController _characterController;
    private float _verticalSpeed;
    private float _jumpSpeed;
    private float _maxJumpCooldown;
    private float _inputHorizontal;
    private float _inputVertical;
    private Vector3 movement;

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _jumpSpeed = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);
    }

    void Update()
    {
        jump();
        move();
        applyGravity();
    }

    private void jump()
    {
        if(Input.GetKeyDown(KirbyConstants.keyJump))
        {
            if(_characterController.isGrounded)
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
        movement = transform.right * _inputHorizontal * moveSpeed;
        movement.y = _verticalSpeed;
        movement = movement * Time.deltaTime;
        _characterController.Move(movement);
    }

    private void applyGravity()
    {
        if(!_characterController.isGrounded)
        {
            _verticalSpeed += Physics.gravity.y * Time.deltaTime;
        }
    }
}
