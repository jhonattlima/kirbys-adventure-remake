using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PoppyBrosJrController : MonoBehaviour
{
    float _moveSpeed = 1f;
    float _jumpHeight = 0.5f;
    bool _FSMIsRunning = false;
    float _jumpSpeed;
    float _verticalSpeed;
    Enemy_actor _enemy;
    Vector3 movement;

    void Start()
    {
        _enemy = GetComponent<Enemy_actor>();
        _enemy.characterController.Move(Vector3.zero);
        _jumpSpeed = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * _jumpHeight);
        lookAtPlayer();
        movement = transform.TransformDirection(Vector3.forward);
    }

    void Update()
    {
        if (!_enemy.isServer || _enemy.healthController.died) return;
        if (!_FSMIsRunning) StartCoroutine(chooseRandomAction());
        _enemy.characterController.Move(movement * _moveSpeed * Time.deltaTime);
        if (_enemy.characterController.isGrounded) _enemy.animator.SetBool(KirbyConstants.ANIM_ENEMY_WALK, true);
        else _enemy.animator.SetBool(KirbyConstants.ANIM_ENEMY_WALK, false);
        applyGravity();
    }

    private void setMove()
    {
        lookAtPlayer();
        movement = transform.TransformDirection(Vector3.forward);
    }

    private void jump()
    {
        if (_enemy.characterController.isGrounded)
        {
            lookAtPlayer();
            _verticalSpeed = _jumpSpeed;
            _enemy.animator.SetTrigger(KirbyConstants.ANIM_ENEMY_JUMP);
        }
    }

    private void lookAtPlayer()
    {
        Vector3 closestPlayer = GameManager.instance.figureOutClosestPlayer(this.transform).position;
        closestPlayer.y = transform.position.y;
        transform.LookAt(closestPlayer);
    }

    private void applyGravity()
    {
        if (!_enemy.characterController.isGrounded) _verticalSpeed += Physics.gravity.y * Time.deltaTime;
        Vector3 movement = Vector3.zero;
        movement.y = _verticalSpeed * Time.deltaTime;
        _enemy.characterController.Move(movement);
    }

    IEnumerator chooseRandomAction()
    {
        _FSMIsRunning = true;
        ActionsPoppyBrosJr nextAction = (ActionsPoppyBrosJr)Random.Range(0, 2);
        switch (nextAction)
        {
            case ActionsPoppyBrosJr.walk:
                setMove();
                break;
            case ActionsPoppyBrosJr.jump:
                jump();
                break;
        }
        yield return new WaitForSeconds(1);
        _FSMIsRunning = false;
    }
}
enum ActionsPoppyBrosJr
{
    walk,
    jump
}
