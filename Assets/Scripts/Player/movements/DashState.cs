using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : AbstractState
{
    private Vector2 dashDirection;
    private float dashTimer;

    public DashState(PlayerMotor player, PlayerController controller) : base(player, controller)
    {
        this.name = "DashState";
    }

    public override void Enter()
    {
        player.canDash = false;
        dashTimer = player.dashTime;
        dashDirection = player.direction > 0 ? Vector2.right : Vector2.left;  // Determine dash direction based on player's facing direction

        // Apply an initial force for the dash
        player.rb.velocity = dashDirection * player.dashForce;
    }

    public override void Update()
    {
        dashTimer -= Time.deltaTime;
    }

    public override void FixedUpdate()
    {
        // You might want to apply a continued force or modify the player's velocity over the course of the dash here
        // For now, let's just maintain the initial dash velocity
        player.rb.velocity = dashDirection * player.dashForce;
    }

    public override void Transition()
    {
        // Transition back to MoveState or AirState once the dash is complete
        // This could be based on a timer, the player's velocity slowing down, input from the player, etc.
        // For now, let's transition back to MoveState after 0.2 seconds have passed
        if (dashTimer <= 0)
        {
            if (player.isGrounded)
                controller.ChangeState("MoveState");
            else
                controller.ChangeState("AirState");
        }
    }
}