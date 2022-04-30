using UnityEngine;

namespace State.Character
{
    public class AttackState : CharacterBaseState
    {

        /* ------------------------------------------ */

        public override Type Type { get => Type.Attack; }

        /* ------------------------------------------ */

        CharacterStateManager _manager;

        Attack _attack;

        bool _packItUp, _init;

        public override void EnterState(CharacterStateManager manager, Argument argument)
        {
            _manager = manager;

            _attack = manager.Character.gameObject.GetComponent<Attack>();

            _init = true;

            if (_attack.CanHaveTarget())
                if (_attack.IsItCloseEnoughToAttack())
                    _attack.ChangeAnimation();

            _packItUp = false;
        }

        public override void UpdateState()
        {
            if (_init)
            {
                IsRunning = true;
                if (_attack.CanHaveTarget())
                {
                    _attack.PointTargetSystem();

                    if (_attack.IsItCloseEnoughToAttack())
                    {
                        _attack.Process();
                    }
                    else
                    {
                        FinishState(Type.Move, new Argument { Character = _attack.Target.gameObject });
                    }
                }
                if (_packItUp && !_attack.IsRunning)
                    IsRunning = false;
            }
        }

        public override void FinishState(Type nextState, Argument argument)
        {
            _packItUp = true;
            _attack.IsRunning = false;
            if (nextState != Type.None)
            {
                _manager.SwitchState(nextState, argument);
            }
        }

        /* ------------------------------------------ */

    }
}