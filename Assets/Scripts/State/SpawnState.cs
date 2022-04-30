using UnityEngine;

namespace State.Character
{
    public class SpawnState : CharacterBaseState
    {

        /* ------------------------------------------ */

        public override Type Type { get => Type.Spawn; }

        /* ------------------------------------------ */

        CharacterStateManager _manager;

        Spawn _spawn;
        bool _init,_packItUp;

        bool _animControl = false;
        public override void EnterState(CharacterStateManager manager, Argument argument)
        {
            _init = true;

            _spawn= manager.Character.gameObject.GetComponent<Spawn>();
            _manager = manager;
            _spawn.ChangeAnimation();
        }

        public override void UpdateState()
        {
            if (_init) 
            {
                if (!_spawn.IsRunning && !_animControl)
                {
                    _spawn.Process();
                    _animControl = true;
                }
                else if (!_spawn.IsRunning && _animControl)
                    FinishState(Type.Idle, new Argument());
            }
        }

        public override void FinishState(Type nextState, Argument argument)
        {
            IsRunning = false;
            _packItUp = true;
            _spawn.IsRunning = false;
            if (nextState != Type.None)
            {
                _manager.SwitchState(nextState, argument);
            }
        }

        /* ------------------------------------------ */

    }
}