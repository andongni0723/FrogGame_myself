using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarIPointController : MonoBehaviour
{
    public List<GameObject> carPrefabList = new List<GameObject>();

    public float newCarSpeedMin = 2;
    public float newCarSpeedMax = 4;

    float timer = 0;
    float targetTime;

    private void Start() {
        targetTime = Random.Range(newCarSpeedMin, newCarSpeedMax);
    }

    private void Update() {
        timer += Time.deltaTime;

        if(timer >= targetTime)
        {
            NewCar();

            // Init
            timer = 0;
            targetTime = Random.Range(newCarSpeedMin, newCarSpeedMax);
        }

        if(Mathf.Abs(transform.position.x) >= 15)
            Destroy(gameObject);
    }

    private void NewCar()
    {        
        GameObject newCar = Instantiate(carPrefabList[Random.Range(0, carPrefabList.Count)], transform.position, Quaternion.identity);
    
        if(tag == "carRightPoint")
        {
            newCar.GetComponent<SpriteRenderer>().flipX = true;
            newCar.GetComponent<CarController>().basicSpeed *= -1; // move direction to left
        }
    }
}
