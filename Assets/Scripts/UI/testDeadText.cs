using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class testDeadText : MonoBehaviour
{
    public GameObject text;

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
        text.SetActive(true);
    }
}
