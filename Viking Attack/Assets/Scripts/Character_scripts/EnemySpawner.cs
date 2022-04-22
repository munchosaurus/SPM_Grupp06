using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour

{
    [SerializeField] private GameObject ball;
    [SerializeField] private bool stop;
    
    private IEnumerator EnemySpawn()
    {
        
        while (!stop)
        {
            Debug.Log("Hej");
            var enemy = Instantiate(ball, gameObject.transform);
            NetworkServer.Spawn(enemy);
            
            
            
            yield return new WaitForSeconds(10);
        }
    }

    public override void OnStartServer()
    {
        StartCoroutine(EnemySpawn());
    }
}
