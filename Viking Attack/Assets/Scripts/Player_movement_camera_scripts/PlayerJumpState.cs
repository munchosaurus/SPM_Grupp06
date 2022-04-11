using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/JumpState")]
public class PlayerJumpState : PlayerState
{
    public override void Enter()
    {
        
    }
    public override void Update()
    {
        if(player.getMyRigidbody3D().GroundedBool())
            player.getMyRigidbody3D().velocity += (Vector3.up * player.jumpForce);

        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            stateMachine.ChangeState<PlayerRunState>();
        else if(!Input.GetKeyDown(KeyCode.Space))
            stateMachine.ChangeState<PlayerBaseState>();
    }
    public override void Exit()
    {
        
    }
}
