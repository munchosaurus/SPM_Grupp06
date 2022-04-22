using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerInteractionScript : NetworkBehaviour
{
    //The text that shows when hovering over an interactable object
    [SerializeField] private UnityEngine.UI.Text interactionText;
    [SerializeField] private Camera mainCamera;
    public void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("CameraMain").GetComponent<Camera>();
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        //Sends a raycast to check for colliders in the InteractableObject layer
        RaycastHit hit;
        if(Physics.SphereCast(mainCamera.transform.position,0.5f, mainCamera.transform.forward,out hit,1,LayerMask.GetMask("InteractableObject")))
        {
            //Changes text to the button and information that is set in the object hit
            interactionText.text = "Press: " + hit.transform.GetComponent<InteractableObjectScript>().ButtonToPress.ToString() + " to " + hit.transform.GetComponent<InteractableObjectScript>().InteractionDescription;
            //Calls the function to say that the object is interacted with
            if(Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), hit.transform.GetComponent<InteractableObjectScript>().ButtonToPress.ToString() )))
            {
                hit.transform.GetComponent<InteractableObjectScript>().buttonPressed();
            }
        }else
        {
            //Set text to nothing
            interactionText.text = "";   
        }
    }
}
