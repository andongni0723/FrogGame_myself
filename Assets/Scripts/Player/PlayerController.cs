using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 mousePos;
    Vector2 targetPos;
    Rigidbody2D rb;

    float jumpDistance = 2f;
    float finalJumpDistance;
    float moveSpeed = 0.134f;
    bool bigJumpPerformed = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = transform.position;
    }

    private void FixedUpdate()
    {
        rb.position = Vector2.Lerp(transform.position, targetPos, moveSpeed);
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
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

        if (context.phase == InputActionPhase.Performed)
        {
            finalJumpDistance = jumpDistance * 2;
            bigJumpPerformed = true;
        }

        // When mouse up
        if (context.phase == InputActionPhase.Canceled && bigJumpPerformed)
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
        
        switch(direction)
        {
            case -1: //left
                x = -finalJumpDistance;
                y = 0;
                break;
            case 0: // forward
                x = 0;
                y = finalJumpDistance;
                break;
            case 1: // right
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
        Vector2 PosAminB = new Vector2( b.x - 0, b.y - a.y);

        float direction;

        if (PosAminB.y >= 0) // go forward
            direction = 0;
        else if (PosAminB.x >= 0) // go right
            direction = 1;
        else
            direction = -1;

        return direction;
    }
}
