using UnityEngine;

public class rbotkl : MonoBehaviour
{   
    void Start()
    {
        // Находим все Rigidbody на сцене в момент старта
        Rigidbody[] allBodies = FindObjectsByType<Rigidbody>(FindObjectsSortMode.None);

        foreach (Rigidbody rb in allBodies)
        {
            rb.useGravity = false;

            
        }
    }
}

