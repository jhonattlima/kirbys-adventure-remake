using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirbyConstants
{
    public const KeyCode KEY_FLY = KeyCode.UpArrow;
    public const KeyCode KEY_JUMP = KeyCode.X;
    public const KeyCode KEY_SUCK = KeyCode.Z;
    public const KeyCode KEY_POOP = KeyCode.LeftShift;
    public const KeyCode KEY_ACTIVATE_POWER = KeyCode.DownArrow;
    public const KeyCode KEY_ENTER_DOOR = KeyCode.UpArrow;
    public const string TAG_ENEMY = "Enemy";
    public const string TAG_PLAYER = "Player";
    public const string ANIM_NAME_TAKE_DAMAGE = "GetDamaged";
    public const string ANIM_NAME_POWER_BEAM = "BeamSweep";
    public const string ANIM_TRIGGER_POWER_BEAM = "Power_Beam";
    public const string ANIM_CHECK_POWER_FIRE = "Power_Fire";
    public const string ANIM_CHECK_POWER_SHOCK = "Power_Shock";
    public const string ANIM_CHECK_POWER_SUCK = "Power_Suck";
    public const string ANIM_CHECK_MOV_IS_WALKING = "IsWalking";
    public const string ANIM_CHECK_MOV_IS_FALLING = "IsFalling";
    public const string ANIM_CHECK_MOV_IS_JUMPING = "IsJumping";
    public const string ANIM_CHECK_MOV_IS_FLYING = "IsFlying";
    public const string ANIM_CHECK_MOV_IS_FULL = "IsFull";
    public const string ANIM_CHECK_MOV_IS_THROWING_AIRBALL = "IsThrowingAir";
    public const int PLAYER_HEALTH_POINTS = 4;
    public const int PLAYER_NORMAL_DAMAGE = 1;
    public const float COOLDOWN_ACTION = 0.5f;
    public const float COOLDOWN_INVULNERABLE = 2f;
    public const float COOLDOWN_TO_RECOVER_FROM_DAMAGE = 1f;
    public const float PUSH_SPEED_WHEN_DAMAGED = 15f;
    public const float KIRBY_SUCK_SPEED = 5f;
    public const float KIRBY_STAR_COOLDOWN_TO_BE_ACTIVE = 3f;
    public const float KIRBY_STAR_BULLET_SPEED = 5f;

    public const string ANIM_ENEMY_ATTACK = "Attack";
    public const string ANIM_ENEMY_JUMP = "Jump";
    public const string ANIM_ENEMY_TAKE_DAMAGE = "GetDamaged";
    public const string ANIM_ENEMY_WALK = "Walk";

}
