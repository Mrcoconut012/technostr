using UnityEngine;
using System.Collections;

public class vertelka : MonoBehaviour
{
    [SerializeField] private Transform obj;
    [SerializeField] private GameObject krupinki;
    [SerializeField] private string tag1 = "krupa";
    [SerializeField] private Transform spawnobj;

    [Header("Настройки спавна")]
    [SerializeField] private float minInterval = 0.05f; // Скорость при макс. наклоне (очень быстро)
    [SerializeField] private float maxInterval = 0.5f;  // Скорость при наклоне 45 градусов (медленно)
    [SerializeField] private float thresholdAngle = 45f; // Угол, с которого начинается спавн
    
    private float spawnTimer;
    void Start()
    {
        obj = GetComponent<Transform>();
        krupinki = GameObject.FindWithTag(tag1);
        
    }
    void Update()
    {
        
            float angleX = Mathf.Abs(NormalizeAngle(obj.eulerAngles.x));
            float angleZ = Mathf.Abs(NormalizeAngle(obj.eulerAngles.z));

            // Находим максимальный наклон из двух осей
            float maxCurrentAngle = Mathf.Max(angleX, angleZ);

            // Если наклон больше порога (45 градусов)
            if (maxCurrentAngle > thresholdAngle)
            {

            Debug.Log("ugol");
                // 1. Вычисляем "интенсивность" наклона от 0 до 1
                // (например, если наклон 90 градусов, а порог 45, интенсивность будет расти)
                float tiltIntensity = Mathf.InverseLerp(thresholdAngle, 180f, maxCurrentAngle);

                // 2. Вычисляем текущую задержку: чем выше интенсивность, тем ближе интервал к minInterval
                float currentInterval = Mathf.Lerp(maxInterval, minInterval, tiltIntensity);

                // 3. Работа таймера
                spawnTimer += Time.deltaTime;
                if (spawnTimer >= currentInterval)
                {
                    SpawnKrupinki();
                    spawnTimer = 0f;
                }
            }
            else
            {
                // Сбрасываем таймер, если объект выровнялся
                spawnTimer = 0f;
            }
        
        
    }

    void SpawnKrupinki()
    {
        Instantiate(krupinki, spawnobj.position, Quaternion.identity);
    }

    float NormalizeAngle(float angle)
    {
        if (angle > 180f) return angle - 360f;
        return angle;
    }
}
