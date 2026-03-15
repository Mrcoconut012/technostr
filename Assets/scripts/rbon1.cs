using UnityEngine;

public class rbon1 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            other.attachedRigidbody.useGravity = false;
        }
    }
}
