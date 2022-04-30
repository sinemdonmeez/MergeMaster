using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State.Character;

public class IdleState : CharacterBaseState
{
    /* ------------------------------------------ */

    public override Type Type { get => Type.Idle; }

    /* ------------------------------------------ */

    Idle _idle;

    CharacterStateManager _manager;

    bool _init, _packItUp;

    /* ------------------------------------------ */

    public override void EnterState(CharacterStateManager manager, Argument argument)
    {
        _init = true;

        _idle = manager.Character.gameObject.GetComponent<Idle>();
        _manager = manager;
        _idle.ChangeAnimation();
    }

    public override void UpdateState()
    {
        if (_init)
        {
            IsRunning = true;



            _idle.Process();

        }
    }

    public override void FinishState(Type nextState, Argument argument)
    {
        IsRunning = false;
        _packItUp = true;

        if (nextState != Type.None)
        {
            _manager.SwitchState(nextState, argument);
        }
    }
    /* ------------------------------------------ */

}
