using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy_sparkyMovement : NetworkBehaviour 
{
    private float _jumpHeight = 0.8f;
    private float _moveSpeed = 30f;
    private float _jumpSpeed;
    private float _verticalSpeed;
    private float _turnSpeed = 10;
    private bool _isLookingRight = true;
    private bool _FSMIsRunning = false;
    private Quaternion _endRotation;

    private CharacterController characterController;

    // This character jumps in closest kirby position and turns shock on if kirby is in range

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, -90f, 0);
        characterController = GetComponent<CharacterController>();
        _endRotation = transform.rotation;
        _jumpSpeed = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * _jumpHeight);
    }

    void Update()
    {
        if(!isServer) return;
        if(!_FSMIsRunning) StartCoroutine(chooseRandomAction());
        applyGravity();
        transform.rotation = Quaternion.Lerp(transform.rotation, _endRotation, Time.deltaTime * _turnSpeed);
    }

    // Jump in direction of the closest kirby
    private void jump()
    {
        Vector3 closestPlayerPosition = GameManager.instance.figureOutClosestPlayer(this.transform).position;
        _endRotation = Quaternion.LookRotation(closestPlayerPosition);
        Vector3 movement = GameManager.instance.figureOutClosestPlayer(this.transform).position * Time.deltaTime / _moveSpeed;
        if(characterController.isGrounded)
        {
            _verticalSpeed = _jumpSpeed;
        }
        characterController.Move(movement);
    }

    // if Kirby is inside shockTriggerCollider, turn shockOn
    private void shockOn()
    {

    }

    private void shockOff()
    {

    }

    private void applyGravity()
    {
        if(!characterController.isGrounded)
        {
            _verticalSpeed += Physics.gravity.y * Time.deltaTime;
        }
        Vector3 movement = Vector3.zero;
        movement.y = _verticalSpeed * Time.deltaTime;
        characterController.Move(movement);
    }

    IEnumerator chooseRandomAction()
    {
        _FSMIsRunning = true;
        yield return new WaitUntil(()=> characterController.isGrounded);
        switch (Random.Range(0, 1))
        {
        case (int)Actions.jump:
            jump();
            break;
        case (int)Actions.shock:
            Debug.Log("Should be shocking");
            break;
        }
        yield return new WaitForSeconds(5);
        _FSMIsRunning = false;
    }
}

enum Actions
{
    jump,
    shock
}
