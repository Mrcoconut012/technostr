using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Slicer : MonoBehaviour
{
    public Material materialAfterSlice;
    public string sliceTag = "Sliceable";
    public string forbiddenTag = "NoKroshki";
    public bool isTouched;
    private Vector3 scale;
    [SerializeField] private GameObject kroshki;

    [Header("Замена маленького куска")]
    [SerializeField]private string[] strings; 
    private int smallPartReplacementPrefabScore;
    [SerializeField] private GameObject[] smallPartReplacementPrefab; // Префаб, на который заменим мелкий кусок

    [Header("Настройки кулдауна")]
    public float sliceCooldown = 0.5f;
    private float lastSliceTime = -1f;
    
    private void Update()
    {
        if (isTouched && Time.time >= lastSliceTime + sliceCooldown)
        {
            isTouched = false;
            lastSliceTime = Time.time;

            Collider[] objectsInRange = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation);

            foreach (Collider objectToBeSliced in objectsInRange)
            {
                if (objectToBeSliced.CompareTag(sliceTag))
                {
                    Slice(objectToBeSliced.gameObject);
                }
            }
        }
    }

    private void Slice(GameObject target)
    {
        SlicedHull slicedObject = target.Slice(transform.position, transform.up, materialAfterSlice);
        scale = target.transform.localScale;
        if (slicedObject != null)
        {
            if(target.gameObject.name == strings[0])
            {
                smallPartReplacementPrefabScore = 0;
            }
            if (target.gameObject.name == strings[1])
            {
                smallPartReplacementPrefabScore = 1;
            }
            if (target.gameObject.name == strings[2])
            {
                smallPartReplacementPrefabScore = 2;
            }
            if (target.gameObject.name == strings[3])
            {
                smallPartReplacementPrefabScore = 3;
            }
            if (target.gameObject.name == strings[4])
            {
                smallPartReplacementPrefabScore = 4;
            }

            GameObject upperHull = slicedObject.CreateUpperHull(target, materialAfterSlice);
            GameObject lowerHull = slicedObject.CreateLowerHull(target, materialAfterSlice);

            // Вычисляем объемы
            float upperVolume = CalculateVolume(upperHull);
            float lowerVolume = CalculateVolume(lowerHull);

            if (upperVolume >= lowerVolume)
            {
                ProcessParts(upperHull, lowerHull, target);
            }
            else
            {
                ProcessParts(lowerHull, upperHull, target);
            }

            // Спавн крошек
            if (!IsInsideForbiddenTrigger() && kroshki != null)
            {
                Instantiate(kroshki, transform.position, transform.rotation);
            }

            Destroy(target);
        }
    }

    private void ProcessParts(GameObject bigPart, GameObject smallPart, GameObject original)
    {
        // 1. Работаем с большим куском: переименовываем
        bigPart.name = original.name;
        bigPart.transform.position = original.transform.position;
        bigPart.transform.rotation = original.transform.rotation;
        CopyData(original, bigPart);

        // 2. Работаем с маленьким куском: заменяем префабом
        if (smallPartReplacementPrefab != null)
        {
            GameObject replacement = Instantiate(smallPartReplacementPrefab[smallPartReplacementPrefabScore], smallPart.transform.position, smallPart.transform.rotation);
            replacement.name = original.name + "_slice";
            replacement.transform.localScale = original.transform.localScale;
            replacement.GetComponent<Rigidbody>().useGravity = false;
            
            // Если нужно, чтобы префаб вел себя так же (физика), вызываем CopyData
            CopyData(original, replacement);

            replacement.tag = "Untagged";

            Destroy(smallPart); // Удаляем сгенерированный меш, так как создали префаб
        }
        else
        {
            // Если префаб не назначен, просто пометим его
            smallPart.name = original.name + "_SmallPart";
            CopyData(original, smallPart);
        }
    }

    private float CalculateVolume(GameObject go)
    {
        Mesh mesh = go.GetComponent<MeshFilter>().sharedMesh;
        if (mesh == null) return 0;

        // Самый быстрый способ — взять объем Bounds (рамки меша)
        Vector3 size = mesh.bounds.size;
        return size.x * size.y * size.z;
    }

    private void CopyData(GameObject original, GameObject part)
    {
        part.tag = original.tag;
        part.layer = original.layer;

        if (!part.GetComponent<Rigidbody>())
        {
            Rigidbody rb = part.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.linearDamping = 0.5f;
        }

        if (!part.GetComponent<Collider>())
        {
            part.AddComponent<MeshCollider>().convex = true;
        }

        if (!part.GetComponent<XRGrabInteractable>())
        {
            part.AddComponent<XRGrabInteractable>();
        }
    }

    private bool IsInsideForbiddenTrigger()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (var hit in hitColliders)
        {
            if (hit.isTrigger && hit.CompareTag(forbiddenTag)) return true;
        }
        return false;
    }
}