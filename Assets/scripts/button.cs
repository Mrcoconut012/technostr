using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

using System.Linq;
public class button : MonoBehaviour
{
    [Header("Список доступных префаборов")]
    public List<GameObject> spawnablePrefabs;

    [Header("Настройки")]
    public float spawnOffset = 0.2f;

    private XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
    }

    void OnEnable()
    {
        interactable.selectEntered.AddListener(HandlePress);
    }

    void OnDisable()
    {
        interactable.selectEntered.RemoveListener(HandlePress);
    }

    private void HandlePress(SelectEnterEventArgs args)
    {
        // 1. Получаем чистое имя (из "button_cucumber" делаем "cucumber")
        string targetName = GetEntityName(gameObject.name);

        // 2. Ищем в списке префаб с таким же именем
        GameObject prefabToSpawn = spawnablePrefabs.FirstOrDefault(p => p.name == targetName);

        if (prefabToSpawn != null)
        {
            SpawnAndRename(prefabToSpawn, targetName);
        }
        else
        {
            Debug.LogWarning($"[SpawnSystem] Префаб '{targetName}' не найден в списке кнопки {gameObject.name}");
        }
    }

    private void SpawnAndRename(GameObject prefab, string cleanName)
    {
        Vector3 spawnPosition = transform.position + Vector3.up * spawnOffset;

        // 3. Создаем объект
        GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

        // 4. Убираем "(Clone)" и ставим чистое имя
        newObject.name = cleanName;

        Debug.Log($"Заспавнен объект: {newObject.name}");
    }

    private string GetEntityName(string fullName)
    {
        int underscoreIndex = fullName.LastIndexOf('_');
        if (underscoreIndex != -1 && underscoreIndex < fullName.Length - 1)
        {
            return fullName.Substring(underscoreIndex + 1);
        }
        return fullName;
    }
}