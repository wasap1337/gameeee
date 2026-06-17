using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Префабы комнат")]
    public GameObject startRoomPrefab;
    public GameObject[] combatRoomPrefabs;
    public GameObject finalRoomPrefab;
    
    [Header("Настройки")]
    public int totalCombatRoomsToWin = 3;

    private int currentRoomIndex = 0;
    private GameObject currentRoomInstance;

    private void Start()
    {
        SpawnRoom(startRoomPrefab);
    }

    public void GoToNextRoom()
    {
        if (currentRoomInstance != null) Destroy(currentRoomInstance);
        currentRoomIndex++;

        if (currentRoomIndex <= totalCombatRoomsToWin)
        {
            int randomIndex = Random.Range(0, combatRoomPrefabs.Length);
            SpawnRoom(combatRoomPrefabs[randomIndex]);
        }
        else if (currentRoomIndex == totalCombatRoomsToWin + 1)
        {
            SpawnRoom(finalRoomPrefab);
        }
        else
        {
            currentRoomIndex = 0;
            SpawnRoom(startRoomPrefab);
        }
    }

    private void SpawnRoom(GameObject prefab)
    {
        if (prefab != null)
        {
            // Спавним строго в нулях мира под игроком
            currentRoomInstance = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
    }
    // === МЕТОД ДЛЯ ПЕРЕЗАПУСКА ИГРЫ ПРИ СМЕРТИ ===
    public void RestartGameOnDeath()
    {
        // Удаляем текущую комнату, если она есть
        if (currentRoomInstance != null) Destroy(currentRoomInstance);

        // Сбрасываем индекс на стартовую комнату
        currentRoomIndex = 0;

        // Спавним первую комнату
        currentRoomInstance = Instantiate(startRoomPrefab, Vector3.zero, Quaternion.identity);

        Debug.Log("[RoomManager]: Игрок погиб. Мир перезапущен, спавним 1-ю комнату.");
    }

}
