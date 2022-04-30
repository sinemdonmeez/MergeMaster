using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : Identity
{
    /* ------------------------------------------ */

    public string AnimationStateName;

    public RuntimeAnimatorController Controller;

    public float Speed;

    public bool IsRunning;

    /* ------------------------------------------ */

    Attack _attack;

    /* ------------------------------------------ */

    private void Awake()
    {
        ComponentManager.instance.Move.Add(identity, this);
        _attack = GetComponent<Attack>();
    }

    private void OnDestroy()
    {
        try
        {
            if (ComponentManager.instance.Move.ContainsKey(identity))
                ComponentManager.instance.Move.Remove(identity);
        }
        catch
        { }
    }

    /* ------------------------------------------ */

    public void Process(GameObject target = null)
    {
        Movement(Speed, target);
    }

    /* ------------------------------------------ */

    public void ChangeAnimation()
    {
        identity.Animator.runtimeAnimatorController = Controller;
    }

    /* ------------------------------------------ */

    void Movement(float speed, GameObject target = null)
    {

        if (target)
        {
            transform.position= Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
        }
        else
        {

            if (_attack.CanHaveTarget())
            {
                _attack.WasThePreviousSequenceAttack = true;
                return;
            }
        }
    }
}

  
