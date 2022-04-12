using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeActivation : BaseObjectActivation
{
    [SerializeField] private float roatitionSpeed;
    //Sets to the starting rotation
    private Quaternion tragetRotation = Quaternion.Euler(0,0,-90);
    //A boolean if the bridge is down or upp
    private bool bridgeDown = false;
    public override void activate()
    {
        //Moves the bridge shaft by 90 degrees
        if(!bridgeDown)
        {
            tragetRotation = Quaternion.Euler(0,0,0);
            bridgeDown = true;
        }else
        {
            tragetRotation = Quaternion.Euler(0,0,-90);
            bridgeDown = false;
        }      
    }
    void Update()
    {
        //Moves the bridge in a motion (Not teleporting)
        transform.rotation = Quaternion.RotateTowards(transform.rotation, tragetRotation, roatitionSpeed * Time.deltaTime);
    }
}
