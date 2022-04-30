using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentManager : MonoBehaviour
{

    /* ------------------------------------------ */

    public static ComponentManager instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<ComponentManager>();

            return _instance;
        }
    }
    static ComponentManager _instance;

    /* ------------------------------------------ */

    public List<Character> PlayerCharacters;
    public List<Character> EnemyCharacters;

    public Dictionary<Character, Idle> Idle = new Dictionary<Character, Idle>();
    public Dictionary<Character, Spawn> Spawn = new Dictionary<Character, Spawn>();
    public Dictionary<Character, Death> Death = new Dictionary<Character, Death>();
    public Dictionary<Character, Move> Move = new Dictionary<Character, Move>();
    public Dictionary<Character, Attack> Attack = new Dictionary<Character, Attack>();
    public Dictionary<Character, Stats> Stats = new Dictionary<Character, Stats>();
    public Dictionary<Character, Win> Win = new Dictionary<Character, Win>();
    public Dictionary<Character, Drag> Drag = new Dictionary<Character, Drag>();
    public Dictionary<Character, TakeDamage> TakeDamage = new Dictionary<Character, TakeDamage>();
    public Dictionary<Character, CharacterStateManager> CharacterStateManager = new Dictionary<Character, CharacterStateManager>();

    /* ------------------------------------------ */

}

