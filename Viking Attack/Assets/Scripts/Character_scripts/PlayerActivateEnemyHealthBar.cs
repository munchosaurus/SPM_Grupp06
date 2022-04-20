using System;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Security;
using UnityEngine;

namespace DefaultNamespace
{
    // WHO TO BLAME: Martin Kings
    public class PlayerActivateEnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask; // The layermask of the enemies
        [SerializeField] private GameObject enemyHealthPrefab; // the prefab of the enemy health bar.
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
                        EnemyInfo enemyInfo = hit.collider.transform.GetComponent<EnemyInfo>();
                        
                        if (hit.collider != null) // checks that the object hasn't been "killed" yet.
                        {
                            enemyInfo.SetHealthBarStatus(false);
                            
                        }
                    }
                }
            }

            // Instantiates an health bar for each enemy in sight
            foreach (var hit in hits)
            {
                EnemyInfo enemyInfo = hit.collider.transform.GetComponent<EnemyInfo>();
                Debug.Log(enemyInfo);
                if (!enemyInfo.CheckHealthBarStatus())
                {
                    enemyInfo.SetHealthBarStatus(true);
                    Transform target = hit.collider.gameObject.transform.Find("Overhead").gameObject.transform;
                    GameObject go = Instantiate(enemyHealthPrefab, gameObject.transform);
                    go.transform.parent = gameObject.transform.Find("UI");
                    go.GetComponent<EnemyHealthBar>().target = target; // determines target for this UI object
                    go.GetComponent<EnemyHealthBar>().SetHealthSource(hit.collider.gameObject); // Sets the health source to an exact copy of the enemy
                    go.GetComponent<EnemyHealthBar>().healthBar.maxValue = enemyInfo.maxHealth; // sets the maximum health of the slider
                }
            }

            previousHits = hits;
        }
    }
}