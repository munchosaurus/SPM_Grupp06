using System;
using System.Linq;
using System.Security;
using UnityEngine;
using Mirror; 

namespace DefaultNamespace
{
    // WHO TO BLAME: Martin Kings
    public class PlayerActivateEnemyHealthBar : NetworkBehaviour
    {
        [SerializeField] private LayerMask layerMask; // The layermask of the enemies
        private RaycastHit[] hits; // the hits detected by the spherecast
         
        private RaycastHit[]
            previousHits; // the previous hits by the spherecast, used for comparison to determine what objects to enable and disable

        private Camera mainCamera;
        private void Awake()
        {
            previousHits = new RaycastHit[] { };
        }
        private void Start()
        {
            if (!isLocalPlayer) return;
            mainCamera = GameObject.FindGameObjectWithTag("CameraMain").GetComponent<Camera>();
        }

        void FixedUpdate()
        {
            if (!isLocalPlayer) return;
            // All enemies detected by the SphereCast
            hits = Physics.SphereCastAll(mainCamera.transform.position, 3,
                mainCamera.transform.forward, 10, layerMask);

            if (previousHits.Length >
                0) // makes sure that the previousHits array contains objects before iterating through it.
            {
                foreach (var hit in previousHits)
                {
                    if (!hits.Contains(hit) &&
                        hit.collider !=
                        null) // sets the healthbar to inactive in the case it no longer is detected by the player.
                    {
                        hit.collider.gameObject.transform.Find("Parent").gameObject.transform.Find("Health_bar")
                            .gameObject.SetActive(false);
                    }
                }
            }

            foreach (var hit in hits)
            {
                hit.collider.gameObject.transform.Find("Parent").gameObject.transform.Find("Health_bar").gameObject
                    .SetActive(true); // sets them to Active
            }

            previousHits = hits;
        }
    }
}