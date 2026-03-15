using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Slicer : MonoBehaviour
{
    public Material materialAfterSlice;
    public string sliceTag = "Sliceable";
    public string forbiddenTag = "NoKroshki"; // Тег триггера, где крошки не спавнятся
    public bool isTouched;
    [SerializeField] private GameObject kroshki;

    [Header("Настройки кулдауна")]
    public float sliceCooldown = 0.5f;
    private float lastSliceTime = -1f;

    private void Update()
    {
        if (isTouched == true && Time.time >= lastSliceTime + sliceCooldown)
        {
            isTouched = false;
            lastSliceTime = Time.time;

            Collider[] objectsInRange = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation);

            foreach (Collider objectToBeSliced in objectsInRange)
            {
                if (objectToBeSliced.CompareTag(sliceTag))
                {
                    SlicedHull slicedObject = SliceObject(objectToBeSliced.gameObject, materialAfterSlice);

                    if (slicedObject != null)
                    {
                        // Генерируем куски
                        GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, materialAfterSlice);
                        GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, materialAfterSlice);

                        upperHullGameobject.transform.position = objectToBeSliced.transform.position;
                        lowerHullGameobject.transform.position = objectToBeSliced.transform.position;

                        // Копируем данные
                        CopyData(objectToBeSliced.gameObject, upperHullGameobject);
                        CopyData(objectToBeSliced.gameObject, lowerHullGameobject);

                        // СПАВН КРОШЕК (с проверкой)
                        if (!IsInsideForbiddenTrigger())
                        {
                            if (kroshki != null)
                            {
                                Instantiate(kroshki, transform.position, transform.rotation);
                            }
                        }

                        Destroy(objectToBeSliced.gameObject);
                    }
                }
            }
        }
    }

    private void CopyData(GameObject original, GameObject part)
    {
        part.tag = original.tag;
        part.layer = original.layer;

        // Добавляем физику
        Rigidbody rb = part.AddComponent<Rigidbody>();
        part.AddComponent<MeshCollider>().convex = true;

        // Добавляем наши умные скрипты, чтобы работала гравитация и магнит
        // Если у тебя скрипт называется SmartObject или smartgrabobject:
        
        part.AddComponent<XRGrabInteractable>();

        // Настройка Rigidbody для работы в невесомости по умолчанию
        rb.useGravity = false;
        rb.linearDamping = 0.5f;
    }

    // Проверка: находится ли нож в запретной зоне?
    private bool IsInsideForbiddenTrigger()
    {
        // Проверяем сферу вокруг точки реза
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (var hit in hitColliders)
        {
            if (hit.isTrigger && hit.CompareTag(forbiddenTag))
            {
                return true; // Нашли запретный триггер
            }
        }
        return false;
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }
}