using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlock : MonoBehaviour
{
    public GameObject ActiveTilemap;
    public GameObject DisabledTilemap;

    public float StartOffset;
    public float SwitchRate;


    private void Start()
    {
        InvokeRepeating("Switch", StartOffset, SwitchRate);
    }


    public void Switch()
    {
        switch(ActiveTilemap.activeSelf)
        {
            case true:
                ActiveTilemap.SetActive(false);
                DisabledTilemap.SetActive(true);
                return;
            case false:
                ActiveTilemap.SetActive(true);
                DisabledTilemap.SetActive(false);
                return;
        }
    }
}
