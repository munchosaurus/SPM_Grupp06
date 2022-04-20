using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeActivation : BaseObjectActivation
{
    
    [SerializeField] private float rotationSpeed;
    //Sets to the starting rotation
    private Quaternion targetRotation;
    //A boolean if the bridge is down or upp
    private bool bridgeDown;
    public override void activate()
    {
        //Moves the bridge shaft by 90 degrees
        if(!bridgeDown)
        {
            targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,0);
            bridgeDown = true;
        }else
        {
            targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,-90);
            bridgeDown = false;
        }      
    }

    void Start()
    {
        targetRotation = transform.rotation;
        
    }

    void Update()
    {
        //Moves the bridge in a motion (Not teleporting)
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
