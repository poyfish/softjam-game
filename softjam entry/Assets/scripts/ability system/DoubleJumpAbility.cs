using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoubleJumpAbility : BaseAbility
{

    [SerializeField] float DoubleJumpForce;

    private bool CanDoubleJump;

    public override void Init(PlayerMovement Player, AbilityManager Manager)
    {
        base.Init(Player,Manager);
    }

    public override void Use(InputAction.CallbackContext context)
    {
        if (!player.IsGrounded() && CanDoubleJump)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x,DoubleJumpForce);
            CanDoubleJump = false;
        }
    }

    public override void StopUse(InputAction.CallbackContext context)
    {
        
    }


    public override void update()
    {
        if (player.IsGrounded())
        {
            CanDoubleJump = true;
        }
    }
}
