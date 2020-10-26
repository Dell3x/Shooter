using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab; // объект пуля
    [SerializeField] protected GameObject rocketPrefab; // объект ракета
    [SerializeField] public Transform gun; // точка вылета пули
    [SerializeField] public Transform rocketgun; // точка вылета ракеты
    [SerializeField] public float shootBulletPower = 5f;// сила выстрела пули
    [SerializeField] public float shootRocketPower = 5f;// сила выстрела ракеты
    [SerializeField] public float bulletDamage = 10f; // урон пулей
    [SerializeField] public float rocketDamage = 20f; // урон ракетой

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShootBullet() // выстрел
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject newBullet = Instantiate(bulletPrefab, gun.position, gun.rotation) as GameObject;
            newBullet.GetComponent<Rigidbody>().AddForce(gun.forward * shootBulletPower); // созадем новую пул.
            Destroy(newBullet, 1); // уничтожаем пулю
            newBullet.GetComponent<Damager>().Damage = bulletDamage;
        }

    }

    public void ShootRocket()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            GameObject newRocket = Instantiate(rocketPrefab, rocketgun.position, rocketgun.rotation) as GameObject;
            newRocket.GetComponent<Rigidbody>().AddForce(rocketgun.forward * shootRocketPower); // созадем новую пул.
            Destroy(newRocket, 3); // уничтожаем пулю
            newRocket.GetComponent<Damager>().Damage = rocketDamage;
        }

    }

    public void Owner()
    {


    }

    

}