using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Sprite CrosshairSprite; // 사용할 커서 이미지
    private SpriteRenderer spriteRenderer;
    private Camera mainCam;

    void Awake()
    {
        mainCam = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();

        Cursor.visible = false; // 기본 커서 숨기기
        spriteRenderer.sprite = CrosshairSprite;
    }

    void Update()
    {
        // 마우스 위치 → 월드 좌표
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        transform.position = mousePos;
    }
}
