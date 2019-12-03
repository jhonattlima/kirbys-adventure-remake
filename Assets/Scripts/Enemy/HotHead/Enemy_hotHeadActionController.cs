using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy_hotHeadActionController : NetworkBehaviour 
{
    private float _moveSpeed = 0.5f;
    private float _verticalSpeed;
    private bool _isKirbyClose = false;
    private bool _FSMIsRunning = false;
    private bool _isMoving = false;
    private CharacterController characterController;
    private Enemy_actor _enemy;
    Vector3 movement;


    void Start()
    {
        _enemy = GetComponent<Enemy_actor>();
        characterController = GetComponent<CharacterController>();
        characterController.Move(Vector3.zero);
    }

    void Update()
    {
        if(!isServer || _enemy.healthController.died) return;
        if(!_FSMIsRunning) StartCoroutine(chooseRandomAction());
        if(_isMoving)
        {
            characterController.Move(movement * _moveSpeed * Time.deltaTime);
            _enemy.animator.SetBool(KirbyConstants.ANIM_ENEMY_WALK, true);
        } 
        else _enemy.animator.SetBool(KirbyConstants.ANIM_ENEMY_WALK, false);
        applyGravity();
    }

    private void setMove()
    {
        transform.LookAt(GameManager.instance.figureOutClosestPlayer(this.transform));
        movement = transform.TransformDirection(Vector3.forward);
        _isMoving = true;
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
            case (int)ActionsFireHead.walk:
                setMove();
                break;
            case (int)ActionsFireHead.fire:
                if(_enemy.isKirbyClose)
                {
                    transform.LookAt(GameManager.instance.figureOutClosestPlayer(this.transform));
                    _isMoving = false;
                    _enemy.animator.SetTrigger(KirbyConstants.ANIM_ENEMY_ATTACK);
                }
                else
                {
                    setMove();
                }
                break;
        }
        yield return new WaitForSeconds(3);
        _FSMIsRunning = false;
    }
}

enum ActionsFireHead
{
    walk,
    fire
}

