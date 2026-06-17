using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [Header("Настройки комнаты")]
    [Tooltip("Поставь галочку только в боевой комнате! В стартовой и финальной сними.")]
    public bool isCombatRoom = false;

    private bool canTeleport = false;

    private void Start()
    {
        // Если комната боевая — телепорт закрыт (ждем 20 убийств). Если мирная — открыт сразу.
        if (isCombatRoom)
        {
            canTeleport = false;
        }
        else
        {
            canTeleport = true;
        }
    }

    // Этот метод вызывается спавнером строго после 20 убийств
    public void OpenTeleport()
    {
        canTeleport = true;
        Debug.Log("[DoorTrigger]: Предохранитель снят вручную спавнером. Телепорт работает!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canTeleport) return;

        if (other.CompareTag("Player"))
        {
            RoomManager manager = Object.FindAnyObjectByType<RoomManager>();
            if (manager != null)
            {
                Debug.Log("[DoorTrigger]: Игрок вошел в рабочий портал. Переключаем комнату.");
                manager.GoToNextRoom();
            }
        }
    }
}
