using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class skovoroda : MonoBehaviour
{

    [SerializeField]private string gametag;
    private string starttag;
    [SerializeField]private GameObject thisgame;
    private XRGrabInteractable grabInteractable;
    void Start()
    {
        starttag = thisgame.tag;
        Debug.Log(starttag);
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Magnet")
        {
            thisgame.tag = gametag;
        }

    }
    void FixedUpdate()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if(grabInteractable.isSelected == true)
        {
            thisgame.tag = starttag;
        }
    }
    public void grabOn()
    {
        
        grabInteractable.enabled = true;
    }
    public void graboff()
    {
        grabInteractable.enabled = false;   
    }
}
