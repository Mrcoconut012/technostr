using UnityEngine;

public class castrulaRice : MonoBehaviour
{
    [SerializeField]private int score;
    [SerializeField]private vertelka vertelka;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "krupa")
        {
            if(score <= 10)
            {
                score += 1;
                Destroy(other.gameObject);
            }
            if(score >= 10)
            {
                vertelka.stop();
            }
        }
    }
}
