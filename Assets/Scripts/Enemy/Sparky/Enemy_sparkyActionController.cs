﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy_sparkyActionController : MonoBehaviour
{
    float _jumpHeight = 0.5f;
    float _moveSpeed = 1f;
    float _jumpSpeed;
    float _verticalSpeed;
    bool _isKirbyClose = false;
    bool _FSMIsRunning = false;
    Enemy_actor _enemy;

    public SphereCollider kirbyDetector;

    // This character jumps in closest kirby position and turns shock on if kirby is in range
    void Start()
    {
        _enemy = GetComponent<Enemy_actor>();
        _enemy.characterController.Move(Vector3.zero);
        _jumpSpeed = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * _jumpHeight);
    }

    void Update()
    {
        if (!_enemy.isServer || _enemy.healthController.died) return;
        if (!_FSMIsRunning) StartCoroutine(chooseRandomAction());
        move();
        applyGravity();
        if (_enemy.characterController.isGrounded) _enemy.animator.SetBool(KirbyConstants.ANIM_ENEMY_JUMP, false);
        else _enemy.animator.SetBool(KirbyConstants.ANIM_ENEMY_JUMP, true);
    }

    // Jump in direction of the closest kirby
    private void jump()
    {
        if (_enemy.characterController.isGrounded)
        {
            _verticalSpeed = _jumpSpeed;
            _enemy.animator.SetTrigger(KirbyConstants.ANIM_ENEMY_JUMP);
        }
    }

    private void move()
    {
        if (_enemy.characterController.isGrounded) return;
        lookAtPlayer();
        Vector3 movement = transform.TransformDirection(Vector3.forward);
        _enemy.characterController.Move(movement * _moveSpeed * Time.deltaTime);
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

        ActionsSparky nextAction = (ActionsSparky)Random.Range(0, 2);
        switch (nextAction)
        {
            case ActionsSparky.jump:
                jump();
                break;
            case ActionsSparky.shock:
                if (_enemy.isKirbyClose)
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
