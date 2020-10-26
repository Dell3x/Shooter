using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float damage = 10.0f;
    [SerializeField] private GameObject owner;
	[SerializeField] private GameObject explosionPrefab;
	 private float radius; 

    public float Damage { get => damage; set => damage = value; }



    private void OnCollisionEnter(Collision collision)
    {

		if (explosionPrefab != null) {

	          ExplosionScript.Create(transform.position, explosionPrefab);
	    }

		
	     if (radius > 0) 
	   {
    		CauseExplosionDamage();
	   }

	   else  
	   {
    		Destructable target = collision.gameObject.GetComponent<Destructable>();
    	 if (target != null) 
		 {
        		target.Hit(Damage);
    	 }
	   }


	}





	private void CauseExplosionDamage() 
	{
	
	     Collider[] explosionVictims = Physics.OverlapSphere(transform.position, radius);

		 for (int i = 0; i<explosionVictims.Length; i++) 
		 {
		    Vector3 vectorToVictim = explosionVictims[i].transform.position - transform.position;
	        float decay = 1 - (vectorToVictim.magnitude / radius);

		     Destructable currentVictim  =  explosionVictims[i].gameObject.GetComponent<Destructable>();

             if (currentVictim != null)	
			 {
                 currentVictim.Hit(damage * decay);
             }

			 Rigidbody victimRigidbody = explosionVictims[i].gameObject.GetComponent<Rigidbody>();

           if (victimRigidbody != null)	
		   {
    	         victimRigidbody.AddForce(vectorToVictim.normalized* decay * 1000);
	       }


		 }
	}
}