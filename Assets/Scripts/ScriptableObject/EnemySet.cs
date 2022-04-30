using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy Type", menuName = "NiceTyrCase/Enemy Set")]
public class EnemySet : ScriptableObject
{
    public int DifficultyLevel;
    public List<GameObject> Prefabs;

}