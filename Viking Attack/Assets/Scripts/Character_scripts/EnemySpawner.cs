using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour

{
    [Header("Drag the prefab you want to spawn in this spawner here")]
    [SerializeField] private GameObject enemyPrefabToSpawn;
    [SerializeField] private bool stop;
    
    private IEnumerator EnemySpawn()
    {
        
        while (!stop)
        {
            var enemy = Instantiate(enemyPrefabToSpawn, gameObject.transform);
            NetworkServer.Spawn(enemy);
            yield return new WaitForSeconds(10);
        }
    }

    public override void OnStartServer()
    {
        StartCoroutine(EnemySpawn());
    }
}
