using UnityEngine;
using UnityEngine.AI;

public class random : MonoBehaviour
{
    public Transform target;       // Точка назначения
    public float speed = 2.0f;     // Скорость движения
    public float wobbleIntensity = 1.5f; // Насколько сильно персонажа «заносит»
    public float wobbleSpeed = 2.0f;     // Частота колебаний (скорость «пьянства»)

    private float noiseStep;

    void Update()
    {
        if (target == null) return;

        // 1. Направление к цели
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        // 2. Генерируем "пьяное" смещение через шум Перлина
        noiseStep += Time.deltaTime * wobbleSpeed;

        // Вычисляем смещение по X и Z (чтобы персонажа шатало в стороны)
        float noiseX = Mathf.PerlinNoise(noiseStep, 0f) * 2 - 1;
        float noiseZ = Mathf.PerlinNoise(0f, noiseStep) * 2 - 1;

        Vector3 wobble = new Vector3(noiseX, 0, noiseZ) * wobbleIntensity;

        // 3. Итоговый вектор движения
        Vector3 finalVelocity = (directionToTarget + wobble).normalized;

        // 4. Движение и поворот
        transform.position += finalVelocity * speed * Time.deltaTime;

        if (finalVelocity != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(finalVelocity), Time.deltaTime * 5f);
        }
    }
}
