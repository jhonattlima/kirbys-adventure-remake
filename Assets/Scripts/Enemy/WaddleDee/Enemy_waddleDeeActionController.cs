using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_waddleDeeActionController : MonoBehaviour
{
    float _moveSpeed = 2f;
    float _verticalSpeed;
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
        if (!_enemy.isServer || _enemy.healthController.died) return;
        if (!_FSMIsRunning) StartCoroutine(chooseRandomAction());
        if (_isMoving)
        {
            _enemy.characterController.Move(movement * _moveSpeed * Time.deltaTime);
            if(!_enemy.animator.GetBool(KirbyConstants.ANIM_ENEMY_WALK))
            {
                _enemy.animator.SetBool(KirbyConstants.ANIM_ENEMY_WALK, true);
                GameManager.instance.localPlayerServerController.RpcChangeBoolAnimationStatus(KirbyConstants.ANIM_ENEMY_WALK, true, this.gameObject);
            }
        }
        else 
        {
            if(_enemy.animator.GetBool(KirbyConstants.ANIM_ENEMY_WALK))
            {
                _enemy.animator.SetBool(KirbyConstants.ANIM_ENEMY_WALK, false);
                GameManager.instance.localPlayerServerController.RpcChangeBoolAnimationStatus(KirbyConstants.ANIM_ENEMY_WALK, false, this.gameObject);
            }
        }
        applyGravity();
    }

    private void setMove()
    {
        lookAtPlayer();
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

    IEnumerator chooseRandomAction()
    {
        _FSMIsRunning = true;
        yield return new WaitUntil(() => _enemy.characterController.isGrounded);

        ActionsWaddleDee nextAction = (ActionsWaddleDee)Random.Range(0, 2);
        switch (nextAction)
        {
            case ActionsWaddleDee.run:
                setMove();
                break;
            case ActionsWaddleDee.stop:
                lookAtPlayer();
                _isMoving = false;
                break;
        }
        yield return new WaitForSeconds(2);
        _FSMIsRunning = false;
    }
}

enum ActionsWaddleDee
{
    run,
    stop
}
