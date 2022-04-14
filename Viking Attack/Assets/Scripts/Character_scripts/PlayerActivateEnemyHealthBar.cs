using System;
using System.Linq;
using System.Security;
using UnityEngine;

namespace DefaultNamespace
{
    // WHO TO BLAME: Martin Kings
    public class PlayerActivateEnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask; // The layermask of the enemies
        private RaycastHit[] hits; // the hits detected by the spherecast

        private RaycastHit[]
            previousHits; // the previous hits by the spherecast, used for comparison to determine what objects to enable and disable


        private void Awake()
        {
            previousHits = new RaycastHit[] { };
        }

        void FixedUpdate()
        {
            // All enemies detected by the SphereCast
            hits = Physics.SphereCastAll(gameObject.transform.Find("Main Camera").transform.position, 3,
                gameObject.transform.Find("Main Camera").transform.forward, 10, layerMask);

            if (previousHits.Length >
                0) // makes sure that the previousHits array contains objects before iterating through it.
            {
                foreach (var hit in previousHits)
                {
                    if (!hits.Contains(hit)) // sets the healthbar to inactive in the case it no longer is detected by the player.
                    {
                        if (hit.collider != null) // checks that the object hasn't been "killed" yet.
                        {
                            hit.collider.gameObject.transform.Find("Parent").gameObject.transform.Find("Health_bar")
                                .gameObject.SetActive(false);
                        }
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