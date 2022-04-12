using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript3D : MonoBehaviour
{
    [SerializeField] private State[] states;
    private MyRigidbody3D myRigidbody;
    private StateMachine stateMachine;
    public float jumpForce = 10f;
    public float acceleration  = 10f;
    public Camera mainCamera;
    public bool firstPerson;
    
    void Awake()
    {
        myRigidbody = GetComponent<MyRigidbody3D>();
        if(states.Length > 0)    
            stateMachine = new StateMachine(this,states);
    }

    void Update()
    {  
        //If there are any added states in the unity inspector
        if(states.Length > 0)      
            stateMachine.Update();
    }
    //Returns myRigidbody
    public MyRigidbody3D MyRigidbody3D => myRigidbody;
}
