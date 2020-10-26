using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{ 
    public float hitPointsCurrent;

    public float hitPoints = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        hitPoints = hitPointsCurrent;
    }

    public void Hit(float damage)
    {
        hitPointsCurrent -= damage;

        if (hitPointsCurrent <= 0)
        {
            Die();
        }

    }


    private void Die()
    {
        BroadcastMessage("Destroyed");
        Destroy(gameObject);
    }
}
