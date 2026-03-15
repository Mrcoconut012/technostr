using UnityEngine;

public class EggBreaker : MonoBehaviour
{
    public GameObject fracturedPrefab; // Префаб разбитого яйца
    public bool checkrazb;
    public float breakForce = 2.0f;    // Минимальная сила удара
    public string targetTag = "Skovoroda"; // Тег объекта, об который можно разбиться
    [SerializeField] private GameObject egg;
    [SerializeField] private Transform pos;
    public timer timer;

    private void OnCollisionEnter(Collision collision)
    {
        // 1. Проверяем, есть ли у объекта, с которым столкнулись, нужный тег
        if (collision.gameObject.CompareTag(targetTag))
        {
            // 2. Проверяем силу удара (чтобы не разбилось, если просто аккуратно положили)
            if (collision.relativeVelocity.magnitude > breakForce)
            {
                BreakEgg();
            }
        }
    }
    void Start()
    {
        GameObject egg = GameObject.Find("eggpos");
        
    }

    void BreakEgg()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 currentVelocity = Vector3.zero;

        if (rb != null)
        {
            // Используем linearVelocity для новых версий Unity (2023+)
            // Если у вас старая версия и linearVelocity подчеркивается красным, 
            // замените на rb.velocity
            currentVelocity = rb.linearVelocity;
        }
        
        GameObject fractured = Instantiate(fracturedPrefab, transform.position, transform.rotation);
        GameObject newEgg = Instantiate(egg, pos.position, Quaternion.identity);
        timer = newEgg.GetComponent<timer>();
        Debug.Log(timer.prosto);
        timer.startcor();
        Rigidbody[] fragments = fractured.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody childRb in fragments)
        {
            childRb.linearVelocity = currentVelocity;
            // Добавим импульс в сторону от точки удара для реалистичности
            childRb.AddExplosionForce(3.0f, transform.position, 0.5f);
            childRb.useGravity = false;
            checkrazb = true;
        }

        Destroy(gameObject);
    }
}