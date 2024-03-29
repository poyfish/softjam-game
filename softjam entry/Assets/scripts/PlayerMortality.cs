using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMortality : MonoBehaviour
{
    public Transform Checkpoint;

    private PlayerMovement player;

    private void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            transform.position = Checkpoint.position;

            player.rb.velocity = Vector2.zero;
        }
    }
}
