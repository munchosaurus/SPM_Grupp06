using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeActivation : BaseObjectActivation
{
    [SerializeField] private float roatitionSpeed;
    private Quaternion tragetRotation = Quaternion.Euler(0,0,-90);
    private bool bridgeDown = false;
    public override void activate()
    {
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
        transform.rotation = Quaternion.RotateTowards(transform.rotation, tragetRotation, roatitionSpeed * Time.deltaTime);
    }
}
