using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerScript3D : NetworkBehaviour
{
    [SerializeField] private State[] states;
    private MyRigidbody3D myRigidbody;
    private StateMachine stateMachine;
    public float jumpForce = 10f;
    public float acceleration = 10f;
    public bool firstPerson;
    
    public override void OnStartLocalPlayer()
    {
        myRigidbody = GetComponent<MyRigidbody3D>();
        if (states.Length > 0)
            stateMachine = new StateMachine(this, states);    
    }


    void Update()
    { //Cancels all updates that aren't the local player
        if (!LocalCheck()) return;
        //If there are any added states in the unity inspector
        if (states.Length > 0)
            stateMachine.Update();
    }

    public bool LocalCheck()
    {
        return isLocalPlayer;
    }
    //Returns myRigidbody
    public MyRigidbody3D MyRigidbody3D => myRigidbody;
}