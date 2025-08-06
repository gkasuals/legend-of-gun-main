using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    [HideInInspector]
    public SpriteRenderer player;

    Vector3 rightPos = new Vector3(0.3f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(-0.3f, -0.15f, 0);
    Quaternion leftRot = Quaternion.Euler(0, 0, -30);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -130);

    void Awake()
    {
        player = GetComponentInParent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        HandleInput(); // 추가된 입력 처리 메서드 호출
    }

    private void HandleInput()
    {
        if (!isLeft) // 오른손만 처리
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                spriter.flipX = true; // 오른손 플립
                spriter.sortingOrder = 4; 
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                spriter.flipX = false; // 오른손 플립 해제
                spriter.sortingOrder = 6;
            }
        }
    }
}