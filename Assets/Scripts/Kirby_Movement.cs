using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_Movement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float turnSpeed = 200f;
    public float jumpHeight = 2f;

    private CharacterController _characterController;
    private float _verticalSpeed;
    private float _jumpSpeed;
    private float _maxJumpCooldown;
    private float _horizontalInput;
    private float _verticalInput;

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _jumpSpeed = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void jumpCheck()
    {
        if(_characterController.isGrounded && Input.GetKeyDown(Constants.keyJump))
        {
            _verticalSpeed = _jumpSpeed;   

        }

        // if (_controller.isGrounded)
        // {
        //     _verticalSpeed = 0f;

        //     if (Input.GetKeyDown(KeyCode.Space))
        //     {
        //         _verticalSpeed = _jumpSpeed;
        //         _jumpStartHeight = transform.position.y;
        //     }

        //     _maxJumpHeight = 0f;
        // }
        // else
        // {
        //     // aplicar gravidade
        //     _verticalSpeed += Physics.gravity.y * Time.deltaTime;

        //     float jumpHeight = transform.position.y - _jumpStartHeight;
        //     _maxJumpHeight = Mathf.Max(_maxJumpHeight, jumpHeight);
        //     Debug.LogFormat("i'M JUMPING YO jumpHeight:{0} _maxJumpHeight:{1}",
        //         jumpHeight, _maxJumpHeight);
        // }

        // Vector3 moveSpeed = transform.forward * verticalInput * MoveSpeed;
        // moveSpeed.y = _verticalSpeed;
        // Vector3 frameMovement = moveSpeed * Time.deltaTime;

        // _controller.Move(frameMovement);



        // //_controller.SimpleMove(moveSpeed);

        // //_controller.ju
    }
}
