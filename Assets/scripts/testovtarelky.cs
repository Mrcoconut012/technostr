using System.Collections.Generic;
using UnityEngine;

public class testovtarelky : MonoBehaviour
{
    [Header("Настройки замены")]
    [SerializeField] private GameObject prefabToSpawn; // Префаб готового объекта
    [SerializeField] private bool destroyIngredients = true; // Удалять ли ингредиенты

    [Header("Список нужных ингредиентов")]
    [SerializeField] private List<string> requiredNames = new List<string>();

    private List<GameObject> objectsInside = new List<GameObject>();
    private bool isFinished = false;

    void OnTriggerEnter(Collider other)
    {
        if (isFinished) return;

        // Проверяем, является ли вошедший объект нужным ингредиентом
        if (requiredNames.Contains(other.gameObject.name) && !objectsInside.Contains(other.gameObject))
        {
            objectsInside.Add(other.gameObject);
            CheckRequirements();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (isFinished) return;

        if (objectsInside.Contains(other.gameObject))
        {
            objectsInside.Remove(other.gameObject);
        }
    }

    void CheckRequirements()
    {
        int matches = 0;
        foreach (string name in requiredNames)
        {
            if (IsObjectPresent(name)) matches++;
        }

        if (matches >= requiredNames.Count)
        {
            ReplaceObject();
        }
    }

    bool IsObjectPresent(string name)
    {
        foreach (GameObject obj in objectsInside)
        {
            if (obj != null && obj.name == name) return true;
        }
        return false;
    }

    void ReplaceObject()
    {
        isFinished = true;

        // 1. Создаем новый объект
        if (prefabToSpawn != null)
        {
            // Создаем объект
            GameObject spawnedObject = Instantiate(prefabToSpawn, transform.position, transform.rotation);

            // ПРИСВАИВАЕМ МАСШТАБ ТЕКУЩЕГО ОБЪЕКТА НОВОМУ
            spawnedObject.transform.localScale = transform.localScale;
        }

        // 2. Удаляем ингредиенты
        if (destroyIngredients)
        {
            foreach (GameObject obj in objectsInside)
            {
                if (obj != null) Destroy(obj);
            }
        }

        // 3. Удаляем сам объект
        Destroy(gameObject);

        Debug.Log("Объект успешно заменен с сохранением масштаба: " + transform.localScale);
    }
}