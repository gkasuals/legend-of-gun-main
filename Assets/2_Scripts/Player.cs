using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class player : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 3f;
    private Vector2 Input;
    
    SpriteRenderer spriter;
    Animator ani;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
       
    }

    private void LateUpdate()
    {
        if (Input.x > 0)
        {
            spriter.flipX = false; // 오른쪽으로 이동
        }
        else if (Input.x < 0)
        {
            spriter.flipX = true; // 왼쪽으로 이동
        }

        ani.SetFloat("Speed", Input.magnitude);
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Input = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical")).normalized;
        rb.linearVelocity = Input * moveSpeed;
    }
}
