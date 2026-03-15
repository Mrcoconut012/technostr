using UnityEngine;

public class magnet_script : MonoBehaviour
{
    [SerializeField] private GameObject manget;
    [SerializeField] private Rigidbody mangetrb;
    private Vector3 magnetsp;
    public bool isSelec;
    void Start()
    {
        //manget = this.GetComponent<GameObject>();
        magnetsp.x = manget.transform.position.x;

        magnetsp.z = manget.transform.position.z;
    }
    void OnCollisionEnter(Collision collision)
    {
        
        if (isSelec != true)
        {
            Debug.Log(collision.gameObject.name);
            magnetsp.y = (collision.gameObject.transform.position.y)-0.01f;
            collision.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            mangetrb = collision.gameObject.GetComponent<Rigidbody>();
            collision.gameObject.transform.position = magnetsp;
            mangetrb.isKinematic = true;
            isSelec = true;
        }
        
    }
    void OnCollisionExit(Collision collision)
    {
        
        mangetrb = null;
        isSelec = false;
        Debug.Log("vishel");
    }
}
