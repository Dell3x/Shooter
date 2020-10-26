
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : Character
{
    [SerializeField] private NavMeshAgent _navMeshAgent;

    [SerializeField] private Transform target;

    public Transform Target;

    private bool seeTarget;

    [SerializeField] private GameObject bulletPrefab; // объект пуля

    [SerializeField] private GameObject rocketPrefab; // объект ракета

    [SerializeField] private Transform gun; // точка вылета пули

    [SerializeField] private Transform rocketgun; // точка вылета ракеты

    [SerializeField] private float shootBulletPower = 5f;// сила выстрела пули

    [SerializeField] private float shootRocketPower = 5f;// сила выстрела ракеты

    [SerializeField] private float bulletDamage = 10f; // урон пулей

    [SerializeField] private float rocketDamage = 20f; // урон ракетой

    [SerializeField] private float shootDelay = 5f; // частота выстрела врага

	[SerializeField] private GameObject explosionPrefab;

    void Start()
    {
        InvokeRepeating("Shoot", 1.0f, shootDelay);
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        _navMeshAgent.SetDestination(target.position);

        CheckTargetVisibility();
       
    }

    private void CheckTargetVisibility()
    {
        Vector3 targetDirection = target.position - gun.transform.position;

        Ray ray = new Ray(gun.transform.position, targetDirection);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == target)
            {
                seeTarget = true;
                return;
            }
        }

        seeTarget = false;
    }

    public void Shoot()
    {
        if (seeTarget == true)
        {
            GameObject newBullet = Instantiate(bulletPrefab, gun.position, gun.rotation) as GameObject;
            Vector3 targetDirection = target.position - gun.transform.position;
            targetDirection.Normalize();

            newBullet.GetComponent<Rigidbody>().AddForce(targetDirection * shootBulletPower);
            newBullet.GetComponent<Damager>().Damage = rocketDamage;

            ShootBullet();
        }
    }

    public void Destroyed() // если объект уничтожен, выпадает аптечка
    {

        ScoreLabel.Score += 25;
		ExplosionScript.Create(transform.position, explosionPrefab);

        if (Random.Range(0, 100) < 50)
        {
            HealthBonus.Create(transform.position);
        }
        
    }




}