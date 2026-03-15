using UnityEngine;
using EzySlice;

public class SliceVr : MonoBehaviour
{
    public LayerMask sliceLayer;
    public Material crossSectionMaterial;

    private Vector3 previousPosition;

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        Vector3 direction = transform.position - previousPosition;

        if (direction.magnitude > 0.01f)
        {
            RaycastHit hit;

            if (Physics.Raycast(previousPosition, direction, out hit, direction.magnitude, sliceLayer))
            {
                SliceObject(hit.collider.gameObject);
            }
        }

        previousPosition = transform.position;
    }

    void SliceObject(GameObject obj)
    {
        SlicedHull hull = obj.Slice(transform.position, transform.up, crossSectionMaterial);

        if (hull != null)
        {
            GameObject upper = hull.CreateUpperHull(obj, crossSectionMaterial);
            GameObject lower = hull.CreateLowerHull(obj, crossSectionMaterial);

            Destroy(obj);

            upper.AddComponent<Rigidbody>();
            lower.AddComponent<Rigidbody>();
        }
    }
}