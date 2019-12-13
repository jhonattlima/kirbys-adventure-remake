using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_animationController : MonoBehaviour
{
    Kirby_actor _kirby;
    Kirby_movement _movement;

    bool lastIsWalking;
    bool lastIsJumping;
    bool lastIsFalling;
    bool lastIsFlying;
    bool lastIsFullOfAir;
    bool lastIsFullOfEnemy;
    bool lastIsSucking;

    void Start()
    {
        _kirby = GetComponent<Kirby_actor>();
        _movement = GetComponent<Kirby_movement>();
    }

    void Update()
    {
        checkMovements();
        checkActor();
    }

    public void checkMovements()
    {
        if(!_kirby.isAlive) return;

        if (_movement.isWalking && _movement.isWalking != lastIsWalking)
        {
            lastIsWalking = _movement.isWalking;
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_WALKING, true);
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_MOV_IS_WALKING, true, this.gameObject);
        }
        else if (!_movement.isWalking && _movement.isWalking != lastIsWalking)
        {
            lastIsWalking = _movement.isWalking;
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_WALKING, false);
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_MOV_IS_WALKING, false, this.gameObject);
        }

        if (_movement.isJumping && _movement.isJumping != lastIsJumping)
        {
            lastIsJumping = _movement.isJumping;
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_JUMPING, true);
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_MOV_IS_JUMPING, true, this.gameObject);
        }
        else if (!_movement.isJumping && _movement.isJumping != lastIsJumping)
        {
            lastIsJumping = _movement.isJumping;
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_JUMPING, false);
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_MOV_IS_JUMPING, false, this.gameObject);
        }

        if (_movement.isFalling && _movement.isFalling != lastIsFalling)
        {
            lastIsFalling = _movement.isFalling;
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_FALLING, true);
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_MOV_IS_FALLING, true, this.gameObject);
        }
        else if (!_movement.isFalling && _movement.isFalling != lastIsFalling)
        {
            lastIsFalling = _movement.isFalling;
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_FALLING, false);
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_MOV_IS_FALLING, false, this.gameObject);
        }

        if (_movement.isFlying && _movement.isFlying != lastIsFlying)
        {
            lastIsFlying = _movement.isFlying;
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_FLYING, true);
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_MOV_IS_FLYING, true, this.gameObject);
        }
        else if (!_movement.isFlying && _movement.isFlying != lastIsFlying)
        {
            lastIsFlying = _movement.isFlying;
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_FLYING, false);
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_MOV_IS_FLYING, false, this.gameObject);
        }
    }

    private void checkActor()
    {
        if(!_kirby.isAlive) return;
        
        if ((_kirby.isFullOfAir && _kirby.isFullOfAir != lastIsFullOfAir) ||
            (_kirby.isFullOfEnemy && _kirby.isFullOfEnemy != lastIsFullOfEnemy))
        {
            lastIsFullOfAir = _kirby.isFullOfAir;
            lastIsFullOfEnemy = _kirby.isFullOfEnemy;
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_FULL, true);
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_MOV_IS_FULL, true, this.gameObject);
        }
        else if ((!_kirby.isFullOfAir && _kirby.isFullOfAir != lastIsFullOfAir) ||
                (!_kirby.isFullOfEnemy && _kirby.isFullOfEnemy != lastIsFullOfEnemy))
        {
            lastIsFullOfAir = _kirby.isFullOfAir;
            lastIsFullOfEnemy = _kirby.isFullOfEnemy;
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_FULL, false);
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_MOV_IS_FULL, false, this.gameObject);
        }


        if (_kirby.isSucking && _kirby.isSucking != lastIsSucking)
        {
            lastIsSucking = _kirby.isSucking;
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_WALKING, false);
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_MOV_IS_WALKING, false, this.gameObject);
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_SUCK, true);
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_POWER_SUCK, true, this.gameObject);
        }
        else if (!_kirby.isSucking && _kirby.isSucking != lastIsSucking)
        {
            lastIsSucking = _kirby.isSucking;
            _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_SUCK, false);
            _kirby.kirbyServerController.changeBoolAnimationStatus(KirbyConstants.ANIM_CHECK_POWER_SUCK, false, this.gameObject);
        }
    }

    // Called by animator when damage is suffered
    public void stopAllOtherAnimations()
    {
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_FLYING, false);
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_THROWING_AIRBALL, false);
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_FULL, false);
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_WALKING, false);
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_SUCK, false);
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_FIRE, false);
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_POWER_SHOCK, false);
        _kirby.animator.SetBool(KirbyConstants.ANIM_TRIGGER_POWER_BEAM, false);
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_THROWING_AIRBALL, false);
        _kirby.animator.SetBool(KirbyConstants.ANIM_CHECK_MOV_IS_THROWING_AIRBALL, false);

    }
}
