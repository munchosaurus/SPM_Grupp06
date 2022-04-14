using System;
using System.Security;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerActivateEnemyHealthBar : MonoBehaviour
    {
        private GameObject targetHealthBar;
        private bool hitting = false;
        private GameObject hitObject;
        [SerializeField] private LayerMask layerMask;

        void Update()
        {
            RaycastHit hit;

            if (Physics.SphereCast(gameObject.transform.Find("Main Camera").transform.position, 2f,
                    gameObject.transform.Find("Main Camera").transform.forward, out hit, 100))
            {
                GameObject go = hit.transform.gameObject;
                if (go.CompareTag("Enemy"))
                {
                    if (hitObject.GetInstanceID() != go.GetInstanceID())
                    {
                        Debug.Log(hitObject);
                        try
                        {
                            go.transform.Find("Parent").gameObject.transform.Find("Health_bar").gameObject
                                .SetActive(true);
                        }
                        catch (Exception e)
                        {
                            Debug.Log("Funkar ej bra");
                        }
                    }
                    else if (hitObject.GetInstanceID() == go.GetInstanceID())
                    {
                        go.transform.Find("Parent").gameObject.transform.Find("Health_bar")
                            .SendMessage("Display", SendMessageOptions.DontRequireReceiver);
                    }
                    else
                    {
                        go.transform.Find("Parent").gameObject.transform.Find("Health_bar")
                            .SendMessage("Deactivate", SendMessageOptions.DontRequireReceiver);
                    }
                }
                else
                {
                    try
                    {
                        hitObject.transform.Find("Parent").gameObject.transform.Find("Health_bar")
                            .SendMessage("Deactivate", SendMessageOptions.DontRequireReceiver);
                    }
                    catch (Exception e)
                    {
                    }
                }

                hitting = true;
                hitObject = go;
            }
        }
    }
}