using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rigidBody;
    private BoxCollider2D coll;
    private Animator animator;
    private float dirX = 0f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>(); // Assign the rigidBody of the player
        animator = GetComponent<Animator>(); // Assign the current animator of the player
        sprite = GetComponent<SpriteRenderer>(); // Assign the current sprite of the player
        coll = GetComponent<BoxCollider2D>(); // Assign the current box collider of the player
    }

    // Update is called once per frame
    private void Update()
    {
        
        Move();

        Jump();

        UpdateAnimationState();
    }

    private void Move()
    {
        // GetAxisRaw go back to 0 when the horizontal key is released
        dirX = Input.GetAxisRaw("Horizontal"); // Get the horizontal direcction of the touched key
        rigidBody.velocity = new Vector2(dirX * moveSpeed, rigidBody.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        // We check if the player is running and we change the Animator parameter
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false; // Turns the player to the right
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true; // Turns the player to the left
        }
        else
        {
            state = MovementState.idle;
        }

        if (rigidBody.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rigidBody.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
