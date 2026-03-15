using UnityEngine;

public class tarelka : MonoBehaviour
{
    private Transform trans;

    void Start()
    {
        trans = gameObject.transform.GetChild(0).GetComponent<Transform>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (transform.childCount <=1)
        {


            if (other.gameObject.tag == "eda")
            {
                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                Vector3 scale = other.gameObject.transform.localScale;
                
                rb.isKinematic = true;
                other.transform.SetParent(this.transform, false);
                other.transform.position = trans.position;
                other.transform.localScale = new Vector3(1,1,1);
                other.transform.rotation = Quaternion.Euler(0, 0, 0);
                
                
            }

        }
    }
}
