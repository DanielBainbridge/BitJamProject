using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePickup : MonoBehaviour
{
    NightTime nightControllerReference;
    public float secondsToDecrease = 1;
    public float amountToDecrease = 3;
    // Start is called before the first frame update
    void Start()
    {
        nightControllerReference = FindObjectOfType<NightTime>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        nightControllerReference.DecreaseCurrentTime(amountToDecrease, secondsToDecrease);
        gameObject.SetActive(false);
        Invoke("Activate", amountToDecrease);
    }

    private void Activate()
    {
        gameObject.SetActive(true);
    }
}
