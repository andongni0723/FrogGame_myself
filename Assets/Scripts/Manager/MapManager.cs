using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<GameObject> MapAreaPrefabList = new List<GameObject>();

    GameObject mapParent;
    GameObject firstMapArea;
    GameObject lastMapArea;
    public GameObject cameraPoint;
    public GameObject cameraDestroyPoint;

    GameObject firstEndPoint;
    GameObject lastEndPoint;
    GameObject mapAreaStartPoint;
    private void Awake()
    {
        mapParent = GameObject.FindWithTag("mapParent");
        GetFirstMapAreaEndPoint();
        SetLastMapAreaEndPoint();
    }
    private void Update()
    {
        CheckCameraInSetMapArea();
        CheckMapAreaOutScene();
    }


    /// <summary>
    /// If camera point in setMapAeraPoint, add new mapArea
    /// </summary>
    public void CheckCameraInSetMapArea()
    {
        if (lastEndPoint != null)
        {
            if (cameraPoint.transform.position.y >= lastEndPoint.transform.position.y)
            {
                NewMapArea();
                SetLastMapAreaEndPoint();
            }
        }
    }

    /// <summary>
    /// If camera destroy point in endPoint, destroy mapArea
    /// </summary>
    public void CheckMapAreaOutScene()
    {
        GetFirstMapAreaEndPoint();

        if(firstEndPoint != null)
        {
            if(cameraDestroyPoint.transform.position.y >= firstEndPoint.transform.position.y)
            {
                Destroy(firstMapArea);
                GetFirstMapAreaEndPoint();
            }
        }
    }

    /// <summary>
    /// Find the endPoint for last mapArea in mapParent
    /// </summary>
    private void SetLastMapAreaEndPoint()
    {
        //Get last mapArea
        lastMapArea = mapParent.transform.GetChild(mapParent.transform.childCount - 1).gameObject;

        for (int i = 0; i < lastMapArea.transform.childCount; i++) // get childObj
        {
            if (lastMapArea.transform.GetChild(i).CompareTag("endPoint"))
                lastEndPoint = lastMapArea.transform.GetChild(i).gameObject;
                break;
        }
    }


    /// <summary>
    /// Find the endPoint for first mapArea in mapParent
    /// </summary>
    private void GetFirstMapAreaEndPoint()
    {
        firstMapArea = mapParent.transform.GetChild(0).gameObject;

        for (int i = 0; i < firstMapArea.transform.childCount; i++) // get childObj
        {
            if (firstMapArea.transform.GetChild(i).CompareTag("endPoint"))
                firstEndPoint = firstMapArea.transform.GetChild(i).gameObject;
                break;
        }
    }

    /// <summary>
    /// Find map area startPoint
    /// </summary>
    /// <param name="mapArea">mapArea</param>
    /// <returns>startPoint</returns>
    private GameObject FindMapAreaStartPoint(GameObject mapArea)
    {
        for (int i = 0; i < mapArea.transform.childCount; i++)// get childObj
        {
            if (mapArea.transform.GetChild(i).CompareTag("startPoint"))
                return mapArea.transform.GetChild(i).gameObject;
        }

        return null;
    }

    /// <summary>
    /// Instantiate new mapArea
    /// </summary>
    public void NewMapArea()
    {
        Instantiate(MapAreaPrefabList[Random.Range(0, MapAreaPrefabList.Count - 1)], lastEndPoint.transform.position, Quaternion.identity, mapParent.transform);
    }
}
