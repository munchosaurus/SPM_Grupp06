using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerActivateFriendlyPlayerName : NetworkBehaviour
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

        private List<uint> instancesOfFriendliesSpotted;

        private List<GameObject> instancesToDisable;

        private Camera mainCamera;

        private void Awake()
        {

            if (isLocalPlayer)
            {
                return;
            }
            //gameObject.GetComponentInChildren<Canvas>().enabled = true;
            instancesToDisable = new List<GameObject>();
            instancesOfFriendliesSpotted = new List<uint>();
            instancesOfFriendlyNames = new List<GameObject>();
            previousHits = new RaycastHit[] { };
            mainCamera = GameObject.FindGameObjectWithTag("CameraMain").GetComponent<Camera>();
        }

        private void FixedUpdate()
        {
            if (!isLocalPlayer)
            {
                return;
            }
            
            // All friendly players detected by the SphereCast
            hits = Physics.SphereCastAll(mainCamera.transform.position, 3,
                mainCamera.transform.forward, 10, layerMask);
            
           

            
            
            // makes sure that the previousHits array contains objects before iterating through it.
            if (previousHits.Length > 0)
            {
                foreach (var previousHit in previousHits)
                {
                    bool shouldDisable = CheckForHit(previousHit.collider.gameObject.GetInstanceID());

                    if (shouldDisable)
                    {
                        foreach (var go in instancesOfFriendlyNames)
                        {
                            if (previousHit.transform.gameObject.GetComponent<NetworkIdentity>().netId ==
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
                foreach (var goToDisable in instancesToDisable)
                {
                    instancesOfFriendliesSpotted.Remove(goToDisable.GetComponent<FriendlyNameDisplay>()
                        .GetPersonalInstanceID());
                    goToDisable.SetActive(false);
                }

                instancesToDisable.Clear(); // clears after the objects have been handled
            }

            // Instantiates an health bar for each enemy in sight if one is missing
            foreach (var hit in hits)
            {
                if (instancesOfFriendliesSpotted.Contains(hit.transform.gameObject.GetComponent<NetworkIdentity>()
                        .netId) == false && hit.collider.gameObject.GetComponent<NetworkIdentity>().netId !=
                    gameObject.GetComponent<NetworkIdentity>().netId)
                {
                    bool alreadyExists = false;

                    foreach (var friendlyName in instancesOfFriendlyNames)
                    {
                        //  Will set the found healthBar to active if it already exists, else a new one will be created
                        if (hit.collider.gameObject.GetComponent<NetworkIdentity>().netId == friendlyName.gameObject
                                .GetComponent<FriendlyNameDisplay>().GetPersonalInstanceID())
                        {
                            friendlyName.SetActive(true);
                            alreadyExists = true;
                            break;
                        }
                    }

                    if (!alreadyExists)
                    {
                        GameObject go = SetupFriendlyName(hit);
                        instancesOfFriendlyNames.Add(go);
                    }


                    // Adds to all friend instances (saves the instanceID)
                    instancesOfFriendliesSpotted.Add(hit.transform.gameObject.GetComponent<NetworkIdentity>()
                        .netId);
                }
            }
            
            previousHits = hits;
        }

        // Sets up the health bar instance and assigns proper values, must be cleaned up
        GameObject SetupFriendlyName(RaycastHit hit)
        {
            var player = hit.collider.transform;
            var go = Instantiate(friendlyNamePrefab,
                gameObject.transform); // creates the friendly name text instance

            //GameObject go1 =NetworkIdentity.spawned[netId].gameObject;
            uint id = hit.collider.gameObject.GetComponent<NetworkIdentity>().netId;
            //go.GetComponent<FriendlyNameDisplay>().Setup(gameObject.transform, player, player.GetComponent<GlobalPlayerInfo>(), mainCamera);
            go.GetComponent<FriendlyNameDisplay>().Setup(gameObject.transform, id, mainCamera);

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