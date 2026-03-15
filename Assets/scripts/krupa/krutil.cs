using UnityEngine;

public class krutil : MonoBehaviour
{
    [SerializeField] private Transform paket;
    [SerializeField] private GameObject krupinki;
    [SerializeField] private Transform spawnobj;
    private float xpos;
    private float zpos;

    void Update()
    {
        zpos = paket.position.z;
        xpos = paket.position.x;
        if ((xpos < -90) | (xpos > 90) | (zpos < -90) | (zpos > 90))
        {
            Debug.Log("Work");
            Instantiate(krupinki, spawnobj.position, Quaternion.identity);
        }
    }
}
