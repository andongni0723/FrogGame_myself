using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static event Action playerDied;
    public static void CallPlayerDied()
    {
        playerDied?.Invoke();
    }
}
