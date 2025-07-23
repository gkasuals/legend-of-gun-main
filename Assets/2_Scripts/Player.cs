using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f;
    private Vector2 Input;
     
    Rigidbody2D rb;
    SpriteRenderer spriter;

    [Header("공격 설정")]
    public GameObject bulletPrefab;
    public float attackRange = 10f;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        HandleMovement();

        // 움직임이 없을 때 공격 시도
        if (Input.magnitude == 0)
        {
            TryAttackNearestEnemy();
        }
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

    }

    void HandleMovement()
    {
        Input = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical")).normalized;
        rb.linearVelocity = Input * moveSpeed;
    }

    void TryAttackNearestEnemy()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        GameObject nearest = FindNearestEnemy();
        if (nearest != null)
        {
            Vector2 direction = (nearest.transform.position - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * 10f;

            lastAttackTime = Time.time;
        }
    }

    GameObject FindNearestEnemy()
    {
        return GameObject.FindGameObjectsWithTag("Enemy")
            .Where(enemy => Vector2.Distance(transform.position, enemy.transform.position) < attackRange)
            .OrderBy(enemy => Vector2.Distance(transform.position, enemy.transform.position))
            .FirstOrDefault();
    }

}
