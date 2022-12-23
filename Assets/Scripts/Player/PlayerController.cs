using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    const float Pos_Max_Difference = 0.1f;

    Vector2 mousePos;

    [SerializeField]
    Vector2 targetPos;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spr;

    RaycastHit2D[] result = new RaycastHit2D[2];

    float jumpDistance = 2f;
    float finalJumpDistance;
    float moveSpeed = 0.134f;
    float maxDifferenceWithStatue; // not jump is 0; is jump is Pos_Max_Difference(const)
    bool bigJumpPerformed = false;
    [SerializeField] bool isMovingX = false;
    [SerializeField] bool isMovingY = false;

    [SerializeField] bool isJump = false;

    [SerializeField] bool onWood = false;

    [SerializeField] Vector3 lastPos = new Vector3();

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

        CheckOnWood();

        //Check Moving
        isMovingX = Mathf.Abs(transform.position.x - targetPos.x) >= Pos_Max_Difference;
        isMovingY = Mathf.Abs(transform.position.y - targetPos.y) >= Pos_Max_Difference;

        // Player is moving or not
        if (isMovingX || isMovingY) // Player is moving
        {
            transform.parent = null;
            
            // if player on the wood, the posX will change with wood's posX. So Disable the jump animation.
            if(!(onWood && isMovingX && !isMovingY))  
                anim.SetBool("isJump", true);

            spr.sortingOrder = 1;
        }
        else if(!isMovingX && !isMovingY && isJump) // Player not moving
        {
            EndJumpEvent();      
            anim.SetBool("isJump", false);
            spr.sortingOrder = 0;
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
            Debug.Log("jump");

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
        
        StartJumpEvent();

        float x = 0;
        float y = 0;

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

    //Check on wood
    void CheckOnWood()
    {
        if(!onWood) return;

        Physics2D.RaycastNonAlloc(transform.position, Vector3.zero, result);
        
        foreach (var item in result)
        {
            if(item.collider == null) continue;

            if(item.collider.tag == "wood") return;
        }

        onWood = false;
    }


    // Player Died
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isJump || GameManager.Instance.playedisDead) return;
                 
        switch (other.tag)
        {
            case "enemy":
                EventManager.CallPlayerDied();
                break;
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(isJump || GameManager.Instance.playedisDead) return;

        if (other.tag == "water" || other.tag == "wood")
        {
            onWood = false;

            Physics2D.RaycastNonAlloc(transform.position, Vector3.zero, result);
            Debug.LogWarning(result);

            foreach (var item in result)
            {
                if(item.collider == null) continue;
                print(item.collider.tag);

                if(item.collider.tag == "wood")
                {
                    onWood = true;
                    transform.parent = item.collider.transform;
                }
            }
            
            if(!onWood)
                EventManager.CallPlayerDied();
        }

        if (other.tag == "enemy" && !isJump)
        {
            EventManager.CallPlayerDied();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(isJump || GameManager.Instance.playedisDead) return;
    }

    #region Animation Event
    public void StartJumpEvent()
    {
        isJump = true;
        print("start");
        transform.parent = null;
    }

    public void EndJumpEvent()
    {
        isJump = false;
        print("end");
    }
    #endregion
}