using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour
{
    [SerializeField] private ItemBase itemBase; // Will need to be updated if another item is being used.
    void Update()
    {
        //Sends a raycast to check for colliders in the Enemy layer
        RaycastHit hit;
        if (Physics.SphereCast(gameObject.transform.Find("Main Camera").transform.position, 1f,
                gameObject.transform.Find("Main Camera").transform.forward, out hit, itemBase.GetRange(), LayerMask.GetMask("Enemy")))
        {
            //Calls the function to say that the object is interacted with
            if (Input.GetMouseButtonDown(0))
            {
                hit.collider.gameObject.GetComponent<EnemyMovement>().UpdateHealth(-itemBase.GetDamage());
            }
        }
    }
}