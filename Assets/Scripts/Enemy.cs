using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 20f;
    public float damage = 10f;

    private NavMeshAgent agent;
    private Transform player;

    [Header("Дроп хп")]
    public GameObject healthPackPrefab;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        if (player != null && agent != null && agent.isOnNavMesh)
        {
            agent.destination = player.position;
        }
    }

    // Урон через триггер — работает безотказно
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log($"[Enemy]: Нанес игроку {damage} урона через триггер!");
            }
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject spawner = GameObject.Find("Spawner");
        if (spawner != null)
        {
            spawner.SendMessage("RegisterEnemyDeath", SendMessageOptions.DontRequireReceiver);
        }

        int roll = Random.Range(0, 100);
        if (roll < 20 && healthPackPrefab != null)
        {
            Instantiate(healthPackPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
