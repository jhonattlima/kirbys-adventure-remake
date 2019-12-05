using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_animationController : MonoBehaviour
{
    Kirby_actor _kirby;
    Kirby_movement _movement;

    // Start is called before the first frame update
    void Start()
    {
        _kirby = GetComponent<Kirby_actor>();
        _movement = GetComponent<Kirby_movement>();
    }

    // Update is called once per frame
    void Update()
    {
        checkMovements();
        checkActor();
    }

    public void checkMovements()
    {
        if(_movement.isWalking) _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_WALKING, true);
        else  _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_WALKING, false);

        if(_movement.isJumping) _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_JUMPING, true);
        else  _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_JUMPING, false);

        if(_movement.isFalling) _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_FALLING, true);
        else  _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_FALLING, false);

        if(_movement.isFlying) _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_FLYING, true);
        else  _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_FLYING, false);
    }

    private void checkActor()
    {
        if(_kirby.isFullOfAir || _kirby.isFullOfEnemy) _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_FULL, true);
        else  _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_FULL, false);


        if(_kirby.isSucking)
        {
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_WALKING, false);
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_SUCK, true);
        }        
        else  _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_SUCK, false);
    }
}
