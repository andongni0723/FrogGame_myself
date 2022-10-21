using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    Rigidbody2D rb;

    public float basicSpeed = 1;

    float gameSpeed = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        CarMoving();
    }

    /// <summary>
    /// Car moving
    /// </summary>
    private void CarMoving()
    {
        transform.Translate(Vector3.right * basicSpeed * Time.deltaTime);
    }

    //TODO: Game speed add for game score (time)
}
