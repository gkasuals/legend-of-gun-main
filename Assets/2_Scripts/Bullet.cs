using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    private Rigidbody2D rb;

    public void Setup(Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * speed;

        // 1초 뒤에 총알 자동 삭제
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Room 태그 또는 이름이 Room이면 무시
        if (collision.CompareTag("Room") || collision.name == "Room")
            return;

        // 그 외에는 총알 삭제
        Destroy(gameObject);
    }
}
