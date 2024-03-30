using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineEditorFix : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(.1f);

        GetComponent<PlayableDirector>().Play();
    }
}
