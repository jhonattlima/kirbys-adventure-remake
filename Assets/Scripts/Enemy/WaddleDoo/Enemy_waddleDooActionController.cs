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
                    _enemy.animator.SetBool(KirbyConstants.ANIM_ENEMY_ATTACK, true);
                }
                else setMove();
                break;
        }
        yield return new WaitForSeconds(3);
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
        Vector3 nextMove = new Vector3();
        if(_isMoving) nextMove.x = movement.x * _moveSpeed * Time.deltaTime;
        _verticalSpeed += Physics.gravity.y * Time.deltaTime;
        nextMove.y = _verticalSpeed * Time.deltaTime;
        _enemy.characterController.Move(nextMove);
        if(_enemy.characterController.isGrounded) _verticalSpeed = 0;
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
