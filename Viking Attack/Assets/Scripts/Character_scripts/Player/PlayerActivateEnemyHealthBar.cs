using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Security;
using Mirror;
using UnityEngine;

namespace DefaultNamespace
{
    // WHO TO BLAME: Martin Kings
    public class PlayerActivateEnemyHealthBar : NetworkBehaviour
    {
        // The layermask of the enemies
        [SerializeField] private LayerMask layerMask;

        // the prefab of the enemy health bar to create.
        [SerializeField] private GameObject enemyHealthPrefab;

        // the hits detected by the spherecast
        private RaycastHit[] hits;

        // the previous hits by the spherecast, used for comparison to determine what objects to enable and disable
        private RaycastHit[] previousHits;
        
        // The instanceIDs of all enemies spotted in each frame
        private List<int> instancesOfEnemiesSpotted;
        
        // All Enemy health bars that exist and belong to an enemy
        private List<GameObject> instancesOfEnemyHealthBars;
        
        // Will be updated when health bars are to be deactivated
        private List<GameObject> instancesToDisable;

        private Camera mainCamera;


        private void Awake()
        {
            instancesOfEnemiesSpotted = new List<int>();
            instancesOfEnemyHealthBars = new List<GameObject>();
            instancesToDisable = new List<GameObject>();
            previousHits = new RaycastHit[] { };
            mainCamera = GameObject.FindGameObjectWithTag("CameraMain").GetComponent<Camera>();
        }

        void FixedUpdate()
        {
            if (!isLocalPlayer) return;
            // All enemies detected by the SphereCast
            hits = Physics.SphereCastAll(mainCamera.transform.position, 3,
                mainCamera.transform.forward, 10, layerMask);

            // makes sure that the previousHits array contains objects before iterating through it.
            if (previousHits.Length > 0)
            {
                foreach (var previousHit in previousHits) // loops through all hits in the previous frame
                {   
                    // checks if the previousHit shouldn't have the health bar left
                    bool shouldDisable = CheckForHit(previousHit.collider.transform.gameObject.GetInstanceID());

                    if (shouldDisable)
                    {
                        foreach (GameObject go in instancesOfEnemyHealthBars)
                        {
                            if (previousHit.transform.gameObject.GetInstanceID() ==
                                go.GetComponent<EnemyHealthBar>().GetPersonalInstanceID())
                            {
                                instancesToDisable.Add(go);
                            }
                        }
                    }
                }
            }

            // Handles instances of the health bar to remove
            if (instancesToDisable.Count > 0)
            {
                foreach (var goToDisable in instancesToDisable)
                {
                    instancesOfEnemiesSpotted.Remove(goToDisable.GetComponent<EnemyHealthBar>().GetPersonalInstanceID());
                    goToDisable.SetActive(false);
                }

                instancesToDisable.Clear(); // clears after the objects have been handled
            }

            // Instantiates an health bar for each enemy in sight if one is missing
            foreach (var hit in hits)
            {
                // if the enemy wasn't spotted in the previous frame, will simply update previousHits and move to the next frame
                if (instancesOfEnemiesSpotted.Contains(hit.transform.gameObject.GetInstanceID()) == false)
                {
                    bool alreadyExists = false; 
                    foreach (var healthBar in instancesOfEnemyHealthBars)
                    {
                        //  Will set the found healthBar to active if it already exists, else a new one will be created
                        if (hit.collider.gameObject.GetInstanceID() == healthBar.gameObject.GetComponent<EnemyHealthBar>().GetPersonalInstanceID())
                        {
                            healthBar.SetActive(true);
                            alreadyExists = true;
                            break;
                        }
                    }

                    if (!alreadyExists)
                    {
                        GameObject go = SetupHealthBar(hit);
                        instancesOfEnemyHealthBars.Add(go);
                    }

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
            
            go.GetComponent<EnemyHealthBar>().Setup(gameObject.transform, uiTargetToFollow, enemy, enemy.GetComponent<EnemyInfo>(), mainCamera);


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