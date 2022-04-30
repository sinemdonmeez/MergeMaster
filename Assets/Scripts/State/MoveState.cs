using UnityEngine;

namespace State.Character
{
    public class MoveState : CharacterBaseState
    {

        /* ------------------------------------------ */

        public override Type Type { get => Type.Move; }

        /* ------------------------------------------ */

        CharacterStateManager _manager;

        Move _move;

        Attack _attack;

        public Transform _target;
        public Vector3 _targetPos;

        bool _packItUp, _init, isTheTargetACharacter;

        /* ------------------------------------------ */

        public override void EnterState(CharacterStateManager manager, Argument argument)
        {
            _manager = manager;

            _move = manager.Character.gameObject.GetComponent<Move>();
            _move.ChangeAnimation();
            _attack = manager.Character.gameObject.GetComponent<Attack>();

            _init = true;

            _packItUp = false;
        }

        public override void UpdateState()
        {
            if (_init)
            {
                IsRunning = true;
                _move.Process(_attack.Target.gameObject);
                if (_attack.IsItCloseEnoughToAttack())
                    FinishState(Type.Attack, new Argument());
            }

            if (_packItUp && !_move.IsRunning)
                IsRunning = false;
        }


        public override void FinishState(Type nextState, Argument argument)
        {
            _packItUp = true;
            _move.IsRunning = false;

            if (nextState != Type.None)
                _manager.SwitchState(nextState);
        }

        /* ------------------------------------------ */

    }
}
