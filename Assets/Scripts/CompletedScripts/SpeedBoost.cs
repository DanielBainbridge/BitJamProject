using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [Range(1,10)]public float speedMultiplier = 1;
    [Range(1,10)]public float speedBoostTime = 1;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        player = other.transform.GetComponent<PlayerController>();
        if(player != null)
        {
            gameObject.SetActive(false);
            Invoke("Activate", speedBoostTime);
            MultiplySpeed();
            Invoke("DivideSpeed", speedBoostTime);
        }
    }

    private void MultiplySpeed()
    {
        player.maxAcceleration *= speedMultiplier;
        player.maxSpeed *= speedMultiplier;
    }
    private void DivideSpeed()
    {
        player.maxAcceleration /= speedMultiplier;
        player.maxSpeed /= speedMultiplier;
    }

    private void Activate()
    {
        gameObject.SetActive(true);
    }

}
