using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBonus : MonoBehaviour
{

  

    void OnTriggerEnter(Collider other)
    {
        Destructable otherHealth = other.gameObject.GetComponent<Destructable>();
        if (otherHealth != null)
        { 
            otherHealth.hitPointsCurrent = otherHealth.hitPoints;
            Destroy(gameObject);
        }
    }

    public static void Create(Vector3 position)
    {
      Instantiate(Resources.Load("HealthBonus"), position, Quaternion.identity);
		
    }

}
