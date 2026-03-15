using UnityEngine;

public class razbegg : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        

    }

    
}
