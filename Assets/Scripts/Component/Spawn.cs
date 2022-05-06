using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : Identity
{

    /* ------------------------------------------ */

    public RuntimeAnimatorController Controller;

    public string AnimationStateName="Spawn";

    public bool IsRunning;

    WaitForSeconds _delay;

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
        identity.Animator.Play(AnimationStateName);

        //Wait till animation ends
        yield return _delay;

        while (identity.Animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationStateName))
            yield return _delay;
        IsRunning = false;
    }

    /* ------------------------------------------ */

    public void ChangeAnimation()
    {
        identity.Animator.runtimeAnimatorController = Controller;
    }
}
