using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenetransition : MonoBehaviour
{

    public float Delay;
    public Animator OutTransition;
    public float TransitionTime;
    public string TransitionSceneName;

    public void TransitionScene()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        yield return new WaitForSeconds(Delay);

        OutTransition.Play("FadeOut");

        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadScene(TransitionSceneName);
    }
}
