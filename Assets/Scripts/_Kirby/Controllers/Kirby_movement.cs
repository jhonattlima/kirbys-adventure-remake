using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Kirby_movement : NetworkBehaviour
{
    float moveSpeed = 2f;
    float flyHeight = 0.8f;
    float jumpHeight = 3f;
    float _turnSpeed;
    float _verticalSpeed;
    float _flySpeed;
    float _jumpSpeed;
    float _inputHorizontal;
    float _inputVertical;
    Quaternion _endRotation;
    Kirby_actor _kirby;
    // Used to handle animationsS
    public bool isFlying;
    public bool isWalking;
    public bool isJumping;
    public bool isFalling;
    public bool isGrounded;
    Vector3 movement;

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

        movement = Vector3.zero;
        if (!_kirby.isParalyzed && !_kirby.isSucking)
        {
            fly();
            move();
            jump();
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, _endRotation, Time.deltaTime * _turnSpeed);
        applyGravity();
        _kirby.characterController.Move(movement);
        isGrounded = _kirby.characterController.isGrounded;
        UpdateFlags();
    }

    // If kirby is not sucking
    // Then move
    private void move()
    {
        _inputHorizontal = Input.GetAxis("Horizontal");
        movement += _kirby.directionRight * _inputHorizontal * moveSpeed * Time.deltaTime;
        turn(_inputHorizontal);
        if (_kirby.characterController.isGrounded && movement.x != 0 && !isWalking)
        {
            isWalking = true;
            AudioPlayerSFXController.instance.play(AudioPlayerSFXController.instance.kirbyWalk);
        }
        else if( (!_kirby.characterController.isGrounded || movement.x == 0) && isWalking)
        {
            isWalking = false;
        }
    }

    // If kirby is not fullOfEnemy
    // and is not sucking
    // and presses fly button
    // becomes fullOfAir
    private void fly()
    {
        if (!_kirby.isFullOfEnemy
            && Input.GetKeyDown(KirbyConstants.KEY_FLY))
        {
            if (_kirby.characterController.isGrounded)
            {
                _verticalSpeed = _flySpeed;
            }
            else
            {
                _verticalSpeed = _flySpeed / 1.8f;
            }
            _kirby.isFullOfAir = true;
            isFlying = true;
            AudioPlayerSFXController.instance.play(AudioPlayerSFXController.instance.kirbyFly);
        }
    }

    private void jump()
    {
        if (!_kirby.isFullOfEnemy
            && !_kirby.isFullOfAir
            && Input.GetKeyDown(KirbyConstants.KEY_JUMP))
        {
            if (_kirby.characterController.isGrounded)
            {
                _verticalSpeed = _jumpSpeed;
                isJumping = true;
                //_kirby.kirbyServerController.playAtPoint(this.gameObject, AudioPlayerSFXController.SFX_NAME_KIRBY_JUMP);
                AudioPlayerSFXController.instance.play(AudioPlayerSFXController.instance.kirbyJump);
            }
        }
    }

    private void turn(float movement)
    {
        if (movement < -0.01f)
        {
            _endRotation = Quaternion.LookRotation(_kirby.directionLeft + _kirby.directionBack * 0.0001f);
            if (_kirby.isLookingRight) _kirby.isLookingRight = !_kirby.isLookingRight;
        }
        else if (movement > 0.01f)
        {
            _endRotation = Quaternion.LookRotation(_kirby.directionRight);
            if (!_kirby.isLookingRight) _kirby.isLookingRight = !_kirby.isLookingRight;
        }
    }

    private void applyGravity()
    {
        _verticalSpeed += Physics.gravity.y * Time.deltaTime;
        movement.y += _verticalSpeed * Time.deltaTime;
    }

    private void UpdateFlags()
    {
        if (_kirby.characterController.isGrounded)
        {
            _verticalSpeed = 0;
        }

        if (_kirby.isFullOfAir && !_kirby.characterController.isGrounded)
        {
            isFlying = true;
            isJumping = false;
            isFalling = false;
        }
        else
        {
            isFlying = false;
            if (movement.y < 0 && !_kirby.characterController.isGrounded)
            {
                isJumping = false;
                isFalling = true;
            }
            else if (movement.y > 0)
            {
                isJumping = true;
                isFalling = false;
            }
        }

        if (_kirby.characterController.isGrounded)
        {
            isJumping = false;
            isFalling = false;
            isFlying = false;
        }
    }
}
