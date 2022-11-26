using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Move Obstacle", menuName = "Game Data/Move Obstacle Data", order = 0)]
public class MoveObstacle_SO : ScriptableObject 
{
    public enum TagType
    {
        Enemy, Wood, Water, Untagged
    }

    public TagType tagType;
    public Sprite itemIcon;
    public float startSpeed;

    public RuntimeAnimatorController animatorController;

    public Vector2 colliderOffset;
    public Vector2 colliderSize;
}
