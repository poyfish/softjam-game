using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static AbilityManager;

public class BaseAbility : MonoBehaviour
{
    [HideInInspector]
    public PlayerMovement player;
    [HideInInspector]
    public AbilityManager manager;
    [HideInInspector]
    public AbilityManager.AbilityObject abilityObject;

    public virtual void Init(PlayerMovement Player, AbilityManager Manager)
    {
        player = Player;
        manager = Manager;
        abilityObject = manager.AbilityToObject(this);
    }

    public void UseInternal(InputAction.CallbackContext context)
    {
        if (!manager.CanUseAbilities)
        {
            return;
        }

        if (manager.CurrentAbility == this)
        {
            return;
        }


        if (manager.CurrentAbility != null)
        {

            switch (abilityObject.mode)
            {
                case AbilityManager.interferenceMode.Ignore:
                    return;


                case AbilityManager.interferenceMode.Override:
                    manager.OnAbilityFinnish(manager.CurrentAbility);
                    break;
            }
        }
        

        Use(context);
    }

    public virtual void Use(InputAction.CallbackContext context)
    {

    }

    public virtual void StopUse(InputAction.CallbackContext context)
    {
        
    }

    public virtual void update() 
    { 

    }
}
