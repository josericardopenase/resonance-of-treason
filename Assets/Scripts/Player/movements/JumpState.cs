using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AbstractState
{
    private float jumpTimer; // how long the jump button has been held
    private float mov;

    public JumpState(PlayerMotor player, PlayerController controller) : base(player, controller)
    {
        this.name = "JumpState";
    }

    public override void Enter()
    {
        player.isGrounded = false;
        player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);
        jumpTimer = player.maxJumpTime; // reset the jump timer
    }

    public override void Update()
    {

        mov = Input.GetAxis("Horizontal") * player.velocity / 10;
        // if the jump button is being held, increment the jump timer
        if (Input.GetKey(KeyCode.Space)) jumpTimer -= Time.deltaTime;
    }

    public override void FixedUpdate()
    {

        player.rb.velocity = new Vector2(mov, player.rb.velocity.y);
        // if the jump button is being held and the jump timer hasn't expired, continue to apply an upwards force
        if (Input.GetKey(KeyCode.Space) && jumpTimer >= 0) player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);
    }

    public override void Transition()
    {
        if (player.rb.velocity.y < 0 || jumpTimer <= 0 || Input.GetKeyUp(KeyCode.Space)) controller.ChangeState("AirState");
        if (Input.GetKeyDown(KeyCode.E) && player.canHook) controller.ChangeState("HookState");
        if (mov * player.direction > 0 && player.isFacingWall && player.rb.velocity.y < 0) controller.ChangeState("WallGripState");
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.canDash) controller.ChangeState("DashState");
    }
}