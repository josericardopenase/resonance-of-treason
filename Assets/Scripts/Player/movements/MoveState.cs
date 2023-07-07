using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : AbstractState
{
    float mov;


    public MoveState(PlayerMotor player, PlayerController controller) : base(player, controller)
    {
        this.name = "MoveState";
    }

    override public void Update()
    {
        mov = Input.GetAxis("Horizontal") * player.velocity / 10;
    }

    override public void FixedUpdate()
    {
        player.rb.velocity = new Vector2(mov, player.rb.velocity.y);
    }


    public override void Transition()
    {
        if (Input.GetKey(KeyCode.Space) && player.isGrounded) controller.ChangeState("JumpState");
        if (Input.GetKeyDown(KeyCode.E) && player.canHook) controller.ChangeState("HookState");
        if (player.rb.velocity.y < 0) controller.ChangeState("AirState");
        // Inside Update() of other states:
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.isGrounded) controller.ChangeState("RollingState");

    }

}