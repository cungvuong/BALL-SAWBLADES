using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Wheel : MonoBehaviour
{
    public float speed;
    protected Rigidbody2D rg;
    protected Vector2 direction;
    public Sprite spriteSet;

    public Wheel(float _speed, Vector2 _direction)
    {
        speed = _speed;
        direction = _direction;
    }

    public Wheel()
    {
        
    }

    public float Speed => speed;
    
    public Rigidbody2D Rg => rg;

    public void SetDirection(Vector2 _direction)
    {
        direction = _direction;
    }

    public void RandomDirecton()
    {
        if (Random.Range(0, 10) > 5)
        {
            direction = new Vector2(Random.Range(-0.5f, -1f), -1);
        }
        else
        {
            direction = new Vector2(Random.Range(0.5f, 1f), -1);
        }
    }
    
    public void SetPosition(Vector2 _position)
    {
        transform.position = _position;
    }

    public abstract void HandleHideWheel();
}
