using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    const float Pos_Max_Difference = 0.01f;

    Vector2 mousePos;

    [SerializeField]
    Vector2 targetPos;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spr;

    float jumpDistance = 2f;
    float finalJumpDistance;
    float moveSpeed = 0.134f;
    bool bigJumpPerformed = false;
    bool isJump;

    [SerializeField]
    bool onWood = false;

    [SerializeField]
    Vector3 lastPos = new Vector2();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();

        targetPos = transform.position;
        anim.SetBool("isForward", true);
    }

    private void FixedUpdate()
    {
        rb.position = Vector2.Lerp(transform.position, targetPos, moveSpeed);

        // Player is moving or not
        if (!(Mathf.Abs(transform.position.x - lastPos.x) <= Pos_Max_Difference &&
             Mathf.Abs(transform.position.y - lastPos.y) <= Pos_Max_Difference)) // Player is moving
        {
            anim.SetBool("isJump", true);
            isJump = true;
        }
        else
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }

        // Update last position
        lastPos = transform.position;
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && !isJump)
        {
            finalJumpDistance = jumpDistance;


            // Test
            //Debug.Log("jump");

            // Move
            Movement();
        }
    }

    public void BigJump(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Performed && !isJump)
        {
            finalJumpDistance = jumpDistance * 2;
            bigJumpPerformed = true;
        }

        // When mouse up
        if (context.phase == InputActionPhase.Canceled && bigJumpPerformed && !isJump)
        {
            // Test
            Debug.Log("BIG jump!");

            // Move
            Movement();
            bigJumpPerformed = false;
        }
    }

    /// <summary>
    /// Player movement
    /// </summary>
    private void Movement()
    {
        float direction = GetDirection(transform.position, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));

        float x = 0;
        float y = 0;

        //test
        //Debug.Log(direction);

        switch (direction)
        {
            case -1: //left
                spr.flipX = false;
                anim.SetBool("isForward", false);
                x = -finalJumpDistance;
                y = 0;
                break;
            case 0: // forward
                spr.flipX = false;
                anim.SetBool("isForward", true);
                x = 0;
                y = finalJumpDistance;
                break;
            case 1: // right
                spr.flipX = true;
                anim.SetBool("isForward", false);
                x = finalJumpDistance;
                y = 0;
                break;
        }

        targetPos = new Vector2(transform.position.x + x, transform.position.y + y);
    }


    /// <summary>
    /// Get a direction for b
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns>0: forword, -1: left, 1: right</returns>
    private float GetDirection(Vector2 a, Vector2 b)
    {
        Vector2 PosAminB = new Vector2(b.x - 0, b.y - a.y);

        float direction;

        if (PosAminB.y >= 0) // go forward
            direction = 0;
        else if (PosAminB.x >= 0) // go right
            direction = 1;
        else
            direction = -1;

        return direction;
    }


    // Player Died
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "enemy":
                EventManager.CallPlayerDied();
                break;
            case "wood":
                onWood = true;
                break;

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "water" && onWood && !isJump)
        {
            EventManager.CallPlayerDied();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "wood":
                onWood = false;
                break;
        }
    }
}