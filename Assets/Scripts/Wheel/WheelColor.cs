using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelColor : Wheel
{
    public WheelColor(float _speed, Vector2 _direction, Sprite _sprite)
    {
        speed = _speed;
        direction = _direction;
        spriteSet = _sprite;
    }
    public void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = spriteSet;
        rg = GetComponent<Rigidbody2D>();
        direction = Vector2.one.normalized;
    }
    private void FixedUpdate()
    {
        rg.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("HorizontalWall"))
        {
            direction.x = -direction.x;
            SetNewDirection();
        }

        if (other.collider.CompareTag("VerticalWall"))
        {
            direction.y = -direction.y;
            SetNewDirection();
        }
        if (other.collider.CompareTag("HideWheel"))
        {
            HandleHideWheel();
        }
    }

    void SetNewDirection()
    {
        if (direction.x < 0f)
        {
            direction.x = Random.Range(-0.5f, -1f);
        }

        if (direction.x > 0f)
        {
            direction.x = Random.Range(0.5f, 1f);
        }
    }
    
    public override void HandleHideWheel()
    {
        gameObject.SetActive(false);
    }
}
