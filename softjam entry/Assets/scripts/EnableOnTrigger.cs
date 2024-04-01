using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnTrigger : MonoBehaviour
{

    public GameObject Object;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Active());
        }
    }

    IEnumerator Active()
    {
        Object.SetActive(true);

        yield return new WaitForSeconds(1f);

        Object.SetActive(false);
    }
}
