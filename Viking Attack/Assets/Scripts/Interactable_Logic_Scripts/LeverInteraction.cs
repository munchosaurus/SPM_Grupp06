using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteraction : BaseObjectInteraction
{
    [SerializeField] private float roatitionSpeed;
    [SerializeField] private GameObject activationObject;
    private bool leverOn;
    private Quaternion tragetRotation = Quaternion.Euler(0,0,45);
    public override void interactedWith()
    {
        activationObject.GetComponent<BaseObjectActivation>().activate();
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
        transform.Find("LeverShaftPivot").transform.rotation = Quaternion.RotateTowards(transform.Find("LeverShaftPivot").transform.rotation, tragetRotation, roatitionSpeed * Time.deltaTime);
    }
}
