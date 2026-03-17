using UnityEngine;
using System.Collections;

public class protiven : MonoBehaviour
{
    private testo testo;

    [Header("Настройки")]
    [SerializeField] private float cookTime = 15f;        // Время готовки
    public Transform spawnpospizza;
    public bool readyPizza = false;

    public bool isCooking = false;

    void OnTriggerEnter(Collider other)
    {
        
        testo = Object.FindAnyObjectByType<testo>();

        if (testo != null && !isCooking)
        {
            // Проверяем условия (колбаса и томаты)
            if (testo.scorekolb >= 2 && testo.tomatoscore >= 2 && testo.ketchupscore >= 1)
            {
                StartCoroutine(CookRoutine());
            }
        }
    }

    IEnumerator CookRoutine()
    {
        isCooking = true;
        Debug.Log("Готовка пошла...");

        // Ждем время приготовления
        yield return new WaitForSeconds(cookTime);

        readyPizza = true;
        isCooking = false;
        

        
    }
}