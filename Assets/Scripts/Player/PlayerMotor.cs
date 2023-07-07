using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerMotor : MonoBehaviour
{

    public Rigidbody2D rb;
    public LineRenderer lineRenderer;  // Add a LineRenderer component to the player GameObject and assign it here
    public float velocity = 100;
    public float rollingVelocity = 100;
    public float rollingTime = 0.3f;
    public float jumpForce = 20;
    public float dashForce = 10;
    public bool canDash = true;
    public float wallJumpForce = 20f;  // Set this to adjust the force of the wall jump
    public float hookSpeed = 18f;  // Adjust this as needed
    public float slowFallSpeed = 2f;  // Adjust this as needed
    public float maxJumpTime = 1f;
    public float dashTime = 0.3f;
    public float dashOffsetY = 1f;
    public LayerMask groundLayer;  // Set this in the Unity Editor to match your ground objects

    public float direction;  // Add this property to track the player's direction
    public bool isGrounded;
    public GameObject hookPoint;
    public bool canHook = false;
    public float groundCheckDistance = 0.2f;  // You can adjust this as needed
    public string hookPointTag = "HookPoint";  // Set this to the tag you're using for hook points
    public float hookDetectionRadius = 5f;  // Adjust this as needed
    public float wallDetectionDistance = 1f;  // Set this to the distance at which you want to detect walls
    public bool isFacingWall;
    public GameObject hook;  // This will store the closest hook point




    // Other methods...
    //Hay que pensarlo mejor

    public void flip() {
        // Flip the player based on their velocity
        if(rb.velocity.x != 0) { 
			if (rb.velocity.x > 0.1f)
			{
			    direction = 1;
			    transform.localScale = new Vector3(Math.Abs(transform.localScale.x), Math.Abs(transform.localScale.y), Math.Abs(transform.localScale.z)); // Moving right
			}
			else if (rb.velocity.x < -0.1f)
			{
			    direction = -1;
			    transform.localScale = new Vector3(-Math.Abs(transform.localScale.x), Math.Abs(transform.localScale.y), Math.Abs(transform.localScale.z)); // Moving right
			}
		}
    }

    public void CanHook()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, hookDetectionRadius);

        float closestDistance = float.MaxValue;
        GameObject closestHookPoint = null;

        
	    if(hook != null) hook.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == hookPointTag)
            {
                float directionToHook = collider.transform.position.x - transform.position.x;

                float distance = Vector2.Distance(transform.position, collider.transform.position);

                if (distance < closestDistance && (directionToHook * direction) > 0 )
                {
                    closestDistance = distance;
                    collider.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    closestHookPoint = collider.gameObject;
                }
            }
        }

        hook = closestHookPoint;
        canHook = (hook != null);
    }


    public void CheckWall()
    {
        // Cast the ray
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3.up * dashOffsetY), Vector2.right * direction, wallDetectionDistance, groundLayer);

        // If the ray hit a collider, the player is facing a wall
        isFacingWall = hit.collider != null;
    }

    public void CheckGround()
    {
        // Cast a ray downwards from the player's position
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        // If the ray hit a ground object, return true
        isGrounded = hit.collider != null;
    }

    public void CanDash() {
        if (isGrounded) canDash = true;
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        CanDash();
        flip();
        CheckGround();
        CanHook();
        CheckWall();
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow circle at the player's position
        Gizmos.color = canHook ? Color.cyan : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hookDetectionRadius);

        //Draw the player ground check distance
        Gizmos.color = isGrounded ? Color.red : Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);

	    // Determine the direction to cast the ray
	    int direction = transform.localScale.x > 0 ? 1 : -1;

	    // Draw the ray
        Gizmos.color = isFacingWall ? Color.red : Color.green;
	    Gizmos.DrawRay(transform.position + (Vector3.up * dashOffsetY), new Vector2(direction, 0) * wallDetectionDistance);
    }

}