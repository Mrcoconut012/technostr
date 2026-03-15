using UnityEngine;

public class KrupaCheck : MonoBehaviour
{
    //[SerializeField] private GameObject krupa;
    //[SerializeField] private LayerMask Layer;
    [SerializeField] private string tagobj;
    [SerializeField] private  int porog = 100;
    public int score;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == tagobj)
        {
            score += 1;
        }
        if(score >= 100 & collision.gameObject.tag == tagobj)
        {
            Destroy(collision.gameObject);
        }
    }
    
}
