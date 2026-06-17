using UnityEngine;
using UnityEngine.UI; // Обязательно для работы с интерфейсом

public class HealthBar : MonoBehaviour
{
    [Header("Ссылки на компоненты")]
    [SerializeField] private Slider healthSlider; // Сама полоска UI
    [SerializeField] private PlayerHealth playerHealth; // Ссылка на ХП игрока

    private void Start()
    {
        // Если забыли перетащить игрока в инспекторе, ищем его автоматически
        if (playerHealth == null)
        {
            playerHealth = Object.FindAnyObjectByType<PlayerHealth>();
        }

        if (playerHealth != null && healthSlider != null)
        {
            // Настраиваем максимум полоски под максимальное ХП игрока
            healthSlider.maxValue = playerHealth.maxHealth;
            healthSlider.value = playerHealth.currentHealth;
        }
    }

    private void Update()
    {
        if (playerHealth != null && healthSlider != null)
        {
            // Каждый кадр плавно или мгновенно обновляем полоску под актуальное ХП
            healthSlider.value = playerHealth.currentHealth;
        }
    }
}
