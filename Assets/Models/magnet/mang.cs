using UnityEngine;

public class mang : MonoBehaviour
{
    [SerializeField] private Transform snapPoint; // Точка, куда "прилипнет" объект
    public bool check = false;
    
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("ads");
        smartgrabobject smartObj = other.GetComponent<smartgrabobject>();

        if (smartObj != null)
        {
            
            Vector3 targetPos = snapPoint != null ? snapPoint.position : transform.position;
            Quaternion targetRot = snapPoint != null ? snapPoint.rotation : Quaternion.identity;
            check = true;
            smartObj.TryFreeze(targetPos, targetRot);
        }
    }
}
