using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;        // Скорость пули
    public float damage = 10f;       // Урон врагу
    public float lifetime = 3f;      // Время жизни пули

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {

        // Пуля летит строго туда, куда указывает её собственная зелёная стрелка Y
        transform.Translate(Vector3.up * speed * Time.deltaTime);


    }

    private void OnTriggerEnter(Collider other)
    {
        // Если попали во врага
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log($"[Bullet]: Попадание! Нанесено {damage} урона.");
            }
            Destroy(gameObject); // Удаляем пулю
        }
        // Если пуля попала во что угодно, кроме Игрока (стены, пол, препятствия)
        else if (!other.CompareTag("Player"))
        {
            Destroy(gameObject); // Просто удаляем пулю без проверки тегов пола
        }
    }
}
