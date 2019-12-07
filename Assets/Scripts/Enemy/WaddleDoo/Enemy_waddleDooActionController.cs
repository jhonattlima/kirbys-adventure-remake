using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_waddleDooActionController : MonoBehaviour
{
    float _moveSpeed = 1f;
    float _verticalSpeed;
    bool _isKirbyClose = false;
    bool _FSMIsRunning = false;
    bool _isMoving = false;
    Enemy_actor _enemy;
    public SphereCollider kirbyDetector;
    Vector3 movement;

    void Start()
    {
        _enemy = GetComponent<Enemy_actor>();
        _enemy.characterController.Move(Vector3.zero);
        lookAtPlayer();
    }

    void Update()
    {
        if (!_enemy.isServer || _enemy.healthController.died) return;
        if (!_FSMIsRunning) StartCoroutine(chooseRandomAction());
        if (_isMoving)
        {
            _enemy.characterController.Move(movement * _moveSpeed * Time.deltaTime);
            _enemy.animator.SetBool(KirbyConstants.ANIM_ENEMY_WALK, true);
        }
        else _enemy.animator.SetBool(KirbyConstants.ANIM_ENEMY_WALK, false);
        applyGravity();
    }

    IEnumerator chooseRandomAction()
    {
        _FSMIsRunning = true;
        yield return new WaitUntil(() => _enemy.characterController.isGrounded);

        ActionWaddleDoo nextAction = (ActionWaddleDoo)Random.Range(0, 2);
        switch (nextAction)
        {
            case ActionWaddleDoo.walk:
                setMove();
                break;
            case ActionWaddleDoo.beam:
                if (_enemy.isKirbyClose)
                {
                    _isMoving = false;
                    lookAtPlayer();
                    //_enemy.animator.SetTrigger(KirbyConstants.ANIM_ENEMY_ATTACK);
                    _enemy.animator.SetBool("IsAttacking", true);
                }
                else setMove();
                break;
        }
        //yield return null;
        yield return new WaitForSeconds(0.5f);
        _enemy.animator.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(2.5f);
        _FSMIsRunning = false;
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
        if (!_enemy.characterController.isGrounded) _verticalSpeed += Physics.gravity.y * Time.deltaTime;
        Vector3 movement = Vector3.zero;
        movement.y = _verticalSpeed * Time.deltaTime;
        _enemy.characterController.Move(movement);
    }

    private void lookAtPlayer()
    {
        Vector3 closestPlayer = GameManager.instance.figureOutClosestPlayer(this.transform).position;
        closestPlayer.y = transform.position.y;
        transform.LookAt(closestPlayer);
    }
}

enum ActionWaddleDoo
{
    walk,
    beam
}
