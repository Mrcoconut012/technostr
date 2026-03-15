using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit.Interactables; // Для новых версий XRIT
using UnityEngine.XR.Interaction.Toolkit;

public class NewSlicer : MonoBehaviour
{
    public Material materialAfterSlice;
    public LayerMask sliceMask;
    public bool isTouched;

    [Header("Настройки кулдауна")]
    public float sliceCooldown = 0.5f;
    private float lastSliceTime = -1f;

    private void Update()
    {
        if (isTouched && Time.time >= lastSliceTime + sliceCooldown)
        {
            isTouched = false;
            lastSliceTime = Time.time;

            // Размер коробки подберите под масштаб вашего лезвия
            Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation, sliceMask);

            foreach (Collider col in objectsToBeSliced)
            {
                // Пытаемся получить оригинальный скрипт захвата, чтобы скопировать настройки
                XRGrabInteractable originalGrab = col.gameObject.GetComponent<XRGrabInteractable>();

                SlicedHull slicedObject = SliceObject(col.gameObject, materialAfterSlice);

                if (slicedObject != null)
                {
                    GameObject upper = slicedObject.CreateUpperHull(col.gameObject, materialAfterSlice);
                    GameObject lower = slicedObject.CreateLowerHull(col.gameObject, materialAfterSlice);

                    // Настраиваем физику и XR
                    ConfigureSlice(upper, col.gameObject, originalGrab);
                    ConfigureSlice(lower, col.gameObject, originalGrab);

                    Destroy(col.gameObject);
                }
            }
        }
    }

    private void ConfigureSlice(GameObject obj, GameObject original, XRGrabInteractable originalGrab)
    {
        // 1. Позиция и масштаб
        obj.transform.position = original.transform.position;
        obj.transform.rotation = original.transform.rotation;
        obj.transform.localScale = original.transform.localScale;

        // 2. Слой (Physics Layer)
        obj.layer = original.layer;

        // 3. Физика
        Rigidbody rb = obj.AddComponent<Rigidbody>();
        MeshCollider collider = obj.AddComponent<MeshCollider>();
        collider.convex = true;

        // 4. XR Interaction
        XRGrabInteractable newGrab = obj.AddComponent<XRGrabInteractable>();

        // КОПИРОВАНИЕ НАСТРОЕК (Чтобы слои взаимодействия совпали)
        if (originalGrab != null)
        {
            // Копируем маску слоев взаимодействия (Interaction Layer Mask)
            newGrab.interactionLayers = originalGrab.interactionLayers;

            // Копируем тип движения (Kinematic, Instantaneous и т.д.)
            newGrab.movementType = originalGrab.movementType;

            // Если на оригинале была гравитация или бросок - можно настроить и здесь
            newGrab.useDynamicAttach = true;
        }

        // 5. Позволяем разрезать новые куски (добавляем этот же скрипт или тег, если нужно)
        // obj.AddComponent<SliceableTag>(); // если фильтруете по скрипту/тегу
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        // transform.up — это нормаль плоскости разреза вашего меча
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }
}