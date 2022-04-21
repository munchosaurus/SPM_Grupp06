using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Security;
using UnityEngine;

namespace DefaultNamespace
{
    // WHO TO BLAME: Martin Kings
    public class PlayerActivateEnemyHealthBar : MonoBehaviour
    {
        // The layermask of the enemies
        [SerializeField] private LayerMask layerMask;

        // the prefab of the enemy health bar to create.
        [SerializeField] private GameObject enemyHealthPrefab;

        // the hits detected by the spherecast
        private RaycastHit[] hits;

        // the previous hits by the spherecast, used for comparison to determine what objects to enable and disable
        private RaycastHit[] previousHits;
        
        private List<int> instancesOfEnemiesSpotted;
        private List<GameObject> instancesOfEnemyHealthBars;
        private List<GameObject> instancesToRemove;


        private void Awake()
        {
            instancesOfEnemiesSpotted = new List<int>();
            instancesOfEnemyHealthBars = new List<GameObject>();
            instancesToRemove = new List<GameObject>();
            previousHits = new RaycastHit[] { };
        }

        void FixedUpdate()
        {
            // All enemies detected by the SphereCast
            hits = Physics.SphereCastAll
            (gameObject.transform.Find("Main Camera").transform.position,
                3,
                gameObject.transform.Find("Main Camera").transform.forward,
                10,
                layerMask);

            // makes sure that the previousHits array contains objects before iterating through it.
            if (previousHits.Length > 0)
            {
                foreach (var previousHit in previousHits) // loops through all hits in the previous frame
                {   
                    // checks if the previousHit shouldn't have the health bar left
                    bool shouldRemove = CheckForHit(previousHit.collider.transform.gameObject.GetInstanceID());

                    if (shouldRemove)
                    {
                        foreach (GameObject go in instancesOfEnemyHealthBars)
                        {
                            if (previousHit.transform.gameObject.GetInstanceID() ==
                                go.GetComponent<EnemyHealthBar>().instanceID)
                            {
                                instancesToRemove.Add(go);
                            }
                        }
                    }
                }
            }

            // Handles instances of the health bar to remove
            if (instancesToRemove.Count > 0)
            {
                foreach (var goToRemove in instancesToRemove)
                {
                    instancesOfEnemiesSpotted.Remove(goToRemove.GetComponent<EnemyHealthBar>().instanceID);
                    instancesOfEnemyHealthBars.Remove(goToRemove);
                    Destroy(goToRemove);
                }

                instancesToRemove.Clear(); // clears after the objects have been handled
            }

            // Instantiates an health bar for each enemy in sight if one is missing
            foreach (var hit in hits)
            {
                if (instancesOfEnemiesSpotted.Contains(hit.transform.gameObject.GetInstanceID()) == false)
                {
                    GameObject go = SetupHealthBar(hit);
                    instancesOfEnemyHealthBars.Add(go);

                    // Adds to all enemy instances (saves the instanceID)
                    instancesOfEnemiesSpotted.Add(hit.transform.gameObject.GetInstanceID());
                }
            }

            previousHits = hits;
        }


        // Sets up the health bar instance and assigns proper values, must be cleaned up
        GameObject SetupHealthBar(RaycastHit hit)
        {
            var enemy = hit.collider.transform;
            var uiTargetToFollow = enemy.Find("Overhead").gameObject.transform; // sets the target location
            var go = Instantiate(enemyHealthPrefab,
                gameObject.transform); // creates the health bar instance
            
            go.GetComponent<EnemyHealthBar>().Setup(gameObject.transform, uiTargetToFollow, enemy, enemy.GetComponent<EnemyInfo>());


            return go;
        }

        // Checks for an instance ID clash 
        private bool CheckForHit(int previousHit)
        {
            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                {
                    // Will return false if there is one found
                    if (hit.collider.transform.gameObject.GetInstanceID() == previousHit)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}