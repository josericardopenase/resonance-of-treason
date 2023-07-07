using UnityEngine;
using System.Collections;

public class RollingState : AbstractState
{
    private float rollingTimer;
    private Vector2 rollingDirection;

    public RollingState(PlayerMotor player, PlayerController controller) : base(player, controller)
    {
        this.name = "RollingState";
    }

    public override void Enter()
    {
        player.GetComponent<SpriteRenderer>().color = Color.red;
        rollingTimer = player.rollingTime; // Set timer duration to the rolling duration
        rollingDirection = player.direction > 0 ? Vector2.right : Vector2.left; // Determine the rolling direction based on the player's current direction
        player.rb.velocity = rollingDirection * player.rollingVelocity; // Apply velocity to the player in the rolling direction
    }

    public override void Update()
    {
        rollingTimer -= Time.deltaTime; // Reduce the timer
    }

    public override void FixedUpdate()
    {
        player.rb.velocity = rollingDirection * player.rollingVelocity; // Maintain the rolling velocity
    }

    public override void Exit()
    {
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public override void Transition()
    {
        // Transition out of the RollingState when the timer has ended
        if (rollingTimer <= 0)
        {
            if (player.isGrounded)
                controller.ChangeState("MoveState");
            else
                controller.ChangeState("AirState");
        }
    }
}
