using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPortal : MonoBehaviour
{
    public UnityEvent SpawnPlayerEvent;

    public void SpawnPlayer()
    {
        SpawnPlayerEvent.Invoke();
    }
}
