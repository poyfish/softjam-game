using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityManager : MonoBehaviour
{
    private PlayerMovement player;

    public bool CanUseAbilities;

    public BaseAbility CurrentAbility;

    public enum interferenceMode
    {
        Ignore,
        Override
    }


    [System.Serializable]
    public struct AbilityObject
    {
        public BaseAbility ability;
        public InputActionReference key;

        public interferenceMode mode;
    }

    public AbilityObject[] abilities;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();

        foreach (AbilityObject abilityObject in abilities)
        {
            abilityObject.ability.Init(player,this);

            abilityObject.key.action.started += abilityObject.ability.UseInternal;

            abilityObject.key.action.canceled += abilityObject.ability.StopUse;
        }
    }

    private void Update()
    {
        foreach (AbilityObject abilityObject in abilities) 
        {
            abilityObject.ability.update();
        }
    }

    public void OnAbilityFinnish(BaseAbility ability)
    {
        CurrentAbility = null;
    }

    public void OnAbilityStart(BaseAbility ability)
    {
        CurrentAbility = ability;
    }

    public AbilityObject AbilityToObject(BaseAbility ability)
    {
        return abilities.Where(a => a.ability == this).FirstOrDefault();
    }


    public void SetCanUseAbilities(bool CanUse)
    {
        CanUseAbilities = CanUse;
    }

}
