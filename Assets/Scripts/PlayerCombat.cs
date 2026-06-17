using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    private Transform cam; 

    void Start()
    {
        // Автоматически находим камеру внутри игрока
        cam = GetComponentInChildren<Camera>().transform;
    }

    void Update()
    {
        // Выстрел по левой кнопке мыши
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        // Пускаем луч из центра камеры вперед
        if (Physics.Raycast(cam.position, cam.forward, out hit, range))
        {
            Debug.Log("Попал в: " + hit.transform.name);

            // Проверяем, есть ли у цели скрипт врага
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
