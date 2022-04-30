using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{

    /* ------------------------------------------ */

    private void Awake()
    {
        ComponentManager.instance.EnemyCharacters.Add(this);
    }

    private void OnDestroy()
    {
        try
        {
            if (ComponentManager.instance.EnemyCharacters.Contains(this))
                ComponentManager.instance.EnemyCharacters.Remove(this);
        }
        catch { }
    }

    /* ------------------------------------------ */

}