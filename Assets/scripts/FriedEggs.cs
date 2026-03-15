using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FriedEggs : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;
    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }
    public void grabOnEgg()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.enabled = true;

    }
    public void grabOffEgg()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.enabled = false;
    }
}
