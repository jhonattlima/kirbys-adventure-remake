using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirbyConstants
{
    public const KeyCode KEY_JUMP = KeyCode.UpArrow;
    public const KeyCode KEY_SUCK = KeyCode.Z;
    public const KeyCode KEY_POOP = KeyCode.LeftShift;
    public const KeyCode KEY_ACTIVATE_POWER = KeyCode.DownArrow;
    public const string TAG_ENEMY = "Enemy";
    public const string TAG_PLAYER = "Player";
    public const int PLAYER_HEALTH_POINTS = 4;
    public const float COOLDOWN_ACTION = 0.5f;
    public const float COOLDOWN_INVULNERABLE = 3f;
    public const float COOLDOWN_TO_RECOVER_FROM_DAMAGE = 2f;
    public const float PUSH_SPEED_WHEN_DAMAGED = 5f;
}
