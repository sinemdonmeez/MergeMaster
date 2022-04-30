using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : Identity
{
    /* ------------------------------------------ */

    public RuntimeAnimatorController Controller;

    public bool IsRunning;

    /* ------------------------------------------ */
    CharacterStateManager _manager;

    Stats _stats;

    private void Awake()
    {
        ComponentManager.instance.Idle.Add(identity, this);

        _manager = GetComponent<CharacterStateManager>();

        _stats = GetComponent<Stats>();
    }

    private void OnDestroy()
    {
        try
        {
            if (ComponentManager.instance.Idle.ContainsKey(identity))
                ComponentManager.instance.Idle.Remove(identity);
        }
        catch { }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Grid") 
        {
           GridStat gridStat= other.gameObject.GetComponent<GridStat>();

            if(!gridStat.IsItFull) 
            {
                _manager.GridStats.Add(gridStat);
            }
        }
    }
    /* ------------------------------------------ */

    public override void Process()
    {
        if(_manager.GridStats.Count>0)
            transform.position = _manager.GridStats[_manager.GridStats.Count - 1].transform.position;
    }
    /* ------------------------------------------ */

    public void ChangeAnimation()
    {
        identity.Animator.runtimeAnimatorController = Controller;
    }

    /* ------------------------------------------ */

}
