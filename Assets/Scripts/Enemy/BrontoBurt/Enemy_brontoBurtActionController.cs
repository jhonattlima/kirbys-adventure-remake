using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_brontoBurtActionController : MonoBehaviour
{
    float _flySpeed = 2f;
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
            _enemy.characterController.Move(movement * _flySpeed * Time.deltaTime);
            if (_enemy.characterController.isGrounded) _isMoving = false;
        }
        lookAtPlayer();
    }

    private void setMove()
    {
        lookAtPlayer();
        movement = transform.TransformDirection(Vector3.forward);
        _isMoving = true;
    }

    private void lookAtPlayer()
    {
        Vector3 closestPlayer = GameManager.instance.figureOutClosestPlayer(this.transform).position;
        transform.LookAt(closestPlayer);
    }

    IEnumerator chooseRandomAction()
    {
        _FSMIsRunning = true;
        ActionsBrontoBurt nextAction = (ActionsBrontoBurt)Random.Range(0, 2);
        switch (nextAction)
        {
            case ActionsBrontoBurt.push:
                setMove();
                break;
            case ActionsBrontoBurt.stop:
                _isMoving = false;
                break;
        }
        yield return new WaitForSeconds(3);
        _FSMIsRunning = false;
    }
}
enum ActionsBrontoBurt
{
    push,
    stop
}