using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private float spawnDelay;

    [SerializeField] private Enemy enemyPrefab;

    public void Spawn()
    {
        Enemy newEnemy = Instantiate(enemyPrefab, transform.position,
 Quaternion.identity) as Enemy;

        newEnemy.Target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0.0f, spawnDelay);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
