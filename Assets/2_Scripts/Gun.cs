using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("크로스헤어")]
    public Transform Crosshair;

    [Header("탄창")]
    public int maxAmmo = 30;
    public float reloadTime = 2f;
    private int currentAmmo;
    private bool isReloading = false;

    [Header("발사 속도")]
    public float fireRate = 0.2f;
    private float nextFireTime = 0.2f;

    private Camera mainCam;
    [SerializeField] private GameObject bulletPrefab;

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

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            if (currentAmmo > 0)
            {
                Fire();
                nextFireTime = Time.time + fireRate;
            }
            else
            {
                Debug.Log("탄약 부족! 재장전 필요 (R 키)");
                StartCoroutine(Reload());
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

        // A지점: 플레이어 오브젝트 위치
        Vector3 playerPos = transform.position;

        // B지점: 크로스헤어 위치
        Vector3 crosshairPos = Crosshair.position;

        // 방향 계산
        Vector2 direction = (crosshairPos - playerPos).normalized;

        // 플레이어 위치에서 총알 생성
        GameObject bulletObj = Instantiate(bulletPrefab, playerPos, Quaternion.identity);

        // Bullet 스크립트에 방향 전달
        bulletObj.GetComponent<Bullet>().Setup(direction);

        // 1초 뒤에 총알 삭제
        Destroy(bulletObj, 1f);
    }
}
