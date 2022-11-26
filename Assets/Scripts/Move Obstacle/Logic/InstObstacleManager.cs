using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InstObstacleManager : MonoBehaviour
{
    enum PointDirection
    {
        LeftPoint,
        RightPoint
    }

    [SerializeField]
    PointDirection pointDirection;
    public List<MoveObstacle_SO> InstObjectDataSO = new List<MoveObstacle_SO>();
    public GameObject InstBasicGameObject;

    public float InstSpeedMin = 2;
    public float InstSpeedMax = 4;

    float timer = 0;
    float targetTime;

    private void Start()
    {
        // Setting instantiate speed (Random)
        targetTime = Random.Range(InstSpeedMin, InstSpeedMax);
    }

    private void Update()
    {
        // Obstacle Instantiate timer
        timer += Time.deltaTime;
        if (timer >= targetTime)
        {
            NewObstacle();

            // INIT Var
            timer = 0;
            targetTime = Random.Range(InstSpeedMin, InstSpeedMax);
        }

        // TODO: don't know this progrem mean
        if (Mathf.Abs(transform.position.x) >= 15)
            Destroy(gameObject);
    }


    /// <summary>
    /// Function to Instantiate gameObject
    /// </summary>
    private void NewObstacle()
    {
        // Instantiate new gameObject and set Data to new Object
        GameObject newObstacle = Instantiate(InstBasicGameObject, transform.position, Quaternion.identity) as GameObject;
        newObstacle.GetComponent<BasicMoveObstacleController>().DataSO = InstObjectDataSO[Random.Range(0, InstObjectDataSO.Count)];
        EventManager.CallobstacleDataLoad();

        // If Instantiate point is Right Side, the gameObject some data must be change
        if (pointDirection == PointDirection.RightPoint)
        {
            newObstacle.GetComponent<SpriteRenderer>().flipX = true;
            newObstacle.GetComponent<BasicMoveObstacleController>().gameSpeed *= -1; // move direction to left
            
        }
    }
}
