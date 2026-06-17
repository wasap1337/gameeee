using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Настройки префабов")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Настройки спавна")]
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private int maxEnemies = 5;

    private float timer;
    private int deathCount = 0;
    private bool canSpawn = true;

    private void Start()
    {
        timer = spawnInterval;
        deathCount = 0;
    }

    private void Update()
    {
        if (!canSpawn) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            int currentEnemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (currentEnemiesCount < maxEnemies)
            {
                SpawnEnemy();
                timer = 0f;
            }
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0) return;

        GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(randomEnemy, randomPoint.position, randomPoint.rotation);
    }

    // Этот метод ловит SendMessage "RegisterEnemyDeath" от кубов
    public void RegisterEnemyDeath()
    {
        deathCount++;
        Debug.Log($"Врагов убито: {deathCount} / 20");

        if (deathCount >= 20)
        {
            canSpawn = false; // Останавливаем спавн

            // Находим телепорт и открываем его логически
            DoorTrigger door = Object.FindAnyObjectByType<DoorTrigger>();
            if (door != null)
            {
                door.OpenTeleport();
            }
        }
    }
}
