using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class TakeDamage : Identity
{
    /* ------------------------------------------ */

    private Stats _stats;

    private CharacterStateManager _characterSM;

    private Character _character => _stats.identity;

    /* ------------------------------------------ */

    private void Awake()
    {
        _stats = GetComponent<Stats>();

        _characterSM = GetComponent<CharacterStateManager>();

        ComponentManager.instance.TakeDamage.Add(identity, this);

    }

    private void OnDestroy()
    {
        try
        {
            if (ComponentManager.instance.TakeDamage.ContainsKey(identity))
                ComponentManager.instance.TakeDamage.Remove(identity);
        }
        catch { }
    }

    /* ------------------------------------------ */

    public void SetDamage(int damage)
    {
        Debug.Log("SetDamage");
        _stats.SetDamage(damage > 0 ? damage : 1);

        if (_stats.Data.Health <= 0)
        {
            if (ComponentManager.instance.PlayerCharacters.Contains(_character))
                ComponentManager.instance.PlayerCharacters.Remove(_character);
            else if (ComponentManager.instance.EnemyCharacters.Contains(_character))
                ComponentManager.instance.EnemyCharacters.Remove(_character);

            _characterSM.SwitchState(State.Character.Type.Death);
        }
    }

    /* ------------------------------------------ */

}

