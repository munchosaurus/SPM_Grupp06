using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerActivateFriendlyPlayerName : MonoBehaviour
    {
        // The layermask of the other player
        [SerializeField] private LayerMask layerMask;

        // the prefab of the friendly name text to create.
        [SerializeField] private GameObject friendlyNamePrefab;

        // the hits detected by the spherecast
        private RaycastHit[] hits;

        // the previous hits by the spherecast, used for comparison to determine what objects to enable and disable
        private RaycastHit[] previousHits;

        private List<GameObject> instancesOfFriendlyNames;

        private List<int> instancesOfFriendliesSpotted;

        private List<GameObject> instancesToDisable;

        private void Awake()
        {
            instancesToDisable = new List<GameObject>();
            instancesOfFriendliesSpotted = new List<int>();
            instancesOfFriendlyNames = new List<GameObject>();
            previousHits = new RaycastHit[] { };
        }

        private void FixedUpdate()
        {
            hits = Physics.SphereCastAll
            (gameObject.transform.Find("Main Camera").transform.position,
                6,
                gameObject.transform.Find("Main Camera").transform.forward,
                60,
                layerMask);

            
            if (previousHits.Length > 0)
            {
                foreach (var previousHit in previousHits)
                {
                    bool shouldRemove = CheckForHit(previousHit.collider.gameObject.GetInstanceID());

                    if (shouldRemove)
                    {
                        foreach (var go in instancesOfFriendlyNames)
                        {
                            if (previousHit.transform.gameObject.GetInstanceID() ==
                                go.GetComponent<FriendlyNameDisplay>().GetPersonalInstanceID())
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
                foreach (var goToRemove in instancesToDisable)
                {
                    instancesOfFriendliesSpotted.Remove(goToRemove.GetComponent<FriendlyNameDisplay>().GetPersonalInstanceID());
                    instancesOfFriendlyNames.Remove(goToRemove);
                    Destroy(goToRemove);
                }

                instancesToDisable.Clear(); // clears after the objects have been handled
            }
            
            // Instantiates an health bar for each enemy in sight if one is missing
            foreach (var hit in hits)
            {
                if (instancesOfFriendliesSpotted.Contains(hit.transform.gameObject.GetInstanceID()) == false && hit.collider.gameObject.GetInstanceID() != gameObject.GetInstanceID())
                {
                    
                    GameObject go = SetupFriendlyName(hit);
                    instancesOfFriendlyNames.Add(go);

                    // Adds to all enemy instances (saves the instanceID)
                    instancesOfFriendliesSpotted.Add(hit.transform.gameObject.GetInstanceID());
                }
            }
            previousHits = hits;
        }
        
        // Sets up the health bar instance and assigns proper values, must be cleaned up
        GameObject SetupFriendlyName(RaycastHit hit)
        {
            var player = hit.collider.transform;
            var go = Instantiate(friendlyNamePrefab,
                gameObject.transform); // creates the health bar instance
            
            go.GetComponent<FriendlyNameDisplay>().Setup(gameObject.transform, player, player.GetComponent<GlobalPlayerInfo>());


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