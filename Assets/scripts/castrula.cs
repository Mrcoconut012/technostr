using UnityEngine;

public class Castrula : MonoBehaviour
{
    

    // Флаги ингредиентов
    private bool hasEgg, hasKolb, hasKart, hasGorox, hasMayonez;
    [SerializeField] private GameObject prefabWithOlivie;
    void OnTriggerEnter(Collider other)
    {
        // Используем switch для быстрой и чистой проверки имени
        switch (other.gameObject.name)
        {
            case "egg":
                hasEgg = true;
                Destroy(other.gameObject);
                break;
            case "sasuage_slice":
                hasKolb = true;
                Destroy(other.gameObject);
                break;
            case "potato_slice":
                hasKart = true;
                Destroy(other.gameObject);
                break;
            case "goroh":
                hasGorox = true;
                Destroy(other.gameObject);
                break;
            case "mayonez":
                hasMayonez = true;
                Destroy(other.gameObject);
                break;
        }

        CheckIfRecipeIsReady();
    }

    private void CheckIfRecipeIsReady()
    {
        if (hasEgg && hasKolb && hasKart && hasGorox && hasMayonez)
        {
            // 1. Сохраняем позицию и ротацию текущей пустой кастрюли
            Vector3 currentPos = transform.position;
            Quaternion currentRot = transform.rotation;

            // 2. Создаем новую кастрюлю с салатом
            GameObject finishedPot = Instantiate(prefabWithOlivie, currentPos, currentRot);

            // 3. Переименовываем новый объект
            finishedPot.name = "castrula_s_olivie";

            // 4. Удаляем старую пустую кастрюлю
            Destroy(gameObject);

            Debug.Log("Пустая кастрюля заменена на кастрюлю с оливье!");
        }
    }
}