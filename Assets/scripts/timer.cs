using UnityEngine;
using System.Collections;
public class timer : MonoBehaviour
{
    private skovoroda skovoroda;
    private FriedEggs FriedEggs;
    public string prosto = "prosto";
    public void startcor()
    {
        StartCoroutine(StartTimer(20f));
    }
    public IEnumerator StartTimer(float seconds)
    {
        skovoroda = GameObject.FindFirstObjectByType<skovoroda>();
        FriedEggs = GameObject.FindFirstObjectByType<FriedEggs>();
        Debug.Log("Таймер запущен на " + seconds + " секунд");
        skovoroda.graboff();
        FriedEggs.grabOffEgg();
        // Ждем указанное количество секунд
        yield return new WaitForSeconds(seconds);

        Debug.Log("прошло 20 сек");
        FriedEggs.grabOnEgg();
        skovoroda.grabOn();
    }

    
}
