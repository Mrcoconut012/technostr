using UnityEngine;

public class rbOn : MonoBehaviour
{
    // Срабатывает, когда объект входит в зону
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            other.attachedRigidbody.useGravity = true;
        }
    }

    // Срабатывает, когда объект покидает зону
    
}