using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, PlayerStatus
{
    public static Player instance;
    public static Player Instance { get => instance; }
    
    [SerializeField] private float fallMultiplier = 0f;

    private Rigidbody2D rg;
    public float jumpPower = 0f;
    public float speed = 0f;
    public float maxX;
    public float minX;
    private bool doubleJump = true;
    private bool isGrounded = true;
    
    private Vector2 newGravity;
    private Animator animatorPlayer;

    private List<GameObject> ListCheckBox = new List<GameObject>();
    public enum StatusPlayer
    {
        Left,
        Right,
        Stop
    }

    StatusPlayer statusCurrent = StatusPlayer.Stop;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        newGravity = new Vector2(0f, -Physics2D.gravity.y);
        animatorPlayer = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (rg.velocity.y < 0f)
        {
            rg.velocity -= newGravity * fallMultiplier * Time.deltaTime;
        }
        switch (statusCurrent)
        {
            case StatusPlayer.Left: Move(-speed); break;
            case StatusPlayer.Right: Move(speed); break;
            default: break;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Jump();
        }
    }
    
    public void Move(float speed)
    {
        transform.position += new Vector3(speed, 0f, 0f);
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = pos;
    }
    
    public void Jump()
    {
        if (Isgrounded())
        {
            rg.velocity = new Vector2(rg.velocity.x, jumpPower);
        }
        else if (doubleJump)
        {
            rg.velocity = new Vector2(rg.velocity.x, jumpPower);
            doubleJump = !doubleJump;
        }
    }

    public void MoveLeft()
    {
        statusCurrent = StatusPlayer.Left;
        RotatePlayer(0f);
        Debug.Log("Left");
    }
    
    public void MoveRight()
    {
        statusCurrent = StatusPlayer.Right;
        RotatePlayer(180f);
        Debug.Log("Right");
    }
    
    public void StopMove()
    {
        statusCurrent = StatusPlayer.Stop;
    }

    void RotatePlayer(float axis)
    {
        transform.rotation = Quaternion.Euler(0f, axis, 0f);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (other.collider.CompareTag("WheelEnemy"))
        {
            Die();   
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            doubleJump = true;
            isGrounded = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CheckBoxEnemy"))
        {
            ListCheckBox.Add(other.gameObject);
        }
        
    }

    bool Isgrounded()
    {
        return isGrounded;
    }

    public void Die()
    {
        animatorPlayer.Play("Die");
    }

    public void CheckBoxToDestroyWheel()
    {
        int maxWheelDestroy = (int)(ListCheckBox.Count/3);
        int countWheelSameParent = 0;
        for (int i = 0; i < ListCheckBox.Count; i++)
        {
            for (int j = i+1; i < ListCheckBox.Count; i++)
            {
                if (ListCheckBox[i] != ListCheckBox[j] &&
                    ListCheckBox[i].transform.parent == ListCheckBox[j].transform.parent)
                {
                    countWheelSameParent++;
                }
            }

            if (countWheelSameParent == 3)
            {
                ListCheckBox[i].GetComponent<CheckBoxWheel>().DestrouyParentWheel();
            }
        }   
    }
}
