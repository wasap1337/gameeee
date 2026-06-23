using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Настройки префабов")]
    public GameObject bulletPrefab;    // Твоя настроенная пуля SM_Bullet_02
    public Transform firePoint;       // Точка, откуда пуля вылетает (дуло автомата)

    [Header("Параметры оружия")]
    public float fireRate = 0.2f;     // Задержка между выстрелами (0.2 — быстрая стрельба)

    private float nextFireTime = 0f;

    void Update()
    {
        // Проверяем нажатие левой кнопки мыши (LMB) и кулдаун стрельбы
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("[PlayerShooting]: Забыли перетащить пулю или точку FirePoint в инспекторе!");
            return;
        }

        // Создаем пулю в точке firePoint с ее направлением вращения
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Debug.Log("[PlayerShooting]: Выстрел!");
    }
}
