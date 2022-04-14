using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerActivateEnemyHealthBar : MonoBehaviour
    {
        private GameObject targetHealthBar;
        private bool hitting = false;
        private GameObject hitObject;

        void Update()
        {
            //     //Sends a raycast to check for colliders in the Enemy layer
            //     RaycastHit hit;
            //
            //     if (Physics.SphereCast(gameObject.transform.Find("Main Camera").transform.position, 2f,
            //             gameObject.transform.Find("Main Camera").transform.forward, out hit, 25)
            //     {
            //         GameObject go = hit.transform.gameObject ;
            //         
            //         if (hitObject == null)
            //         {
            //             try
            //             {
            //                 go.transform.Find("Parent").gameObject.transform.Find("Health_bar").SendMessage("Activate");
            //             }
            //             catch (Exception e)
            //             {
            //                 Debug.Log("Funkar ej bra");
            //             }
            //             
            //         }
            //         else if( hitObject.GetInstanceID() == go.GetInstanceID() )
            //         {
            //             hitObject.SendMessage( "Display" );
            //         }
            //     }
            // }
        }
    }
}