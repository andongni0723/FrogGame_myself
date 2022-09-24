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
    public float moveSpeed = 0.134f;
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
            Debug.Log("jump" + GetAngle(transform.position, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue())).ToString());

            // Move
            Movment();
            //targetPos = new Vector2(transform.position.x, transform.position.y + finalJumpDistance);
        }
    }

    public void BigJump(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Performed)
        {
            finalJumpDistance = jumpDistance * 2;
            //bigJumpPerformed = true;
        }

        // When mouse up
        if (context.phase == InputActionPhase.Canceled && bigJumpPerformed)
        {
            // Test
            Debug.Log("big jump DDIS!");
            Debug.Log(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));


            // Move
            targetPos = new Vector2(transform.position.x, transform.position.y + finalJumpDistance);
            bigJumpPerformed = false;
        }
    }

    private void Movment()
    {
        float angle = GetAngle(transform.position, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));

        float upLeftLine = -45;
        float upRightLine = 45;
        float leftRightLint = 179;

        if (upLeftLine <= angle && angle <= upRightLine) // go forward
        {
            Debug.Log("UP UP UP");
        }
        else if (-leftRightLint <= angle && angle <= upLeftLine) // go left
        {
            Debug.Log("LEFT LEFT");
        }
        else // go right
        {
            Debug.Log("RIGHT RIGHT");
        }
    }

    private float GetAngle(Vector2 a, Vector2 b)
    {
        // position can't equal
        // if (a.x == b.x && a.y >= b.y) return 0;

        // b -= a;
        // Vector2 c = new Vector2(a.x, a.y + 1);

        // float d1 = (c.x * b.x) + (c.y * b.y);
        // float d2 = c.magnitude * b.magnitude;
        // float angle = Mathf.Acos(d1 / d2) * (180 / Mathf.PI);

        // if (b.x < 0) angle *= -1;

        // return angle;

        Vector3 targetDir = b - a; // 目标坐标与当前坐标差的向量

        float angle = Vector3.Angle(transform.forward, targetDir); // 返回当前坐标与目标坐标的角度

        return angle;
    }
}
