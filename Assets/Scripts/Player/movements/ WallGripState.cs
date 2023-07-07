using UnityEngine;
using System.Collections;

public class WallGripState : AbstractState
{
    private float mov;

    public WallGripState(PlayerMotor player, PlayerController controller) : base(player, controller)
    {
        this.name = "WallGripState";
    }

    public override void Enter()
    {
        // Slow the fall speed
    }

    public override void Update()
    {
        player.rb.velocity = new Vector2(0, -player.slowFallSpeed);
        mov = Input.GetAxis("Horizontal");
        //if (Input.GetKeyDown(KeyCode.Space)) player.rb.velocity = new Vector2(player.rb.velocity.x, player.wallJumpForce);

    }

    public override void Transition()
    {
        if (mov * player.direction < 0 || !player.isFacingWall) controller.ChangeState("AirState");
        if (player.isGrounded) controller.ChangeState("MoveState");
    }
}