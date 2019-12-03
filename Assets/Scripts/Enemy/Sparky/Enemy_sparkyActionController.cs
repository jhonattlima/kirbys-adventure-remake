using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy_sparkyActionController : NetworkBehaviour 
{
    private float _jumpHeight = 0.5f;
    private float _moveSpeed = 1f;
    private float _jumpSpeed;
    private float _verticalSpeed;
    private bool _isKirbyClose = false;
    private bool _FSMIsRunning = false;

    private Enemy_actor _enemy;
    public SphereCollider kirbyDetector;
    private CharacterController characterController;

    // This character jumps in closest kirby position and turns shock on if kirby is in range
    void Start()
    {
        _enemy = GetComponent<Enemy_actor>();
        characterController = GetComponent<CharacterController>();
        characterController.Move(Vector3.zero);
        _jumpSpeed = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * _jumpHeight);
    }

    void Update()
    {
        if(!isServer || _enemy.healthController.died) return;
        if(!_FSMIsRunning) StartCoroutine(chooseRandomAction());
        move();
        applyGravity();
        if(characterController.isGrounded) _enemy.animator.SetBool(KirbyConstants.ANIM_ENEMY_JUMP, false);
        else _enemy.animator.SetBool(KirbyConstants.ANIM_ENEMY_JUMP, true);
    }

    // Jump in direction of the closest kirby
    private void jump()
    {
        if(characterController.isGrounded){
            _verticalSpeed  = _jumpSpeed;   
            _enemy.animator.SetTrigger(KirbyConstants.ANIM_ENEMY_JUMP);
        } 
    }

    private void move()
    {
        if(characterController.isGrounded) return;
        Transform closestPlayer = GameManager.instance.figureOutClosestPlayer(this.transform);
        transform.LookAt(closestPlayer);
        Vector3 movement = transform.TransformDirection(Vector3.forward);
        characterController.Move(movement * _moveSpeed * Time.deltaTime);
    }

    private void applyGravity()
    {
        if(!characterController.isGrounded) _verticalSpeed += Physics.gravity.y * Time.deltaTime;
        Vector3 movement = Vector3.zero;
        movement.y = _verticalSpeed * Time.deltaTime;
        characterController.Move(movement);
    }

    IEnumerator chooseRandomAction()
    {
        _FSMIsRunning = true;
        yield return new WaitUntil(()=> characterController.isGrounded);
        switch (Random.Range(0, 2))
        {
            case (int)ActionsSparky.jump:
                jump();
                break;
            case (int)ActionsSparky.shock:
                if(_enemy.isKirbyClose)
                {
                    transform.LookAt(Camera.main.transform);
                    _enemy.animator.SetTrigger(KirbyConstants.ANIM_ENEMY_ATTACK);
                }
                else jump();
                break;
        }
        yield return new WaitForSeconds(3);
        _FSMIsRunning = false;
    }
}

enum ActionsSparky
{
    jump,
    shock
}
