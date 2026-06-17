using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
        Debug.Log("Игрок вылечен! Текущее ХП: " + currentHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
        Debug.Log("Игрок получил урон! Осталось ХП: " + currentHealth);

        if (currentHealth <= 0f)
        {
            DieAndRestart();
        }
    }

    private void DieAndRestart()
    {
        Debug.Log("Игрок мертв! Перезапуск игры...");

        // Находим RoomManager на сцене через актуальный метод
        RoomManager manager = Object.FindAnyObjectByType<RoomManager>();
        if (manager != null)
        {
            manager.RestartGameOnDeath();
        }
        else
        {
            Debug.LogError("[PlayerHealth]: Не удалось найти RoomManager на сцене для перезапуска!");
        }

        // Восстанавливаем здоровье игрока для новой попытки
        currentHealth = maxHealth;
    }
}
