using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/RunState")]
public class PlayerRunState : PlayerState
{
    private Vector3 input;
    public override void Enter()
    {
        
    }
    public override void Update()
    {
        input = Vector3.right * Input.GetAxisRaw("Horizontal") + Vector3.forward * Input.GetAxisRaw("Vertical");
        input = player.mainCamera.transform.rotation * input;
        input = Vector3.ProjectOnPlane(input,player.getMyRigidbody3D().Grounded().normal);
        input = input.normalized * player.acceleration;

        if(!player.getMyRigidbody3D().GroundedBool())
            input = new Vector3(input.x,0f,input.z);
        player.getMyRigidbody3D().velocity += input * player.acceleration;

        if(Input.GetKeyDown(KeyCode.Space))
            stateMachine.ChangeState<PlayerJumpState>();
        else if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            stateMachine.ChangeState<PlayerBaseState>();
    }
    public override void Exit()
    {
        
    }
}