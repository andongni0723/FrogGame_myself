using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveObstacleController : MonoBehaviour
{
    public MoveObstacle_SO DataSO;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator anim;
    BoxCollider2D coll;

    public float gameSpeed;
    bool isInit = false;

    private void OnEnable()
    {
        EventManager.obstacleDataLoad += InitData;
    }
    private void OnDisable()
    {
        EventManager.obstacleDataLoad -= InitData;
    }

    void InitData()
    {
        if (!isInit)
        {
            gameSpeed = DataSO.startSpeed;
            spriteRenderer.sprite = DataSO.itemIcon;
            anim.runtimeAnimatorController = DataSO.animatorController;
            coll.offset = DataSO.colliderOffset;
            coll.size = DataSO.colliderSize;

            switch(DataSO.tagType)
            {
                case MoveObstacle_SO.TagType.Enemy:
                    gameObject.tag = "enemy";
                    break;
                case MoveObstacle_SO.TagType.Wood:
                    gameObject.tag = "wood";
                    break;
                case MoveObstacle_SO.TagType.Water:
                    gameObject.tag = "water";
                    break;
                case MoveObstacle_SO.TagType.Untagged:
                    gameObject.tag = "Untagged";
                    break;
            }

            isInit = true;
        }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        ObstacleMoving();

        if(Mathf.Abs(transform.position.x) >= 15) Destroy(gameObject);
    }


    /// <summary>
    /// Obstacle movement
    /// </summary>
    private void ObstacleMoving()
    {
        transform.Translate(Vector3.right * gameSpeed * Time.deltaTime);
    }

    //TODO: Game speed add for game score (time)
}
