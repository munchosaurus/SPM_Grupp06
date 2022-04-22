using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/JumpState")]
//Used as a state when the player presses "spacebar"
public class PlayerJumpState : PlayerState
{
    public override void Enter()
    {
        
    }
    public override void Update()
    {

        if(Player.MyRigidbody3D.GroundedBool())
            Player.MyRigidbody3D.velocity += (Vector3.up * Player.jumpForce);

        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            stateMachine.ChangeState<PlayerRunState>();
        else if(!Input.GetKeyDown(KeyCode.Space))
            stateMachine.ChangeState<PlayerBaseState>();
    }
    public override void Exit()
    {
        
    }
}
