using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteraction : BaseObjectInteraction
{
    [SerializeField] private float roatitionSpeed;
    //The object that the lever is activating
    [SerializeField] private GameObject activationObject;
    //A boolean if the lever is on or off
    private bool leverOn;
    //Sets to the starting rotation
    private Quaternion tragetRotation = Quaternion.Euler(0,0,45);
    //Is called from InteractableObjectScript when the player press the chosen button
    public override void interactedWith()
    {
        //Calls the object to activate (uses the BaseObjectActivation so i can call different objects)
        activationObject.GetComponent<BaseObjectActivation>().activate();
        //Moves the lever shaft by 90 degrees
        if(!leverOn)
        {
            tragetRotation = Quaternion.Euler(0,0,-45);
            leverOn = true;
        }else
        {
            tragetRotation = Quaternion.Euler(0,0, 45);
            leverOn = false;
        }   
    }
    void Update()
    {
        //Moves the lever in a motion (Not teleporting)
        transform.Find("LeverShaftPivot").transform.rotation = Quaternion.RotateTowards(transform.Find("LeverShaftPivot").transform.rotation, tragetRotation, roatitionSpeed * Time.deltaTime);
    }
}
