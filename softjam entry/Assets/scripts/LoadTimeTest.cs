using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTimeTest : MonoBehaviour
{
    public string scene;

    [Button("test load time")]
    void TestLoadTime()
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
