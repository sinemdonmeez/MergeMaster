using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : Identity
{
    public CharacterData Data;

    public int Health;

    public int Attack;

    private void Awake()
    {
        Health = Data.Health;

        Attack = Data.Health;
    }

    public void SetDamage(int damage)
    {
        Debug.Log("SetDamage");

        Health -= damage;
    }
}
