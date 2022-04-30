using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identity : MonoBehaviour
{

    /* ------------------------------------------ */

    public Character identity
    {
        get
        {
            if (!_identity)
                _identity = GetComponent<Character>();

            return _identity;
        }
    }
    Character _identity;

    /* ------------------------------------------ */

    public virtual void Process() { }

    /* ------------------------------------------ */

}
