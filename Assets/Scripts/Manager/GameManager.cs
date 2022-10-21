using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float score;

    private void OnEnable()
    {
        EventManager.playerDied += OnPlayerDied;
    }

    private void OnDisable()
    {
        EventManager.playerDied -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        Debug.Log("PLAYER DIED!!!!!");
    }
}
