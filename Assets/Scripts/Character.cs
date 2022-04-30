using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    /* ------------------------------------------ */

    public UnitType Type;

    public Animator Animator;

    public SpriteRenderer SpriteRenderer;

    public GameObject Graphic;

    /* ------------------------------------------ */

}

public enum UnitType
{
    Ranger = 1,
    Warrior = 2,
}