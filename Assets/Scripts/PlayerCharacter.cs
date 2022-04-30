using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{

    /* ------------------------------------------ */

    private void Awake()
    {
        ComponentManager.instance.PlayerCharacters.Add(this);
    }

    private void OnDestroy()
    {
        try
        {
            if (ComponentManager.instance.PlayerCharacters.Contains(this))
                ComponentManager.instance.PlayerCharacters.Remove(this);
        }
        catch { }
    }

    /* ------------------------------------------ */

}
