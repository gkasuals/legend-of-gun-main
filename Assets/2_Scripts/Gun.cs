using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("총 관련")]
    public Transform Crosshair; // 총구 위치

    [Header("탄창 관련")]
    public int maxAmmo = 30;          // 최대 탄환 수
    public float reloadTime = 2f;    // 재장전 시간
    private int currentAmmo;
    private bool isReloading = false;

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        Cursor.visible = false;
        currentAmmo = maxAmmo;
        UIManager.Instance.UpdateAmmoText(currentAmmo, maxAmmo);
    }

    private void Update()
    {
        if (isReloading)
            return;

        // R 키로 재장전
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        // 좌클릭 발사 - 탄약이 있을 때만
        if (Input.GetMouseButtonDown(0))
        {
            if (currentAmmo > 0)
            {
                Fire();
            }
            else
            {
                Debug.Log("탄환 없음! 재장전 필요 (R 키)");
            }
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("재장전 중...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        UIManager.Instance.UpdateAmmoText(currentAmmo, maxAmmo);
        isReloading = false;
        Debug.Log("재장전 완료!");
    }

    private void Fire()
    {
        currentAmmo--;
        UIManager.Instance.UpdateAmmoText(currentAmmo, maxAmmo);

        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - Crosshair.position).normalized;

        // 레이캐스트 (첫 번째 충돌체만 확인)
        RaycastHit2D hit = Physics2D.Raycast(Crosshair.position, direction, 100f);
        if (hit.collider != null && hit.collider.gameObject != gameObject)
        {
            Debug.Log("Hit: " + hit.collider.name);
        }

        // 총알 궤적 표시
        GameObject trailObj = new GameObject("BulletTrail");
        LineRenderer lr = trailObj.AddComponent<LineRenderer>();

        lr.positionCount = 2;
        lr.SetPosition(0, Crosshair.position);
        lr.SetPosition(1, Crosshair.position + (Vector3)(direction * 100f));

        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;

        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.gray;
        lr.endColor = Color.gray;
        lr.sortingOrder = 10;

        Destroy(trailObj, 0.05f);
    }
}
