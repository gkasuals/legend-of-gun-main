using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;

    private Rigidbody2D rb;

    // Gun.cs 에서 방향을 전달받음
    public void Setup(Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
