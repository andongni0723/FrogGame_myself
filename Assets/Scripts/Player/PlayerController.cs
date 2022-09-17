using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public void Jump(InputAction.CallbackContext context)
    {       
        if(context.phase == InputActionPhase.Performed)
        {
            Debug.Log("jump");
        }
    }

    public void BigJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Debug.Log("big jump!");
        }
    }
}
