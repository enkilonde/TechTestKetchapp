using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collision : MonoBehaviour
{
    private bool isPlayer;
    public Health health;
    public VehicleMove vehicleMove;

    // Start is called before the first frame update
    void Awake()
    {
        isPlayer = CompareTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer)
            PlayerCollision(other);
        else
            CopCollision(other);


    }


    private void PlayerCollision(Collider other)
    {
        if (other.CompareTag("Cop"))
            health.Hit();
        else if (other.CompareTag("Coin"))
        {
            GlobalManager.instance.coins++;
            vehicleMove.Boost();
            Destroy(other.gameObject);
            SoundManager.instance.CoinPickup();
        }
    }

    private void CopCollision(Collider other)
    {
        if (other.CompareTag("Cop"))
        {
            other.GetComponent<Health>().Hit();
            health.Hit();
        }
    }


}
