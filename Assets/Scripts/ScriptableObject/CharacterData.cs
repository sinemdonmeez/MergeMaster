using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "NiceTyrCase/Character")]
public class CharacterData : ScriptableObject
{
    public int Level;
    public int Health;
    public int Attack;
    public UnitType Type;
}