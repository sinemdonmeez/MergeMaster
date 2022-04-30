using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : Identity
{

    /* ------------------------------------------ */

    public RuntimeAnimatorController Controller;

    public bool IsRunning;


    private void Awake()
    {
        ComponentManager.instance.Spawn.Add(identity, this);
    }


    private void OnDestroy()
    {
        try
        {
            if (ComponentManager.instance.Spawn.ContainsKey(identity))
                ComponentManager.instance.Spawn.Remove(identity);
        }
        catch { }

    }

    /* ------------------------------------------ */

    public override void Process()
    {
        Debug.Log("Spawn Process");

        StartCoroutine(IEProcess());
    }

    IEnumerator IEProcess()
    {

        Debug.Log("Spawn IEProcess");
        IsRunning = true;
        yield return new WaitForSeconds(2.5f);
        IsRunning = false;
    }

    /* ------------------------------------------ */

    public void ChangeAnimation()
    {
        identity.Animator.runtimeAnimatorController = Controller;
    }
}
