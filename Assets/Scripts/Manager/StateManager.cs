using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    /* ------------------------------------------ */

    public static StateManager instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<StateManager>();

            return _instance;
        }
    }
    static StateManager _instance;

    /* ------------------------------------------ */

    public List<CharacterStateManager> CharacterStateManagers;

    public GridBehaviour Player, Enemy;

    /* ------------------------------------------ */

    public void ChangeStateAttack() 
    {
        Player.gameObject.SetActive(false);
        Enemy.gameObject.SetActive(false);
        for (int i = 0; i < CharacterStateManagers.Count; i++) 
        {

            CharacterStateManagers[i].SwitchState(State.Character.Type.Attack, new State.Character.Argument());
        }
    }
}
