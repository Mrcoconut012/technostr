using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class sandwich : MonoBehaviour
{
    private string sasuage = "sasuage_slice";
    private string tomato = "tomato_slice";
    [SerializeField] private GameObject sasuagespawn1;
    [SerializeField] private GameObject sasuagespawn2;
    [SerializeField] private GameObject tomatospawn;
    [SerializeField]private int score;
    private Vector3 pos;
    private int tomatoscore;
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == sasuage & score == 0)
        {
            Vector3 scale = collision.transform.localScale;
            collision.transform.SetParent(this.transform, false);
            collision.transform.localScale = scale;
            collision.transform.position = sasuagespawn1.transform.position;
            score += 1;
            collision.gameObject.name = "sosiska";
            collision.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.gameObject.GetComponent<MeshCollider>().enabled = false;
            collision.transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        if (collision.gameObject.name == sasuage & score == 1)
        {
            Vector3 scale = collision.transform.localScale;
            collision.transform.SetParent(this.transform, false);
            collision.transform.localScale = scale;
            collision.transform.position = sasuagespawn2.transform.position;
            score += 1;
            collision.gameObject.name = "sosiska";
            collision.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.gameObject.GetComponent<MeshCollider>().enabled = false;
            collision.transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        if(collision.gameObject.name == tomato & tomatoscore == 0)
        {
            Vector3 scale = collision.transform.localScale;
            collision.transform.SetParent(this.transform, false);
            collision.transform.localScale = scale;
            collision.transform.position = sasuagespawn1.transform.position;
            tomatoscore += 1;
            collision.gameObject.name = "sosiska";
            collision.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.gameObject.GetComponent<MeshCollider>().enabled = false;
            collision.transform.localRotation = Quaternion.Euler(0, 90, 0);
        }

    }
    void Update()
    {
        if(tomatoscore >= 1 && score >= 2 && gameObject.tag != "eda")
        {
            gameObject.tag = "eda";
        }
    }
}
