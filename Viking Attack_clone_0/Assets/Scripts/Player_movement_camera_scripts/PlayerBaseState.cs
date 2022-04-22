using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PlayerState/BaseState")]
//Used as a state when the player does nothing
public class PlayerBaseState : PlayerState
{
    public override void Enter()
    {
        
    }
    public override void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState<PlayerJumpState>();
        }
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            stateMachine.ChangeState<PlayerRunState>();
        }
    }
    public override void Exit()
    {
        
    }
}
