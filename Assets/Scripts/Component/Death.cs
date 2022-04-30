using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : Identity
{
    public void Prosses() 
    {
        Destroy(this.gameObject);
    }
}
