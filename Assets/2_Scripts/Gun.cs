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
    private float nextFireTime = 0f;

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

        // 마우스 위치 → 월드 좌표 변환
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - Crosshair.position).normalized;

        // 총구 위치에서 총알 생성
        GameObject bulletObj = Instantiate(bulletPrefab, Crosshair.position, Quaternion.identity);

        // Bullet 스크립트에 방향 전달
        bulletObj.GetComponent<Bullet>().Setup(direction);
    }
}
