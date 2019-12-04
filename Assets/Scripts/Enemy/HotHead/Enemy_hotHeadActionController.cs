using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy_hotHeadActionController : NetworkBehaviour 
{
    float _moveSpeed = 0.5f;
    float _verticalSpeed;
    bool _isKirbyClose = false;
    bool _FSMIsRunning = false;
    bool _isMoving = false;
    Enemy_actor _enemy;
    Vector3 movement;

    void Start()
    {
        _enemy = GetComponent<Enemy_actor>();
        _enemy.characterController.Move(Vector3.zero);
    }

    void Update()
    {
        if(!isServer || _enemy.healthController.died) return;
        if(!_FSMIsRunning) StartCoroutine(chooseRandomAction());
        if(_isMoving)
        {
            _enemy.characterController.Move(movement * _moveSpeed * Time.deltaTime);
            _enemy.animator.SetBool(KirbyConstants.ANIM_ENEMY_WALK, true);
        } 
        else _enemy.animator.SetBool(KirbyConstants.ANIM_ENEMY_WALK, false);
        applyGravity();
    }

    private void setMove()
    {
        Vector3 mewLookPosition = GameManager.instance.figureOutClosestPlayer(this.transform).position;
        mewLookPosition.y = transform.position.y;
        transform.LookAt(mewLookPosition);
        movement = transform.TransformDirection(Vector3.forward);
        _isMoving = true;
    }

    private void applyGravity()
    {
        if(!_enemy.characterController.isGrounded) _verticalSpeed += Physics.gravity.y * Time.deltaTime;
        Vector3 movement = Vector3.zero;
        movement.y = _verticalSpeed * Time.deltaTime;
        _enemy.characterController.Move(movement);
    }

    IEnumerator chooseRandomAction()
    {
        _FSMIsRunning = true;
        yield return new WaitUntil(()=> _enemy.characterController.isGrounded);

        ActionsFireHead nextAction = (ActionsFireHead)Random.Range(0, 2);
        switch (nextAction)
        {
            case ActionsFireHead.walk:
                setMove();
                break;
            case ActionsFireHead.fire:
                if(_enemy.isKirbyClose)
                {
                    Vector3 mewLookPosition = GameManager.instance.figureOutClosestPlayer(this.transform).position;
                    mewLookPosition.y = transform.position.y;
                    transform.LookAt(mewLookPosition);
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

