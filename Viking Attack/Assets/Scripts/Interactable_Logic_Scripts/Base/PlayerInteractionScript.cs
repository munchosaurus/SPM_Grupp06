using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text interactionText;
    void Update()
    {
        RaycastHit hit;
        if(Physics.SphereCast(gameObject.transform.Find("Main Camera").transform.position,0.5f,gameObject.transform.Find("Main Camera").transform.forward,out hit,1,LayerMask.GetMask("InteractableObject")))
        {
            interactionText.text = "Press: " + hit.transform.GetComponent<InteractableObjectScript>().ButtonToPress.ToString() + " to " + hit.transform.GetComponent<InteractableObjectScript>().InteractionDescription;
            if(Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), hit.transform.GetComponent<InteractableObjectScript>().ButtonToPress.ToString() )))
            {
                hit.transform.GetComponent<InteractableObjectScript>().buttonPressed();
            }
        }else
            interactionText.text = "";   
    }
}
