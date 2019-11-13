using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_powerAirBall : MonoBehaviour
{
    private float _airSpeed = 5f;
    private Vector3 direction;

    void Start()
    {
        Destroy(gameObject, 1);
    }

    void Update() 
    {
        transform.position += direction * _airSpeed * Time.deltaTime;
    }

    public void setBulletDirection(Vector3 directionTogo)
    {
        direction = directionTogo;
    }
}
