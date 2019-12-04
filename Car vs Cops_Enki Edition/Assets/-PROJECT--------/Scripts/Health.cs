using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hitPoints = 1;
    public float invicibilityTime = 1;
    private float invicibilityTimer = 0;
    
    [HideInInspector]
    public int currentHitPoints;

    private bool isPlayer;

    public ParticleSystem smoke;
    public ParticleSystem fire;
    public GameObject explosionFX_Prefab;

    private void Awake()
    {
        currentHitPoints = hitPoints;
        isPlayer = CompareTag("Player");
    }

    private void Update()
    {
        if(invicibilityTimer > 0)
            invicibilityTimer -= Time.deltaTime;
    }

    public void Hit()
    {
        if (invicibilityTimer > 0)
            return;
        invicibilityTimer = invicibilityTime;

        SoundManager.instance.Collision();

        UIDebug.WriteLine(name + " hit ");

        currentHitPoints--;

        switch (currentHitPoints)
        {
            case 2:
                smoke.Play();
                break;
            case 1:
                smoke.Stop();
                fire.Play();
                break;
            case 0:
                Death();
                break;
            default:
                break;
        }

    }

    private void Death()
    {
        GameObject explosion = Instantiate(explosionFX_Prefab, transform.position, Quaternion.identity);
        Destroy(explosion, 3);
        SoundManager.instance.Explosion();

        if(isPlayer)
        {
            GlobalManager.instance.EndGame();
        }
        else
        {
        }
            Destroy(gameObject);
    }
}
