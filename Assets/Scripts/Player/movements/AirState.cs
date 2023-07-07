using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AirState : AbstractState
{
    float mov;
    float baseVelx;
    public AirState(PlayerMotor player, PlayerController controller) : base(player, controller)
    {
        this.name = "AirState";
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
        if (player.isGrounded) controller.ChangeState("MoveState");
        if (Input.GetKeyDown(KeyCode.E) && player.canHook) controller.ChangeState("HookState");
        if (mov * player.direction > 0 && player.isFacingWall) controller.ChangeState("WallGripState");
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.canDash) controller.ChangeState("DashState");
    }

}