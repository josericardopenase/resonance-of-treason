using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookState : AbstractState
{
    // Other properties...
    public HookState(PlayerMotor player, PlayerController controller) : base(player, controller)
    {
        this.name = "HookState";
    }

    private bool hasPassedHookPoint = false;
    private const float boostForce = 0.3f;  // Adjust this as needed
    private GameObject hook;
    private Vector2 initialVelocity;

    // Other methods...
    public override void Enter()
    {

        player.lineRenderer.enabled = true;
        hook = player.hook;
        hasPassedHookPoint = false;
    }

    private float timer;
    private const float timeAfterPassingHook = 0.2f;  // Set this to how long you want the timer to last

    // Other methods...

    public override void Update()
    {
        if (!hasPassedHookPoint)
        {
            Vector2 directionToHook = (hook.transform.position - player.transform.position).normalized;
            player.rb.velocity = directionToHook * player.hookSpeed;


            // Check if player has passed the hook point
            if (Vector2.Distance(player.transform.position, hook.transform.position) < 1f)
            {
                hasPassedHookPoint = true;
                // Give the player a boost in their current direction
                // Start the timer
                timer = timeAfterPassingHook;

                // Store the player's initial velocity
                initialVelocity = player.rb.velocity;
            }

            player.lineRenderer.positionCount = 2;
            player.lineRenderer.SetPosition(0, player.transform.position + (Vector3.up * player.dashOffsetY));
            player.lineRenderer.SetPosition(1, hook.transform.position);
        }
        else if (timer > 0)
        {
            // Decrement the timer
            timer -= Time.deltaTime;

            // Gradually decrease the player's velocity
            player.rb.velocity = Vector2.Lerp(initialVelocity, Vector2.zero, 1 - timer / timeAfterPassingHook);


        }
    }

    public override void Exit()
    {
        player.lineRenderer.enabled = false;
    }
    public override void Transition()
    {
        if (hasPassedHookPoint && timer <= 0) controller.ChangeState("AirState");
    }
}