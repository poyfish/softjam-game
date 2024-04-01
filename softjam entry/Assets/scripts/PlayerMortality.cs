using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMortality : MonoBehaviour
{
    public Transform Checkpoint;

    private PlayerMovement player;

    public UnityEvent OnDeath;

    private void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    public IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            OnDeath.Invoke();
            yield return new WaitForEndOfFrame();
            ReturnToCheckpoint();
        }
    }

    public void ReturnToCheckpoint()
    {
        transform.position = Checkpoint.position;

        player.rb.velocity = Vector2.zero;
    }
}
