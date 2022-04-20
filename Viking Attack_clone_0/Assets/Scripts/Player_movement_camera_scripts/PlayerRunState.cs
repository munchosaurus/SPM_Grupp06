using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/RunState")]
//Used as a state when the player inputs for Horizontal and Vertical movement
public class PlayerRunState : PlayerState
{
    private Camera mainCamera;
    private Vector3 input;
    private Transform cameraTransform; //VW
    private GameObject cameraParent;

    public override void Enter()
    {
    }
    public void Start()
    {
        mainCamera = Camera.main;
        //cameraTransform = Camera.main.gameObject.transform;
        cameraParent = GameObject.FindWithTag("MainCamera");
    }

    public override void Update()
    {
        //get player input
        input = Vector3.right * Input.GetAxisRaw("Horizontal") + Vector3.forward * Input.GetAxisRaw("Vertical");
        input = cameraParent.transform.rotation * input; //Justerar med initial-kommentarer ovan
        input = Vector3.ProjectOnPlane(input,Player.MyRigidbody3D.Grounded().normal);
        input = input.normalized * Player.acceleration;

        //If player is grounded set input vector to follow the ground 
        if(!Player.MyRigidbody3D.GroundedBool())
            input = new Vector3(input.x,0f,input.z);
        Player.MyRigidbody3D.velocity += input * Player.acceleration;

        if(Input.GetKeyDown(KeyCode.Space))
            stateMachine.ChangeState<PlayerJumpState>();
        else if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            stateMachine.ChangeState<PlayerBaseState>();
    }
    public override void Exit()
    {
        
    }
}