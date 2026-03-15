using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class smartgrabobject : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grab;
    private mang mang;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        // Подписываемся на оба события: взял и отпустил
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease); // Добавили это
    }

    private void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrab);
        grab.selectExited.RemoveListener(OnRelease); // И это
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Когда взяли — выключаем кинематику, чтобы объект следовал за рукой
        rb.isKinematic = false;
        
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // КРИТИЧНО: Когда отпустили — тоже выключаем кинематику, 
        // чтобы объект начал падать (или лететь по инерции)
        rb.isKinematic = false;
        
    }

    public void TryFreeze(Vector3 pos, Quaternion rot)
    {
        if (!grab.isSelected)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Используем Lerp для плавности, чтобы не было рывков
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 15f);
            transform.rotation = Quaternion.Euler(0,Quaternion.identity.y,0);
        
        }
    }
}
