using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public float score;
    public bool playedisDead;
    
    //test
    public GameObject gameoverPanel;
 

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
        playedisDead = true;
        gameoverPanel.SetActive(true); //TODO:Test
    }

    
}
