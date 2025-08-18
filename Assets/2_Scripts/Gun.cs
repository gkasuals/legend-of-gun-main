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

        Vector3 playerPos = transform.position;
        Vector3 crosshairPos = Crosshair.position;
        Vector2 direction = (crosshairPos - playerPos).normalized;

        // 기본 각도 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 만약 프리팹이 Y축을 기준으로 되어 있다면, +90도 보정
        Quaternion bulletRotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);

        GameObject bulletObj = Instantiate(bulletPrefab, playerPos, bulletRotation);
        bulletObj.GetComponent<Bullet>().Setup(direction);
        Destroy(bulletObj, 1f);
    }
}
