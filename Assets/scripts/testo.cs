using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class testo : MonoBehaviour
{
    private string sasuage = "sasuage_slice";
    private string tomato = "tomato_slice";
    private string ketchup = "ketchup";

    [SerializeField] private GameObject readyPizzaPrefab; // Ссылка на префаб готовой пиццы
    [SerializeField] public int scorekolb;
    [SerializeField] private GameObject pos;
    public int tomatoscore;
    public int ketchupscore;

    private protiven protivenScript; // Переименовал для ясности

    void Start()
    {
        // Ищем скрипт один раз при старте
        protivenScript = Object.FindAnyObjectByType<protiven>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (protivenScript.isCooking == true && other.gameObject.tag == "pech")
        {

            gameObject.GetComponent<XRGrabInteractable>().enabled = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.position = protivenScript.spawnpospizza.transform.position;

        }
    }
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name == sasuage && scorekolb < 2)
        {
            Destroy(collision.gameObject);
            scorekolb += 1;
        }

        if (collision.gameObject.name == ketchup && ketchupscore < 1)
        {
            Destroy(collision.gameObject);
            ketchupscore += 1;
        }

        if (collision.gameObject.name == tomato && tomatoscore < 2)
        {
            Destroy(collision.gameObject);
            tomatoscore += 1;
        }
    }

    void Update() // Для проверки условий лучше использовать Update
    {
        
        if (protivenScript != null && protivenScript.readyPizza == true)
        {
            
            ReplaceObject();
        }
    }

    void ReplaceObject()
    {
        if (readyPizzaPrefab != null)
        {
            // 1. Создаем новый объект и сохраняем ссылку на него
            GameObject newPizza = Instantiate(readyPizzaPrefab, transform.position, transform.rotation);
            newPizza.GetComponent<Rigidbody>().useGravity = false;
            // 2. Меняем имя нового объекта
            newPizza.name = "tarelka_pizza"; // <-- Установка нужного имени
        }

        // 3. Уничтожаем старое тесто
        Destroy(gameObject);
    }
}