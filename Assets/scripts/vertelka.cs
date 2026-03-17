using UnityEngine;

public class vertelka : MonoBehaviour
{
    [Header("Настройки материалов")]
    [SerializeField] private Material materialNormal; // Обычный материал
    [SerializeField] private Material materialTilted; // Материал при наклоне

    [Header("Компоненты")]
    [SerializeField] private Renderer targetRenderer; // Рендерер, на котором меняем материал

    [Header("Настройки спавна")]
    [SerializeField] private GameObject krupinki;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float thresholdAngle = 40f;
    [SerializeField] private float minInterval = 0.05f;
    [SerializeField] private float maxInterval = 0.5f;

    private float spawnTimer;

    void Start()
    {
        // Если рендерер не назначен, пытаемся найти его на этом же объекте
        if (targetRenderer == null) targetRenderer = GetComponent<Renderer>();

        // Устанавливаем начальный материал
        if (targetRenderer != null && materialNormal != null)
            targetRenderer.material = materialNormal;
    }
    public void stop()
    {
        gameObject.GetComponent<vertelka>().enabled = false;
    }
    void Update()
    {
        // Считаем угол относительно вертикали
        float currentAngle = Vector3.Angle(transform.up, Vector3.up);

        if (currentAngle > thresholdAngle)
        {
            // Меняем материал на "наклонный", если он еще не стоит
            if (targetRenderer.sharedMaterial != materialTilted && materialTilted != null)
            {
                targetRenderer.material = materialTilted;
            }

            // Логика таймера для крупинок
            float intensity = Mathf.InverseLerp(thresholdAngle, 90f, currentAngle);
            float currentInterval = Mathf.Lerp(maxInterval, minInterval, intensity);

            spawnTimer += Time.deltaTime;
            if (spawnTimer >= currentInterval)
            {
                Spawn();
                spawnTimer = 0f;
            }
        }
        else
        {
            // Возвращаем обычный материал
            if (targetRenderer.sharedMaterial != materialNormal && materialNormal != null)
            {
                targetRenderer.material = materialNormal;
            }
            spawnTimer = 0f;
        }
    }

    void Spawn()
    {
        if (krupinki != null && spawnPoint != null)
            Instantiate(krupinki, spawnPoint.position, Quaternion.identity);
    }
}