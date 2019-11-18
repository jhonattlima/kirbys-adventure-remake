using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_powerAirBall : MonoBehaviour
{
    private float _airSpeed = 5f;
    private Vector3 direction;

    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    void Update() 
    {
        transform.position += direction * _airSpeed * Time.deltaTime;
    }

    public void setBulletDirection(Vector3 direction)
    {
        this.direction = direction;
    }
}
