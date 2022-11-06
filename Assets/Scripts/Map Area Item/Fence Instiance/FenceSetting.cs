using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceSetting : MonoBehaviour
{
    public List<GameObject> InstItemList = new List<GameObject>();
    public GameObject obstacleParent;

    public int instCount = 3;

    private void Awake() {
        if(obstacleParent.transform.childCount == 0)
        {
            InstFence(instCount);
        }
    }

    /// <summary>
    /// Instantiate fence gameobject
    /// </summary>
    /// <param name="_instCount">number of instantiate gameobject count</param>
    void InstFence(int _instCount)
    {
        for (int i = 0; i < _instCount; i++)
        {
            var newObject = Instantiate(InstItemList[Random.Range(0, InstItemList.Count)], transform.position, Quaternion.identity, obstacleParent.transform) as GameObject;
            newObject.transform.localPosition = RandomFenceLocalPos(i + 1);
        }
    }

    /// <summary>
    /// Random local position of fence, and position y will change with instantiate gameobject count
    /// </summary>
    /// <returns></returns>
    Vector2 RandomFenceLocalPos(int _instCount)
    {
        Vector2 pos = new Vector2(Random.Range(-4, 1), -7 + (4 * _instCount));
        //Debug.Log(pos);
        return pos;
    }
}
