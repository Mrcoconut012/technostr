using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class isSelected : MonoBehaviour
{
    [SerializeField]private XRGrabInteractable grabInteractable;
    [SerializeField] private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }
    void Update()
    {
        if (grabInteractable.isSelected == false)
        {
            rb.isKinematic = false;
        }
    }
}
